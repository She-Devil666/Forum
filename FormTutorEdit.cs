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
    public partial class FormTutorEdit : Form
    {
        public FormTutorEdit()
        {
            InitializeComponent();
            comboBoxdDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }
        public FormTutor form = new FormTutor();
        private void buttonBack_Click(object sender, EventArgs e)
        {
            form.labelLogin.Text = textBoxLogin.Text;
            this.Hide();
            form.Show();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (Tutor tutor in db.TutorSet)
                {
                    if (tutor.Login == textBoxLogin.Text)
                    {
                        if (textBoxCity.Text != ""
                            && textBoxName.Text != ""
                            && textBoxSurname.Text != ""
                            && maskedTextBoxPhone.Text != ""
                            && comboBoxdDirection.Text != "")
                        {
                            tutor.Name = textBoxName.Text;
                            tutor.Patronymic = textBoxPatronymic.Text;
                            tutor.Surname = textBoxSurname.Text;
                            tutor.Phone = maskedTextBoxPhone.Text;
                            tutor.Birthday = dateTimePickerBirthday.Value;
                            tutor.Direction = comboBoxdDirection.Text;
                            tutor.Documents = checkBoxDocs.Checked;
                            tutor.City = textBoxCity.Text;
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
                foreach (Tutor tutor in db.TutorSet)
                {
                    if (tutor.Login == textBoxLogin.Text)
                    {
                        foreach (User user in db.UserSet)
                        {
                            if(user.Login == textBoxLogin.Text)
                                textBoxEmail.Text = user.Email;
                        }
                        textBoxName.Text = tutor.Name;
                        textBoxPatronymic.Text = tutor.Patronymic;
                        textBoxSurname.Text = tutor.Surname;
                        maskedTextBoxPhone.Text = tutor.Phone;
                        dateTimePickerBirthday.Value = tutor.Birthday;
                        textBoxCity.Text = tutor.City;
                        comboBoxdDirection.Text = tutor.Direction;
                        checkBoxDocs.Checked = tutor.Documents;
                    }
                }
            }
        }
    }
}
