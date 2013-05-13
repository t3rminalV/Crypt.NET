using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using AppNetDotNet;
using AppNetDotNet.Model;
using AppNetDotNet.ApiCalls;
using System.Web.UI.WebControls;
using System.Web;

// This is the main form

namespace ProjectClient
{
    public partial class Mainform : Form
    {        
        public Tuple<Token, ApiCallResponse> token;
        public List<String> cryptposts = new List<String>();

        public Mainform()
        {
            InitializeComponent();
        }

        private void Mainform_Load(object sender, EventArgs e)
        {     
            checkLogin();
        }

        private bool checkForKey()
        {
            if (System.IO.File.Exists(String.Format(@"{0}\key.txt", System.Windows.Forms.Application.StartupPath)))
            {
                Properties.Settings.Default.PrivateKey = System.IO.File.ReadAllText(String.Format(@"{0}\key.txt", System.Windows.Forms.Application.StartupPath));
                return true;
            }
            else
            {
                return false;
            }
        }

        private void checkLogin()
        {
            if (Properties.Settings.Default.AccessToken == "")
            {
                //not logged in
                this.Hide();
                AppNetDotNet.Model.Authorization.clientSideFlow apnAuthProcess = new AppNetDotNet.Model.Authorization.clientSideFlow("CKxdaAdhT44PmH8h4CrYR4nYcsgCx2sU", "http://adammckissock.com/redirect.html", "basic stream write_post messages");
                apnAuthProcess.AuthSuccess += authProcess_AuthSuccess;
                apnAuthProcess.showAuthWindow();
            } 
        }

        void authProcess_AuthSuccess(object sender, AuthorizationWindow.AuthEventArgs e)
        {
            if (e != null)
            {
                if (e.success)
                {
                    //System.Windows.MessageBox.Show(e.accessToken, "Access token");
                    token = Tokens.get(e.accessToken);
                    Properties.Settings.Default.AccessToken = e.accessToken;

                    lblStatus.Text = "Logged in as " + token.Item1.user.ToString();
                    picAvatar.ImageLocation = token.Item1.user.avatar_image.url;

                    Tuple<User, ApiCallResponse> result;
                    ParametersMyStream parameters = new ParametersMyStream();
                    parameters.count = 100;
                    parameters.include_annotations = true;
                    result = AppNetDotNet.ApiCalls.Users.getUserByUsernameOrId(Properties.Settings.Default.AccessToken, "me");
                    if (result.Item2.success)
                    {
                        User me = result.Item1;
                        Properties.Settings.Default.UserName = me.username;
                    }

                    if (checkForKey())
                    {
                        getStream();
                        this.Show();
                    }
                    else
                    {
                        Keys key = new Keys();
                        MessageBox.Show("You have not generated keys, redirecting to the key generation page");
                        key.Show();
                    }                
                }
                else
                {
                    System.Windows.MessageBox.Show(e.error, "Authorization failed");
                }
            }
        }

        private void getStream()
        {
            listBox1.Items.Clear();

            string replaceme = "@" + Properties.Settings.Default.UserName + " [crypt]";

            Tuple<List<Post>, ApiCallResponse> result;
            ParametersMyStream parameters = new ParametersMyStream();
            parameters.count = 100;
            parameters.include_annotations = true;
            result = AppNetDotNet.ApiCalls.Posts.getMentionsOfUsernameOrId(Properties.Settings.Default.AccessToken, "me");
            if (result.Item2.success) 
            {
                List<Post> posts = result.Item1;                
                for (int i = 0; i < posts.Count(); i++)
                {
                    if (posts[i].text.Contains("[crypt]"))
                    {
                        listBox1.Items.Add(posts[i].user.ToString() + ":");
                        string code = posts[i].text.Replace(replaceme, "");
                        listBox1.Items.Add(parseCode(code));
                    }                    
                }
            }            
        }

        private string parseCode(string code)
        {
            string url = string.Format("https://www.adammckissock.com/crypt/getmessage.php?id={0}", code);
            
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; WIndows NT 9.0; en-US)";

                var response = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var html = reader.ReadToEnd();

                    if (html == "error")
                    {
                        return "Post Removed";
                    }
                    else
                    {
                        string decrypted = AsymmetricEncryption.DecryptText(html, 1024, Properties.Settings.Default.PrivateKey);
                        return decrypted;
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }            
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();               
        }

        private void keysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkForKey())
            {
                MessageBox.Show("Regenerating keys will mean that you will loose access to previous messages that have been sent to you forever!!");               
            }
            Keys key = new Keys();
            key.Show();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            string encrypted;
            string pubkey;

            string url = string.Format("https://www.adammckissock.com/crypt/getkey.php?user={0}", Properties.Settings.Default.UserName);

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; WIndows NT 9.0; en-US)";

                var response = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var html = reader.ReadToEnd();

                    if (html == "error")
                    {
                        MessageBox.Show("User has no key");
                    }
                    else
                    {
                        pubkey = HttpUtility.UrlDecode(html);
                        encrypted = AsymmetricEncryption.EncryptText(message, 1024, html);
                        uploadStuff(encrypted);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }            
        }

        private void uploadStuff(string encrypted)
        {
            string user = txtUser.Text;

            string url = string.Format("https://www.adammckissock.com/crypt/putmessage.php?user={0}&post={1}", Properties.Settings.Default.UserName, HttpUtility.UrlEncode(encrypted));
            
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; WIndows NT 9.0; en-US)";

                var response = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var html = reader.ReadToEnd();

                    if (html == "error")
                    {
                        MessageBox.Show("Error sending message");
                    }
                    else
                    {
                        adnPost(html);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void adnPost(string code)
        {
            string user = txtUser.Text;

            string constructed = "@" + user + " [crypt]" + code;

            AppNetDotNet.ApiCalls.Posts.create(Properties.Settings.Default.AccessToken, constructed);

            lblStatus.Text = "Message Sent!";
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getStream();
        }

        private void stuffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Written and copyright held by Adam James McKissock 2013
This file is part of Crypt.NET.
Crypt.NET. is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Crypt.NET. is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Crypt.NET..  If not, see <http://www.gnu.org/licenses/>.");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This isn't implemented");
        }
    }
}
