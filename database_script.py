from xlrd import open_workbook

wb = open_workbook('umkcprojectdata.xlsx')
values = []
for s in wb.sheets():
    #print 'Sheet:',s.name
    for row in range(1, s.nrows):
        col_names = s.row(0)
        col_value = []
        for name, col in zip(col_names, range(s.ncols)):
            value  = (s.cell(row,col).value)
            try : value = str(int(value))
            except : pass
            col_value.append((name.value, value))
        values.append(col_value)


file = open("query.txt","w")
"""
def get_account_type(account_type):
    if account_type == "Checking":
        return 1
    if account_type == "Savings":
        return 2
    if account_type == "Credit Card":
        return 3
"""
def is_null(string):
    if string == "":
        return "NULL"
    else:
        return string
"""
def get_transaction_type_id(transaction_type):
    if transaction_type == "DR":
        return 2
    elif transaction_type == "CR":
        return 1
"""

for item in values:
    print("INSERT INTO Accounts" + " (account_type, account_number, trans_date, trans_type, amount, description, category_ID),")
    print("VALUES(",
        item[0][1], ",", #Account type
        item[1][1], ",",#Account #
        item[2][1],",",#Processing Date
        item[3][1],",",#transaction type
        item[4][1], ",",#amount
        item[5][1], ",",#description
        item[6][1], ")") #Category ID
    print("")
"""
    file.write("INSERT INTO Accounts" + " (account_type, account_number, trans_date, trans_type, amount, description, category_ID),")
    string = "VALUES("
    + str(item[0][1])#Account type
    + str(item[1][1]) #Account #
    + str(item[2][1])#Processing Date
    + str(item[3][1])#transaction type
    + str(item[4][1]) #amount
    + str(item[5][1]) #description
    + str(item[6][1]) + ")" #Category ID

    file.write(string)
    file.write("")
"""

#file.close()

