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
    public partial class FormAdminShedule : Form
    {
        public FormAdminShedule()
        {
            InitializeComponent();
            comboBoxAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSpeaker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxSpeaker_MouseEnter(object sender, EventArgs e)
        {
            comboBoxSpeaker.Items.Clear();
            using (LRContainer db = new LRContainer())
            {
                foreach(Speaker speaker in db.SpeakerSet)
                {
                    comboBoxSpeaker.Items.Add(speaker.Name + " " + speaker.Surname);
                }
            }
        }

        private void buttonShowShedule_Click(object sender, EventArgs e)
        {
            new FormAdminShowSchedule().Show();
            this.Hide();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new FormAdminMain().Show();
            this.Hide();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using(LRContainer db = new LRContainer())
            {
                if (comboBoxCity.Text != ""
                    && comboBoxAddress.Text != ""
                    && maskedTextBoxPhone.Text != ""
                    && maskedTextBox1.Text != ""
                    && comboBoxSpeaker.Text != "")
                {
                    foreach(Speaker speaker in db.SpeakerSet)
                    {
                        if(comboBoxSpeaker.Text == speaker.Name + " " + speaker.Surname)
                        {
                            Schedule schedule = new Schedule() { City = comboBoxCity.Text, Address = comboBoxAddress.Text, Date_time = DateTime.Parse(maskedTextBox1.Text), Phone = maskedTextBoxPhone.Text, Speaker = speaker };
                            db.ScheduleSet.Add(schedule);
                            MessageBox.Show("Успешно добавлено!");
                            comboBoxCity.Text = "";
                            comboBoxAddress.Text = "";
                            maskedTextBox1.Clear();
                            maskedTextBoxPhone.Clear();
                            comboBoxSpeaker.Text = "";
                        }
                    }
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Некорректный ввод.");
                }
            }
        }
    }
}