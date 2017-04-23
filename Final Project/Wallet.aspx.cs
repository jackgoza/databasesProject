using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Tester;
using System.Web.Services;


public partial class Wallet : System.Web.UI.Page
{
    static int userID;
    DataSet transactions;

    protected void Page_Load(object sender, EventArgs e)
    {
        DataAbstract DA = new DataAbstract();
        if (Convert.ToInt32(Session["userID"]) != 0)
        {
            userID = Convert.ToInt32(Session["userID"]);
            DataSet accountData = DA.get_Accounts(userID); //gets all accounts
            System.Data.DataTable accountsTable = accountData.Tables[0]; //table holding all account entries for the user
            object s = accountsTable.Rows[0].Field<object>("AcctNumber"); //sets the default account to the first of the user's accounts. LIKELY CHANGE LATER.
            Session["account"] = Convert.ToInt64(s);                        //saves the first account as the default account during the session
            userID = Convert.ToInt16(Session["userID"]);                    //saves the Session userID to the variable on this page 
        }
        else
        {
            userID = 1;  //temporary solution for demo 3/19/2017
            Session["account"] = 211111110;
        }
    }

    protected void Page_PreRender()
    {
        if (!IsPostBack)
        {
            DataAbstract DA = new DataAbstract();
            if (Convert.ToInt32(Session["userID"]) != 0)
            {
                userID = Convert.ToInt32(Session["userID"]);
                DataSet accountData = DA.get_Accounts(userID); //gets all accounts
                System.Data.DataTable accountsTable = accountData.Tables[0]; //table holding all account entries for the user
                object s = accountsTable.Rows[0].Field<object>("AcctNumber"); //sets the default account to the first of the user's accounts. LIKELY CHANGE LATER.
                Session["account"] = Convert.ToInt64(s);                        //saves the first account as the default account during the session
                userID = Convert.ToInt16(Session["userID"]);                    //saves the Session userID to the variable on this page 
            }
            else
            {
                userID = 1;  //temporary solution for demo 3/19/2017
                Session["account"] = 211111110;
            }
            // long, datetime, datetime int, int 
            transactions = DA.get_Transactions(Convert.ToInt64(Session["account"]));
            TransactionsList.DataSource = transactions;
            TransactionsList.DataBind();
        }

    }
    
    public void searchTransactions(object sender, EventArgs e)
    {
        DataAbstract DA = new DataAbstract();
        DateTime d1 = new DateTime(2000, 1, 1);
        DateTime d2 = new DateTime(2020, 12, 12);
        string searchkey = Search.Text;

        if (searchkey != "" && searchkey != "Search")
        {
            transactions = DA.get_desc_Transactions(Convert.ToInt64(Session["account"]), d1, d2, searchkey);
            TransactionsList.DataSource = transactions;
            TransactionsList.DataBind();
        }
        if (searchkey == "All" || searchkey == "all")
        {
            transactions = DA.get_Transactions(Convert.ToInt64(Session["account"]));
            TransactionsList.DataSource = transactions;
            TransactionsList.DataBind();
        }
    }
}