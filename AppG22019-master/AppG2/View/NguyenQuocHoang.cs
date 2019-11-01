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
using AppG2.Controller;
using AppG2.Model;
namespace AppG2.View
{
    public partial class NguyenQuocHoang : Form
    {
        String pathcontact;
       
        public NguyenQuocHoang()
        {
            InitializeComponent();
            pathcontact = Application.StartupPath + @"\Data\contact.txt";

            datacontact.DataSource = null;
            dtcontact.AutoGenerateColumns = false;
            var list1 = Contactservice.GetContact(pathcontact);
            var listContact = list1.OrderBy(x => x.Name).ToList();
            if (listContact == null)
                throw new Exception("Không có liên lạc ");
            else
            {
                datacontact.DataSource = listContact;
            }
            dtcontact.DataSource = datacontact;
            AddNewLabel();


        }

     

        public void AddNewLabel()
        {
            flowLayoutPanel1.Controls.Clear();
            List<string> listLabelDuplicate = new List<string>();
            var listContactNoSort = Contactservice.GetContact(pathcontact);
            foreach (var item in listContactNoSort)
            {
                listLabelDuplicate.Add(item.Ma);
            }
            List<String> labels = listLabelDuplicate.Distinct().ToList();
            labels.Sort();
            for (int i = 0; i < labels.Count; i++)
            {
                Label lbl = new Label();
                lbl.Text = labels[i];
                lbl.Click += new System.EventHandler(this.Label_Click);
                flowLayoutPanel1.Controls.Add(lbl);
            }
        }

        private void Btndelete_Click(object sender, EventArgs e)
        {
            var rs = MessageBox.Show(
                 "Bạn Có Muốn Xóa Không?",
                 "Thông Báo",
                 MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Warning);
            if (rs == DialogResult.OK)
            {


                var ct = datacontact.Current as contact;

                if (ct != null)
                {
                    String ma = ct.Ma;
                    String name = ct.Name;
                    String phone = ct.Phone;
                    String email = ct.Email;
                    String td = name + "#" + phone + "#" + email;
                    File.WriteAllLines(pathcontact,
                    File.ReadLines(pathcontact).Where(l => (l != td)).ToList());

                }

                datacontact.DataSource = null;
                dtcontact.DataSource = null;
                dtcontact.AutoGenerateColumns = false;
                datacontact.DataSource = Contactservice.GetContact(pathcontact);
                dtcontact.DataSource = datacontact;
                MessageBox.Show(
                    "Xóa Thành Công" + ct.Name);
                AddNewLabel();

            }
            else
            {
                MessageBox.Show("Không Xóa");
            }
        }

        private void Btnadd_Click(object sender, EventArgs e)
        {
            var f = new  ThemContact();
            if (f.ShowDialog() == DialogResult.OK)
            {
                datacontact.DataSource = null;

                var list1 = Contactservice.GetContact(pathcontact);
                var newContactList = list1.OrderBy(x => x.Name);
                datacontact.DataSource = newContactList;
                datacontact.ResetBindings(true);
                AddNewLabel();
            }
        }

        private void Btnedit_Click(object sender, EventArgs e)
        {
            var ct = datacontact.Current as contact;
            if (ct != null)
            {
                var f = new ThemContact(ct);


                if (f.ShowDialog() == DialogResult.OK)
                {
                    var list1 = Contactservice.GetContact(pathcontact);
                    var newContactList = list1.OrderBy(x => x.Name);
                    datacontact.DataSource = newContactList;
                    datacontact.ResetBindings(true);
                    AddNewLabel();
                }

            }
        }
        private void Label_Click(object sender, EventArgs e)
        {
            var labelName = ((Label)sender).Text;
            var listContactNoSort = Contactservice.GetContactInAlphabetic(labelName, pathcontact);
            var newContactList = listContactNoSort.OrderBy(x => x.Name).ToList();
            datacontact.DataSource = newContactList;
            datacontact.ResetBindings(true);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Txttiimkiem_TextChanged(object sender, EventArgs e)
        {
            var contactListNoSort = Contactservice.GetContactBySearch(txttiimkiem.Text, pathcontact);
            datacontact.DataSource = contactListNoSort;
            datacontact.ResetBindings(true);
        }
    }
}
