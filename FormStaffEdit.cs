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
    public partial class FormStaffEdit : Form
    {
        public FormStaffEdit()
        {
            InitializeComponent();
            comboBoxPlace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }
        public FormStaff form = new FormStaff();
        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (Staff staff in db.StaffSet)
                {
                    if (staff.Login == textBoxLogin.Text)
                    {
                        if (textBoxName.Text != ""
                            && textBoxSurname.Text != ""
                            && maskedTextBoxPhone.Text != ""
                            && comboBoxPlace.Text != "")
                        {
                            staff.Name = textBoxName.Text;
                            staff.Patronymic = textBoxPatronymic.Text;
                            staff.Surname = textBoxSurname.Text;
                            staff.Phone = maskedTextBoxPhone.Text;
                            staff.Birthday = dateTimePickerBirthday.Value;
                            staff.Documents = checkBoxDocuments.Checked;
                            staff.Place = comboBoxPlace.Text;
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
            this.Hide();
            form.Show();
        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
        }
    }
}
