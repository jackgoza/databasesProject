using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tester;
using System.Web.Services;
using System.Data;

public partial class Budget : System.Web.UI.Page
{
    static int userID;
    DataSet BudgetDS;

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

        

        BudgetDS = DA.returnBudgets(Convert.ToInt64(Session["account"]));

        //Sets the source for the listview 

        BudgetList.DataSource = BudgetDS;
        BudgetList.DataBind();
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

    //BELOW IS CODE NEEDING SPECIFICS SUCH AS BUDGET ADDING FUNCTION AND VALUE IDS
    /*
    public void addBudgetClick(Object sender,
                          EventArgs e)
    {
        DataAbstract DA = new DataAbstract();

        int categoryID = 55; //REAL ONE WILL USE INDEX OF DROP-DOWN? 
        string name = BudgetIDUserInput.Text;
        long accountNum = Convert.ToInt64(Session["account"]); //uses the session to recieve the account
        double amount = Convert.ToDouble(BudgetAmtUserInput.Text);     //holds amount of the goal

        //CHECK LINE BELOW FOR PROPER VALUES 
        DateTime endDate = Convert.ToDateTime(BudgetDateUserInput.Text);
        DateTime startDate = Convert.ToDateTime(BudgetStartDateUserInput.Text);
        //ADD CODE TO GET CHECKBOX VALUE 
        int favorited = 0;
        CheckBox CB = new CheckBox();  //REPLACE THIS WITH CHECKBOX VALUE!!
        if (CB.Checked) favorited = 1;
        DA.addBudget(accountNum, categoryID, startDate, endDate, amount, favorited);
        //Reset form data after button click. Form data was resent upon refresh without this.
        Session["ViewState"] = null;
        Response.Redirect("Goals.aspx");
    }
    
    */

    public void imageChoose(object sender, EventArgs e)
    {
        //Code for favorite button
    }

    public void toggleFavorite(object sender, CommandEventArgs e)
    {
        //Code to favorite a budget
    }

    [WebMethod]
     public static string[,] getbudgetData()   //return string[,] later. 
     {
 
         DataAbstract DA = new DataAbstract();
 
         DataSet accountData = DA.get_Accounts(userID);
         System.Data.DataTable accountsTable = accountData.Tables[0]; //table holding all account entries for the user
         int accountCount = accountsTable.Rows.Count; //the total number of accounts for the user
 
         int totalBudgetCount = 0;
 
         //grab each account under the userID, grab the goals for each account, sum number of all goals
         for (int num = 0; num<accountCount; ++num)
         {
             System.Data.DataRow accountRow = accountsTable.Rows[num];
             object s = accountRow.Field<object>("AcctNumber");
             long accountNum = Convert.ToInt64(s);
             totalBudgetCount += DA.returnBudgets(accountNum).Tables[0].Rows.Count;
         }
 
 
         string[,] result = new string[totalBudgetCount, 9]; //will hold data for all goals in all accounts
 
         int total = 0;
 
         //populate the 2D array
 
         for (int i = 0; i<accountCount; ++i) //variable i iterates through accounts
         {
            System.Data.DataRow accountRow = accountsTable.Rows[i];
            long accountNum = Convert.ToInt64(accountRow.Field<object>("AcctNumber"));
            DataSet D = DA.returnBudgets(accountNum);
 
 
             System.Data.DataTable T = D.Tables[0];         //Gets the actual Goals Table, filtered appropriately 
             int rowCount = T.Rows.Count;                    //Number of entries
 
             for (int j = 0; j<rowCount; ++j) //variable j iterates through budgets of an account
             {
                 //RAW DATABASE OBJECTS
                 object BudgetIDData, CategoryIDData, StartDateData, EndDateData, MaxAmtData, CompletedData, FailedData, CurrentAmt;
                 //BUDGET INFOS TO BECOME STRINGS
                 string BudgetID, CategoryID, AcctNumber, StartDate, EndDate, MaxAmt, Completed, Failed, Current;
                 System.Data.DataRow DR = T.Rows[j];         //gets the current row we're copying 
 
                 //BudgetID
                 BudgetIDData = DR.Field<object>("BudgetID");
                 BudgetID = Convert.ToString(BudgetIDData);
                 //CategoryID
                 CategoryIDData = DR.Field<object>("CategoryID");
                 CategoryID = DA.get_categoryName(Convert.ToInt32(CategoryIDData));
                 //AcctNumber
                 AcctNumber = Convert.ToString(accountNum);
                 //StartDate
                 StartDateData = DR.Field<object>("StartDate");
                 StartDate = Convert.ToDateTime(StartDateData).ToString("M dd, yyyy");
                 //EndDate
                 EndDateData = DR.Field<object>("EndDate");
                EndDate = Convert.ToDateTime(EndDateData).ToString("M dd, yyyy");
                 //MaxAmy
                 MaxAmtData = DR.Field<object>("MaxAmt");
                 MaxAmt = Convert.ToInt32(MaxAmtData).ToString("C2");
                 //Completed?
                 CompletedData = DR.Field<object>("Completed");
                 Completed = Convert.ToString(CompletedData);
                 //Failed? 
                 FailedData = DR.Field<object>("Failed");
                 Failed = Convert.ToString(FailedData);
 
                 CurrentAmt = DR.Field<object>("CurrentAmt");
                 Current = Convert.ToString(CurrentAmt);
 
                 result[total, 0] = BudgetID;
                 result[total, 1] = CategoryID;
                 result[total, 2] = AcctNumber;
                 result[total, 3] = StartDate;
                 result[total, 4] = EndDate;
                 result[total, 5] = MaxAmt;
                 result[total, 6] = Completed;  // "false" or "true"
                 result[total, 7] = Failed;    // "false" or "true"
                 result[total, 8] = Current;
 
 
                 ++total;
             }//end goal
 
         }
         return result;
     }
 
 
  

}

