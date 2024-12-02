using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.View;

namespace WindowsFormsApp3.Model
{
    public partial class frmCloud : Form
    {
        public frmCloud()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); /// closeing the page clicking this corner button;
        }

        private void PropartyPanel_Paint(object sender, PaintEventArgs e)
        {
        //    
        //    dataGridView1.BorderStyle = BorderStyle.Fi
        }

        private void frmCloud_Load(object sender, EventArgs e)
        {
            //This for datagread view using for show all details about given ;
            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();
            LoadPropartys();
        }
        // Auto showing the categort that all have all ready;
        private void AddCategory()
        {
            string qr = "Select * from tblcategory";
            SqlCommand cmd = new SqlCommand(qr, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CategoryPanel.Controls.Clear();
            if (dt.Rows.Count > 0)
           {
                /// using for each loop beacuse all buttons are create autoMaticaly;
                foreach (DataRow row in dt.Rows)
                {
                    Button b = new Button();
                    //converting colour ; backcolor ;
                    b.BackColor = Color.FromArgb(50, 58, 89);
                    //converting colour ; fontcolor ;
                    b.ForeColor = Color.Yellow;

                    b.Size = new Size(172, 53);
                    b.Text = row["catName"].ToString();
                    b.Click += new EventHandler(_Click);
                    CategoryPanel.Controls.Add(b);
                }
            }

        }
        /// <summary>
        //  This one for category Button;

        //private void _Click(object sender, EventArgs e)
        //{
        //    Button b = (Button)sender;
        //    foreach (var item in PropartyPanel.Controls)
        //    {
        //        var pro = (UserControlProparty)item;
        //        pro.Visible = pro.PCategory == int.Parse(b.Text.Trim());

        //        //pro.Visible = pro.PCategory.ToLower().Contains(b.Text.Trim().ToLower());
        //    }
        //}
        private void _Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            foreach (var item in PropartyPanel.Controls)
            {
                if (item is UserControlProparty pro) // Ensure type safety
                {
                    // Convert both the button text and pro.PCategory to strings, 
                    // and perform a comparison in a case-insensitive manner for strings
                    string buttonText = Convert.ToString(b.Text).Trim();
                    string proCategory = Convert.ToString(pro.PCategory).Trim();

                    // Check if the proCategory contains the buttonText (case-insensitive comparison)
                    pro.Visible = proCategory.IndexOf(buttonText, StringComparison.OrdinalIgnoreCase) >= 0;
                }
            }
        }





        //private void _Click(object sender, EventArgs e)
        //{
        //    Button b = (Button)sender;
        //    foreach (var item in PropartyPanel.Controls)
        //    {
        //        var pro = (UserControlProparty)item;

        //        // Ensure PCategory is treated as a string and use ToLower()
        //        string category = pro.PCategory.ToString().ToLower();

        //        // Compare category to the button's text
        //        pro.Visible = category.Contains(b.Text.Trim().ToLower());
        //    }
        //}


        private void AddItems(string id, string name, int cat, int price, Image Pimage)
        {
            var w = new UserControlProparty()
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = Pimage,
                id = Convert.ToInt32(id)
            };
            PropartyPanel.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (UserControlProparty)ss;
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (Convert.ToInt32(item.Cells["dgvid"].Value) == wdg.id)
                    {
                        item.Cells["dgcDtl"].Value = int.Parse(item.Cells["dgvDtl"].ToString()) + 1;
                        item.Cells["dgcPrice"].Value = int.Parse(item.Cells["dgvDtl"].ToString());

                    }
                    //dataGridView1.Rows.Add(new object[] { 0, wdg.id, wdg.PName, wdg.PPrice, wdg.PCategory });

                }
                dataGridView1.Rows.Add(new object[] { 0, wdg.id, wdg.PName, wdg.PPrice, wdg.PCategory });
            };
        }
        //Geting propartys from database;
        //private void LoadPropartys()
        //{
        //    string qr = "";
        //    //string qr = "Select * from tblcategory";
        //    SqlCommand cmd = new SqlCommand(qr, MainClass.con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        Byte[] imagaearray = (byte[])item["pImage"];
        //        byte[] imagebytearray = imagaearray;
        //        //AddItems(item["pID"].ToString(), item["pName"].ToString(), item["catName"].ToString(),Image.FromStream(new MemoryStream(imagaearray)));
        //        AddItems(item["pID"].ToString(), item["pName"].ToString(), item["catName"].ToString(), item["pPrice"].ToString(), Image.FromStream(new MemoryStream(imagaearray)));
        //    }

        //}
        //private void AddItems(string id, string name, int cat, int price)
        //{
        //    var w = new UserControlProparty()
        //    {
        //        PName = name,
        //        PPrice = price,
        //        PCategory = cat,
        //        id = Convert.ToInt32(id)
        //    };
        //    PropartyPanel.Controls.Add(w);

        //    w.onSelect += (ss, ee) =>
        //    {
        //        var wdg = (UserControlProparty)ss;
        //        foreach (DataGridViewRow item in dataGridView1.Rows)
        //        {
        //            if (Convert.ToInt32(item.Cells["dgvid"].Value) == wdg.id)
        //            {
        //                item.Cells["dgcDtl"].Value = int.Parse(item.Cells["dgvDtl"].Value.ToString()) + 1;
        //                item.Cells["dgcPrice"].Value = int.Parse(item.Cells["dgvDtl"].Value.ToString());
        //                return;
        //            }
        //        }
        //        dataGridView1.Rows.Add(new object[] { 0, wdg.id, wdg.PName, wdg.PPrice, wdg.PCategory });
        //    };
        //}

        //// Getting properties from the database
        //private void LoadPropartys()
        //{
        //    string qr = "SELECT * FROM tbl_Propartys INNER JOIN tblcategory ON catID = CatagoryID"; // Replace with the correct SQL query
        //    SqlCommand cmd = new SqlCommand(qr, MainClass.con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    foreach (DataRow item in dt.Rows)
        //    {
        //        // Skip image part and only process the remaining data
        //        AddItems(
        //            item["pID"].ToString(),
        //            item["pName"].ToString(),
        //            Convert.ToInt32(item["catName"]),
        //            Convert.ToInt32(item["pPrice"])
        //        );
        //    }
        //}
        private void AddItems(string id, string name, int cat, int price)
        {
            var w = new UserControlProparty()
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                id = Convert.ToInt32(id)
            };
            PropartyPanel.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (UserControlProparty)ss;
                bool itemFound = false;

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (Convert.ToInt32(item.Cells["dgvid"].Value) == wdg.id)
                    {
                        item.Cells["dgcDtl"].Value = int.Parse(item.Cells["dgvDtl"].Value.ToString()) + 1;
                        item.Cells["dgcPrice"].Value = int.Parse(item.Cells["dgvDtl"].Value.ToString());
                        itemFound = true;
                        break;
                    }
                }

                if (!itemFound)
                {
                    dataGridView1.Rows.Add(new object[] { 0, wdg.id, wdg.PName, wdg.PPrice, wdg.PCategory });
                }
            };
        }

        private void LoadPropartys()
        {
            try
            {
                // SQL query to fetch properties
                string qr = "SELECT * FROM tbl_Propartys INNER JOIN tblcategory ON catID = CatagoryID";
                SqlCommand cmd = new SqlCommand(qr, MainClass.con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    // Extract and validate data from the DataTable
                    string id = item["pID"].ToString();
                    string name = item["pName"].ToString();

                    int cat;
                    if (!int.TryParse(item["catName"].ToString(), out cat))
                    {
                        cat = 0; // Default value for category if parsing fails
                    }

                    int price;
                    if (!int.TryParse(item["pPrice"].ToString(), out price))
                    {
                        price = 0; // Default value for price if parsing fails
                    }

                    // Add the property to the UI, skipping the image part
                    AddItems(id, name, cat, price);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show($"An error occurred while loading properties: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void userControlProparty1_Load(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach(var item in PropartyPanel.Controls)
            {
                var pro = (UserControlProparty)item;
                pro.Visible = pro.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //AddControls(new frmLandlordView());
            // frmLandlordView view = new frmLandlordView();
            // PropartyPanel.Controls.Clear(); // Clears any existing controls, optional
            //PropartyPanel.Controls.Add(view);
            // Show the form as a modal dialog or a separate window
            frmLandlordView view = new frmLandlordView();
            view.ShowDialog(); // This will open the form as a modal dialog
                               // view.Show();     // Use this if you want it as a separate non-blocking window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmTableView view = new frmTableView();
            view.ShowDialog();
            //AddControls(new frmTableView())
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

