using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    class MainClass
    {
        public static readonly string cs = "Data Source=DESKTOP-7F419FE\\SQLEXPRESS;Initial Catalog=RestoDB;Integrated Security=True;";
        public static SqlConnection con = new SqlConnection(cs);

        //Check User Validation

        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;
            // string qr = @"select userid,username,upass,uname,uphone from tblusers where username= '" + user + "' and upass='" + pass + "'";

            // SqlCommand cmd = new SqlCommand(qr, con);
            SqlCommand cmd = new SqlCommand("sp_UserLogin", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", user);
            cmd.Parameters.AddWithValue("@upass", pass);
            
            DataTable dt=new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0 )
            {
                isValid = true;
                USER = dt.Rows[0]["uname"].ToString();
            }


            return isValid;
        }

        //create proparty for username

        public static string user;

        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }

        //create reusable methode for Insert Update and Delete operation

        public static int SQL(string qr, Hashtable ht)
        {
            int res = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(qr, con);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry item in ht)
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if (con.State == ConnectionState.Closed) { con.Open(); }
                res = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }

            }
            catch (Exception ex)
            {
                con.Close(); MessageBox.Show(ex.Message);
            }
            return res;
        }

        // Lode Data from SQL Database

        public static void LoadData(string qr,DataGridView gv,ListBox lb)
        {
            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);
            try
            {
                SqlCommand cmd = new SqlCommand(qr, con);
               cmd.CommandType = CommandType.Text;
                //cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string colName = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[colName].DataPropertyName = dt.Columns[i].ToString();
                }

                //dataSources
                gv.DataSource = dt;
            }
            catch (Exception ex)
            {
                con.Close(); MessageBox.Show(ex.Message);
            }
        }
        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;
            int count = 0;
            foreach (DataGridViewRow row in gv.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }
        //Bluring the background when open the add form ;
        //public static void BlurBackground(Form Model)
        //{
        //    Form Background = new Form();
        //    using (Model)
        //    {
        //        Background.StartPosition = FormStartPosition.Manual;
        //        Background.FormBorderStyle = FormBorderStyle.None;
        //        Background.Opacity = 0.5d;
        //        Background.BackColor = Color.Black;
        //        Background.Size = frmMain.Instance.Size;
        //        Background.Location = frmMain.Instance.Location;
        //        Background.ShowInTaskbar = false;
        //        Background.Show();
        //        Model.Owner = Background;
        //        Model.ShowDialog(Background);
        //        Background.Dispose();                            
        //    }
        //}

        // Catagory informations functions all info store and came frm ther;
        public static void CBFill(string qr,ComboBox cb)
        {
            SqlCommand cmd = new SqlCommand(qr,con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cb. DisplayMember = "name";
            cb.ValueMember = "id";
            cb.DataSource = dt;
            cb.SelectedIndex = -1;
        }
    }

}
