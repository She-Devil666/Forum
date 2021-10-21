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
    public partial class FormAdminShowSchedule : Form
    {
        public FormAdminShowSchedule()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new FormAdminShedule().Show();
            this.Hide();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach(Schedule schedule in db.ScheduleSet)
                {
                    foreach (Speaker speaker in db.SpeakerSet)
                    {
                        if ((string)listBox1.SelectedItem == ("г." + schedule.City + " " + schedule.Address + " " + schedule.Date_time + " ведет " + speaker.Name + " " + speaker.Surname))
                        {
                            db.ScheduleSet.Remove(schedule);
                            MessageBox.Show("Успешно удалено!");
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseEnter(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using(LRContainer db = new LRContainer())
            {
                foreach(Schedule schedule in db.ScheduleSet)
                {
                    foreach (Speaker speaker in db.SpeakerSet)
                    {
                        if(speaker == schedule.Speaker)
                        {
                            listBox1.Items.Add("г." + schedule.City + " " + schedule.Address + " " + schedule.Date_time + " ведет " + speaker.Name + " " + speaker.Surname);
                        }   
                    }
                }
            }
        }
    }
}
