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
    public partial class FormPlayer : Form
    {
        public FormPlayer(DataGridViewRow d)
        {
            InitializeComponent();
            SetValue(d, label1, textBox1, "Big");
            SetValue(d, label2, textBox2, "Chr");
            SetValue(d, label3, textBox3, "Mor");
            SetValue(d, label4, textBox4, "Con");
            SetValue(d, label5, textBox5, "Dir");
            SetValue(d, label6, textBox6, "Adap");
            SetValue(d, label7, textBox7, "Agg");
            SetValue(d, label8, textBox8, "Cre");
            SetValue(d, label9, textBox9, "Det");
            SetValue(d, label10, textBox10, "Dri");
            SetValue(d, label11, textBox11, "Fla");
            SetValue(d, label12, textBox12, "Hea");
            SetValue(d, label13, textBox13, "Inf");
            SetValue(d, label14, textBox14, "Inj");
            SetValue(d, label15, textBox15, "Off");
            SetValue(d, label16, textBox16, "Pac");
            SetValue(d, label17, textBox17, "Pas");
            SetValue(d, label18, textBox18, "Posi");
            SetValue(d, label19, textBox19, "Set");
            SetValue(d, label20, textBox20, "Sho");
            SetValue(d, label21, textBox21, "Sta");
            SetValue(d, label22, textBox22, "Str");
            SetValue(d, label23, textBox23, "Tac");
            SetValue(d, label24, textBox24, "Tec");
            labelName.Text = d.Cells["Name"].Value.ToString();
            //labelWage.Text = "$ " + ((int)d.Cells["Price"].Value).ToString("N0");
            labelClub.Text = d.Cells["Club"].Value.ToString();
            SetValue(d, label25, textBox25, "GK");
            SetValue(d, label26, textBox26, "SW");
            SetValue(d, label27, textBox27, "D");
            SetValue(d, label28, textBox28, "DM");
            SetValue(d, label29, textBox29, "M");
            SetValue(d, label30, textBox30, "AM");
            SetValue(d, label31, textBox31, "S");
            SetValue(d, label32, textBox32, "L");
            SetValue(d, label33, textBox33, "C");
            SetValue(d, label34, textBox34, "R");

            SetValue(d, label35, textBox35, "Rating");
            SetValue(d, label36, textBox36, "Price");
            SetValue(d, label37, textBox37, "Wage");
            SetValue(d, label38, textBox38, "Apps");
            SetValue(d, label39, textBox39, "Goal");
            SetValue(d, label40, textBox40, "Asst");
            label41.Text= "Goal/App";
            double ratio = ((double)(int)d.Cells["Goal"].Value) / (int)d.Cells["Apps"].Value;
            textBox41.Text = ratio.ToString("N2");
        }

        public void SetValue(DataGridViewRow d, Label lbl, TextBox tb, string s)
        {
            lbl.Text = s.ToUpper();
            tb.Text = d.Cells[s].Value.ToString();
            switch (d.Cells[s].ValueType.ToString())
            {
                case "System.Byte":
                    tb.TextAlign = HorizontalAlignment.Center;
                    break;
                case "System.Int16":
                case "System.UInt16":
                case "System.Int32":
                case "System.UInt32":
                    tb.TextAlign = HorizontalAlignment.Right;
                    tb.Text = ((int)d.Cells[s].Value).ToString("N0");
                    break;
                case "System.Double":
                case "System.Decimal":
                    tb.TextAlign = HorizontalAlignment.Right;
                    tb.Text = ((double)d.Cells[s].Value).ToString("N2");
                    break;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}
