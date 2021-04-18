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
        public static List<Team> teamNameList;
        public static List<Competition> competitionNameList;
        public static List<Competition> competitionList;
        public static List<Player> playerList;
        public static List<Team> teamList;
        public static List<Manager> managerList;
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

            competitionNameList = ReadCompetitionData(true);
            teamNameList = ReadTeamData(false, false);
            managerList = ReadManagerData();
            competitionList = ReadCompetitionData(false);
            playerList = ReadPlayersData(false);
            teamList = ReadTeamData(false, true);
        }

        public static int MyClubID()
        {
            return managerList[managerList.Count - 1].clubID;
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
            //List<Team> teamNameList = ReadTeamData(true,false);
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

        private unsafe static List<Competition> ReadCompetitionData(bool ReadNameOnly)
        {
            //List<Team> teamNameList = ReadTeamData(true, false);
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

        private unsafe static List<Team> ReadTeamData(bool ReadTeamNameOnly, bool ReadPlayerName, string sourcedir = "")
        {
            List<Team> teamList = new List<Team>();
            //List<Team> teamNameList = new List<Team>();
            //List<Player> playerNameList = new List<Player>();
            //List<Competition> competitionList = new List<Competition>();
            if (ReadTeamNameOnly == false)
            {
                teamNameList = ReadTeamData(true, false, sourcedir);
                competitionList = ReadCompetitionData(true);
            }
            //if (ReadPlayerName)
            //    playerNameList = ReadPlayersData(ReadNameOnly: true, sourcedir);


            if (sourcedir == "")
                SaveGame.TeamFileName = SaveGame.path + "TMDATA" + SaveGame.SelectedSaveGame + ".S16";
            else
                SaveGame.TeamFileName = SaveGame.path + "tmp\\TMDATA" + SaveGame.SelectedSaveGame + ".S16";
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
                            for (int i = 0; i < t.numofplayers; i++)
                            {
                                if (t.squad[i] < playerList.Count && t.squad[i] > 1)
                                    team.squad = team.squad + "," + playerList[t.squad[i]].Name;
                            }
                            if (team.squad != null) team.squad = System.Text.RegularExpressions.Regex.Replace(team.squad, "^,", "");
                            for (int i = 0; i < 40; i+=5)
                            {
                                //if (System.Diagnostics.Debugger.IsAttached && team.name.Equals("Sunderland"))
                                //    System.Diagnostics.Debugger.Break();
                                int player_id = (t.shortlist[i] | t.shortlist[i + 1]<<8);
                                if (player_id < playerList.Count && player_id > 1)
                                    team.shortlist += "," + playerList[player_id].Name;

                            }
                            if (team.shortlist != null) team.shortlist = System.Text.RegularExpressions.Regex.Replace(team.shortlist,"^,","");
                            
                        }
                        teamList.Add(team);
                    }
                }
            }
            return teamList;
        }

        
        public static DateTime ReadCurDate(int i, string sourcedir = "")
        {
            string filename;
            if (sourcedir == "")
                filename= SaveGame.path + "GNDATA" + i + ".S16";
            else
                filename = SaveGame.BackupPath + "tmp\\GNDATA" + i + ".S16";
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



        public unsafe static List<Player> ReadPlayersData(bool ReadNameOnly, string sourcedir = "") 
        {
            //List<Team> teamList = ReadTeamData(false,false, sourcedir);
            DateTime curDate = ReadCurDate(SelectedSaveGame,sourcedir);

            List<Player> playerList = new List<Player>();
            if (sourcedir == "")
                SaveGame.PlayerFileName = SaveGame.path + "PLDATA" + SaveGame.SelectedSaveGame + ".S16";
            else
                SaveGame.PlayerFileName = SaveGame.BackupPath + "tmp\\PLDATA" + SaveGame.SelectedSaveGame + ".S16";
            using (FileStream fs = new FileStream(PlayerFileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    int totalRecords = reader.ReadUInt16();
                    for (int i=0;i<totalRecords;i++)
                    {
                        Player player = new Player();
                        player.Name = reader.ReadName();
                        player.ID1 = i;
                        Data data = reader.ReadStruct<Data>(360);
                        //player.data = data;
                        
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
                            player.crep = teamNameList[data.club].rep;
                            player.cpop = teamNameList[data.club].pop;
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
                            player.cn = teamNameList[data.cn].name;
                            player.fgn = (teamNameList[data.cn].EEC == 0 ? "yes" : "no");
                            player.club = teamNameList[data.club].name;
                            player.club_cn = teamNameList[data.club].cn;
                            player.div = teamNameList[data.club].division;
                            try
                            {
           
                                player.age = new DateTime(curDate.Subtract(new DateTime(data.yy + 1900, data.mm, Math.Max(1,(int)data.dd))).Ticks).Year - 1;
                                if (data.joindate_mm == 2) data.joindate_dd = Math.Min((byte)28, data.joindate_dd);
                                player.join = new DateTime(data.joindate_yy + 1900, data.joindate_mm, Math.Max(1,(int)data.joindate_dd));
                                player.days = curDate.Subtract(new DateTime(data.joindate_yy + 1900, data.joindate_mm, Math.Max(1, (int)data.joindate_dd))).Days;
                                //player.months= curDate.Subtract(new DateTime(data.joindate_yy + 1900, data.joindate_mm, data.joindate_dd)).;
                                player.mon = (curDate.Year - (data.joindate_yy + 1900)) * 12 + (curDate.Month - data.joindate_mm);
                            }
                            catch { }
                            
                            player.buy = data.interested;
                            player.TRF = Player.transferStatus[data.status];
                            
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
                            for (int x = 0; x < 24; x++)
                                player.skill += data.skill[x];
                            // subtract morale, influence, injury prone
                            player.skill -= (player.Mor + player.Inf + player.Inj);
                            //fix injury prones and dirtiness 1-good 20-bad
                            player.skill += (int)(20 - player.Dir - player.Dir);                            
                            player.avg = (double)player.skill/21;
                            
                            //player.DDM = (int)(player.Tac * 20 + player.Posi * 20 + player.Hea * 15 + player.Det * 10 + player.Sta * 10) * player.pot / 75 / 18;
                            //player.FC = (int)(player.Off * 20 + player.Sho * 20 + player.Cre * 10 + player.Hea * 10 + player.Det * 10 + player.Sta * 10) * player.pot / 80 / 18;
                            player.DEF = (double)(player.Tac*5 + player.Posi*3 + player.Hea + player.Det +player.Agg) / 11;  //tony adams, steve bould
                            //player.SS = (double)(player.Cre*2 + player.Det + player.Dri + player.Cre  + player.Pac + player.Pas*2 + player.Tec) /9;
                            player.SS = (double)(player.Cre + player.Fla + player.Pas + player.Tec) / 4;   //zidane, beckham, pires
                            player.SC = (double)(player.Off*5 + player.Sho*5 + 
                                player.Det+ player.Hea + player.Pac +  player.Fla + player.Str  + player.Tec ) / 16;    //klinsman, fowler, branca

                            //determine if player is for sell
                            if (player.TRF == "REQ" || player.TRF == "CLU")
                                player.avail = "yes";
                            else if (player.TRF != "N/A")
                            {
                                if (player.price >= teamNameList[data.club].balance)
                                    player.avail = "yes";
                                //otherwise player that play is less than 85 will be on sale based on 
                                //player potential , club popularity and club reputation
                                //player need to join club longer than 16 month (still need to observe)
                                //player need to be less than 190 potential
                                else if (player.play < 85 &&  player.pot<190)
                                {
                                    //for player with potential  150
                                    //pop 20 pot<195
                                    //pop 19 pot<190
                                    //pop 18 pot<185
    
                                    int clubreputation_varian =(int)(teamNameList[data.club].rep / 2) + 120;    //(150/2)+120 = 195     
                                    int clubpopularity_varian = (20 - teamNameList[data.club].pop) * 5;         //20-18 * 5 = 10
                                    int req = clubreputation_varian - clubpopularity_varian;
                                    
                                    if (player.pot < req)
                                    {
                                        if (player.mon > 16)
                                            player.avail = "yes";
                                        else if (player.mon > 11)
                                            player.avail = "maybe";
                                    }
                                }

                                if (player.avail == null)
                                {
                                    //birmingham money 1060 player price 1057 and player available for sale, player is interested by arsenal and chelsea
                                    //player is for sale if player price higher than club balance
                                    //tested until team balance below 2M, still available
                                    if (player.buy > 0 && player.price > teamNameList[data.club].balance / 2)
                                        player.avail = "maybe";                                   
                                }
                            }
                            //check for bcr
                            if (player.bcr.Equals("yes"))// && player.avail == null)
                            {
                                int myclub = managerList[managerList.Count - 1].clubID;
                                if (player.cpop < teamNameList[myclub].pop && player.crep < teamNameList[myclub].rep - 35)
                                    player.avail = "bcr";
                            }

                            //determine player join chance
                            if (SaveGame.MyClubID() <= teamNameList.Count)
                            {
                                Team MyTeam = teamNameList[SaveGame.MyClubID()];
                                int clubreputation_varian = MyTeam.rep - player.crep;
                                int clubpopularity_varian = (MyTeam.pop - player.cpop) * 5;
                                int varian = clubreputation_varian + clubpopularity_varian;
                                if (player.TRF == "REQ" || player.TRF == "CLU" || player.TRF == "LOA")
                                    varian += 50;
                                if (varian >= 100)
                                    player.chance = "100%";                                
                                else if (varian>0)
                                    player.chance = varian + "%";
                                else if (varian >= -10)
                                    player.chance = "1%";
                            }
                        }
                        playerList.Add(player);
                    }
                }
            }
            return playerList;
        }
        

        public static void SavePlayerData(List<Player> playersList)
        {
            using (FileStream fs = new FileStream(PlayerFileName, FileMode.CreateNew, FileAccess.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    foreach (Player player in playersList)
                    {
                        Data data = new Data();//= player.data;
                        data.ability = player.abi;
                        data.potential = player.pot;
                        data.rep = player.rep;
                        data.talent = player.tal;
                        writer.Write(player.Name);
                        writer.Write(data.getBytes());
                    }
                }
            }
        }
    }
}

