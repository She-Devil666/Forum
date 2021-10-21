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
    public partial class FormTutor : Form
    {
        private byte[] imageBytes;

        public FormTutor()
        {
            InitializeComponent();
        }

        private void FormTutor_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new FormMainScreen().Show();
            this.Hide();
        }

        private void FormTutor_MouseEnter(object sender, EventArgs e)
        {
        }

        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {            
            using (LRContainer db = new LRContainer())
            {
                foreach (Tutor tutor in db.TutorSet)
                {
                    if (tutor.Login == labelLogin.Text)
                    {
                        labelDirection.Text = tutor.Direction;
                        labelSurname_Name.Text = tutor.Name + " " + tutor.Surname;
                        labelBirthday.Text = tutor.Birthday.ToShortDateString();
                        labelChildrens.Text = tutor.Childrens.ToString();
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
            FormTutorEdit edit = new FormTutorEdit();
            edit.textBoxLogin.Text = labelLogin.Text;
            using (LRContainer db = new LRContainer())
            {
                foreach (Tutor tutor in db.TutorSet)
                {
                    if (tutor.Login == edit.textBoxLogin.Text)
                    {
                        foreach (User user in db.UserSet)
                        {
                            if (user.Login == edit.textBoxLogin.Text)
                                edit.textBoxEmail.Text = user.Email;
                        }
                        edit.textBoxName.Text = tutor.Name;
                        edit.textBoxPatronymic.Text = tutor.Patronymic;
                        edit.textBoxSurname.Text = tutor.Surname;
                        edit.maskedTextBoxPhone.Text = tutor.Phone;
                        edit.dateTimePickerBirthday.Value = tutor.Birthday;
                        edit.textBoxCity.Text = tutor.City;
                        edit.comboBoxdDirection.Text = tutor.Direction;
                        edit.checkBoxDocs.Checked = tutor.Documents;
                    }
                }
            }
            this.Hide();
            edit.Show();
        }

        private void buttonSpeakers_Click(object sender, EventArgs e)
        {
            FormShowSpeakers speakers = new FormShowSpeakers();
            speakers.labelLogin.Text = labelLogin.Text;
            speakers.Show();
            this.Hide();
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
