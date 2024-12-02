using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3.Model
{
    public partial class frmCategoryAdd : SampleAdd
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }

        public int id = 0;

        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qr = "";
            if(id == 0)
            {
                qr = "sp_AddsCategory"; // inseart category
            }
            else
            {
                qr = "sp_UpdateCategory"; // Update category
            }

            Hashtable ht = new Hashtable();
            ht.Add("@catID", id);
            ht.Add("@catName",txtName.Text);
            //ht.Add("@catType", txtType.Text);
            if(MainClass.SQL(qr,ht)>0)
            {
                MessageBox.Show("Save Successfully", "Restaurant Management System",MessageBoxButtons.OK,MessageBoxIcon.Information);
                id = 0;txtName.Clear();
                txtName.Focus();
            }
        }
    }
}
