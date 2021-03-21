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
                            //team.val = (int)t.val;
                            team.player_sales = (int)t.player_sales;
                            team.balance = (int)(t.start_balance + t.season_ticket + t.gate_receipt + t.tv_prize + t.player_sales + t.other_income);
                            team.balance -= (int)(t.player_wages + t.player_bonus + t.player_purchase + t.other_cost);
                            if (t.division < competitionList.Count && t.division >1)
                                team.division = competitionList[t.division].name2;
                            team.EEC = t.EEC;
                            team.rep = t.reputation;
                        }
                        if (ReadPlayerName)
                        { 
                            for (int j = 0; j < t.numofplayers; j++)
                            {
                                if (t.squad[j] < playerNameList.Count && t.squad[j] > 1)
                                {
                                    team.squad = team.squad + "," + playerNameList[t.squad[j]].Name;
                                }
                            }
                            if (t.shortlist0 < playerNameList.Count && t.shortlist0 > 1) team.shortlist += "," + playerNameList[t.shortlist0].Name;
                            if (t.shortlist1 < playerNameList.Count && t.shortlist1 > 1) team.shortlist += "," + playerNameList[t.shortlist1].Name;
                            if (t.shortlist2 < playerNameList.Count && t.shortlist2 > 1) team.shortlist += "," + playerNameList[t.shortlist2].Name;
                            if (t.shortlist3 < playerNameList.Count && t.shortlist3 > 1) team.shortlist += "," + playerNameList[t.shortlist3].Name;
                            if (t.shortlist4 < playerNameList.Count && t.shortlist4 > 1) team.shortlist += "," + playerNameList[t.shortlist4].Name;
                            if (t.shortlist5 < playerNameList.Count && t.shortlist5 > 1) team.shortlist += "," + playerNameList[t.shortlist5].Name;
                            if (t.shortlist6 < playerNameList.Count && t.shortlist6 > 1) team.shortlist += "," + playerNameList[t.shortlist6].Name;
                            if (t.shortlist7 < playerNameList.Count && t.shortlist7 > 1) team.shortlist += "," + playerNameList[t.shortlist7].Name;
                            if (t.shortlist8 < playerNameList.Count && t.shortlist8 > 1) team.shortlist += "," + playerNameList[t.shortlist8].Name;
                            if (t.shortlist9 < playerNameList.Count && t.shortlist9 > 1) team.shortlist += "," + playerNameList[t.shortlist9].Name;
                            if (t.shortlist10 < playerNameList.Count && t.shortlist10 > 1) team.shortlist += "," + playerNameList[t.shortlist10].Name;
                            if (t.shortlist11 < playerNameList.Count && t.shortlist11 > 1) team.shortlist += "," + playerNameList[t.shortlist11].Name;
                            if (t.shortlist12 < playerNameList.Count && t.shortlist12 > 1) team.shortlist += "," + playerNameList[t.shortlist12].Name;
                            if (t.shortlist13 < playerNameList.Count && t.shortlist13 > 1) team.shortlist += "," + playerNameList[t.shortlist13].Name;
                            if (t.shortlist14 < playerNameList.Count && t.shortlist14 > 1) team.shortlist += "," + playerNameList[t.shortlist14].Name;
                            if (t.shortlist15 < playerNameList.Count && t.shortlist15 > 1) team.shortlist += "," + playerNameList[t.shortlist15].Name;
                            
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
                        //player.ID = i;
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
                            player.CLBREP = teamList[data.club].rep;
                            player.play = data.play;
                            player.price = (int)Math.Round(data.price);
                            player.wage = (int)data.wage;
                            player.bcr = (data.bcr == 1?"yes":"no");
                            player.apps = data.domapps + data.eu_apps;
                            player.goal = data.domgoals + data.eu_goals;
                            player.asst = data.domasst + data.eu_asst;
                            if (data.domapps > 0 || data.eu_apps > 0)
                            {
                                player.rating = Math.Round((double)(data.domrate + data.eu_rating) / (double)(data.domapps + data.eu_apps),2);
                            }
                            player.injury = data.injury_length;
                            player.phy = data.physicalcondition;
                            player.recovery = (double)data.recovery_percentage/100;
                            player.cn = teamList[data.cn].name;
                            player.fgn = (teamList[data.cn].EEC == 0 ? "yes" : "no");
                            player.club = teamList[data.club].name;
                            player.club_cn = teamList[data.club].cn;
                            player.div = teamList[data.club].division;
                            try
                            {
                                player.age = new DateTime(curDate.Subtract(new DateTime(data.yy + 1900, data.mm, Math.Max(1,(int)data.dd))).Ticks).Year - 1;
                                player.join = new DateTime(data.joindate_yy + 1900, data.joindate_mm, Math.Max(1,(int)data.joindate_dd));
                                player.days = curDate.Subtract(new DateTime(data.joindate_yy + 1900, data.joindate_mm, Math.Max(1, (int)data.joindate_dd))).Days;
                                //player.months= curDate.Subtract(new DateTime(data.joindate_yy + 1900, data.joindate_mm, data.joindate_dd)).;
                                player.clbpop = teamList[data.club].pop;
                                player.mon = (curDate.Year - (data.joindate_yy + 1900)) * 12 + (curDate.Month - data.joindate_mm);
                            }
                            catch { }
                            
                            player.buy = data.interested;
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
                            player.skill += (int)((20 - player.Inj - player.Inj) + (20 - player.Dir - player.Dir));                            
                            player.avg = (double)player.skill/24;
                            
                            player.DDM = (int)(player.Tac * 20 + player.Posi * 20 + player.Hea * 15 + player.Det * 10 + player.Sta * 10) * player.pot / 75 / 18;
                            player.FC = (int)(player.Off * 20 + player.Sho * 20 + player.Cre * 10 + player.Hea * 10 + player.Det * 10 + player.Sta * 10) * player.pot / 80 / 18;

                            //determine if player is for sell
                            if (player.TRF == "REQ" || player.TRF == "CLU")
                                player.avail = "yes";
                            else if (player.TRF == "UNK")
                            {
                                //player is for sale if player price higher than club balance
                                if (player.price >= teamList[data.club].balance)
                                    player.avail = "yes";
                                //otherwise player that play is less than 85 will be on sale based on 
                                //player potential , club popularity and club reputation
                                //player need to join club longer than 16 month (still need to observe)
                                //player need to be less than 190 potential
                                else if (player.play < 85 && player.mon >= 16 && player.pot<190 && teamList[data.club].name.Contains("Blackburn"))
                                {
                                    //for player with potential  150
                                    //pop 20 pot<195
                                    //pop 19 pot<190
                                    //pop 18 pot<185

                                    int clubreputation_varian =(int)(teamList[data.club].rep / 2) + 120;    //(150/2)+120 = 195     
                                    int clubpopularity_varian = (20 - teamList[data.club].pop) * 5;         //20-18 * 5 = 10
                                    int req = clubreputation_varian - clubpopularity_varian;
                                    
                                    if (player.pot < req)
                                        player.avail = "yes";
                                    
                                }

                            }

                        }
                        playerList.Add(player);
                    }
                }
            }
            return playerList;
        }
    }
}

