using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cm
{
    public partial class FormSelect : Form
    {
        public FormSelect()
        {
            InitializeComponent();
        }

        private void FormSelect_Load(object sender, EventArgs e)
        {
            var list = SaveGame.ReadSaveGameList();
            for (int i=0;i<list.Count;i++)
            {
                Button b = new Button();
                b.Text = list.ElementAt(i).SaveName + (list.ElementAt(i).Available ? " - " + list.ElementAt(i).SaveDateTime.ToShortDateString():"");
                b.Tag = list.ElementAt(i).index;
                b.Click += button_Click;
                b.Size = new Size(250, 25);
                b.Location = new Point(63, 30 + (i*30));
                b.Enabled = list.ElementAt(i).Available;

                this.Controls.Add(b);                
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            SaveGame.SelectSaveGame((int)((Button)sender).Tag);
            this.Close();
        }
    }
}
