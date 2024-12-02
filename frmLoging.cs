using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class frmLoging : Form
    {
        public frmLoging()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainClass.IsValidUser(txtUserName.Text.Trim(), txtPass.Text.Trim()) == false)
                {
                    MessageBox.Show("Invalid Username or Password");
                    return;
                }

                else
                {
                    //MessageBox.Show("Login Successfully");
                    this.Hide();
                    frmMain frm = new frmMain();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    frmSignIn signIn = new frmSignIn();
        //    signIn.Show();
        //    this.Hide();
        //}
    }
}
