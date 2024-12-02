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
    public partial class frmPropartyView : SampleView
    {
        public frmPropartyView()
        {
            InitializeComponent();
        }

        private void frmPropartyView_Load(object sender, EventArgs e)
        {
            GetData(); // becacuse all informations are show when open proparty list;
        }
        public void GetData()  //select p.pID,p.pName,p.pPrice,p.CatagoryID,p.pImage,p.pDetails from tbl_Proparty p inner join tblcategory c on c.catID =p.CatagoryID where p.pName
        {
            //string qr = "select p.pID,p.pName,p.pPrice,p.CatagoryID,p.pImage,p.pDetails from tblProparty p inner join tblcategory c on c.catID =p.CatagoryID where p.pName like '%" + txtSearch.Text + "%' ";
            string qr = "select p.pID,p.pName,p.pPrice,p.CatagoryID,p.pDetails,c.catName from tbl_Propartys p inner join tblcategory c on c.catID =p.CatagoryID where p.pName like '%" + txtSearch.Text + "%' ";
            //string qr = "sp_LoadCategory";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPrice);
            lb.Items.Add(dgvcatID);
            lb.Items.Add(dgvcat);
            lb.Items.Add(dgvdtls);
            //lb.Items.Add(dgvType);
            MainClass.LoadData(qr, dataGridView1, lb);
        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            frmPropartyAdd frm = new frmPropartyAdd();
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
                frmPropartyAdd frm = new frmPropartyAdd();
                frm.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.cID = Convert.ToInt32 (dataGridView1.CurrentRow.Cells["dgvcatID"].Value);
                //frm.txtName.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvName"].Value);
                //frm.txtPrice.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvPrice"].Value);
                // frm.txtDetails.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvdtls"].Value);
                // frm.cmbCat.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvcat"].Value);
                frm.ShowDialog();
                GetData();

            }
            if (dataGridView1.CurrentCell.OwningColumn.Name == "dgvDle")
            {
                if (DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string qr = "sp_delete_Propartys";
                    Hashtable ht = new Hashtable();
                    ht.Add("@pID", id);
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
    }
}
