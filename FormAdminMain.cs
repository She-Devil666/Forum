using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR_5
{
    public partial class FormAdminMain : Form
    {
        public FormAdminMain()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new FormMainScreen().Show();
            this.Hide();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            new FormAdmin().Show();
            this.Hide();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            new FormAdminDelete().Show();
            this.Hide();
        }

        private void buttonShedule_Click(object sender, EventArgs e)
        {
            new FormAdminShedule().Show();
            this.Hide();
        }
    }
}
