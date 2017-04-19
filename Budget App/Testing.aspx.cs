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

    //page for testing data retrieval functions in abstract layer-- binds data to grid view
    //queries can be found in App_Code/DataAbstract.cs
    //go into web.config and change the connection string to reference your local db
    
    protected void Page_Load(object sender, EventArgs e)
    {
        DataAbstract DA = new DataAbstract();
        DateTime d1 = new DateTime(2016,11,14);
        DateTime d2 = new DateTime(2017, 2, 15);
        
        
        //DA.create_Category("Travel");
        //DA.recategorize_Transaction(320, 9, 211111110);
        //DA.addBudget(211111110, 1, d1, d2, 300.00, 1);
        string cat = DA.get_categoryName(1);
        double t__ = DA.get_Category_Bound_Spending(2,211111110, d1, d2);
        System.Diagnostics.Debug.WriteLine("total spending = " + t__);
        DA.get_categoryName(1);
        
        
        //displaying data from dataset
        DataSet t1 = new DataSet();
        t1 = DA.get_desc_Transactions(211111110, d1, d2,"Starbucks");
        GridView1.DataSource = t1;
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //refresh page
        
        return;
    }
}
