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
    public partial class FormAdmin : Form
    {
        private byte[] imageBytes;
        public FormAdmin()
        {
            InitializeComponent();
            comboBoxDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxPlace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxdDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxR_site.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new FormMainScreen().Show();
            this.Hide();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if(comboBoxRole.Text != ""
                && textBoxLogin.Text != ""
                && textBoxName.Text != ""
                && textBoxEmail.Text != ""
                && textBoxPass.Text != ""
                && textBoxSurname.Text != ""
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
                        User user = new User { Login = textBoxLogin.Text, Email = textBoxEmail.Text, Password = this.GetHashString(textBoxPass.Text), Role = comboBoxRole.Text, Image = imageBytes};
                        db.UserSet.Add(user);
                        db.SaveChanges();
                        if (user.Role == "Персонал")
                        {
                            if (comboBoxPosition.Text != ""
                                && comboBoxPlace.Text != "")
                            {
                                Staff staff = new Staff
                                {
                                    Login = textBoxLogin.Text,
                                    Surname = textBoxSurname.Text,
                                    Name = textBoxName.Text,
                                    Patronymic = textBoxPatronymic.Text,
                                    Position = comboBoxPosition.Text,
                                    Phone = maskedTextBoxPhone.Text,
                                    Birthday = (DateTime)dateTimePickerBirthday.Value,
                                    Documents = checkBoxDocuments.Checked,
                                    R_site = comboBoxR_site.Text,
                                    Place = comboBoxPlace.Text
                                };
                                db.StaffSet.Add(staff);
                                db.SaveChanges();
                                MessageBox.Show("Пользователь с уровнем доступа персонал зарегистрирован.");
                                textBoxLogin.Clear();
                                textBoxSurname.Clear();
                                textBoxName.Clear();
                                textBoxPatronymic.Clear();
                                maskedTextBoxPhone.Clear();
                                checkBoxDocuments.Checked = false;
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Заполните все поля.");
                                db.UserSet.Remove(user);
                                db.SaveChanges();
                            }
                        }
                        if (user.Role == "Спикер")
                        {
                            if (Int32.TryParse(textBoxSalary.Text, out int salary)
                                && comboBoxDirection.Text != "")
                            {
                                Speaker speaker = new Speaker
                                {
                                    Login = textBoxLogin.Text,
                                    Surname = textBoxSurname.Text,
                                    Name = textBoxName.Text,
                                    Patronymic = textBoxPatronymic.Text,
                                    Phone = maskedTextBoxPhone.Text,
                                    Birthday = (DateTime)dateTimePickerBirthday.Value,
                                    Mail = textBoxEmail.Text,
                                    Transfer = checkBoxTransfer.Checked,
                                    Salary = Int32.Parse(textBoxSalary.Text),
                                    Direction = comboBoxDirection.Text
                                };
                                db.SpeakerSet.Add(speaker);
                                db.SaveChanges();
                                MessageBox.Show("Пользователь с уровнем доступа спикер зарегистрирован.");
                                textBoxLogin.Clear();
                                textBoxSurname.Clear();
                                textBoxName.Clear();
                                textBoxPatronymic.Clear();
                                maskedTextBoxPhone.Clear();
                                textBoxEmail.Clear();
                                checkBoxTransfer.Checked = false;
                                textBoxSalary.Clear();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Некорректный ввод.");
                                db.UserSet.Remove(user);
                                db.SaveChanges();
                            }
                        }
                        if (user.Role == "Тьютор")
                        {
                            if (textBoxCity.Text != ""
                                && comboBoxdDirection.Text != "")
                            {
                                Tutor tutor = new Tutor
                                {
                                    Login = textBoxLogin.Text,
                                    Surname = textBoxSurname.Text,
                                    Name = textBoxName.Text,
                                    Patronymic = textBoxPatronymic.Text,
                                    Phone = maskedTextBoxPhone.Text,
                                    Birthday = (DateTime)dateTimePickerBirthday.Value,
                                    Documents = checkBoxDocs.Checked,
                                    City = textBoxCity.Text,
                                    Direction = comboBoxdDirection.Text,
                                    Childrens = checkedListBox1.CheckedItems.Count
                                };
                                db.TutorSet.Add(tutor);
                                db.SaveChanges();
                                MessageBox.Show("Пользователь с уровнем доступа тьютор зарегистрирован.");
                                textBoxLogin.Clear();
                                textBoxSurname.Clear();
                                textBoxName.Clear();
                                textBoxPatronymic.Clear();
                                maskedTextBoxPhone.Clear();
                                checkBoxDocs.Checked = false;
                                textBoxCity.Clear();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Некорректный ввод.");
                                db.UserSet.Remove(user);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните поля.");
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

        private void comboBoxRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRole.Text == "Персонал")
            {
                groupBoxStaff.Visible = true;
                groupBoxSpeaker.Visible = false;
                groupBoxTutor.Visible = false;
            }
            else if (comboBoxRole.Text == "Спикер")
            {
                groupBoxSpeaker.Visible = true;
                groupBoxStaff.Visible = false;
                groupBoxTutor.Visible = false;
            }
            else if (comboBoxRole.Text == "Тьютор") 
            {
                groupBoxStaff.Visible = false;
                groupBoxSpeaker.Visible = false;
                groupBoxTutor.Visible = true;
            }
            else
            {
                groupBoxStaff.Visible = false;
                groupBoxSpeaker.Visible = false;
                groupBoxTutor.Visible = false;
            }
        }

        private void comboBoxR_site_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxR_site_MouseEnter(object sender, EventArgs e)
        {
            comboBoxR_site.Items.Clear();
            using (LRContainer db = new LRContainer())
            {
                foreach (Staff staff in db.StaffSet)
                {
                    comboBoxR_site.Items.Add(staff.Surname + " " + staff.Name);
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new FormAdminMain().Show();
            this.Hide();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_MouseEnter(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            using(LRContainer db = new LRContainer())
            {
                foreach(Participant participant in db.ParticipantSet)
                {
                    if(participant.Direction==comboBoxdDirection.Text)
                        checkedListBox1.Items.Add(participant.Surname + " " + participant.Name);
                }
            }
        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (User user in db.UserSet)
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

