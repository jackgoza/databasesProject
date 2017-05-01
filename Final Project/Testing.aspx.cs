using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataConnect;
using Tester;
using System.Web.Services;

public partial class Testing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataAbstract DA = new DataAbstract();
        DateTime d1 = new DateTime(2016,11,14);
        DateTime d2 = new DateTime(2017, 5, 15);
        //DA.updateGoalAmt(19, 200.00, 211111110);
        //DA.updateGoalEndDate(19, d2, 211111110);
        //DA.updateGoalTotalAmt(19, 10000.00,211111110);
        //DA.updateGoalName(19, "pet dog", 211111110);
        DA.addGoal(3011111130, "Summer Vacation", 3100.00, d2, "Monthly", 125, "trip to Mars");
        //DA.returnGoals(211111110);
        //DA.create_Category("Travel");
        //DA.updateTransactionCategory(320, 9, 211111110);
        //DA.addBudget(211111110, 1, d1, d2, 300.00, 1);
        //DA.deleteBudget(10);
        int cat = DA.returnCompleteGoalsCount(211111110);
        double t__ = DA.returnTransactionCategoryBoundTotals(2,211111110, d1, d2);
        System.Diagnostics.Debug.WriteLine("goals count = " + cat);
        DA.returnCategoryName(1);
        
        

        DataSet t1 = new DataSet();
        t1 = DA.returnTransactionsSearch(211111110, d1, d2,"bes");
        GridView1.DataSource = t1;
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //calls page_load
        
        return;
    }
}
