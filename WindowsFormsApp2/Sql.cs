using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    class Sql
    {
        private static DataTable _dt;
        private static SqlDataAdapter _da;
        private static DataSet _ds;

        /// <summary>
        /// Ask the database and fill the list with the results
        /// </summary>
        /// <param name="start">From day in format: "yyyy/MM/dd" </param>
        /// <param name="stop">To day in format: "yyyy/MM/dd"</param>
        /// <param name="numberOfStation">Number of Station to show. Pass "WSZYSTKIE" to display all stations</param>
        /// <param name="listview">ListView to fill</param>
        public static void FillLvFromSql(string start, string stop, string numberOfStation, ListView listview)
        {
            string cmdString;
            listview.Items.Clear();
            DateTime now = DateTime.Now;

            if (numberOfStation.Equals("WSZYSTKIE"))
                cmdString = "SELECT  [sekcja],[stacja],[opis],[min],[czas_start],[czas_stop] FROM [dbo].[awarieGM3] WHERE czas_start BETWEEN " + "'" + start + "'" + " AND " + " '" + stop + "'";
            else
                cmdString = "SELECT  [sekcja],[stacja],[opis],[min],[czas_start],[czas_stop] FROM [dbo].[awarieGM3] WHERE czas_start <" + "'" + stop + "'" + "AND " + "czas_start >=" + "'" + start + "'" + "AND sekcja = " + "'" + numberOfStation + "'";


            string connString = ConfigurationManager.ConnectionStrings["production"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {

                    comm.Connection = conn;
                    comm.CommandText = cmdString;
                    //            comm.Parameters.AddWithValue("@val1", textBox1.Text);
                    //            comm.Parameters.AddWithValue("@val2", textBox2.Text);
                    //            comm.Parameters.AddWithValue("@val3", textBox3.Text);
                    //            comm.Parameters.AddWithValue("@val4", textBox3.Text);
                    //            comm.Parameters.AddWithValue("@val5", textBox3.Text);
                    try
                    {
                        _da = new SqlDataAdapter(comm.CommandText, comm.Connection);
                        _ds = new DataSet();
                        _da.Fill(_ds, "asd");

                        _dt = _ds.Tables["asd"];

                        int i;
                        for (i = 0; i <= _dt.Rows.Count - 1; i++)
                        {
                            listview.Items.Add(_dt.Rows[i].ItemArray[0].ToString());
                            listview.Items[i].SubItems.Add(_dt.Rows[i].ItemArray[1].ToString());
                            listview.Items[i].SubItems.Add(_dt.Rows[i].ItemArray[2].ToString());
                            listview.Items[i].SubItems.Add(_dt.Rows[i].ItemArray[3].ToString());
                            listview.Items[i].SubItems.Add(_dt.Rows[i].ItemArray[4].ToString());
                            listview.Items[i].SubItems.Add(_dt.Rows[i].ItemArray[5].ToString());
                            //listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Exception: {ex.Message}");
                    }

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Exception: {ex.Message}");
                    }
                }
            }

        }



    }
}
