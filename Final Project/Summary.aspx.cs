using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tester;
using System.Web.Services;
using System.Data;

public partial class Summary : System.Web.UI.Page
{
    static int userID;
    DataSet GoalDS, BudgetDS;
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

        getPieValues(Convert.ToInt64(Session["account"]));
        
    }

    
    protected void Page_PreRender(object sender, EventArgs e)
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

        

        BudgetDS = DA.get_Favorites("Budget", Convert.ToInt64(Session["account"]));

        //Sets the source for the listview 

        FaveBudgetsList.DataSource = BudgetDS;
        FaveBudgetsList.DataBind();
    } 

    [WebMethod]
    public static string[,] getPieValues(long account)
    {

        DataAbstract DA = new DataAbstract();
        DateTime DT = System.DateTime.Now;
        DateTime before = DT.AddMonths(-1);
        DataSet accountData = DA.get_Accounts(userID);
        System.Data.DataTable accountsTable = accountData.Tables[0]; //table holding all account entries for the user

        //object accountObject = accountsTable.Rows[0].Field<object>("AcctNumber");
        //long accountNumber = Convert.ToInt64(accountObject);

        DataSet catData = DA.get_Categories(userID);
        System.Data.DataTable catTable = catData.Tables[0];
        int resLength = catTable.Rows.Count;
        string[,] result = new string[resLength, 3];
        double totalTransactions = 0;

        for (int i = 0; i < catTable.Rows.Count; ++i) //get name and total spent for each category
        {

            object catIDData = catTable.Rows[i].Field<object>("CategoryID");
            object nameData = catTable.Rows[i].Field<object>("Name");
            int catID = Convert.ToInt32(catIDData);
            string category = Convert.ToString(nameData);
            DateTime start = DateTime.Now.AddMonths(-1);
            DateTime end = DateTime.Now;
            //double catSum = DA.get_Category_Bound_Spending(catID, account, start, end); //one month span
            //TEMPORARY RANGE WITH USEABLE DATA
            double catSum = DA.get_Category_Bound_Spending(catID, account, Convert.ToDateTime("1/1/2000"), Convert.ToDateTime("12/12/2024")); //other span
            totalTransactions += catSum;
            result[i, 0] = category;
            result[i, 1] = Convert.ToString(catSum);

        } //end of for loop
          //result has names and totals of each category
        double degreeCheck = 0;
        for (int i = 0; i < catTable.Rows.Count; ++i) //get degrees of pie chart for each category
        {
            double catSum = Convert.ToDouble(result[i, 1]);

            double percent = catSum / totalTransactions;
            if (catSum == 0) percent = 0;
            double degrees = percent * 360;
            result[i, 2] = Convert.ToString(Math.Round(degrees));
            degreeCheck += degrees;
        }

        return result;
    }

    public string widthString(double num, double denom)
    {
        double width = (num / denom) * 100;
        if (width > 100) width = 100;
        string s = "width: " + width.ToString() + "%";
        return s;
    }

    //Take two doubles and get their percent as an integer, always rounded down
    public int getPercent(double numerator, double denominator)
    {
        double realVal = numerator / denominator;
        realVal *= 100;
        realVal = realVal - (realVal % 1);
        int result = Convert.ToInt32(realVal);
        return result;
    }
}