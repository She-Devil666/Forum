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
    public partial class FormLogIn : Form
    {
        public FormLogIn()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new FormMainScreen().Show();
            this.Hide();
        }

        private void buttonForgetPass_Click(object sender, EventArgs e)
        {
            new FormForgetPass().Show();
            this.Hide();
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach(User user in db.UserSet)
                {
                    if(textBoxLogin.Text==user.Login&&this.GetHashString(textBoxPass.Text)==user.Password)
                    {
                        MessageBox.Show("Вход выполнен успешно!");

                        if (user.Role == "Участник")
                        {
                            FormParticipant formParticipant = new FormParticipant();
                            formParticipant.labelLogin.Text = (string)user.Login;
                            formParticipant.Show();
                            this.Hide();
                        }
                        if (user.Role == "Администратор")
                        {
                            new FormAdminMain().Show();
                            this.Hide();
                        }
                        if (user.Role == "Спикер")
                        {
                            FormSpeaker formSpeaker = new FormSpeaker();
                            formSpeaker.labelLogin.Text = user.Login;
                            formSpeaker.Show();
                            this.Hide();
                        }
                        if (user.Role == "Тьютор")
                        {
                            FormTutor formTutor = new FormTutor();
                            formTutor.labelLogin.Text = user.Login;
                            formTutor.Show();
                            this.Hide();
                        }
                        if (user.Role == "Персонал") 
                        {
                            FormStaff formStaff = new FormStaff();
                            formStaff.labelLogin.Text = user.Login;
                            formStaff.Show();
                            this.Hide();
                        }
                        return;
                    }
                }
                MessageBox.Show("Логин или пароль указан неверно!");
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
