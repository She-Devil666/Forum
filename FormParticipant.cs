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
    public partial class FormParticipant : Form
    {
        private byte[] imageBytes;

        public FormParticipant()
        {
            InitializeComponent();
        }
        private void FormParticipant_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new FormMainScreen().Show();
            this.Hide();
        }

        private void FormParticipant_MouseEnter(object sender, EventArgs e)
        {
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            FormParticipantEdit edit = new FormParticipantEdit();
            edit.textBoxLogin.Text = labelLogin.Text;
            using (LRContainer db = new LRContainer())
            {
                foreach (Participant participant in db.ParticipantSet)
                {
                    if (participant.Login == edit.textBoxLogin.Text)
                    {
                        edit.textBoxCity.Text = participant.City;
                        edit.textBoxEmail.Text = participant.Mail;
                        edit.textBoxName.Text = participant.Name;
                        edit.textBoxPatronymic.Text = participant.Patronymic;
                        edit.textBoxSurname.Text = participant.Surname;
                        edit.maskedTextBoxPhone.Text = participant.Phone;
                        edit.dateTimePickerBirthday.Value = participant.Birtday;
                        edit.checkBoxTransfer.Checked = participant.Transfer;
                        edit.comboBoxDirection.SelectedItem = participant.Direction;
                    }
                }
            }
            edit.Show();
            this.Hide();
        }

        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void buttonSpeakers_Click(object sender, EventArgs e)
        {
            FormShowSpeakers speakers = new FormShowSpeakers();
            speakers.labelLogin.Text = labelLogin.Text;
            this.Hide();
            speakers.Show();
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
            
        }

        private void tableLayoutPanel1_MouseEnter_1(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (Participant participant in db.ParticipantSet)
                {
                    if (participant.Login == labelLogin.Text)
                    {
                        labelCity.Text = participant.City;
                        labelDirection.Text = participant.Direction;
                        labelName_Surname.Text = participant.Name + " " + participant.Surname;
                        labelBirthday.Text = participant.Birtday.ToShortDateString();
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

        private void buttonPhoto_Click(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach(User user in db.UserSet)
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

