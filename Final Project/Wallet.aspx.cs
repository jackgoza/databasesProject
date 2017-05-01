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
    DataAbstract DA;
    static Boolean DescFlag;
    static Boolean AmtFlag;
    static Boolean CatFlag;
    static Boolean DateFlag;
    static Boolean TypeFlag;
    int TID = -1;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DescFlag = false;
            AmtFlag = false;
            CatFlag = false;
            DateFlag = false;
            TypeFlag = false;
        } // end if
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    protected void Page_PreRender()
    {
        if (!IsPostBack)
        {
            DataAbstract DA = new DataAbstract();
            if (Convert.ToInt32(Session["userID"]) != 0)
            {
                userID = Convert.ToInt32(Session["userID"]);
                DataSet accountData = DA.returnAccounts(userID); //gets all accounts
                System.Data.DataTable accountsTable = accountData.Tables[0]; //table holding all account entries for the user
                object s = accountsTable.Rows[0].Field<object>("AcctNumber"); //sets the default account to the first of the user's accounts. LIKELY CHANGE LATER.
                if (Session["account"] == null)
                {
                    Session["account"] = Convert.ToInt64(s);                        //saves the first account as the default account during the session
                }                      
                userID = Convert.ToInt16(Session["userID"]);                    //saves the Session userID to the variable on this page 
            }
            else
            {
                Session["userID"] = 1;  //temporary solution for demo 3/19/2017
                Session["account"] = 211111110;
            }
            // long, datetime, datetime int, int 
            transactions = DA.returnTransactions(Convert.ToInt64(Session["account"]));
            TransactionsList.DataSource = transactions;
            TransactionsList.DataBind();
        }

    }

    public void searchTransactions(object sender, EventArgs e)
    {
        DataAbstract DA = new DataAbstract();
        DateTime d1 = new DateTime(2000, 1, 1);
        DateTime d2 = new DateTime(2100, 12, 12);
        string searchkey = Search.Text;

        if (searchkey != "" && searchkey != "Search")
        {
            transactions = DA.returnTransactionsSearch(Convert.ToInt64(Session["account"]), d1, d2, searchkey);
            TransactionsList.DataSource = transactions;
            TransactionsList.DataBind();
        }
        if (searchkey == "All" || searchkey == "all")
        {
            transactions = DA.returnTransactions(Convert.ToInt64(Session["account"]));
            TransactionsList.DataSource = transactions;
            TransactionsList.DataBind();
        }
    }

    protected void DescFilter_Click(object sender, EventArgs e)
    {
        AmtFlag = false;
        CatFlag = false;
        DateFlag = false;
        TypeFlag = false;

        transactions = DA.returnTransactionsCategorySort(Convert.ToInt64(Session["account"]), 0, DescFlag);

        DescFlag = !DescFlag;

        TransactionsList.DataSource = transactions;
        TransactionsList.DataBind();
    }

    protected void AmtFilter_Click(object sender, EventArgs e)
    {
        DescFlag = false;
        CatFlag = false;
        DateFlag = false;
        TypeFlag = false;

        transactions = DA.returnTransactionsCategorySort(Convert.ToInt64(Session["account"]), 1, AmtFlag);

        AmtFlag = !AmtFlag;

        TransactionsList.DataSource = transactions;
        TransactionsList.DataBind();
    }

    protected void CatFilter_Click(object sender, EventArgs e)
    {
        DescFlag = false;
        AmtFlag = false;
        DateFlag = false;
        TypeFlag = false;

        transactions = DA.returnTransactionsCategorySort(Convert.ToInt64(Session["account"]), 2, CatFlag);

        CatFlag = !CatFlag;

        TransactionsList.DataSource = transactions;
        TransactionsList.DataBind();
    }

    protected void DateFilter_Click(object sender, EventArgs e)
    {
        DescFlag = false;
        AmtFlag = false;
        CatFlag = false;
        TypeFlag = false;

        transactions = DA.returnTransactionsCategorySort(Convert.ToInt64(Session["account"]), 3, DateFlag);

        DateFlag = !DateFlag;

        TransactionsList.DataSource = transactions;
        TransactionsList.DataBind();
    }

    protected void TypeFilter_Click(object sender, EventArgs e)
    {
        DescFlag = false;
        AmtFlag = false;
        CatFlag = false;
        DateFlag = false;

        transactions = DA.returnTransactionsCategorySort(Convert.ToInt64(Session["account"]), 4, TypeFlag);

        TypeFlag = !TypeFlag;

        TransactionsList.DataSource = transactions;
        TransactionsList.DataBind();
    }

    public void logoutClick(Object sender, EventArgs e)
    {
        Session["ViewState"] = null;
        Session["userID"] = null;
        Session["account"] = null;
        Response.Redirect("Login.aspx");
    }

    public void openModal(Object sender, CommandEventArgs e)
    {
        changeCategory.Attributes["style"] = "display: block;";
        TID = Convert.ToInt32(e.CommandArgument);
    }

    public void editCategory()
    {

    }
}