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
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnMyCountry = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbRep = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTalent = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbAbility = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPotential = new System.Windows.Forms.TextBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.sqlDatagridview1 = new SqlDataGridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sqlDatagridview1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(97, 27);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "FC GOOD";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(3, 27);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(88, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "D/DM GOOD";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(838, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(47, 21);
            this.button3.TabIndex = 3;
            this.button3.Text = "Filter";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(250, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(635, 47);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(97, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "FC BEST";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "D/DM BEST";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.splitContainer2.Panel1.Controls.Add(this.btnMyCountry);
            this.splitContainer2.Panel1.Controls.Add(this.button10);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.tbRep);
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            this.splitContainer2.Panel1.Controls.Add(this.tbTalent);
            this.splitContainer2.Panel1.Controls.Add(this.button9);
            this.splitContainer2.Panel1.Controls.Add(this.button8);
            this.splitContainer2.Panel1.Controls.Add(this.button7);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.tbAbility);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.tbPotential);
            this.splitContainer2.Panel1.Controls.Add(this.cbStatus);
            this.splitContainer2.Panel1.Controls.Add(this.button12);
            this.splitContainer2.Panel1.Controls.Add(this.button11);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.textBox2);
            this.splitContainer2.Panel1.Controls.Add(this.button6);
            this.splitContainer2.Panel1.Controls.Add(this.button3);
            this.splitContainer2.Panel1.Controls.Add(this.textBox1);
            this.splitContainer2.Panel1.Controls.Add(this.button5);
            this.splitContainer2.Panel1.Controls.Add(this.button2);
            this.splitContainer2.Panel1.Controls.Add(this.button1);
            this.splitContainer2.Panel1.Controls.Add(this.button4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.sqlDatagridview1);
            this.splitContainer2.Size = new System.Drawing.Size(894, 426);
            this.splitContainer2.SplitterDistance = 74;
            this.splitContainer2.TabIndex = 4;
            // 
            // btnMyCountry
            // 
            this.btnMyCountry.Location = new System.Drawing.Point(714, 53);
            this.btnMyCountry.Name = "btnMyCountry";
            this.btnMyCountry.Size = new System.Drawing.Size(69, 21);
            this.btnMyCountry.TabIndex = 29;
            this.btnMyCountry.Text = "My Country";
            this.btnMyCountry.UseVisualStyleBackColor = true;
            this.btnMyCountry.Click += new System.EventHandler(this.btnMyCountry_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(789, 53);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(43, 21);
            this.button10.TabIndex = 28;
            this.button10.Text = "Reset";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(368, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Rep";
            // 
            // tbRep
            // 
            this.tbRep.Location = new System.Drawing.Point(396, 54);
            this.tbRep.Name = "tbRep";
            this.tbRep.Size = new System.Drawing.Size(26, 20);
            this.tbRep.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(425, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Tal";
            // 
            // tbTalent
            // 
            this.tbTalent.Location = new System.Drawing.Point(451, 54);
            this.tbTalent.Name = "tbTalent";
            this.tbTalent.Size = new System.Drawing.Size(26, 20);
            this.tbTalent.TabIndex = 24;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(178, 51);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(66, 23);
            this.button9.TabIndex = 23;
            this.button9.Text = "GK FREE";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(97, 51);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 22;
            this.button8.Text = "FC FREE";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(3, 51);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(88, 23);
            this.button7.TabIndex = 21;
            this.button7.Text = "D/DM FREE";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Ability";
            // 
            // tbAbility
            // 
            this.tbAbility.Location = new System.Drawing.Point(284, 54);
            this.tbAbility.Name = "tbAbility";
            this.tbAbility.Size = new System.Drawing.Size(26, 20);
            this.tbAbility.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(558, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Pot";
            // 
            // tbPotential
            // 
            this.tbPotential.Location = new System.Drawing.Point(339, 54);
            this.tbPotential.Name = "tbPotential";
            this.tbPotential.Size = new System.Drawing.Size(26, 20);
            this.tbPotential.TabIndex = 16;
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
            this.cbStatus.Location = new System.Drawing.Point(600, 53);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(49, 21);
            this.cbStatus.TabIndex = 15;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(178, 27);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(66, 23);
            this.button12.TabIndex = 14;
            this.button12.Text = "GK GOOD";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(178, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(66, 23);
            this.button11.TabIndex = 13;
            this.button11.Text = "GK BEST";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(481, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Age";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(509, 54);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(35, 20);
            this.textBox2.TabIndex = 9;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(655, 53);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(53, 21);
            this.button6.TabIndex = 6;
            this.button6.Text = "My Club";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
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
            this.sqlDatagridview1.Size = new System.Drawing.Size(894, 348);
            this.sqlDatagridview1.TabIndex = 0;
            this.sqlDatagridview1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.sqlDatagridview1_CellContentDoubleClick);
            this.sqlDatagridview1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sqlDatagridview1_CellContentClick);
            this.sqlDatagridview1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.sqlDatagridview1_CellContentClick);
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
            this.menuStrip1.Size = new System.Drawing.Size(894, 24);
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 450);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "CM2 Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sqlDatagridview1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataView dv;

        private DataTable dt;



        private SqlDataGridView sqlDatagridview1;

        private Button button1;

        private Button button2;

        private Button button3;

        private TextBox textBox1;

        private Button button4;

        private Button button5;

        private SplitContainer splitContainer2;

        private Button button6;

        private Label label1;

        private TextBox textBox2;

        private ImageList imageList1;

        private Button button11;

        private Button button12;

        private MenuStrip menuStrip1;

        private ToolStripMenuItem playerDBToolStripMenuItem;

        private ToolStripMenuItem teamDBToolStripMenuItem;

        private ToolStripMenuItem searchNameToolStripMenuItem;

        private ToolStripMenuItem searchTeamToolStripMenuItem;

        private ComboBox cbStatus;

        private Label label2;

        private TextBox tbPotential;

        private Label label3;

        private Label label4;

        private TextBox tbAbility;

        private ToolStripMenuItem managerDBToolStripMenuItem;

        private Button button9;

        private Button button8;

        private Button button7;

        private ToolStripMenuItem backupToolStripMenuItem;

        private ToolStripMenuItem restoreToolStripMenuItem;

        private Label label5;

        private TextBox tbRep;

        private Label label6;

        private TextBox tbTalent;

        private Button button10;
        private Button btnMyCountry;
        private ToolStripMenuItem competitionDBToolStripMenuItem;
    }
}