using System.Data;
using System.Windows.Forms;

namespace cm
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
       	private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.playerDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teamDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.competitionDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managerDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchTeamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.btnMyCountry = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tbTalent = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.button10 = new System.Windows.Forms.Button();
            this.tbPotential = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRep = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.tbAbility = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnMyDivision = new System.Windows.Forms.Button();
            this.sqlDatagridview1 = new SqlDataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sqlDatagridview1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerDBToolStripMenuItem,
            this.teamDBToolStripMenuItem,
            this.competitionDBToolStripMenuItem,
            this.managerDBToolStripMenuItem,
            this.searchNameToolStripMenuItem,
            this.searchTeamToolStripMenuItem,
            this.backupToolStripMenuItem,
            this.restoreToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1035, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // playerDBToolStripMenuItem
            // 
            this.playerDBToolStripMenuItem.Name = "playerDBToolStripMenuItem";
            this.playerDBToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.playerDBToolStripMenuItem.Text = "Player DB";
            this.playerDBToolStripMenuItem.Click += new System.EventHandler(this.playerDBToolStripMenuItem_Click);
            // 
            // teamDBToolStripMenuItem
            // 
            this.teamDBToolStripMenuItem.Name = "teamDBToolStripMenuItem";
            this.teamDBToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.teamDBToolStripMenuItem.Text = "Team DB";
            this.teamDBToolStripMenuItem.Click += new System.EventHandler(this.teamDBToolStripMenuItem_Click);
            // 
            // competitionDBToolStripMenuItem
            // 
            this.competitionDBToolStripMenuItem.Name = "competitionDBToolStripMenuItem";
            this.competitionDBToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.competitionDBToolStripMenuItem.Text = "Competition DB";
            this.competitionDBToolStripMenuItem.Click += new System.EventHandler(this.competitionDBToolStripMenuItem_Click);
            // 
            // managerDBToolStripMenuItem
            // 
            this.managerDBToolStripMenuItem.Name = "managerDBToolStripMenuItem";
            this.managerDBToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.managerDBToolStripMenuItem.Text = "Manager DB";
            this.managerDBToolStripMenuItem.Click += new System.EventHandler(this.managerDBToolStripMenuItem_Click);
            // 
            // searchNameToolStripMenuItem
            // 
            this.searchNameToolStripMenuItem.Name = "searchNameToolStripMenuItem";
            this.searchNameToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.searchNameToolStripMenuItem.Text = "Search Name";
            this.searchNameToolStripMenuItem.Click += new System.EventHandler(this.searchNameToolStripMenuItem_Click);
            // 
            // searchTeamToolStripMenuItem
            // 
            this.searchTeamToolStripMenuItem.Name = "searchTeamToolStripMenuItem";
            this.searchTeamToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.searchTeamToolStripMenuItem.Text = "Search Team";
            this.searchTeamToolStripMenuItem.Click += new System.EventHandler(this.searchTeamToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.restoreToolStripMenuItem.Text = "Restore";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.sqlDatagridview1);
            this.splitContainer2.Size = new System.Drawing.Size(1035, 426);
            this.splitContainer2.SplitterDistance = 142;
            this.splitContainer2.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnMyDivision);
            this.splitContainer1.Panel1.Controls.Add(this.button13);
            this.splitContainer1.Panel1.Controls.Add(this.button14);
            this.splitContainer1.Panel1.Controls.Add(this.button15);
            this.splitContainer1.Panel1.Controls.Add(this.btnMyCountry);
            this.splitContainer1.Panel1.Controls.Add(this.button9);
            this.splitContainer1.Panel1.Controls.Add(this.button8);
            this.splitContainer1.Panel1.Controls.Add(this.button7);
            this.splitContainer1.Panel1.Controls.Add(this.button12);
            this.splitContainer1.Panel1.Controls.Add(this.button11);
            this.splitContainer1.Panel1.Controls.Add(this.button6);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.button5);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.button4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbTalent);
            this.splitContainer1.Panel2.Controls.Add(this.textBox2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.cbStatus);
            this.splitContainer1.Panel2.Controls.Add(this.button10);
            this.splitContainer1.Panel2.Controls.Add(this.tbPotential);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.tbRep);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.tbAbility);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Size = new System.Drawing.Size(1035, 142);
            this.splitContainer1.SplitterDistance = 419;
            this.splitContainer1.TabIndex = 34;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(99, 51);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 61;
            this.button13.Text = "FK/Corner";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(99, 27);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 60;
            this.button14.Text = "MF GOOD";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(99, 3);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 59;
            this.button15.Text = "MF BEST";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // btnMyCountry
            // 
            this.btnMyCountry.Location = new System.Drawing.Point(333, 27);
            this.btnMyCountry.Name = "btnMyCountry";
            this.btnMyCountry.Size = new System.Drawing.Size(69, 23);
            this.btnMyCountry.TabIndex = 58;
            this.btnMyCountry.Text = "My Country";
            this.btnMyCountry.UseVisualStyleBackColor = true;
            this.btnMyCountry.Click += new System.EventHandler(this.btnMyCountry_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(261, 51);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(66, 23);
            this.button9.TabIndex = 52;
            this.button9.Text = "GK FAIR";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(180, 51);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 51;
            this.button8.Text = "FC FAIR";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(5, 51);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(88, 23);
            this.button7.TabIndex = 50;
            this.button7.Text = "D/DM FAIR";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(261, 27);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(66, 23);
            this.button12.TabIndex = 43;
            this.button12.Text = "GK GOOD";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(261, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(66, 23);
            this.button11.TabIndex = 42;
            this.button11.Text = "GK BEST";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(333, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(69, 23);
            this.button6.TabIndex = 39;
            this.button6.Text = "My Club";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(5, 80);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(411, 57);
            this.textBox1.TabIndex = 35;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(5, 27);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(88, 23);
            this.button5.TabIndex = 37;
            this.button5.Text = "D/DM GOOD";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(180, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 34;
            this.button2.Text = "FC BEST";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "D/DM BEST";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(180, 27);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 38;
            this.button4.Text = "FC GOOD";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tbTalent
            // 
            this.tbTalent.Location = new System.Drawing.Point(213, 90);
            this.tbTalent.Name = "tbTalent";
            this.tbTalent.Size = new System.Drawing.Size(26, 20);
            this.tbTalent.TabIndex = 53;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(271, 90);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(35, 20);
            this.textBox2.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Age";
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "",
            "UNK",
            "CLU",
            "REQ",
            "LOA",
            "FRE",
            "N/A",
            "<>N/A",
            "CHEAP"});
            this.cbStatus.Location = new System.Drawing.Point(362, 89);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(49, 21);
            this.cbStatus.TabIndex = 44;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(190, 116);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(43, 21);
            this.button10.TabIndex = 57;
            this.button10.Text = "Reset";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // tbPotential
            // 
            this.tbPotential.Location = new System.Drawing.Point(101, 90);
            this.tbPotential.Name = "tbPotential";
            this.tbPotential.Size = new System.Drawing.Size(26, 20);
            this.tbPotential.TabIndex = 45;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(130, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Rep";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Pot";
            // 
            // tbRep
            // 
            this.tbRep.Location = new System.Drawing.Point(158, 90);
            this.tbRep.Name = "tbRep";
            this.tbRep.Size = new System.Drawing.Size(26, 20);
            this.tbRep.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(187, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Tal";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(137, 116);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(47, 21);
            this.button3.TabIndex = 36;
            this.button3.Text = "Filter";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbAbility
            // 
            this.tbAbility.Location = new System.Drawing.Point(46, 90);
            this.tbAbility.Name = "tbAbility";
            this.tbAbility.Size = new System.Drawing.Size(26, 20);
            this.tbAbility.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Ability";
            // 
            // btnMyDivision
            // 
            this.btnMyDivision.Location = new System.Drawing.Point(333, 51);
            this.btnMyDivision.Name = "btnMyDivision";
            this.btnMyDivision.Size = new System.Drawing.Size(69, 23);
            this.btnMyDivision.TabIndex = 62;
            this.btnMyDivision.Text = "My Division";
            this.btnMyDivision.UseVisualStyleBackColor = true;
            this.btnMyDivision.Click += new System.EventHandler(this.btnMyDivision_Click);
            // 
            // sqlDatagridview1
            // 
            this.sqlDatagridview1.AllowUserToAddRows = false;
            this.sqlDatagridview1.AllowUserToDeleteRows = false;
            this.sqlDatagridview1.AllowUserToOrderColumns = true;
            this.sqlDatagridview1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.sqlDatagridview1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.sqlDatagridview1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.sqlDatagridview1.DefaultCellStyle = dataGridViewCellStyle2;
            this.sqlDatagridview1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlDatagridview1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.sqlDatagridview1.Location = new System.Drawing.Point(0, 0);
            this.sqlDatagridview1.MultiSelect = false;
            this.sqlDatagridview1.Name = "sqlDatagridview1";
            this.sqlDatagridview1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.sqlDatagridview1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.sqlDatagridview1.RowHeadersVisible = false;
            this.sqlDatagridview1.RowTemplate.Height = 16;
            this.sqlDatagridview1.RowTemplate.ReadOnly = true;
            this.sqlDatagridview1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.sqlDatagridview1.Size = new System.Drawing.Size(1035, 280);
            this.sqlDatagridview1.TabIndex = 0;
            this.sqlDatagridview1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.sqlDatagridview1_CellContentDoubleClick);
            this.sqlDatagridview1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sqlDatagridview1_CellContentClick);
            this.sqlDatagridview1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.sqlDatagridview1_CellContentClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 450);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "CM2 Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sqlDatagridview1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataView dv;

        private DataTable dt;



        private SqlDataGridView sqlDatagridview1;

        private SplitContainer splitContainer2;

        private ImageList imageList1;

        private MenuStrip menuStrip1;

        private ToolStripMenuItem playerDBToolStripMenuItem;

        private ToolStripMenuItem teamDBToolStripMenuItem;

        private ToolStripMenuItem searchNameToolStripMenuItem;

        private ToolStripMenuItem searchTeamToolStripMenuItem;

        private ToolStripMenuItem managerDBToolStripMenuItem;

        private ToolStripMenuItem backupToolStripMenuItem;

        private ToolStripMenuItem restoreToolStripMenuItem;
        private ToolStripMenuItem competitionDBToolStripMenuItem;
        private SplitContainer splitContainer1;
        private Button button13;
        private Button button14;
        private Button button15;
        private Button btnMyCountry;
        private Button button10;
        private Label label5;
        private TextBox tbRep;
        private Label label6;
        private TextBox tbTalent;
        private Button button9;
        private Button button8;
        private Button button7;
        private Label label4;
        private TextBox tbAbility;
        private Label label3;
        private Label label2;
        private TextBox tbPotential;
        private ComboBox cbStatus;
        private Button button12;
        private Button button11;
        private Label label1;
        private TextBox textBox2;
        private Button button6;
        private Button button3;
        private TextBox textBox1;
        private Button button5;
        private Button button2;
        private Button button1;
        private Button button4;
        private Button btnMyDivision;
    }
}