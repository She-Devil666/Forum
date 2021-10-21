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
    public partial class FormSpeakerEdit : Form
    {
        public FormSpeakerEdit()
        {
            InitializeComponent();
            comboBoxDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }
        public FormSpeaker form = new FormSpeaker();
        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            form.labelLogin.Text = textBoxLogin.Text;
            form.Show();
            this.Hide();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (Speaker speaker in db.SpeakerSet)
                {
                    if (speaker.Login == textBoxLogin.Text)
                    {
                        if (int.TryParse(textBoxSalary.Text, out int salary)
                            && textBoxName.Text != ""
                            && textBoxSurname.Text != ""
                            && maskedTextBoxPhone.Text != ""
                            && comboBoxDirection.Text != "")
                        {
                            speaker.Name = textBoxName.Text;
                            speaker.Patronymic = textBoxPatronymic.Text;
                            speaker.Surname = textBoxSurname.Text;
                            speaker.Phone = maskedTextBoxPhone.Text;
                            speaker.Birthday = dateTimePickerBirthday.Value;
                            speaker.Transfer = checkBoxTransfer.Checked;
                            speaker.Direction = comboBoxDirection.Text;
                            speaker.Salary = int.Parse(textBoxSalary.Text);
                            form.labelLogin.Text = textBoxLogin.Text;
                            form.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Некорректный ввод.");
                        }
                    }
                }
                db.SaveChanges();
            }

        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (Speaker speaker in db.SpeakerSet)
                {
                    if (speaker.Login == textBoxLogin.Text)
                    {
                        textBoxEmail.Text = speaker.Mail;
                        textBoxName.Text = speaker.Name;
                        textBoxPatronymic.Text = speaker.Patronymic;
                        textBoxSurname.Text = speaker.Surname;
                        maskedTextBoxPhone.Text = speaker.Phone;
                        dateTimePickerBirthday.Value = speaker.Birthday;
                        checkBoxTransfer.Checked = speaker.Transfer;
                        comboBoxDirection.SelectedItem = speaker.Direction;
                        textBoxSalary.Text = speaker.Salary.ToString();
                    }
                }
            }

        }
    }
}
