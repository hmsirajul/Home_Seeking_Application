using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.Model;
using WindowsFormsApp3.View;

namespace WindowsFormsApp3
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        //creating funtion for blur background whene it call;
        //static frmMain _obj;
        //public static frmMain Instance
        //{
        //    get
        //    {
        //        if(_obj == null)
        //        {
        //            _obj = new frmMain();
        //        }
        //        return _obj;
        //    }
            
        //}

        // create a method to add controls in main form inside panel3

        public void AddControls(Form f)
        {
            panel3.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           AboutBox1 abt = new AboutBox1();
            abt.Show();
            //lblUsername.Text = MainClass.USER;
            //_obj = this;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            //AddControls(new frmHome());
            AddControls(new frmCloud());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            AddControls(new frmCategory());
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            AddControls(new frmTableView());
        }

        private void btnLandlord_Click(object sender, EventArgs e)
        {
            AddControls(new frmLandlordView());
        }

        private void btnPropertylist_Click(object sender, EventArgs e)
        {
            AddControls(new frmPropartyView());
        }
    }
}
