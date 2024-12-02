using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp3.Model
{
    public partial class frmPropartyAdd : SampleAdd
    {
        public frmPropartyAdd()
        {
            InitializeComponent();
        }
        public int id = 0;
        public int cID = 0;
        private void frmPropartyAdd_Load(object sender, EventArgs e)
        {
            string qr = "select catID as id ,catName as name from tblcategory";
            MainClass.CBFill(qr, cmbCat);
            if(cID>0)
            {
                cmbCat.SelectedValue = cID;
            }
            if(id>0)
            {
                ForUpdateLoadDate();
            }
        }
        string filePath;
        Byte[] imageByteArray;
        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg,.png)|* .png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                filePath = ofd.FileName;
                txtImage.Image = new Bitmap(filePath);

            }

        }

        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qr = "";
            //if (id == 0)
            //{
            //    qr = "sp_addProparty"; // inseart category
            //}
            //else
            //{
            //    qr = "sp_updateProparty"; // Update category
            //}

            if (id == 0)
            {
                qr = "sp_add_Propartys"; // inseart category
            }
            else
            {
                qr = "sp_update_Propartys"; // Update category
            }
            // for image uplode;
            Image temp = new Bitmap(txtImage.Image); // faceing problem in image types;
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            imageByteArray = ms.ToArray();


            Hashtable ht = new Hashtable();
            ht.Add("@pID", id);
            ht.Add("@pName", txtName.Text);
            ht.Add("@pPrice", txtPrice.Text);
            ht.Add("@CatagoryID", Convert.ToInt32(cmbCat.SelectedValue));
            ht.Add("@pImage", txtDetails.Text);
            ht.Add("@pDetails", txtDetails.Text);
            //ht.Add("@catType", txtType.Text);
            if (MainClass.SQL(qr, ht) > 0)
            {
                MessageBox.Show("Save Successfully", "Home Seeking Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id = 0;
                cID = 0;
                txtName.Clear();
                txtPrice.Clear();
                cmbCat.SelectedIndex = -1;
                txtName.Focus();
            }
        }
        private void ForUpdateLoadDate()
        {
            SqlCommand cmd = new SqlCommand("Select * from tblProducts where pID=" + id + "", MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count >0)
            {
                txtName.Text = dt.Rows[0]["pName"].ToString();
                txtPrice.Text = dt.Rows[0]["pPrice"].ToString();
                Byte[] imageArray = (byte[])(dt.Rows[0]["pImage"]);
                byte[] imageByteArray = imageArray;
                txtImage.Image = Image.FromStream(new MemoryStream(imageArray));
            }
        }
    }
}


