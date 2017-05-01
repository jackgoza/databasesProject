using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tester;
using loginVals;

namespace loginVals
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        protected void onClick(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            string x = btn.CommandArgument ;



            string n = username.Value;
            string p = password.Value;
            DataAbstract DA = new DataAbstract();

            int authenticated = DA.login(n, p);


            if (authenticated > -1)
            {
                //The userID of matching username and password is stored globally as Session["userID"]
               // Session.Add("userID", authenticated);
                Session["userID"] = authenticated;
                Response.Redirect("~/Summary.aspx");
            }
            //just a way to show authentication failed
            else username.Value = "FAIL";
        }
    }
}

