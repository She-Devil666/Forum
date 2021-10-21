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
    public partial class FormShowSchedule : Form
    {
        public FormShowSchedule()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            using(LRContainer db = new LRContainer())
            {
                foreach (User user in db.UserSet)
                {
                    if (user.Login == labelLogin.Text)
                    {
                        if (user.Role == "Участник")
                        {
                            FormParticipant form = new FormParticipant();
                            form.labelLogin.Text = user.Login;
                            this.Hide();
                            form.Show();
                        }
                        if (user.Role == "Тьютор")
                        {
                            FormTutor form = new FormTutor();
                            form.labelLogin.Text = user.Login;
                            this.Hide();
                            form.Show();
                        }
                        if (user.Role == "Спикер")
                        {
                            FormSpeaker form = new FormSpeaker();
                            form.labelLogin.Text = user.Login;
                            this.Hide();
                            form.Show();
                        }
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listBox1_MouseEnter(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (LRContainer db = new LRContainer())
            {
                foreach (User user in db.UserSet)
                {
                    if (user.Login == labelLogin.Text)
                    {
                        if (user.Role == "Участник" || user.Role == "Тьютор")
                        {
                            foreach (Schedule schedule in db.ScheduleSet)
                            {
                                foreach (Speaker speaker in db.SpeakerSet)
                                {
                                    if (speaker.Direction == labelDirection.Text && speaker == schedule.Speaker)
                                    {
                                        listBox1.Items.Add("г." + schedule.City + " " + schedule.Address + " " + schedule.Date_time + " ведет " + speaker.Name + " " + speaker.Surname);
                                    }
                                }
                            }
                        }
                        if (user.Role == "Спикер")
                        {
                            foreach(Schedule schedule in db.ScheduleSet)
                            {
                                foreach(Speaker speaker in db.SpeakerSet)
                                {
                                    if (speaker == schedule.Speaker && speaker.Login == labelLogin.Text) 
                                    {
                                        listBox1.Items.Add("г." + schedule.City + " " + schedule.Address + " " + schedule.Date_time);
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
