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
        DateTime d2 = new DateTime(2017, 2, 15);
        //DA.update_Goalamt(19, 200.00, 211111110);
        //DA.mod_Goaldate(19, d2, 211111110);
        //DA.mod_Goaltotal(19, 10000.00,211111110);
        //DA.mod_Goalname(19, "pet dog", 211111110);
        //DA.addGoal(211111110, "Test", 2000.00, d, "Biweekly", 50);
        //DA.returnGoals(211111110);
        //DA.create_Category("Travel");
        //DA.recategorize_Transaction(320, 9, 211111110);
        //DA.addBudget(211111110, 1, d1, d2, 300.00, 1);
        string cat = DA.get_categoryName(1);
        double t__ = DA.get_Category_Bound_Spending(2,211111110, d1, d2);
        System.Diagnostics.Debug.WriteLine("total spending = " + cat);
        DA.get_categoryName(1);
        
        

        DataSet t1 = new DataSet();
        t1 = DA.get_desc_Transactions(211111110, d1, d2,"bes");
        GridView1.DataSource = t1;
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //calls page_load
        
        return;
    }
}
