using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR_5
{
    public partial class FormRegister : Form
    {
        private byte[] imageBytes;

        public FormRegister()
        {
            InitializeComponent();
            comboBoxDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new FormMainScreen().Show();
            this.Hide();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (textBoxCity.Text != ""
                && textBoxEmail.Text != ""
                && textBoxLogin.Text != ""
                && textBoxName.Text != ""
                && textBoxPass.Text != ""
                && textBoxSurname.Text != ""
                && comboBoxDirection.Text != ""
                && maskedTextBoxPhone.Text != "")
            {
                using (LRContainer db = new LRContainer())
                {
                    if (imageBytes == null)
                    {
                        MessageBox.Show("Выберите картинку");
                    }
                    else
                    {
                        User user = new User { Login = textBoxLogin.Text, Email = textBoxEmail.Text, Password = this.GetHashString(textBoxPass.Text), Role = "Участник", Image = imageBytes };
                        db.UserSet.Add(user);
                        db.SaveChanges();
                        if (user.Role == "Участник")
                        {
                            Participant participant = new Participant { Login = textBoxLogin.Text, Surname = textBoxSurname.Text, Name = textBoxName.Text, Patronymic = textBoxPatronymic.Text, Phone = maskedTextBoxPhone.Text, Birtday = (DateTime)dateTimePickerBirthday.Value, Mail = textBoxEmail.Text, City = textBoxCity.Text, Direction = comboBoxDirection.Text, Transfer = checkBoxTransfer.Checked };
                            db.ParticipantSet.Add(participant);
                            db.SaveChanges();
                            FormParticipant formParticipant = new FormParticipant();
                            formParticipant.labelLogin.Text = participant.Login;
                            formParticipant.Show();
                            this.Hide();
                            return;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Не все поля были заполнены.");
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

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {
            using (LRContainer db=new LRContainer())
            {
                foreach(User user in db.UserSet)
                {
                    if (textBoxLogin.Text == user.Login)
                    {
                        MessageBox.Show("Пользователь с таким логином уже зарегистрирован.");
                        textBoxLogin.Clear();
                    }
                }
            }
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (User user in db.UserSet)
                {
                    if (textBoxEmail.Text == user.Email)
                    {
                        MessageBox.Show("Пользователь с такой почтой уже зарегистрирован.");
                        textBoxEmail.Clear();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для корректного отображения необходимо добавлять картинки формата 4х3");
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP; *.JPG; *.GIF; *.PNG)| *.BMP; *.JPG; *.GIF; *.PNG | All files(*.*) | *.* "; 
                if (open_dialog.ShowDialog() == DialogResult.OK)
                {
                try
                    {
                    this.imageBytes = File.ReadAllBytes(open_dialog.FileName);
                    MessageBox.Show("Успешно");
                    }
                catch
                    {
                    MessageBox.Show("Ошибка");
                    }
                }
            }
    }
}
