using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO.Compression;    //require .NET 4.5
using System.Net;
using System.Threading.Tasks;

namespace cm
{
    public partial class FormMain : Form
    {

        public FormMain()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SaveGame game = SaveGame.ReadSaveGameList().First(s => s.index == SaveGame.SelectedSaveGame);
            this.Text = "CM2 Viewer - " + game.SaveName + " (" + game.SaveDateTime.ToLongDateString() +")";
            //Bind(SaveGame.ReadPlayersData(ReadNameOnly: false));
            Bind(SaveGame.playerList);
            dv.Sort = "POT DESC";
            PopulateRestore(); 
            for (int i=0;i<Player.skillName.Length;i++)
            {
                Label lbl = new Label() {
                    Text = Player.skillLongName[i],
                    Top = 10 + ((i % 4) * 20),
                    Left = 10 + ((int)(i / 4) * 120),
                    Size = new Size(50, 15)
                };
                NumericUpDown nud = new NumericUpDown() {
                    Name = Player.skillName[i],
                    Top = 10 + ((i % 4) * 20),
                    Left = 70 + ((int)(i / 4) * 120),
                    Size = new Size(40, 15),
                    Maximum = 20,
                    TextAlign = HorizontalAlignment.Center
                    
                };
                nud.ValueChanged += (s, ex) => filter();     
                splitContainer1.Panel2.Controls.Add(lbl);
                splitContainer1.Panel2.Controls.Add(nud);
            }
            tbAbility.TextChanged += (s, ex) => filter();
            tbPotential.TextChanged += (s, ex) => filter();
            tbRep.TextChanged += (s, ex) => filter();
            tbTalent.TextChanged += (s, ex) => filter();
            tbAge.TextChanged += (s, ex) => filter();
            cbStatus.TextChanged += (s, ex) => filter();
            cbForeign.TextChanged += (s, ex) => filter();
            Rating.ValueChanged += (s, ex) => filter();
            cbPosition.TextChanged += (s, ex) => filter();
            cbSide.TextChanged += (s, ex) => filter();
            tbPrice.TextChanged += (s, ex) => filter();
            tbSkill.TextChanged += (s, ex) => filter();
            int[] list = { 6,7,8, 9, 10, 11,12,13,14 };
            foreach (int i in list)
                fontSizeToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(i.ToString(),null, FontSizeDropDown_Click));
            foreach (Control c in splitContainer1.Panel1.Controls)
            {
                if (c is Button) ((Button)c).Click += new EventHandler(btnClick);
            }
        }

  

        public void FontSizeDropDown_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            Font f = new Font(sqlDatagridview1.DefaultCellStyle.Font.FontFamily, float.Parse(clickedItem.Text));
            sqlDatagridview1.DefaultCellStyle.Font = f;
            

        }
        public void SetButtons()
        {
            button1.Enabled = (dt.TableName == "Player");
            button2.Enabled = (dt.TableName == "Player");
            button4.Enabled = (dt.TableName == "Player");
            button5.Enabled = (dt.TableName == "Player");
            button11.Enabled = (dt.TableName == "Player");
            button12.Enabled = (dt.TableName == "Player");
            button7.Enabled = (dt.TableName == "Player");
            button8.Enabled = (dt.TableName == "Player");
            button9.Enabled = (dt.TableName == "Player");
            button15.Enabled = (dt.TableName == "Player");
            button14.Enabled = (dt.TableName == "Player");
            button13.Enabled = (dt.TableName == "Player");
            button16.Enabled = (dt.TableName == "Player");
            sqlDatagridview1.ReadOnly = (dt.TableName != "Player");
        }

        public void Bind<T>(List<T> list)
        {
            dt = Helper.BuildDataTable(list);
            BindDataTable(dt);
        }

        public void BindDataTable(DataTable dt)
        { 
            dv = dt.DefaultView;
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dt;
            sqlDatagridview1.Bind_to_Bindingsource(bindingSource);
            sqlDatagridview1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            SetButtons();
        }
       



        private void filter()
        {
            if (dt.TableName == "Player")
            {
                if (!textBox1.Text.Trim().Equals(""))
                    textBox1.Text = " AND " + textBox1.Text + " ";

                //remove filter source from textbox
                //for (int i = 0; i < Player.skillName.Length; i++)
                //    textBox1.Text = Regex.Replace(textBox1.Text, " AND " + Player.skillName[i].ToUpper() + "([<>])=([0-9,.]+) ", " ");
                //textBox1.Text = Regex.Replace(textBox1.Text, " AND RATING>=([0-9,.]+) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND ABI>=(\\S+) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND POT>=(\\S+) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND REP<=(\\S+) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND TAL<=(\\S+) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND AGE<=(\\S+) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND FGN='(\\S+)' ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND CN='(\\S+)' ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND CN IN ((\\S+)) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND Rating>=(\\S+) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND (GK|SW|D|DM|M|AM|S|R|L|C)>=([0-9]+)", "");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND PRICE<=([0-9]+) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND SKILL>=([0-9]+) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND TRF([<=>]+'[A-Z/]+') ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND (\\(TRF.+ OR .+\\)) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND AVAIL IN ((\\S+)) ", " ");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND CHANCE IS NOT NULL ", " ");

                //the end result is custom filter entered by user
                //string customFilter = textBox1.Text.Trim();
                //textBox1.Text = "";
                textBox1.Text = textBox1.Text.TrimEnd();
                foreach (Control c in splitContainer1.Panel2.Controls)
                {
                    if (c is NumericUpDown)
                    {
                        NumericUpDown nud = (NumericUpDown)c;

                        if (nud.Value > 0)
                        {
                            textBox1.Text = Regex.Replace(textBox1.Text, " AND " + nud.Name.ToUpper() + "([<>])=([0-9,.]+)", "");
                            if (nud.Name.Equals("Dir") || nud.Name.Equals("Inj"))
                                textBox1.Text += " AND " + nud.Name.ToUpper() + "<=" + nud.Value.ToString().Replace(',', '.');
                            else
                                textBox1.Text += " AND " + nud.Name.ToUpper() + ">=" + nud.Value.ToString().Replace(',', '.');
                        }
                        else
                            textBox1.Text = Regex.Replace(textBox1.Text + " ", " AND " + nud.Name.ToUpper() + "([<>])=1 ", " ").TrimEnd();
                    }
                }
                

                if (tbAbility.Text != "") textBox1.Text += " AND ABI>=" + tbAbility.Text;
                if (tbPotential.Text != "") textBox1.Text += " AND POT>=" + tbPotential.Text;
                if (tbRep.Text != "") textBox1.Text += " AND REP<=" + tbRep.Text;
                if (tbTalent.Text != "") textBox1.Text += " AND TAL<=" + tbTalent.Text;
                if (tbAge.Text != "") textBox1.Text += " AND AGE<=" + tbAge.Text;                
                if (cbStatus.SelectedItem != null)
                {
                    if (cbStatus.SelectedItem.ToString() == "LIST")
                        textBox1.Text += " AND (TRF='CLU' OR TRF='REQ' OR TRF='LOA')";
                    else if (cbStatus.SelectedItem.ToString() == "<>N/A")
                        textBox1.Text += " AND TRF<>'N/A'";
                    else if (cbStatus.SelectedItem.ToString() == "CHEAP")
                        textBox1.Text += " AND (TRF='FRE' OR PRICE<100000)";
                    else if (cbStatus.SelectedItem.ToString() == "AVAILABLE")
                        textBox1.Text += " AND AVAIL IN ('yes','maybe','bcr')";
                    else if (cbStatus.SelectedItem.ToString() == "CHANCE")
                        textBox1.Text += " AND AVAIL IN ('yes','maybe','bcr') AND CHANCE IS NOT NULL";
                    else if (cbStatus.SelectedItem.ToString() != "")
                        textBox1.Text += " AND TRF='" + cbStatus.SelectedItem.ToString() + "'";
                }
                if (cbForeign.SelectedItem != null && !cbForeign.SelectedItem.Equals(""))
                {
                    if (cbForeign.SelectedItem.Equals("my country"))
                    {
                        List<Manager> list = SaveGame.ReadManagerData();
                        if (list[list.Count - 1].cn.Equals("England"))
                            textBox1.Text += " AND CN IN ('England','Wales','Eire','Scotland')";
                        else
                            textBox1.Text += " AND CN='" + list[list.Count - 1].cn + "'";
                    }
                    else
                        textBox1.Text += " AND FGN='" + cbForeign.SelectedItem + "'";
                }
                if (cbPosition.SelectedItem != null && !cbPosition.SelectedItem.Equals(""))
                    textBox1.Text += " AND " + cbPosition.SelectedItem + ">=1";
                if (cbSide.SelectedItem != null && !cbSide.SelectedItem.Equals(""))
                    textBox1.Text += " AND " + cbSide.SelectedItem + ">=1";
                if (tbPrice.Text != "")
                {
                    int price = int.TryParse(tbPrice.Text, out price) ? price * 1000 : 0;
                    textBox1.Text += " AND PRICE<=" + price;
                }
                if (tbSkill.Text != "") textBox1.Text += " AND SKILL>=" + tbSkill.Text;

                //final filter based on textbox input and previously entered custom filter
                textBox1.Text = Regex.Replace(textBox1.Text.Trim(), "^AND ", "", RegexOptions.IgnoreCase);
            }

            try
            {
                dv.RowFilter = textBox1.Text + (textBox1.Text.Equals("")?"": " OR Name = 'Average'") ;
                //if (dt.TableName == "Player")
                //   dv.Sort = "POT DESC";
                string lastRecordName = dv.Table.Rows[0]["Name"].ToString();
                if (lastRecordName.Equals("Average")) dv.Table.Rows[0].Delete();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            switch (b.Text)
            {
                case "D/DM BEST":
                    textBox1.Text = "(SW=2 OR D=2 OR DM=2 OR M=2) AND DET>=10 AND TAC>=15 AND POSI>=15 AND HEA>=10";
                    filter();
                    break;
                case "D/DM GOOD":
                    textBox1.Text = "(SW=2 OR D=2 OR DM=2 OR M=2) AND DET>=10 AND TAC>=12 AND POSI>=12 AND HEA>=10";
                    filter();
                    break;
                case "D/DM FAIR":
                    textBox1.Text = "(SW=2 OR D=2 OR DM=2 OR M=2) AND DET>=5 AND TAC>=10 AND POSI>=10 AND HEA>=8";
                    filter();
                    break;
                case "MF BEST":
                    //MRL CRE 15 DET 15 OFF 15 PASS 15 POSI 10 TAC 10 PAC 10
                    //MC CRE 6 DET 15 OFF 15 POS 15 TAC 10 PAC 10 PASS 10
                    //80x1.5 + 60 = max 180 50% 66% 75%
                    textBox1.Text = "(DM=2 OR M=2 OR AM=2) AND ((CRE+DET+OFF+PAS)*1.5+POSI+TAC+PAC)>140";
                    filter();
                    break;
                case "MF GOOD":
                    textBox1.Text = "(DM=2 OR M=2 OR AM=2) AND ((CRE+DET+OFF+PAS)*1.5+POSI+TAC+PAC)>120";
                    filter();
                    break;
                case "MF FAIR":
                    textBox1.Text = "(DM=2 OR M=2 OR AM=2) AND ((CRE+DET+OFF+PAS)*1.5+POSI+TAC+PAC)>90";
                    filter();
                    break;
                case "FC BEST":
                    textBox1.Text = "(AM=2 OR S=2) AND DET>=10 AND OFF>=15 AND SHO>=15 AND HEA>=10";
                    filter();
                    break;
                case "FC GOOD":
                    textBox1.Text = "(M=2 OR AM=2 OR S=2) AND DET>=10 AND OFF>=12 AND SHO>=12 AND HEA>=5";
                    filter();
                    break;
                case "FC FAIR":
                    textBox1.Text = "(M=2 OR AM=2 OR S=2) AND DET>=5 AND OFF>=10 AND SHO>=10 AND HEA>=5";
                    filter();
                    break;
                case "GK BEST":
                    textBox1.Text = "GK=2 AND DET>=10 AND HEA>=15 AND POSI>=18 AND TAC>=18";
                    filter();
                    break;
                case "GK GOOD":
                    textBox1.Text = "GK=2 AND DET>=10 AND HEA>=15 AND POSI>=15 AND TAC>=15";
                    filter();
                    break;
                case "GK FAIR":
                    textBox1.Text = "GK=2 AND HEA>=10 AND POSI>=15 AND TAC>=15";
                    filter();
                    break;
                case "My Club":
                    if (dt.TableName == "Team")
                        textBox1.Text = "Name='" + SaveGame.managerList[SaveGame.managerList.Count - 1].club + "'";
                    else
                        textBox1.Text = "Club='" + SaveGame.managerList[SaveGame.managerList.Count - 1].club + "'";
                    dv.RowFilter = textBox1.Text;

                    break;
                case "My Country":
                    if (dt.TableName == "Player")
                        textBox1.Text = "CLUB_CN='" + SaveGame.managerList[SaveGame.managerList.Count - 1].cn + "'";
                    else
                        textBox1.Text = "CN='" + SaveGame.managerList[SaveGame.managerList.Count - 1].cn + "'";
                    dv.RowFilter = textBox1.Text;
                    break;
                case "My Shortlist":
                    Team myTeam = SaveGame.teamList[SaveGame.managerList[SaveGame.managerList.Count - 1].clubID];

                    String filename = SaveGame.BackupPath + "cmshortlist" + SaveGame.SelectedSaveGame + ".txt";
                    string additionalShortlist = "";
                    if (File.Exists(filename))
                    {
                        additionalShortlist = File.ReadAllText(filename).Replace("\r\n", ",");
                    }
                    textBox1.Text = "Name IN ('" + (additionalShortlist + myTeam.shortlist).Replace("'", "''").Replace(",,", ",").Replace(",", "','") + "')";

                    dv.RowFilter = textBox1.Text;
                    break;
                case "FK/Corner":
                    textBox1.Text = "Club='" + SaveGame.managerList[SaveGame.managerList.Count - 1].club + "' AND (CRE+PAS+SET)>=35";
                    dv.RowFilter = textBox1.Text;
                    break;
                case "Show Average":
                    DataRow dr = dt.NewRow();
                    dr["Name"] = "Average";
                    foreach (DataGridViewColumn col in sqlDatagridview1.Columns)
                    {
                        switch (SqlDataGridView.GetValueType(col.ValueType))
                        {
                            case "Double":
                            case "Int":
                                dr[col.Name] = dv.Table.Compute("Avg(" + col.Name + ")", textBox1.Text);
                                break;
                        }
                    }
                    dv.Table.Rows.InsertAt(dr, 0);
                    dv.Sort = "";
                    break;
                case "Filter":
                    filter();
                    break;
                case "Reset":
                    foreach (Control c in splitContainer1.Panel2.Controls)
                    {
                        if (c is NumericUpDown)
                        {
                            ((NumericUpDown)c).Value = 0;
                        }
                        else if (c is TextBox)
                            ((TextBox)c).Text = "";
                        else if (c is ComboBox)
                            ((ComboBox)c).SelectedIndex = 0;
                    }
                    textBox1.Text = "";
                    filter();
                    break;

            }

        }
  
     

        private void sqlDatagridview1_CellContentClick(object sender, MouseEventArgs e)
        {
            if ((e.Clicks == 2 && e.Button == MouseButtons.Left) || e.Button == MouseButtons.Right)
            {
                if (dt.TableName.Equals("Player"))
                {
                    if (sqlDatagridview1.CurrentCell.OwningColumn.Name.Equals("buy"))
                    {
                        string name = sqlDatagridview1.CurrentRow.Cells["Name"].Value.ToString();
                        //List<Team> teamList = SaveGame.ReadTeamData(false, true);
                        string interestedClub = "";
                        for (int i = 0; i < SaveGame.teamList.Count; i++)
                        {
                            if (SaveGame.teamList[i].shortlist != null && SaveGame.teamList[i].shortlist.Contains(name))
                            {
                                interestedClub += SaveGame.teamList[i].name + " - " + SaveGame.teamList[i].division+ " - " + SaveGame.teamList[i].balance.ToString("N0") + "\n";
                            }
                        }
                        MessageBox.Show(interestedClub, "Interested Club");
                    }
                    else if (sqlDatagridview1.CurrentCell.OwningColumn.Name.Equals("chance"))
                    {
                        string clubName = sqlDatagridview1.CurrentRow.Cells["club"].Value.ToString();
                        int playerID = int.Parse(sqlDatagridview1.CurrentRow.Cells["ID1"].Value.ToString());
                        Player p = SaveGame.playerList[playerID];
                        Team PlayerTeam = SaveGame.teamList.Find(x => x.name.Equals(clubName));
                        Team MyTeam = SaveGame.teamList[SaveGame.MyClubID()];
                        //check for bcr condition met
                        bool bcr = (p.bcr.Equals("yes") && p.cpop < MyTeam.pop && p.crep < MyTeam.rep - 35);
                        string msg = p.Name + "\n\n" +
                            "Player Rep: " + p.rep + "\nPlayer Pot: " + p.pot + "\n\n"+
                            clubName + " Rep: " + PlayerTeam.rep + "\n" + clubName + " Pop: " + PlayerTeam.pop + "\n\n" +
                            MyTeam.name + " Rep: " + MyTeam.rep + "\n" + MyTeam.name +" Pop: " + MyTeam.pop +  "\n\n";

                        //player will negotiate depends on:
                        //1. player potential, reputation, player on transfer list or loan list
                        //2. player's club reputation and popularity and league
                        //3. My club reputation and popularity and league
                        int clubreputation_varian = MyTeam.rep - PlayerTeam.rep;
                        int clubpopularity_varian = (MyTeam.pop - PlayerTeam.pop) * 5;
                        int varian = clubreputation_varian + clubpopularity_varian;
                        msg += "Rep Varian: " + clubreputation_varian + (bcr ? " (bcr)" : "") + "\nPop Varian: " + clubpopularity_varian + "\n\n";
                        if (p.TRF.Equals("REQ") || p.TRF.Equals("LOA") || p.TRF.Equals("CLU"))
                        {
                            msg += "Player is Listed\n";
                            varian += 50; //add chance of join when listed
                        }
                        msg += "Varian Assume: " + varian + "\n\n";
                        if (varian < 0 )
                            msg += "Join Chance: 0 %";
                        else if (varian >= 100)
                            msg += "Join Chance: 100 %";
                        else if (varian >= 50)
                            msg += "Join Chance: 90 %";
                        else
                            msg += "Join Chance: 50/50";
                        MessageBox.Show(msg, "Join Chance Calculation");
                    }
                    else if (sqlDatagridview1.CurrentCell.OwningColumn.Name.Equals("club") || 
                        sqlDatagridview1.CurrentCell.OwningColumn.Name.Equals("avail"))
                    {
                        string clubName = sqlDatagridview1.CurrentRow.Cells["club"].Value.ToString();
                        string clubRep = sqlDatagridview1.CurrentRow.Cells["CRep"].Value.ToString();
                        string clubPop = sqlDatagridview1.CurrentRow.Cells["CPop"].Value.ToString();
                        Team team = SaveGame.teamList.Find(x => x.name.Equals(clubName));
                        string shortlisted = clubName + " balance: " + team.balance.ToString("N0") +
                            "\nClub Rep: " + clubRep + "  Club Pop: " + clubPop + "\n\n";

                        int clubreputation_varian = (int)(int.Parse(clubRep) / 2) + 120;    //(150/2)+120 = 195     
                        int clubpopularity_varian = (20 - int.Parse(clubPop)) * 5;         //20-18 * 5 = 10
                        int req = clubreputation_varian - clubpopularity_varian;
                        //169/2 + 120 84+120 = 204 - 15 = 194
                        shortlisted += "Player Potential For sale < " + req + "\n\n";
                        if (team.shortlist != null)
                        {
                            foreach (string playerName in team.shortlist.Split(','))
                            {
                                Player p = SaveGame.playerList.Find(x => x.Name.Equals(playerName));
                                shortlisted += p.Name + " - " + p.club + " - " + p.price.ToString("N0") + "\n";
                            }
                        }
                        MessageBox.Show(shortlisted, clubName + " shortlist");
                    }
                    else
                    {
                        FormPlayer form = new FormPlayer(sqlDatagridview1.CurrentRow);
                        form.ShowDialog(this);
                    }
                }
            }
        }

     
        
        private void playerDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //playerList = SaveGame.ReadPlayersData(ReadNameOnly: false);
            SaveGame.SelectSaveGame(SaveGame.SelectedSaveGame);
            Bind(SaveGame.playerList);
            dv.Sort = "POT DESC";
            SaveGame game = SaveGame.ReadSaveGameList().First(s => s.index == SaveGame.SelectedSaveGame);
            this.Text = "CM2 Viewer - " + game.SaveName + " (" + game.SaveDateTime.ToLongDateString() + ")";
        }

        private void teamDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Bind(SaveGame.ReadTeamData(false,true));
            Bind(SaveGame.teamList);
        }

        private void managerDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bind(SaveGame.ReadManagerData());
        }
        private void competitionDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bind(SaveGame.competitionList);
        }

        private void searchNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = "";
            if (Helper.InputBox("", "", ref value) == DialogResult.OK)
            {
                textBox1.Text = "Name LIKE '%" + value + "%'";
                dv.RowFilter = textBox1.Text;
            }
        }

        private void searchTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = "";
            if (Helper.InputBox("", "", ref value) == DialogResult.OK)
            {
                if (dt.TableName == "Team")
                    textBox1.Text = "Name LIKE '%" + value + "%'";
                else
                    textBox1.Text = "CLUB LIKE '%" + value + "%'";
                dv.RowFilter = textBox1.Text;
            }
            
        }

        private async void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string notes = "";
            if (Helper.InputBox("Backup", "Enter Backup Notes", ref notes) != DialogResult.OK)
                return;

            /*
            foreach (var file in Directory.GetFiles(SaveGame.path, "??????" + SaveGame.SelectedSaveGame.ToString() + ".S16"))
            {
                string destFileName = Path.Combine(SaveGame.BackupPath, Path.GetFileName(file));
                File.Copy(file, destFileName,true);
            }*/
            string filename = await DoBackup(notes);
            new System.Threading.Thread(() =>
            {
                //prevent msgbox from blocking ??? seems to minimize ui lag
                Console.WriteLine("done backup 1");
                MessageBox.Show("done backup to " + filename);
            }).Start();
            PopulateRestore();  
        }

        private static async Task<string> DoBackup(string notes)
        {
            string fileName = "";
            await Task.Run(() =>
            {
                fileName = SaveGame.BackupPath + SaveGame.SelectedSaveGame + "S16." + SaveGame.ReadCurDate(SaveGame.SelectedSaveGame).ToString("yyMMdd") + notes + ".zip";
                if (File.Exists(fileName)) File.Delete(fileName);
                using (var zip = ZipFile.Open(fileName, ZipArchiveMode.Create))
                {
                    foreach (var file in Directory.GetFiles(SaveGame.path, "??????" + SaveGame.SelectedSaveGame.ToString() + ".S16"))
                    {
                        zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                    }
                }
                
            });
            return fileName;
        }

        private void PopulateRestore()
        {
            PopulateRestore(restoreToolStripMenuItem);
            PopulateRestore(compareDBToolStripMenuItem);
        }

        private async void PopulateRestore(ToolStripMenuItem ts)
        {
            await Task.Run(() =>
            {
                ts.DropDownItems.Clear();
                //compareDBToolStripMenuItem.DropDownItems.Clear();

                /*IOrderedEnumerable<string> list1 = from d in Directory.GetDirectories(SaveGame.path)
                                                   select d.Split('\\').Last() into d
                                                   where Regex.Match(d, "^[0-9]+").Success
                                                   orderby d descending
                                                   select d;
                */
                IOrderedEnumerable<string> list2 = from d in Directory.GetFiles(SaveGame.BackupPath, SaveGame.SelectedSaveGame + "S16.*.zip")
                                                   select d.Split('\\').Last() into d
                                                   orderby d descending
                                                   select d;
                foreach (string name in list2) //.Concat(list1))
                {
                    Action addAction = delegate ()
                    {
                        ToolStripItem toolStripItem = ts.DropDownItems.Add(name);
                        toolStripItem.Click += delegate {
                            if (ts == restoreToolStripMenuItem)
                                restore(name);
                            else
                                comparedb(name); 
                        };                       
                    };
                    ts.GetCurrentParent().Invoke(addAction);
                }
            });
        }

        private void comparedb(string name)
        {
            if (name.EndsWith(".zip"))
            {
                if (!Directory.Exists(SaveGame.BackupPath + "tmp\\"))
                    Directory.CreateDirectory(SaveGame.BackupPath + "tmp\\");
                using (ZipArchive zip = ZipFile.OpenRead(SaveGame.BackupPath + name))
                {
                    foreach (var file in zip.Entries)
                    {
                        file.ExtractToFile(SaveGame.BackupPath + "tmp\\" + file.FullName, true);
                    }
                }
            }
            List<Player> playerList1 = SaveGame.ReadPlayersData(false);
            DataTable dt1 = Helper.BuildDataTable(playerList1, "db",1);
            List<Player> playerList2 = SaveGame.ReadPlayersData(false, SaveGame.BackupPath + "tmp\\");
            DataTable dt2 = Helper.BuildDataTable(playerList2, "db", 2);
            dt1.Merge(dt2);

            BindDataTable(dt1);
            sqlDatagridview1.Columns["db"].DisplayIndex = 0;
            dv.Sort = "POT DESC, Name ASC";
        }

        private void restore(string name)
        {

            if (name.EndsWith(".zip"))
            {
                using (ZipArchive zip = ZipFile.OpenRead(SaveGame.BackupPath + name))
                {
                    foreach (var file in zip.Entries)
                    {
                        file.ExtractToFile(SaveGame.path + file.FullName, true);
                    }
                }
            }
            else
            {
                String fileName = SaveGame.path + name;
                if (Directory.Exists(fileName))
                {
                    foreach (string filename in Directory.GetFiles(fileName, "*" + SaveGame.SelectedSaveGame +  ".S16"))
                    {
                        string destFileName = Path.Combine(SaveGame.path, Path.GetFileName(filename));
                        File.Copy(filename, destFileName, true);
                    }
                }
            }
            //reload datagridview
            playerDBToolStripMenuItem.PerformClick();
            //MessageBox.Show("done restore from " + name);
        }

  
        private void updateAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string url = "https://github.com/cm2viewer/cm/releases/download/v1.0/cm.exe";
            string exeName = System.Reflection.Assembly.GetEntryAssembly().Location;
            byte[] b;
            using (WebClient wc = new WebClient())
            {
                b = wc.DownloadData(url);                
            }
            if (b != null)
            {
                if (!b.SequenceEqual(File.ReadAllBytes(exeName)))
                {
                    if (File.Exists(exeName + ".bak")) File.Delete(exeName + ".bak");
                    File.Move(exeName, exeName + ".bak");
                    File.WriteAllBytes(exeName, b);
                    MessageBox.Show("Application will restart now");
                    Application.Restart();
                    Environment.Exit(0);
                }
                else
                {
                    MessageBox.Show("Application is the latest version");
                }
            }
        }

     
        private void localAppUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string exeName = System.Reflection.Assembly.GetEntryAssembly().Location;
            String filename = "c:\\repos\\bin\\debug\\cm.exe";
            if (File.Exists(filename))
            {

                if (File.Exists(exeName + ".bak")) File.Delete(exeName + ".bak");
                File.Move(exeName, exeName + ".bak");
                File.Copy(filename, exeName);
                Application.Restart();
                Environment.Exit(0);
            }
        }

       
    }
}
