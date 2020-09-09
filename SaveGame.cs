using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace cm
{

    public class SaveGame
    {
        public static int SelectedSaveGame;
        public static string path;
        public static string BackupPath;
        public static string PlayerFileName;
        public static string TeamFileName;
        public static string ManagerFileName;
        public static string GeneralFileName;
        public static string CompetitionFileName;
        public int index;
        public string SaveName;
        public bool Available;
        public DateTime SaveDateTime;

        public static void SelectSaveGame(int i)
        {
            SaveGame.SelectedSaveGame = i;
            SaveGame.BackupPath = SaveGame.path + "Backup\\";
            if (!Directory.Exists(BackupPath))
                Directory.CreateDirectory(BackupPath);
            SaveGame.GeneralFileName = SaveGame.path + "GNDATA" + SaveGame.SelectedSaveGame + ".S16";
            SaveGame.PlayerFileName = SaveGame.path + "PLDATA" + SaveGame.SelectedSaveGame + ".S16";
            SaveGame.TeamFileName = SaveGame.path + "TMDATA" + SaveGame.SelectedSaveGame + ".S16";
            SaveGame.ManagerFileName = SaveGame.path + "MGDATA" + SaveGame.SelectedSaveGame + ".S16";
            SaveGame.CompetitionFileName = SaveGame.path + "CPDATA" + SaveGame.SelectedSaveGame + ".S16";
        }

        public static List<SaveGame> ReadSaveGameList()
        {
            List<SaveGame> SaveGameList = new List<SaveGame>();
            String filename = path + "GAMESS16.IDX";
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    for (int i = 0; i < 7; i++)
                    {
                        SaveGame save = new SaveGame();
                        save.index = i + 1;
                        save.SaveName = Encoding.UTF7.GetString(reader.ReadBytes(80)).Replace("\0", "");                        
                        save.SaveDateTime = ReadCurDate(i + 1);
                        save.Available = (save.SaveDateTime != DateTime.FromOADate(0));
                        SaveGameList.Add(save);
                    }
                }
            }
            return SaveGameList;

        }

        public static List<Manager> ReadManagerData()
        {
            List<Team> teamNameList = ReadTeamData(true,false);
            List<Manager> managerList = new List<Manager>();
            using (FileStream fs = new FileStream(ManagerFileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    int totalRecords = reader.ReadUInt16();
                    for (int i=0;i<totalRecords;i++)
                    {
                        Manager manager = new Manager();
                        manager.id = i;
                        manager.name = reader.ReadName();
                        MData mData = reader.ReadStruct<MData>(39);
                        if (mData.cn < teamNameList.Count)
                        {
                            manager.cn = teamNameList[mData.cn].name;
                        }
                        if (mData.club < teamNameList.Count)
                        {
                            manager.clubID = mData.club;
                            manager.club = teamNameList[mData.club].name;
                        }
                        manager.rep = mData.rep;
                        manager.yr = mData.year;
                        managerList.Add(manager);
                    }
                }
            }
            return managerList;
        }

        public unsafe static List<Competition> ReadCompetitionData(bool ReadNameOnly)
        {
            List<Team> teamNameList = ReadTeamData(true, false);
            List<Competition> competitionList = new List<Competition>();
            using (FileStream fs = new FileStream(CompetitionFileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    int totalRecords = reader.ReadUInt16();
                    for (int id = 0; id < totalRecords; id++)
                    {
                        Competition c = new Competition();
                        c.id = id;
                        c.name1 = reader.ReadName(false);
                        c.name2 = reader.ReadName(false);
                        c.name3 = reader.ReadName(false);
                        c.name4 = reader.ReadName(false);
                        c.name5 = reader.ReadName(false);
                        CompetitionData competitionData = reader.ReadStruct<CompetitionData>(2004);
                        if (ReadNameOnly == false)
                        {
                            c.numTeams = competitionData.numOfTeams;
                            for (int j = 0; j < 25; j++)
                            {
                                if (competitionData.teams[j] < teamNameList.Count && competitionData.teams[j] > 1)
                                {
                                    c.teams += teamNameList[competitionData.teams[j]].name + ",";
                                    c.teamsID.Add(competitionData.teams[j]);
                                }
                            }
                        }
                        competitionList.Add(c);
                    }
                }
            }
            return competitionList;
        }

        public unsafe static List<Team> ReadTeamData(bool ReadTeamNameOnly, bool ReadPlayerName)
        {
            List<Team> teamList = new List<Team>();
            List<Team> teamNameList = new List<Team>();
            List<Player> playerNameList = new List<Player>();
            List<Competition> competitionList = new List<Competition>();
            if (ReadTeamNameOnly == false)
            {
                teamNameList = ReadTeamData(true, false);
                competitionList = ReadCompetitionData(true);
            }
            if (ReadPlayerName)
                playerNameList = ReadPlayersData(ReadNameOnly: true);


            using (FileStream fs = new FileStream(TeamFileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    int totalRecords = reader.ReadUInt16();
                    for (int id=0;id<totalRecords;id++)
                    {
                        Team team = new Team();
                        team.id = id;
                        team.name = reader.ReadName(false);
                        //skip 6 strings
                        for (int i = 0; i < 5; i++)
                            reader.ReadName(false);
                        team.stadium = reader.ReadName(false);
                        TeamData t = reader.ReadStruct<TeamData>(1466);
                        if (!ReadTeamNameOnly)
                        {
                            if (t.cn < teamNameList.Count)
                                team.cn = teamNameList[t.cn].name;
                            team.loc = t.x + "," + t.y;
                            team.pop = t.pop;
                            team.flex = t.flex;
                            team.supp = t.supp;
                            team.cap = t.cap;
                            team.seat = t.seat;
                            team.shortlist = t.shortlist;
                            //team.val = (int)t.val;
                            team.player_sales = (int)t.player_sales;
                            team.balance = (int)(t.start_balance + t.season_ticket + t.gate_receipt + t.tv_prize + t.player_sales + t.other_income);
                            team.balance -= (int)(t.player_wages + t.player_bonus + t.player_purchase + t.other_cost);
                            if (t.division < competitionList.Count && t.division >1)
                                team.division = competitionList[t.division].name2;
                            team.EEC = t.EEC;
                        }
                        if (ReadPlayerName)
                        { 
                            for (int j = 0; j < 32; j++)
                            {
                                if (t.p1[j] < playerNameList.Count && t.p1[j] > 1)
                                {
                                    team.p1 = team.p1 + "," + playerNameList[t.p1[j]].Name;
                                }
                            }
                            for (int k = 0; k < 22; k++)
                            {
                                if (t.p2[k] < playerNameList.Count && t.p2[k] > 1)
                                {
                                    team.p2 = team.p2 + "," + playerNameList[t.p2[k]].Name;
                                }
                            }
                        }
                        teamList.Add(team);
                    }
                }
            }
            return teamList;
        }

        
        public static DateTime ReadCurDate(int i)
        {
            string filename = path + "GNDATA" + i + ".S16";
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        reader.ReadBytes(3);
                        int dd = reader.ReadByte();
                        int mm = reader.ReadByte();
                        int yy = reader.ReadByte();
                        yy += ((yy > 90) ? 1900 : 2000);
                        return new DateTime(yy, mm, dd);
                    }
                }
            }
            return DateTime.FromOADate(0);
        }
        


        public unsafe static List<Player> ReadPlayersData(bool ReadNameOnly) 
        {
            List<Team> teamList = ReadTeamData(false,false);
            DateTime curDate = ReadCurDate(SelectedSaveGame);

            List<Player> playerList = new List<Player>();
            using (FileStream fs = new FileStream(PlayerFileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    int totalRecords = reader.ReadUInt16();
                    byte[] array = new byte[360];
                    for (int i=0;i<totalRecords;i++)
                    {
                        Player player = new Player();
                        player.Name = reader.ReadName();
                        Data data = reader.ReadStruct<Data>(360);
                        if (!ReadNameOnly)
                        {
                            //5D DD CF 00 F5 5C CE 00 FE 02 00 00 00 03 00 5A 02 FF FF 64 1C 01 4E 02 00 00 88 00 98 00 85 00 83 00 8C
                            //01 02 03 04 05 06 07 cn cn cap11 12 13 14 cl cl 16 17 18 19 dd mm yy posit
                            player.GK = (byte)(data.position & 3);
                            player.SW = (byte)((data.position >> 2) & 3);
                            player.D = (byte)((data.position >> 4) & 3);
                            player.DM = (byte)((data.position >> 6) & 3);
                            player.M = (byte)((data.position >> 8) & 3);
                            player.AM = (byte)((data.position >> 10) & 3);
                            player.S = (byte)((data.position >> 12) & 3);
                            player.R = (byte)((data.position >> 14) & 3);
                            player.L = (byte)(data.side & 3);
                            player.C = (byte)((data.side >> 2) & 3);

                           
                            player.alt = "";
                            if (player.GK == 1) player.alt += (string.IsNullOrEmpty(player.alt) ? "" : "/") + "GK";
                            if (player.SW == 1) player.alt += (string.IsNullOrEmpty(player.alt) ? "" : "/") + "SW";
                            if (player.D == 1) player.alt += (string.IsNullOrEmpty(player.alt) ? "" : "/") + "D";
                            if (player.DM == 1) player.alt += (string.IsNullOrEmpty(player.alt) ? "" : "/") + "DM";
                            if (player.M == 1) player.alt += (string.IsNullOrEmpty(player.alt) ? "" : "/") + "M";
                            if (player.AM == 1) player.alt += (string.IsNullOrEmpty(player.alt) ? "" : "/") + "AM";
                            if (player.S == 1) player.alt += (string.IsNullOrEmpty(player.alt) ? "" : "/") + "S";
                            if (player.R == 1) player.alt += "R";
                            if (player.L == 1) player.alt += "L";
                            if (player.C == 1) player.alt += "C";

                            player.pos = "";
                            if (player.GK == 2) player.pos += (string.IsNullOrEmpty(player.pos) ? "" : "/") + "GK";
                            if (player.SW == 2) player.pos += (string.IsNullOrEmpty(player.pos) ? "" : "/") + "SW";
                            if (player.D == 2) player.pos += (string.IsNullOrEmpty(player.pos) ? "" : "/") + "D";
                            if (player.DM == 2) player.pos += (string.IsNullOrEmpty(player.pos) ? "" : "/") + "DM";
                            if (player.M == 2) player.pos += (string.IsNullOrEmpty(player.pos) ? "" : "/") + "M";
                            if (player.AM == 2) player.pos += (string.IsNullOrEmpty(player.pos) ? "" : "/") + "AM1";
                            if (player.S == 2) player.pos += (string.IsNullOrEmpty(player.pos) ? "" : "/") + "S";
                            if (player.R == 2) player.pos += "R";
                            if (player.L == 2) player.pos += "L";
                            if (player.C == 2) player.pos += "C";

                            //DM1/M/AM1 -> DM/AM
                            //DM1/M/AM1/S -> DM/M/FC

                            player.pos = player.pos.Replace("AM1/S", "F");
                            player.pos = player.pos.Replace("M/AM1", "AM");
                            player.pos = player.pos.Replace("AM1", "FO");   //player with support position only

                            player.abi = data.ability;
                            player.pot = data.potential;
                            player.rep = data.rep;
                            player.tal = data.talent;
                            player.caps = data.caps;
                            player.teamval = (int)teamList[data.club].val;
                            player.price = (int)Math.Round(data.price);
                            player.wage = (int)data.wage;
                            player.apps = data.domapps + data.euapps;
                            player.goal = data.domgoals + data.eugoals;
                            player.asst = data.domasst + data.euasst;
                            if (data.domapps > 0 || data.euapps > 0)
                            {
                                player.rating = Math.Round((double)(data.domrate + data.eurate) / (double)(data.domapps + data.euapps),2);
                            }
                            player.cn = teamList[data.cn].name;
                            player.fgn = (teamList[data.cn].EEC == 0 ? "yes" : "no");
                            player.club = teamList[data.club].name;
                            player.club_cn = teamList[data.club].cn;
                            player.division = teamList[data.club].division;
                            try
                            {
                                player.age = new DateTime(curDate.Subtract(new DateTime(data.yy + 1900, data.mm, data.dd)).Ticks).Year - 1;
                            }
                            catch { }
                            switch (data.status)
                            {
                                case 0:
                                    player.TRF = "UNK";
                                    player.sell = ((teamList[data.club].val >= 250000 && teamList[data.club].val <= 500000) ? "*" : "");
                                    break;
                                case 1:
                                    player.TRF = "CLU";
                                    player.sell = "*";
                                    break;
                                case 2:
                                    player.TRF = "REQ";
                                    player.sell = "*";
                                    break;
                                case 3:
                                    player.TRF = "LOA";
                                    player.sell = ((teamList[data.club].val >= 250000 && teamList[data.club].val <= 500000) ? "*" : "");
                                    break;
                                case 4:
                                    player.TRF = "FRE";
                                    player.sell = "*";
                                    break;
                                case 5:
                                    player.TRF = "N/A";
                                    break;
                                default:
                                    player.TRF = "ERR";
                                    break;
                            }
                            /*
                            byte[] array2 = BitConverter.GetBytes(data.skill1).
                                Concat(BitConverter.GetBytes(data.skill2)).
                                Concat(BitConverter.GetBytes(data.skill3))
                                .ToArray();
                                */
                            byte* array2 = data.skill;
                            //player.playerskill = new byte[24];
                            //System.Runtime.InteropServices.Marshal.Copy((IntPtr)data.skill,player.playerskill,0,24);
                            
                            player.Agg = array2[0];
                            player.Big = array2[1];     //big
                            player.Chr = array2[2];     //character
                            player.Mor = array2[3];     //Morale
                            player.Con = array2[4];     //consistency
                            player.Cre = array2[5];
                            player.Det = array2[6];
                            player.Dir = array2[7];
                            player.Dri = array2[8];
                            player.Fla = array2[9];
                            player.Hea = array2[10];
                            player.Inf = array2[11];
                            player.Inj = array2[12];
                            player.Adap = array2[13];
                            player.Off = array2[14];
                            player.Pac = array2[15];
                            player.Pas = array2[16];
                            player.Posi = array2[17];
                            player.Set = array2[18];
                            player.Sho = array2[19];
                            player.Sta = array2[20];
                            player.Str = array2[21];
                            player.Tac = array2[22];
                            player.Tec = array2[23];
                            //sum all skill
                            //player.skill = array2.Sum(x => x);
                            for (int x = 0; x < 24; x++)
                                player.skill += data.skill[x];
                            //big and con worth 2x value, subtract morale and character
                            player.skill += (player.Big + player.Con - player.Mor - player.Chr);
                            //fix injury prones and dirtiness 1-good 20-bad
                            player.skill += (20 - player.Inj - player.Inj) + (20 - player.Dir - player.Dir);                            
                            player.avg = (double)player.skill/24;
                            
                            player.DDM_pot = (player.Tac * 20 + player.Posi * 20 + player.Hea * 15 + player.Det * 10 + player.Sta * 10) * player.pot / 75 / 18;
                            player.FC_pot = (player.Off * 20 + player.Sho * 20 + player.Cre * 10 + player.Hea * 10 + player.Det * 10 + player.Sta * 10) * player.pot / 80 / 18;
                        }
                        playerList.Add(player);
                    }
                }
            }
            return playerList;
        }
    }
}

