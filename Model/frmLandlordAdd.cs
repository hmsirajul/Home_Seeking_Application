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
    public partial class frmLandlordAdd : SampleAdd
    {
        public frmLandlordAdd()
        {
            InitializeComponent();
        }
        public int id = 0;

        private void frmLandlordAdd_Load(object sender, EventArgs e)
        {

        }

        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qr = "";
            if (id == 0)
            {
                qr = "sp_addlandlord"; // inseart category
            }
            else
            {
                qr = "sp_updateslandlord"; // Update category
            }

            Hashtable ht = new Hashtable();
            ht.Add("@landlordid", id);
            ht.Add("@lname", txtName.Text);
            ht.Add("@lphone", textMobile.Text);
            ht.Add("@llocation", textBox2.Text);
            ht.Add("@lrole", cmbRole.Text);
            //ht.Add("@catType", txtType.Text);
            if (MainClass.SQL(qr, ht) > 0)
            {
                MessageBox.Show("Save Successfully", "Home Seeking Application ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id = 0; txtName.Clear();
                textMobile.Clear();
                cmbRole.SelectedIndex = -1;
                txtName.Focus();
            }
        }
    }
}
