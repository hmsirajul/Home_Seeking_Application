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
using WindowsFormsApp3.Model;

namespace WindowsFormsApp3.View
{
    public partial class frmLandlordView : SampleView
    {
        public frmLandlordView()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            string qr = "select landlordid, lname, lphone, llocation, lrole from tblLandlord where lname like '%" + txtSearch.Text + "%' ";
            //string qr = "sp_LoadCategory";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPhone);
            lb.Items.Add(dgvLocation);
            lb.Items.Add(dgvRole);
            //lb.Items.Add(dgvType);
            MainClass.LoadData(qr, dataGridView1, lb);
        }

        private void frmLandlordView_Load(object sender, EventArgs e)
        {
            GetData();
        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            frmLandlordAdd frm =new frmLandlordAdd();
            frm.ShowDialog();
            //MainClass.BlurBackground(new frmLandlordAdd());
            GetData();
        }

        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmLandlordAdd frm = new frmLandlordAdd();
                frm.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvName"].Value);
                frm.textMobile.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvPhone"].Value);
                frm.textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvLocation"].Value);
                frm.cmbRole.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvRole"].Value);
                frm.ShowDialog();
                GetData();

            }
            if (dataGridView1.CurrentCell.OwningColumn.Name == "dgvDle")
            {
                if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string qr = "sp_deleteslandlord";
                    Hashtable ht = new Hashtable();
                    ht.Add("@landlordid", id);
                    if (MainClass.SQL(qr, ht) > 0)
                    {
                        MessageBox.Show("Deleted Successfully", "Home Seeking Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //After Delete refresh all
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("Deletion Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }


                }

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
