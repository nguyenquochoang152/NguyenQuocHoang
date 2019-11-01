using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppG2.Model;
namespace AppG2.Controller
{
    class Contactservice
    {

        public static List<contact> GetContact(string pathcontact)
        {
            if (File.Exists(pathcontact))
            {

                var listLines = File.ReadAllLines(pathcontact);
                List<contact> listcontact = new List<contact>();
                foreach (var line in listLines)
                {
                    if (line != "")
                    {
                        var rs = line.Split(new char[] { '#' });
                        contact ct = new contact
                        {
                            Name = rs[0],
                            Phone = rs[1],
                            Email = rs[2]

                        };
                        listcontact.Add(ct);
                    }

                }
                return listcontact;
            }
            else
                return null;
        }
        public static List<contact> GetContactBySearch(string originalText, string pathDataFile)
        {
            string text = originalText.ToLower();
            List<contact> newListContact = new List<contact>();
            if (!text.Equals(""))
            {
                List<contact> listContact = GetContact(pathDataFile);

                foreach (var item in listContact)
                {
                    if (item.Name.ToLower().Contains(text) || item.Phone.ToLower().Contains(text) || item.Email.ToLower().Contains(text))
                    {
                        newListContact.Add(item);
                    }
                }
                return newListContact;
            }
            else
            {
                return GetContact(pathDataFile);
            }

        }

        public static List<contact> GetContactInAlphabetic(string text, string pathDataFile)
        {
            List<contact> newListContact = new List<contact>();
            if (!text.Equals("All"))
            {
                List<contact> listContact = GetContact(pathDataFile);

                foreach (var item in listContact)
                {
                    if (String.Compare(item.Ma, text) >= 0)
                    {
                        newListContact.Add(item);
                    }
                }
                return newListContact;
            }
            else
            {
                return GetContact(pathDataFile);
            }
        }
    }
}
