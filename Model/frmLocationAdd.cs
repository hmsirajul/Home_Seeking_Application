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
    public partial class frmLocationAdd : SampleAdd
    {
        public frmLocationAdd()
        {
            InitializeComponent();
        }
        public int id = 0;

        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qr = "";
            if (id == 0)
            {
                qr = "sp_AddLocation"; // inseart category
            }
            else
            {
                qr = "sp_UpdateLocation"; // Update category
            }

            Hashtable ht = new Hashtable();
            ht.Add("@tid", id);
            ht.Add("@tname", txtName.Text);
            //ht.Add("@catType", txtType.Text);
            if (MainClass.SQL(qr, ht) > 0)
            {
                MessageBox.Show("Save Successfully", "Home Seeking Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id = 0; txtName.Clear();
                txtName.Focus();
            }
        }
    }
}
