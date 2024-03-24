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
            /*AuthenticationPage.Visible = true;*/
        }

        protected void Home_Click(object sender, EventArgs e)
        {
            HomePage.Visible = true;
            AboutPage.Visible = false;
        }

        protected void About_Click(object sender, EventArgs e)
        {
            HomePage.Visible = false;
            AboutPage.Visible = true;
        }

        protected void Contacts_Click(object sender, EventArgs e)
        {

        }
    }
}