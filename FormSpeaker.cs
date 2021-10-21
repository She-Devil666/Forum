using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR_5
{
    public partial class FormSpeaker : Form
    {
        private byte[] imageBytes;

        public FormSpeaker()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new FormMainScreen().Show();
            this.Hide();
        }

        private void FormSpeaker_MouseEnter(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (Speaker speaker in db.SpeakerSet)
                {
                    if (speaker.Login == labelLogin.Text)
                    {
                        labelDirection.Text = speaker.Direction;
                        labelSurname_Name.Text = speaker.Name + " " + speaker.Surname;
                        labelBirthday.Text = speaker.Birthday.ToShortDateString();
                        labelSalary.Text = speaker.Salary.ToString();
                    }
                }
            }
        }

        private void tableLayoutPanel2_MouseEnter(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (Speaker speaker in db.SpeakerSet)
                {
                    if (speaker.Login == labelLogin.Text)
                    {
                        labelDirection.Text = speaker.Direction;
                        labelSurname_Name.Text = speaker.Name + " " + speaker.Surname;
                        labelBirthday.Text = speaker.Birthday.ToShortDateString();
                        labelSalary.Text = speaker.Salary.ToString();
                    }
                }
            }
            using (LRContainer db = new LRContainer())
            {
                foreach (User user in db.UserSet)
                {
                    if (user.Login == labelLogin.Text)
                    {
                        MemoryStream stream = new MemoryStream(user.Image);
                        Image image1 = Image.FromStream(stream);
                        pictureBox2.Image = Image.FromStream(stream);
                    }
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            FormSpeakerEdit edit = new FormSpeakerEdit();
            edit.textBoxLogin.Text = labelLogin.Text;
            using (LRContainer db = new LRContainer())
            {
                foreach (Speaker speaker in db.SpeakerSet)
                {
                    if (speaker.Login == edit.textBoxLogin.Text)
                    {
                        edit.textBoxEmail.Text = speaker.Mail;
                        edit.textBoxName.Text = speaker.Name;
                        edit.textBoxPatronymic.Text = speaker.Patronymic;
                        edit.textBoxSurname.Text = speaker.Surname;
                        edit.maskedTextBoxPhone.Text = speaker.Phone;
                        edit.dateTimePickerBirthday.Value = speaker.Birthday;
                        edit.checkBoxTransfer.Checked = speaker.Transfer;
                        edit.comboBoxDirection.SelectedItem = speaker.Direction;
                        edit.textBoxSalary.Text = speaker.Salary.ToString();
                    }
                }
            }
            this.Hide();
            edit.Show();
        }

        private void buttonSchedule_Click(object sender, EventArgs e)
        {
            FormShowSchedule schedule = new FormShowSchedule();
            this.Hide();
            schedule.Show();
            schedule.labelLogin.Text = labelLogin.Text;
            schedule.labelDirection.Text = labelDirection.Text;
        }

        private void buttonImage_Click(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (User user in db.UserSet)
                {
                    if (user.Login == labelLogin.Text)
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
                                user.Image = this.imageBytes;
                            }
                            catch
                            {
                                MessageBox.Show("Ошибка");
                            }
                        }

                    }

                }
                db.SaveChanges();
            }
        }
    }
}
