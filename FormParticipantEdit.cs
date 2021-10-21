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
    public partial class FormParticipantEdit : Form
    {
        public FormParticipantEdit()
        {
            InitializeComponent();
            comboBoxDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }
        public FormParticipant form = new FormParticipant();
        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach(Participant participant in db.ParticipantSet)
                {
                    if (participant.Login == textBoxLogin.Text)
                    {
                        if (textBoxCity.Text != ""
                            && textBoxName.Text != ""
                            && textBoxSurname.Text != ""
                            && maskedTextBoxPhone.Text != ""
                            && comboBoxDirection.Text != "")
                        {
                            participant.City = textBoxCity.Text;
                            participant.Name = textBoxName.Text;
                            participant.Patronymic = textBoxPatronymic.Text;
                            participant.Surname = textBoxSurname.Text;
                            participant.Phone = maskedTextBoxPhone.Text;
                            participant.Birtday = dateTimePickerBirthday.Value;
                            participant.Transfer = checkBoxTransfer.Checked;
                            participant.Direction = comboBoxDirection.Text;
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

        private void buttonBack_Click(object sender, EventArgs e)
        {
            form.labelLogin.Text = textBoxLogin.Text;
            form.Show();
            this.Hide();
        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
        }
    }
}
