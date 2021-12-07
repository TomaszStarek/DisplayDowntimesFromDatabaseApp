using System;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        FormWindowState LastWindowState = FormWindowState.Minimized;

        public Form1()
        {
            InitializeComponent();
            listView1.GridLines = true;

            comboBox1.Text = "WSZYSTKIE";
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);
            dateTimePicker1.Value = yesterday;

            LvCtl.registerLV(listView1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime endDate = dateTimePicker2.Value.Date.AddDays(1);
            Sql.FillLvFromSql(dateTimePicker1.Value.Date.ToString("yyyy/MM/dd"), endDate.ToString("yyyy/MM/dd"), comboBox1.Text, listView1);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime endDate = dateTimePicker2.Value.Date.AddDays(1);
            if (dateTimePicker2.Value > dateTimePicker1.Value)
                Sql.FillLvFromSql(dateTimePicker1.Value.Date.ToString("yyyy/MM/dd"), endDate.ToString("yyyy/MM/dd"), comboBox1.Text, listView1);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime endDate = dateTimePicker2.Value.Date.AddDays(1);
            if (dateTimePicker2.Value > dateTimePicker1.Value)
                Sql.FillLvFromSql(dateTimePicker1.Value.Date.ToString("yyyy/MM/dd"), endDate.ToString("yyyy/MM/dd"), comboBox1.Text, listView1);
        }


        private void Form1_Resize(object sender, EventArgs e)
        {

            if (WindowState != LastWindowState)
            {
                LastWindowState = WindowState;


                if (WindowState == FormWindowState.Maximized)
                {
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                }
                if (WindowState == FormWindowState.Normal)
                {
                    listView1.Columns[2].Width = 420;
                }
            }
        }    


    }
}
