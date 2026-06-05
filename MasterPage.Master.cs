using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_rehistro
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnLogout.Visible = SessionManager.IsLoggedIn;

            if (!IsPostBack)
            {
                if (SessionManager.IsLoggedIn)
                {
                    if (SessionManager.IsAdmin)
                        ShowPage("AdminPage");
                    else
                        ShowPage("HomePage");
                }
                else
                {
                    ShowPage("AuthenticationPage");
                }
            }
        }

        /// <summary>
        /// Shows the specified page and hides all others.
        /// Centralizes visibility management to prevent state leaks.
        /// </summary>
        public void ShowPage(string pageName)
        {
            AuthenticationPage.Visible = (pageName == "AuthenticationPage");
            AdminPage.Visible = (pageName == "AdminPage");
            HomePage.Visible = (pageName == "HomePage");
            RegistrationPage.Visible = (pageName == "RegistrationPage");
            FirstRegistrationForm.Visible = (pageName == "FirstRegistrationForm");
            SecondRegistrationForm.Visible = (pageName == "SecondRegistrationForm");
            UploadDocumentPage.Visible = (pageName == "UploadDocumentPage");
            PendingStatusPage.Visible = (pageName == "PendingStatusPage");
            DeclinedStatusPage.Visible = (pageName == "DeclinedStatusPage");
            VerifiedStatusPage.Visible = (pageName == "VerifiedStatusPage");
            VoterIDInfo.Visible = (pageName == "VoterIDInfo");
            NewsAndEventsPage.Visible = (pageName == "NewsAndEventsPage");
            AboutPage.Visible = (pageName == "AboutPage");
            ContactsPage.Visible = (pageName == "ContactsPage");
        }

        public void Home_Click(object sender, EventArgs e)
        {
            ShowPage("HomePage");
        }

        public void Registration_Click(object sender, EventArgs e)
        {
            ShowPage("RegistrationPage");
        }

        protected void NewsAndEvents_Click(object sender, EventArgs e)
        {
            ShowPage("NewsAndEventsPage");
        }

        protected void About_Click(object sender, EventArgs e)
        {
            ShowPage("AboutPage");
        }

        protected void Contacts_Click(object sender, EventArgs e)
        {
            ShowPage("ContactsPage");
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            SessionManager.Clear();
            ShowPage("AuthenticationPage");
        }
    }
}