using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppG2.Controller;
using AppG2.Model;
using System.IO;

namespace AppG2
{
    public partial class ThemContact : Form
    {
        contact ct;
        String pathcontact = Application.StartupPath + @"\Data\contact.txt";
        public ThemContact(contact ct = null)
        {
            InitializeComponent();
            this.ct = ct;
            if (ct != null)
            {
                this.Text = "chỉnh sửa mới contact";
                txtname.Text = ct.Name;
                txtphone.Text = ct.Phone;
                txtemail.Text = ct.Email;
            }
            else
            {
                
                this.Text = "Thêm mới contact";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ct != null)
            {
               
                var listContact = Contactservice.GetContact(pathcontact);
                string moi = txtname.Text + "#" + txtphone.Text + "#" + txtemail.Text;
                               
                String cu = ct.Name + "#" + ct.Phone + "#" + ct.Email;
                var Lines = File.ReadAllLines(pathcontact);
                var newLines = Lines.Where(line => !line.Contains(cu));
                File.WriteAllLines(pathcontact, newLines);
                File.AppendAllText(pathcontact, moi);
            }
            else
            {
                string newContact = txtname.Text + "#" + txtphone.Text + "#" + txtemail.Text;
                File.AppendAllText(pathcontact, "\n" + newContact);
                Console.WriteLine("\nđã thêm");
            }
            MessageBox.Show("đã cập nhật dữ liệu thành công");
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
