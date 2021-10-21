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
    public partial class FormShowSpeakers : Form
    {
        public FormShowSpeakers()
        {
            InitializeComponent();
            comboBoxDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (User user in db.UserSet)
                {
                    if(user.Login == labelLogin.Text)
                    {
                        if(user.Role == "Участник")
                        {
                            FormParticipant form = new FormParticipant();
                            form.labelLogin.Text = user.Login;
                            this.Hide();
                            form.Show();
                        }
                        if(user.Role == "Тьютор")
                        {
                            FormTutor form = new FormTutor();
                            form.labelLogin.Text = user.Login;
                            this.Hide();
                            form.Show();
                        }
                    }
                }
            }
        }

        private void comboBoxDirection_MouseEnter(object sender, EventArgs e)
        {

        }

        private void comboBoxDirection_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void comboBoxDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (comboBoxDirection.Text != "")
            {
                using (LRContainer db = new LRContainer())
                {
                    foreach (Speaker speaker in db.SpeakerSet)
                    {
                        if(comboBoxDirection.Text == speaker.Direction)
                        {
                            listBox1.Items.Add(speaker.Name + " " + speaker.Surname);
                        }
                    }
                }
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelInfo.Text = "";
            if ((string)listBox1.SelectedItem != "")
            {
                using (LRContainer db = new LRContainer())
                {
                    foreach (Speaker speaker in db.SpeakerSet)
                    {
                        if ((string)listBox1.SelectedItem == (speaker.Name + " " + speaker.Surname))
                        {
                            string birthday = speaker.Birthday.ToShortDateString();
                            labelInfo.Text = "ФИО:" + speaker.Name + " " + speaker.Surname + " " + speaker.Patronymic + "\n" + "Дата рождения: " + birthday + "\n" + "Направление: " + speaker.Direction + "\n";
                            labelInfo.Visible = true;
                        }
                    }
                }
            }
            using (LRContainer db = new LRContainer())
            {
                foreach (User user in db.UserSet)
                {
                    foreach (Speaker speaker in db.SpeakerSet)
                    {
                        if ((string)listBox1.SelectedItem == (speaker.Name + " " + speaker.Surname) && user.Login == speaker.Login)
                        {
                            MemoryStream stream = new MemoryStream(user.Image);
                            Image image1 = Image.FromStream(stream);
                            pictureBox1.Image = Image.FromStream(stream);
                            pictureBox1.Visible = true;
                        }

                    }
                }
            }
        }
    }
}