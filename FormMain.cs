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
            this.Text += " - " + SaveGame.ReadSaveGameList().First(s => s.index == SaveGame.SelectedSaveGame).SaveName;
            Bind(SaveGame.ReadPlayersData(ReadNameOnly: false));
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
            int[] list = { 6,7,8, 9, 10, 11,12,13,14 };
            foreach (int i in list)
                fontSizeToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(i.ToString(),null, FontSizeDropDown_Click));
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
        }

        public void Bind<T>(List<T> list)
        {
            dt = BuildDataTable(list);
            dv = dt.DefaultView;
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dt;
            sqlDatagridview1.Bind_to_Bindingsource(bindingSource);
            sqlDatagridview1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            SetButtons();
        }
       

        public static DataTable BuildDataTable<T>(IList<T> lst)
        {
            DataTable dataTable = CreateTable<T>();
            Type typeFromHandle = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeFromHandle);
            foreach (T item in lst)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (PropertyDescriptor item2 in properties)
                {
                    dataRow[item2.Name] = item2.GetValue(item);
                }
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        private static DataTable CreateTable<T>()
        {
            Type typeFromHandle = typeof(T);
            DataTable dataTable = new DataTable(typeFromHandle.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeFromHandle);
            foreach (PropertyDescriptor item in properties)
            {
                dataTable.Columns.Add(item.Name, item.PropertyType);
            }
            return dataTable;
        }

        private void filter()
        {
            if (dt.TableName == "Player")
            {
                if (!textBox1.Text.Trim().Equals(""))
                    textBox1.Text = " AND " + textBox1.Text;
                for (int i = 0; i < Player.skillName.Length; i++)
                {
                    textBox1.Text = Regex.Replace(textBox1.Text,
                        " AND " + Player.skillName[i] + ">=([0-9]+)", "");
                }
                textBox1.Text = Regex.Replace(textBox1.Text, " AND ABI>=(.*)", "");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND POT>=(.*)", "");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND REP<=(.*)", "");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND TAL<=(.*)", "");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND AGE<=(.*)", "");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND FGN='(.*)'", "");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND Rating>=(.*)", "");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND (GK|SW|D|DM|M|AM|S|R|L|C)>=(.*)", "");
                textBox1.Text = Regex.Replace(textBox1.Text, " AND PRICE<=(.*)", "");
                foreach (Control c in splitContainer1.Panel2.Controls)
                {
                    if (c is NumericUpDown)
                    {
                        NumericUpDown nud = (NumericUpDown)c;
                        if (nud.Value > 0)
                        {
                            textBox1.Text = Regex.Replace(textBox1.Text, " AND " + nud.Name.ToUpper() + "([<>])=([0-9,.]+)", "");
                            String s = textBox1.Text;
                            if (nud.Name.Equals("Dir") || nud.Name.Equals("Inj"))
                                textBox1.Text = s + " AND " + nud.Name.ToUpper() + "<=" + nud.Value.ToString().Replace(',','.');
                            else
                                textBox1.Text = s + " AND " + nud.Name.ToUpper() + ">=" + nud.Value.ToString().Replace(',', '.');
                        }
                    }
                }
                
               
                if (tbAbility.Text != "") textBox1.Text += " AND ABI>=" + tbAbility.Text;
                if (tbPotential.Text != "") textBox1.Text += " AND POT>=" + tbPotential.Text;
                if (tbRep.Text != "") textBox1.Text += " AND REP<=" + tbRep.Text;
                if (tbTalent.Text != "") textBox1.Text += " AND TAL<=" + tbTalent.Text;
                if (this.tbAge.Text != "") textBox1.Text += " AND AGE<=" + this.tbAge.Text;
                if (cbStatus.SelectedItem != null)
                {
                    textBox1.Text = Regex.Replace(textBox1.Text, " AND TRF([<=>]+'[A-Z/]+')", "");
                    textBox1.Text = Regex.Replace(textBox1.Text, " AND (\\(TRF.+ OR .+\\))", "");
                    if (cbStatus.SelectedItem.ToString() == "<>N/A")
                    {
                        textBox1.Text += " AND TRF<>'N/A'";
                    }
                    else if (cbStatus.SelectedItem.ToString() == "CHEAP")
                    {
                        textBox1.Text += " AND (TRF='FRE' OR PRICE<100000)";
                    }
                    else if (cbStatus.SelectedItem.ToString() != "")
                    {
                        TextBox textBox6 = textBox1;
                        textBox6.Text = textBox6.Text + " AND TRF='" + cbStatus.SelectedItem.ToString() + "'";
                    }
                }
                if (cbForeign.SelectedItem != null && !cbForeign.SelectedItem.Equals(""))
                    textBox1.Text += " AND FGN='" + cbForeign.SelectedItem + "'";
                if (cbPosition.SelectedItem != null && !cbPosition.SelectedItem.Equals(""))
                    textBox1.Text += " AND " + cbPosition.SelectedItem + ">=1";
                if (cbSide.SelectedItem != null && !cbSide.SelectedItem.Equals(""))
                    textBox1.Text += " AND " + cbSide.SelectedItem + ">=1";
                if (tbPrice.Text != "") textBox1.Text += " AND PRICE<=" + (int.Parse(tbPrice.Text)*1000);

            }

            textBox1.Text = Regex.Replace(textBox1.Text, "^ AND ", "",RegexOptions.IgnoreCase);
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(SW=2 OR D=2 OR DM=2 OR M=2) AND DET>=10 AND TAC>=15 AND POSI>=15 AND HEA>=10 AND STA>=10";
            filter();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(SW=2 OR D=2 OR DM=2 OR M=2) AND DET>=10 AND TAC>=12 AND POSI>=12 AND HEA>=10 AND STA>=10";
            filter();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(SW=2 OR D=2 OR DM=2 OR M=2) AND DET>=5 AND TAC>=10 AND POSI>=10 AND HEA>=8 AND STA>=8";
            filter();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(DM=2 OR M=2 OR AM=2) AND (CRE+OFF+PAC+POSI+SHO)>70";
            filter();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(DM=2 OR M=2 OR AM=2) AND (CRE+OFF+PAC+POSI+SHO)>60";
            filter();
        }
        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(DM=2 OR M=2 OR AM=2) AND (CRE+OFF+PAC+POSI+SHO)>50";
            filter();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            List<Manager> list = SaveGame.ReadManagerData();
            textBox1.Text = "Club='" + list[list.Count - 1].club + "' AND (CRE+PAS+SET)>=35";
            dv.RowFilter = textBox1.Text;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(AM=2 OR S=2) AND DET>=10 AND OFF>=15 AND SHO>=15 AND HEA>=10 AND STA>=10";
            filter();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(M=2 OR AM=2 OR S=2) AND DET>=10 AND OFF>=12 AND SHO>=12 AND HEA>=5 AND STA>=10";
            filter();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = "(M=2 OR AM=2 OR S=2) AND DET>=5 AND OFF>=10 AND SHO>=10 AND HEA>=5 AND STA>=8";
            filter();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            filter();
        }

     

        private void sqlDatagridview1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void sqlDatagridview1_CellContentClick(object sender, MouseEventArgs e)
        {
            if ((e.Clicks == 2 && e.Button == MouseButtons.Left) || e.Button == MouseButtons.Right)
            {
                if (dt.TableName.Equals("Player"))
                {
                    FormPlayer form = new FormPlayer(sqlDatagridview1.CurrentRow);
                    form.ShowDialog(this);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = "GK=2 AND DET>=10 AND HEA>=15 AND POSI>=18 AND TAC>=18";
            filter();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = "GK=2 AND DET>=10 AND HEA>=15 AND POSI>=15 AND TAC>=15";
            filter();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "GK=2 AND HEA>=10 AND POSI>=15 AND TAC>=15";
            filter();
        }

        private void playerDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bind(SaveGame.ReadPlayersData(ReadNameOnly: false));
        }

        private void teamDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bind(SaveGame.ReadTeamData(false,true));
        }

        private void managerDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bind(SaveGame.ReadManagerData());
        }
        private void competitionDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bind(SaveGame.ReadCompetitionData(false));
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
            if (dt.TableName == "Player")
            {
                string value = "";
                if (Helper.InputBox("", "", ref value) == DialogResult.OK)
                {
                    textBox1.Text = "CLUB LIKE '%" + value + "%'";
                    dv.RowFilter = textBox1.Text;
                }
            }
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string notes = "";
            if (Helper.InputBox("Backup", "Enter Backup Notes", ref notes) != DialogResult.OK)
                return;
            string fileName = SaveGame.BackupPath +SaveGame.SelectedSaveGame + "S16." + SaveGame.ReadCurDate(SaveGame.SelectedSaveGame).ToString("yyMMdd") + notes + ".zip";

            if (File.Exists(fileName)) File.Delete(fileName);
            using (var zip = ZipFile.Open(fileName, ZipArchiveMode.Create))
            {
                foreach (var file in Directory.GetFiles(SaveGame.path, "*" + SaveGame.SelectedSaveGame + ".S16"))
                {
                    zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                }
            }

            /*
            if (value != "")
            {
                text = text + " " + value;
            }
            string path = "D:\\Download\\cm9798\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*7.S16");
                string[] array = files;
                foreach (string text2 in array)
                {
                    string destFileName = Path.Combine(text, Path.GetFileName(text2));
                    File.Copy(text2, destFileName, overwrite: true);
                }
            }
            */
            PopulateRestore();
            MessageBox.Show("done backup to " + fileName);
        }

        private void PopulateRestore()
        {
            restoreToolStripMenuItem.DropDownItems.Clear();
            
            IOrderedEnumerable<string> list1 = from d in Directory.GetDirectories(SaveGame.path)
                                               select d.Split('\\').Last() into d
                                                           where Regex.Match(d, "^[0-9]+").Success
                                                           orderby d descending
                                                           select d;
                                                           
            IOrderedEnumerable<string> list2 = from d in Directory.GetFiles(SaveGame.BackupPath, SaveGame.SelectedSaveGame + "S16.*.zip")
                                                           select d.Split('\\').Last() into d
                                                           orderby d descending
                                                           select d;
            foreach (string name in list2.Concat(list1))
            {
                ToolStripItem toolStripItem = restoreToolStripMenuItem.DropDownItems.Add(name);
                toolStripItem.Click += delegate
                {
                    restore(name);
                };
            }
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
            MessageBox.Show("done restore from " + name);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            tbAbility.Text = "";
            tbPotential.Text = "";
            tbTalent.Text = "";
            tbRep.Text = "";
            cbStatus.SelectedIndex = 0;
            tbAge.Text = "";
            foreach (Control c in splitContainer1.Panel2.Controls)
            {
                if (c is NumericUpDown)
                {
                    ((NumericUpDown)c).Value = 0;
                }
            }
            filter();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<Manager> list = SaveGame.ReadManagerData();
            if (dt.TableName == "Team")
                textBox1.Text = "Name='" + list[list.Count - 1].club + "'";
            else 
                textBox1.Text = "Club='" + list[list.Count - 1].club + "'";
            dv.RowFilter = textBox1.Text;

        }
        private void btnMyCountry_Click(object sender, EventArgs e)
        {
            List<Manager> list = SaveGame.ReadManagerData();
            if (dt.TableName == "Player")
                textBox1.Text = "CLUB_CN='" + list[list.Count - 1].cn + "'";
            else
                textBox1.Text = "CN='" + list[list.Count - 1].cn + "'";
            dv.RowFilter = textBox1.Text;
            
        }

        private void btnMyDivision_Click(object sender, EventArgs e)
        {
            List<Manager> list = SaveGame.ReadManagerData();
            List<Team> teamList = SaveGame.ReadTeamData(false,false);
            textBox1.Text = "Division='" + teamList[list[list.Count - 1].clubID].division + "'";
            dv.RowFilter = textBox1.Text;
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

        private void button17_Click(object sender, EventArgs e)
        {
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
        }

        
    }
}
