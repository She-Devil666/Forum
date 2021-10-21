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
    public partial class FormStaff : Form
    {
        private byte[] imageBytes;

        public FormStaff()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new FormMainScreen().Show();
            this.Hide();
        }

        private void FormStaff_MouseEnter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (Staff staff in db.StaffSet)
                {
                    if (staff.Login == labelLogin.Text)
                    {
                        labelSurname_Name.Text = staff.Name + " " + staff.Surname;
                        labelBirthday.Text = staff.Birthday.ToShortDateString();
                        labelPosition.Text = staff.Position;
                        labelR_site.Text = staff.R_site;
                        labelPlace.Text = staff.Place;
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

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            FormStaffEdit edit = new FormStaffEdit();
            edit.textBoxLogin.Text = labelLogin.Text;
            using (LRContainer db = new LRContainer())
            {
                foreach (Staff staff in db.StaffSet)
                {
                    if (staff.Login == edit.textBoxLogin.Text)
                    {
                        foreach (User user in db.UserSet)
                        {
                            if (user.Login == edit.textBoxLogin.Text)
                                edit.textBoxEmail.Text = user.Email;
                        }
                        edit.textBoxName.Text = staff.Name;
                        edit.textBoxPatronymic.Text = staff.Patronymic;
                        edit.textBoxSurname.Text = staff.Surname;
                        edit.maskedTextBoxPhone.Text = staff.Phone;
                        edit.dateTimePickerBirthday.Value = staff.Birthday;
                        edit.checkBoxDocuments.Checked = staff.Documents;
                        edit.comboBoxPlace.Text = staff.Place;
                    }
                }
            }
            this.Hide();
            edit.Show();
        }

        private void buttonImage_Click(object sender, EventArgs e)
        {
            using (LRContainer db = new LRContainer())
            {
                foreach (User user in db.UserSet)
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
