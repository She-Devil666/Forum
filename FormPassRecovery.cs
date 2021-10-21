using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR_5
{
    public partial class FormPassRecovery : Form
    {
        public FormParticipant formParticipant = new FormParticipant();
        public FormPassRecovery()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new FormForgetPass().Show();
            this.Hide();
        }

        private void buttonNewPass_Click(object sender, EventArgs e)
        {
            User SelectedUser = null;
            using(LRContainer db = new LRContainer())
            {
                foreach(User user in db.UserSet)
                {
                    if (user.Password == label3.Text)
                    {
                        SelectedUser = user;
                    }
                }
                SelectedUser.Password = this.GetHashString(textBoxNewPass.Text);
                db.SaveChanges();
                MessageBox.Show("Пароль успешно изменен!");
                this.Hide();
                if (SelectedUser.Role == "Участник")
                {
                    FormParticipant formParticipant = new FormParticipant();
                    formParticipant.labelLogin.Text = (string)SelectedUser.Login;
                    formParticipant.Show();
                    this.Hide();
                }
                if (SelectedUser.Role == "Спикер")
                {
                    FormSpeaker formSpeaker = new FormSpeaker();
                    formSpeaker.labelLogin.Text = SelectedUser.Login;
                    formSpeaker.Show();
                    this.Hide();
                }
                if (SelectedUser.Role == "Тьютор")
                {
                    FormTutor formTutor = new FormTutor();
                    formTutor.labelLogin.Text = SelectedUser.Login;
                    formTutor.Show();
                    this.Hide();
                }
                if (SelectedUser.Role == "Персонал")
                {
                    FormStaff formStaff = new FormStaff();
                    formStaff.labelLogin.Text = SelectedUser.Login;
                    formStaff.Show();
                    this.Hide();
                }

            }
        }
        private string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = "";
            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }
            return hash;
        }
    }
}
