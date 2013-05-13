using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Web;

// This class handles the generation of key pairs.

namespace ProjectClient
{
    public partial class Keys : Form
    {
        public Keys()
        {
            InitializeComponent();
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            const int keySize = 1024;
            string publicAndPrivateKey;
            string publicKey;
            string url;

            AsymmetricEncryption.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            lblPriv.Text = publicAndPrivateKey;
            lblPub.Text = publicKey;
            Properties.Settings.Default.PrivateKey = publicAndPrivateKey;

            url = string.Format("https://www.adammckissock.com/crypt/insertkey.php?key={0}&user={1}", HttpUtility.UrlEncode(publicKey), Properties.Settings.Default.UserName);
            
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; WIndows NT 9.0; en-US)";

                var response = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var html = reader.ReadToEnd();
                    if (html == "inserted")
                    {
                        MessageBox.Show("Public key has been uploaded to repository");
                    }
                    else if (html == "updated")
                    {
                        MessageBox.Show("Public key has been updated in the repository");
                    }
                    writePriv(publicAndPrivateKey);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Mainform mainform = new Mainform();
            mainform.Show();
        }

        private void writePriv(string key)
        {
            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter(String.Format(@"{0}\key.txt", Application.StartupPath));
            file.WriteLine(key);
            file.Close();
        }
    }
}
