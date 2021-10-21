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
    public partial class FormAdminDelete : Form
    {
        public FormAdminDelete()
        {
            InitializeComponent();
            comboBoxRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxLogin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new FormMainScreen().Show();
            this.Hide();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new FormAdminMain().Show();
            this.Hide();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (comboBoxLogin.Text != "" && comboBoxRole.Text != "")
            {
                if (comboBoxLogin.Text == "admin")
                {
                    MessageBox.Show("Администратора удалить нельзя!");
                }
                else
                {
                    using (LRContainer db = new LRContainer())
                    {
                        foreach (User user in db.UserSet.ToList())
                        {
                            if (user.Login == (string)comboBoxLogin.SelectedItem)
                            {
                                db.UserSet.Remove(user);
                                db.SaveChanges();
                                MessageBox.Show("Пользователь успешно удален.");
                                comboBoxRole.SelectedItem = "";
                            }
                            if (user.Role == "Персонал")
                            {
                                foreach (Staff staff in db.StaffSet)
                                {
                                    if (staff.Login == comboBoxLogin.Text)
                                    {
                                        db.StaffSet.Remove(staff);
                                        //db.SaveChanges();
                                    }
                                }
                            }
                            if (user.Role == "Участник")
                            {
                                foreach (Participant participant in db.ParticipantSet)
                                {
                                    if (participant.Login == comboBoxLogin.Text)
                                    {
                                        db.ParticipantSet.Remove(participant);
                                        //db.SaveChanges();
                                    }
                                }
                            }
                            if (user.Role == "Тьютор")
                            {
                                foreach (Tutor tutor in db.TutorSet)
                                {
                                    if (tutor.Login == comboBoxLogin.Text)
                                    {
                                        db.TutorSet.Remove(tutor);
                                        //db.SaveChanges();
                                    }
                                }
                            }
                            if (user.Role == "Спикер")
                            {
                                foreach (Speaker speaker in db.SpeakerSet)
                                {
                                    if (speaker.Login == comboBoxLogin.Text)
                                    {
                                        db.SpeakerSet.Remove(speaker);
                                        //db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя!");
            }
        }

        private void comboBoxLogin_MouseEnter(object sender, EventArgs e)
        {
            comboBoxLogin.Items.Clear();
            using (LRContainer db = new LRContainer())
            {
                if (comboBoxRole.Text == "Пользователь") 
                {
                    foreach(User user in db.UserSet)
                    {
                        comboBoxLogin.Items.Add(user.Login);
                    }
                }
                if (comboBoxRole.Text == "Участник")
                {
                    foreach(Participant participant in db.ParticipantSet)
                    {
                        comboBoxLogin.Items.Add(participant.Login);
                    }
                }
                if (comboBoxRole.Text == "Спикер")
                {
                    foreach(Speaker speaker in db.SpeakerSet)
                    {
                        comboBoxLogin.Items.Add(speaker.Login);
                    }
                }
                if (comboBoxRole.Text == "Персонал")
                {
                    foreach(Staff staff in db.StaffSet)
                    {
                        comboBoxLogin.Items.Add(staff.Login);
                    }
                }
                if (comboBoxRole.Text == "Тьютор")
                {
                    foreach(Tutor tutor in db.TutorSet)
                    {
                        comboBoxLogin.Items.Add(tutor.Login);
                    }
                }
            }
        }
    }
}
