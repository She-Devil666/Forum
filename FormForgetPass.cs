using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR_5
{
    public partial class FormForgetPass : Form
    {
        public FormPassRecovery passRecovery = new FormPassRecovery();
        public Random Random = new Random();
        public FormForgetPass()
        {
            InitializeComponent();
        }

        private void buttonSendPass_Click(object sender, EventArgs e)
        {
            try
            {
                using (LRContainer db = new LRContainer())
                {
                    foreach (User user in db.UserSet)
                    {
                        if (textBoxEmail.Text == user.Email)
                        {
                            int cod = Random.Next(1000, 9999);
                            MailAddress from = new MailAddress("forum_2022@mail.ru", "Forum");
                            MailAddress to = new MailAddress(textBoxEmail.Text);
                            MailMessage m = new MailMessage(from, to);
                            m.Subject = "Форум";
                            m.Attachments.Add(new Attachment("C:/Users/ASUS/source/repos/Fotolia_143605968_Subscription_Monthly_XXL-1 (1).jpg"));
                            m.Body = "<h1>Привет! </h1>\n<h3>Забыли пароль? Введите данный код для изменения пароля.</h3>";
                            m.Body += "<h3>Код: " + cod + "</h3>";
                            m.Body += "Если вы этого не делали, то игнорируйте письмо.";
                            passRecovery.label3.Text = cod.ToString();
                            user.Password = cod.ToString();
                            m.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                            smtp.Credentials = new NetworkCredential("forum_2022@mail.ru", "LR_OAIP_Forum");
                            smtp.EnableSsl = true;
                            smtp.Send(m);
                            passRecovery.Show();
                            this.Hide();
                            
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Проверьте подключение к интернету.");
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new FormMainScreen().Show();
            this.Hide();
        }
    }
}
