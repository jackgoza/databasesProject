

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace Tester
{
    public class DataAbstract
    {
        private const int DATA_ERROR_VALUE = -1;
        private string connectionString;
        private SqlConnection connect;

        public DataAbstract()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["authenticateEntities"].ConnectionString;
            connect = new SqlConnection(connectionString);
        } // end default constructor

        

        // Supply the first name, last name, username, and password
        // username must be unique, no duplicates are allowed
        // Returns number of rows added, or returns error value if username already exists
        // Throws exception if parameters are empty or null
        public int addUser(string firstName, string lastName, string username, string password, string email)
        {
            // checks values for valid data
            if (firstName == "" || firstName == null)
            {
                throw new System.ArgumentException("Parameter cannot be empty", "First Name");
            } // end if

            if (lastName == "" || lastName == null)
            {
                throw new System.ArgumentException("Parameter cannot be empty", "Last Name");
            } // end if

            if (username == "" || username == null)
            {
                throw new System.ArgumentException("Parameter cannot be empty", "Username");
            } // end if

            if (password == "" || password == null)
            {
                throw new System.ArgumentException("Parameter cannot be empty", "Password");
            } // end if

            // calls data input function
            return addUserChecked(firstName, lastName, username, password, email);
        } // end addUser

        // Supply the username and password
        // returns the user id or an error value
        // throws excpetions if paramters are empty or null
        public int login(string username, string password)
        {
            // checks values for valid data
            if (username == "" || username == null)
            {
                throw new System.ArgumentException("Parameter cannot be empty", "Username");
            } // end if

            if (password == "" || password == null)
            {
                throw new System.ArgumentException("Parameter cannot be empty", "Password");
            } // end if

            return loginChecked(username, password);
        } // end login

        // Supply the name of the category
        // The name must be unique, no duplicates allowed
        // returns true if successful
        public Boolean addCategory(string name)
        {
            string insertSQL = "INSERT INTO Categories (Name) VALUES ('" + name + "')";

            SqlCommand cmd = new SqlCommand(insertSQL, connect);

            connect.Open();
            int num = cmd.ExecuteNonQuery();
            connect.Close();

            if (num == 0)
            {
                return false;
            } // end if

            return true;
        } // end addCategory

        
        
        //add budget for a user acct number
        //takes acct#, categoryID, startDate, endDate, maxAmt, fav
        public void addBudget(long acctNumber, int categoryID, DateTime stDate, DateTime endDate, double maxAmt, int fav)
        {
            string insertSQL = "INSERT INTO Budgets (AcctNumber, CategoryID,  StartDate, EndDate, MaxAmt, Favorite, Completed, Failed) VALUES (";
            insertSQL += "@acctNumber, @categoryID, @stDate, @endDate, @maxAmt, @fav," + 0 + ", " + 0 + ")";


            SqlCommand cmd = new SqlCommand(insertSQL, connect);
            cmd.Parameters.AddWithValue("@acctNumber", acctNumber);
            cmd.Parameters.AddWithValue("@categoryID", categoryID);
            cmd.Parameters.AddWithValue("@stDate", stDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@maxAmt", maxAmt);
            cmd.Parameters.AddWithValue("@fav", fav);

            
            connect.Open();
            int num = cmd.ExecuteNonQuery();
            connect.Close();
            return;
        }

        // Supply the user ID
        // returns a DataSet of the user
        public DataSet returnUser(int userNumber)
        {
            string selectSQL = "SELECT FirstName, LastName, Email, GoalsNotifications, BudgetsNotifications FROM Users WHERE UserID = @userNum";

            SqlCommand cmd = new SqlCommand(selectSQL, connect);
            cmd.Parameters.AddWithValue("@userNum", userNumber);

            DataSet temp = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            connect.Open();

            adapter.Fill(temp);

            connect.Close();

            return temp;
        } // end returnUser

  

        //supply account number
        //returns DataSet of all the Budgets associated with the account number
        public DataSet returnBudgets(long acctNumber)
        {
            string selectSQL = "SELECT * FROM Budgets JOIN Categories ON Budgets.CategoryID = Categories.CategoryID WHERE AcctNumber = " + acctNumber + " ORDER BY BudgetID DESC";

            SqlCommand cmd = new SqlCommand(selectSQL, connect);

            DataSet temp = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            connect.Open();

            adapter.Fill(temp);

            connect.Close();

            return temp;
        } // end returnBudgets

        public DataSet get_Categories(int userID)
        {
            DataSet temp = new DataSet();
            string selectSQL = "SELECT * FROM Categories WHERE (UserID = " + userID + " OR UserID IS NULL)";

            SqlCommand cmd = new SqlCommand(selectSQL, connect);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            connect.Open();

            adapter.Fill(temp);

            connect.Close();
            return temp;
        }

        //provide the type of favorites and account number
        //returns Dataset of favorites for either goals or budgets
        public DataSet get_Favorites(string fav_type, long acctNumber)
        {
            DataSet temp = new DataSet();

            if (fav_type == "Budget") {
                string selectSQL = "SELECT * FROM Budgets JOIN Categories ON Budgets.CategoryID = Categories.CategoryID WHERE AcctNumber = " + acctNumber + " AND Favorite = " + 1;

                SqlCommand cmd = new SqlCommand(selectSQL, connect);

                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                connect.Open();

                adapter.Fill(temp);

                connect.Close();
            }
            if (fav_type == "Goal")
            {
                string selectSQL = "SELECT * FROM Goals WHERE AcctNumber = " + acctNumber + " AND Favorite = " + 1;

                SqlCommand cmd = new SqlCommand(selectSQL, connect);

                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                connect.Open();

                adapter.Fill(temp);

                connect.Close();
            }
            return temp; 
        }//end get_Favorites

        // Inserts data from parameters into sql statements to add a user to the database
        // fistName, lastName, username, and password are required to input a user
        // returns the number of rows affected, or returns an error code if the username is taken
        private int addUserChecked(string firstName, string lastName, string username, string password, string email)
        {
            // builds the select sql string
            string selectSQL = "SELECT * FROM Users WHERE Username = @Username";

            SqlCommand selectCmd = new SqlCommand(selectSQL, connect);
            SqlDataReader reader;

            // adds the passed in username to the sql string
            selectCmd.Parameters.AddWithValue("@Username", username);

            connect.Open();
            reader = selectCmd.ExecuteReader();

            // if the reader returns rows return error value
            if (reader.HasRows)
            {
                connect.Close();
                return DATA_ERROR_VALUE;
            } // end if

            connect.Close();

            // builds the insert sql string
            string insertSQL = "INSERT INTO Users (FirstName, LastName, Username, Password, Email) VALUES (";
            insertSQL += "@FirstName, @LastName, @Username, @Password, @Email)";

            SqlCommand cmd = new SqlCommand(insertSQL, connect);

            // adds the passed in values to the sql string
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Email", email);

            // holds the number of rows added
            int added = 0;

            // attempts to add the row to the database
            connect.Open();
            added = cmd.ExecuteNonQuery();
            connect.Close();

            return added;
        } // end addUserChecked

        // Queries the database to see if username and password match
        // if username does not exist, or password is wrong returns error value
        private int loginChecked(string username, string password)
        {
            // builds the select sql string
            string selectSQL = "SELECT UserID, Password FROM Users WHERE Username = @Username";

            SqlCommand cmd = new SqlCommand(selectSQL, connect);
            SqlDataReader reader;

            // adds the passed in username to the sql string
            cmd.Parameters.AddWithValue("@Username", username);

            connect.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                if (reader["Password"].ToString() == password)
                {
                    int id = (int)reader["UserID"];
                    reader.Close();
                    connect.Close();
                    return id;
                } // end if
            } // end if      

            reader.Close();
            connect.Close();
            return DATA_ERROR_VALUE;
        } // end loginChecked

        // Supply the userID
        // returns a DataSet of all the Accounts associated with the userID
        public DataSet get_Accounts(int userID)
        {
            //selects accounts for a specific suer id
            string selectSQL = "SELECT * FROM Accounts WHERE UserID = " + userID;

            SqlCommand cmd = new SqlCommand(selectSQL, connect);

            DataSet temp = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            connect.Open();

            adapter.Fill(temp);

            connect.Close();

            return temp;
        } // end get_Accounts

        //need checkID function -- check to see if the id exists


        //creates a new account for a user
        public void create_Account(int userID, string acct_type, long acct_num, double balance)
        {
            // builds the insert sql string
            string insertSQL = "INSERT INTO Accounts (AcctNumber, UserID, Balance, AccountType) VALUES (";
            insertSQL += "@AcctNumber, @UserID, @Balance, @AccountType)";

            SqlCommand cmd = new SqlCommand(insertSQL, connect);

            // adds the passed in values to the sql string
            cmd.Parameters.AddWithValue("@AcctNumber", acct_num);
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@Balance", balance);
            cmd.Parameters.AddWithValue("@AccountType", acct_type);

            connect.Open();
            cmd.ExecuteNonQuery();
            connect.Close();
            return;
        } //end create_Account

        //get_Transactions returns a data set of the transactions for a given account in chronological order
        public DataSet get_Transactions(long acct_num)
        {
            //selects accounts for a specific suer id

            string selectSQL = "SELECT TransactionID, Description, Deposit, Name, TransDate, TransType FROM Transactions JOIN Categories ON Transactions.CategoryID = Categories.CategoryID WHERE AcctNumber = " + acct_num;
            SqlCommand cmd = new SqlCommand(selectSQL, connect);

            DataSet temp = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            connect.Open();

            adapter.Fill(temp);

            connect.Close();

            return temp;

        } //end get_Transactions 
        //from given dates and category (if category isn't needed, use 0)
        public DataSet get_Bound_Transactions(long acct_num, DateTime stDate, DateTime endDate, int category, int sort)
        {
            DataSet temp = new DataSet();
            string ordering = "ASC";
            if (sort < 0) ordering = "DESC";
            if (category != 0)
            {
                string selectSQL = "SELECT * FROM Transactions WHERE AcctNumber = " + acct_num + " AND TransDate >= @stDate AND TransDate <= @endDate ";//ordered by date
                selectSQL += "AND CategoryID = @category ORDER BY TransDate " + ordering;

                SqlCommand cmd = new SqlCommand(selectSQL, connect);

                cmd.Parameters.AddWithValue("@stDate", stDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@category", category);

                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                connect.Open();

                adapter.Fill(temp);

                connect.Close();
            }
            else
            {
                string selectSQL = "SELECT * FROM Transactions WHERE AcctNumber = " + acct_num + " AND TransDate >= @stDate AND TransDate <= @endDate"; //ordered by date
                selectSQL += " ORDER BY TransDate " +ordering;

                SqlCommand cmd = new SqlCommand(selectSQL, connect);

                cmd.Parameters.AddWithValue("@stDate", stDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                

                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                connect.Open();

                adapter.Fill(temp);

                connect.Close();
            }
            
            return temp;
        } //end get_Transactions

        //gets transactions with a specific description in the bounded dates
        //returns Dataset of transactions with that description
        public DataSet get_desc_Transactions(long acct_num, DateTime stDate, DateTime endDate, string desc)
        {
            DataSet temp = new DataSet();
            string selectSQL = "SELECT * FROM Transactions WHERE AcctNumber = " + acct_num + " AND TransDate >= @stDate AND TransDate <= @endDate ";//ordered by date
            selectSQL += "AND Description = @description ORDER BY TransDate";

            SqlCommand cmd = new SqlCommand(selectSQL, connect);

            cmd.Parameters.AddWithValue("@stDate", stDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@description", desc);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            connect.Open();

            adapter.Fill(temp);

            connect.Close();
            return temp;
        } //end get_desc_Transactions



        //executes a transaction -- will eventually return -1 for failure or > 0 for success
        public void do_Transaction(long acct_num, string trans_type, double amount, string description, int category_id)
        {
            string insertSQL = "INSERT INTO Transactions (AcctNumber, CategoryID, Deposit, Description, TransDate, TransType) VALUES (";
            insertSQL += "@AcctNumber, @CategoryID, @Deposit, @Description, @TransDate, @TransType)";

            SqlCommand cmd = new SqlCommand(insertSQL, connect);

            // adds the passed in values to the sql string
            cmd.Parameters.AddWithValue("@AcctNumber", acct_num);
            cmd.Parameters.AddWithValue("@CategoryID", category_id);
            cmd.Parameters.AddWithValue("@Deposit", amount);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@TransDate", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@TransType", trans_type);


            connect.Open();
            cmd.ExecuteNonQuery();
            connect.Close();
            return;

        } //end do_Transaction

        

        public void setFavor(int goalID, bool fav)
        {
            string updateSQL = "UPDATE Goals SET Favorite = @favor WHERE GoalID = " + goalID;
            SqlCommand Cmd = new SqlCommand(updateSQL, connect);
            Cmd.Parameters.AddWithValue("@favor", fav);
            connect.Open();
            Cmd.ExecuteNonQuery();
            connect.Close();
        } //end mod goal name

        //delete goal
        

        //return total of a category's spending from transactions   ---for use in pie chart or elsewhere
        public double get_Category_Total_Spending(int category, long acctNumber)
        {
            string selectSQL = "SELECT SUM(Deposit) as Total FROM Transactions WHERE CategoryID = @CategoryID AND AcctNumber = @AcctNumber";
            SqlCommand cmd = new SqlCommand(selectSQL, connect);
            cmd.Parameters.AddWithValue("@CategoryID", category);
            cmd.Parameters.AddWithValue("@AcctNumber", acctNumber);
            double total = 0.0;

            SqlDataReader rdr;
            connect.Open();
            rdr = cmd.ExecuteReader();
            rdr.Read();


            total = Double.Parse(rdr["Total"].ToString(), System.Globalization.NumberStyles.Currency);

            connect.Close();


            return total;
        } // end get category total spending

        //return total of a category's spending based on a given time frame
        public double get_Category_Bound_Spending(int category, long acctNumber, DateTime stDate, DateTime endDate)
        {
            string selectSQL = "SELECT SUM(Deposit) as Total FROM Transactions WHERE CategoryID = @CategoryID AND AcctNumber = @AcctNumber";
            selectSQL += " AND TransDate >= @stDate AND TransDate <= @endDate AND TransType = 'DR'";
            SqlCommand cmd = new SqlCommand(selectSQL, connect);
            cmd.Parameters.AddWithValue("@CategoryID", category);
            cmd.Parameters.AddWithValue("@AcctNumber", acctNumber);
            cmd.Parameters.AddWithValue("@stDate", stDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            double total = 0.0;

            SqlDataReader rdr;
            connect.Open();
            rdr = cmd.ExecuteReader();
            rdr.Read();

            if (rdr["Total"].ToString() != "")
            {
                total = Double.Parse(rdr["Total"].ToString()); //, System.Globalization.NumberStyles.Currency);
            }
            connect.Close();


            return total;
        } // end get category total spending

        //creates a new category when supplied with a string name
        //returns -1 if it already exists, 1 if successful
        public int create_Category(string newcat)
        {
            string selectSQL = "SELECT * FROM Categories WHERE Name = @newcat";

            SqlCommand selectCmd = new SqlCommand(selectSQL, connect);
            SqlDataReader reader;

            // adds the passed in username to the sql string
            selectCmd.Parameters.AddWithValue("@newcat", newcat);

            connect.Open();
            reader = selectCmd.ExecuteReader();

            // if the reader returns rows return error value
            if (reader.HasRows)
            {
                connect.Close();
                return DATA_ERROR_VALUE;
            } // end if

            connect.Close();

            string insertSQL = "INSERT INTO Categories (Name) Values (@newcat)";
            SqlCommand cmd = new SqlCommand(insertSQL, connect);
            cmd.Parameters.AddWithValue("@newcat", newcat);

            connect.Open();
            cmd.ExecuteNonQuery();
            connect.Close();

            return 1;

            
        }//end create_Category

        //given the name of a category and a user id
        //return the category number -- we include user ID for user defined categories
        public int get_categoryID(string cat_name, int userID)
        {
            int new_cat = 0;
            string selectSQL = "SELECT CategoryID FROM Categories WHERE Name = @cat_name AND (UserID = @userID OR UserID IS NULL)";
            SqlCommand cmd = new SqlCommand(selectSQL, connect);
            cmd.Parameters.AddWithValue("@cat_name", cat_name);
            cmd.Parameters.AddWithValue("@userID", userID);

            connect.Open();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Read();
            new_cat = (int)reader["CategoryID"];
            connect.Close();

            return new_cat;
        }

        //provide the category id, get the name
        public string get_categoryName(int catID)
        {
            string name;
            string selectSQL = "SELECT Name FROM Categories WHERE CategoryID = " + catID;
            SqlCommand cmd = new SqlCommand(selectSQL, connect);
            connect.Open();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Read();
            name = reader["Name"].ToString();
            connect.Close();

            return name;
        }

        //user selects a transaction and a new category to assign it to
        public void recategorize_Transaction(int transid, int new_category, long acctNumber)
        {
            string updateSQL = "UPDATE Transactions SET CategoryID = @new_category WHERE TransactionID = @transid AND AcctNumber = " + acctNumber; //hardcode on transaction id depends on if user inputs the id or selects it through abstraction
            SqlCommand cmd = new SqlCommand(updateSQL, connect);
            cmd.Parameters.AddWithValue("@new_category", new_category);
            cmd.Parameters.AddWithValue("@transid", transid);

            connect.Open();
            cmd.ExecuteNonQuery();
            connect.Close();


            return;
        }//end recategorize_Transaction

        
    } // end class
} // end namespace