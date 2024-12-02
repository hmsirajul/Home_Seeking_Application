using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.Model;

namespace WindowsFormsApp3.View
{
    //inhariteing sampleview with frmcatego
    public partial class frmCategory : SampleView
    {
        public frmCategory()
        {
            InitializeComponent();
        }
        // public void GetData()
        // {
        //    string qr = "sp_LoadCategory";
        //    ListBox lb= new ListBox();
        //  lb.Items.Add(dgvid);
        //  lb.Items.Add(dgvName);
        //lb.Items.Add(dgvType);
        //  MainClass.LoadData(qr, dataGridView1, lb);
        //}

        public void GetData()
        {
           string qr = "select catID,catName from tblcategory where catName like '%"+txtSearch.Text+"%' ";
            //string qr = "sp_LoadCategory";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            //lb.Items.Add(dgvType);
            MainClass.LoadData(qr, dataGridView1, lb);
        }
        private void frmCategory_Load(object sender, EventArgs e)
        {
            GetData();

        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            frmCategoryAdd frm=new frmCategoryAdd();
            frm.ShowDialog();
           // MainClass.BlurBackground(new frmCategoryAdd());
            GetData();
        }

        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmCategoryAdd frm = new frmCategoryAdd();
                frm.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["dgvName"].Value);
                frm.ShowDialog();
                GetData();

            }
            if(dataGridView1.CurrentCell.OwningColumn.Name == "dgvDle")
            {
                if(DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string qr = "sp_DeleteCategory";
                    Hashtable ht = new Hashtable();
                    ht.Add("@catID", id);
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
