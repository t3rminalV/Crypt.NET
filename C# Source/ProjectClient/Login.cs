using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using AppNetDotNet;
using AppNetDotNet.Model;
using AppNetDotNet.ApiCalls;

namespace ProjectClient
{
    public partial class Login : Form
    {      
        public Login()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
            Authorization.registerAppInRegistry(Authorization.registerBrowserEmulationValue.IE8Always);            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Authorization.clientSideFlow apnAuthProcess = new Authorization.clientSideFlow("CKxdaAdhT44PmH8h4CrYR4nYcsgCx2sU", "http://adammckissock.com/redirect.html", "basic stream write_post messages");
            apnAuthProcess.AuthSuccess += authProcess_AuthSuccess;
            apnAuthProcess.showAuthWindow();
        }
        
        void authProcess_AuthSuccess(object sender, AuthorizationWindow.AuthEventArgs e)
        {
            if (e != null)
            {
                if (e.success)
                {
                    //System.Windows.MessageBox.Show(e.accessToken, "Access token");
                    Tuple<Token, ApiCallResponse> token = Tokens.get(e.accessToken);
                    Properties.Settings.Default.AccessToken = e.accessToken;
                    Redirect();
                }
                else
                {
                    System.Windows.MessageBox.Show(e.error, "Authorization failed");
                }
            }
        }
        
        private void llblInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StringBuilder messageBuilder = new StringBuilder(200);

            messageBuilder.Append("By logging into this application you are giving authourisation for it to do the following:");
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("\t\u2022 Access basic information about your APP.NET account.");
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("\t\u2022 Read your messages and post stream.");
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("\t\u2022 Make posts and messages on your behalf.");

            System.Windows.MessageBox.Show(messageBuilder.ToString(), "Info");
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AccessToken != "")
            {
                Redirect();
            }
        }

        private void Redirect()
        {
            Mainform main = new Mainform();
            main.Show();
            this.Visible = false;
        }
    }
}

