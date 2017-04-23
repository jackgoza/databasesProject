<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Budget.aspx.cs" Inherits="Budget" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8"/>
		<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
		<meta name="viewport" content="width=device-width, initial-scale=1"/>
		<title>Budgeteer - Budgets</title>
		<link href="bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
		<link href="css/styles.css" rel="stylesheet"/>
		<link href="css/fonts.css" rel="stylesheet"/>

</head>
<body>
	<form id="form1" runat="server">
	<div id="container" class="container">
				<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
				<!-- header -->
				<div id="header" class="col-xs-12">
					<div class ="textLeft col-xs-4">
						<h4>Budgeteer</h4>
					</div>
					<div class="textCenter col-xs-4">
						<h4>Your Budgets</h4>
					</div>
					<div class="textRight col-xs-4">
						<asp:Button ID="logOutButton"  CssClass="aspButton" style="background-color: #e1e1e1; color: #006847;" runat="server" Text="Logout" />
					</div>
				</div>
				<!-- Page Content -->
				<div id="pageContent" class="row">
					<div id="mobileNavTop" class="col-xs-12">
						<div id="summary_m" class="navBtn"><h3>Summary</h3></div>
						<div id="budget_m" class="navBtn"><h3>Budget</h3></div>
					</div>
					<div id="navigation" class="col-xs-2">
						<div id="summary" class="navBtn"><h3>Summary</h3></div>
						<div id="budget" class="navBtn"><h3>Budget</h3></div>
						<div id="wallet" class="navBtn"><h3>Wallet</h3></div>
						
					</div>
					<div id="content" class="col-xs-10">
						<div id="contentHeader" class="col-xs-12">
							<div class="col-xs-9 textLeft">
								<h4><b>Create and manage your budgets.</b></h4>
							</div>
							<div class="col-xs-3 textRight">
								<!-- Requires additional work. -->
								<!-- Requires validation; user cannot specifiy amount of type double for flat rate. -->
								<div id="addBudgetBtn" class="darkBtn textCenter"><h5>+ Add Budget Item</h5></div>
							</div>
						</div>
						<div id="histogram" class="col-xs-12" style:"display: block; margin: 0 auto;"></div> <br />
						<!-- Currently not functional. Requires code behind. -->
						<!-- OnItemEditing="BudgetList_ItemEditing"  OnItemUpdating="BudgetList_ItemUpdating" OnItemCanceling="BudgetList_ItemCancel" -->
						<asp:ListView ID="BudgetList"  runat="server">
						<LayoutTemplate>
							<div id="itemPlaceholder" runat="server"></div>
							
						</LayoutTemplate>
						<ItemTemplate>
							<!-- Numerical values associated with dollar amounts need to be truncated. -->
							<div class="col-xs-12" runat="server">
								<div class="col-xs-1">
									
								</div>
								<div class="col-xs-3 textLeft">
									<asp:Label ID="CategoryName" CssClass="aspLabel" runat="server" Text='<%#Eval("Name") %>' />
								</div>
								<div class="col-xs-4 textCenter">

								</div>
								<div class="col-xs-4 textRight">
									<p>Spend no more than <asp:Label ID="MaxbudgetAmt" runat="server" Text='<%#Eval("MaxAmt", "{0:c}") %>' />
									by <asp:Label ID="BudgetInterval" CssClass="aspLabel" runat="server" Text='<%#Eval("EndDate", "{0:d}") %>' /></p>
								</div>
							</div>
							<div class="col-xs-12" runat="server">
								<div class="col-xs-12">
									<div class="progress">
										<!-- It might be beneficial to derive this value elsewhere. Data binding cannot work with a CSS property. It may require a PageMethod and Javascript to manipulate the width of the progress bar.-->
										<!-- The value should be truncated. -->
										<div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow='<%#:(getPercent(Convert.ToDouble(Eval("CurrentAmt")) , Convert.ToDouble(Eval("MAxAmt")))).ToString() %>' aria-valuemin="0" aria-valuemax="100" style='<%#:widthString(Convert.ToDouble(Eval("CurrentAmt")) , Convert.ToDouble(Eval("MaxAmt")))%>'>
											You've spent <asp:Label ID="CurrentAmt" runat="server" Text='<%#Eval("CurrentAmt", "{0:c}") %>' />. (<%#:(getPercent(Convert.ToDouble(Eval("CurrentAmt")) , Convert.ToDouble(Eval("MaxAmt")))).ToString() %>% of Limit.)
										</div>
									</div>
								</div>
								
							</div>
						</ItemTemplate>
						<EditItemTemplate >
							<div class="col-xs-12 itemHeader" runat="server" style="color: #000;">
								
							</div>
							<div class="col-xs-12 itemContent" runat="server">
								
							</div>
						</EditItemTemplate>
					</asp:ListView>
					<div class="col-xs-12 textRight">
						<div class="col-xs-10"></div>
						<div id="desktopMode" class="col-xs-2">Desktop Mode</div>
					</div>
					</div>
					<div id="mobileNavBottom" class="col-xs-12">
						
						<div id="wallet_m" class="navBtn"><h3>Wallet</h3></div>
						
					</div>
				</div>
				
		</div>
		<!-- Add Budget Modal -->
		<div id="addBudget" class="modal" onclick="">
			<!-- Modal content -->
			<div class="row modal-content">
				<!-- Info for further validation: https://msdn.microsoft.com/en-us/library/ff650303.aspx -->
				<div class="col-xs-12">
					<div class="col-xs-6 textLeft">
						<h3>Add a Budget Item</h3><br />
					</div>
					<div class="col-xs-6 textRight">
						<span class="close">&times;</span>
					</div>
				</div>
				<div class="col-xs-12">
					<div class="col-xs-6">
						<asp:Label ID="Categroy" runat="server"><p>Choose a category:</p></asp:Label>
					</div>
					<div class="col-xs-6 textRight">
						<asp:DropDownList ID="CategorySelect" runat="server">
						</asp:DropDownList>
					</div>
				</div>
				<div class="col-xs-12">
					<div class="col-xs-6">
						<asp:Label ID="Account" runat="server"><p>Choose an account:</p></asp:Label>
					</div>
					<div class="col-xs-6 textRight">
						<asp:DropDownList ID="AccountList" runat="server">
						</asp:DropDownList>
					</div>
				</div>
				<div class="col-xs-12 hidden">
					<div class="col-xs-6">
						<asp:Label ID="CustCat" runat="server"><p>Name your custom category:</p></asp:Label>
					</div>
					<div class="col-xs-6 textRight">
						<asp:TextBox ID="CustomCategory" runat="server"></asp:TextBox>
					</div>
				</div>
				<div class="col-xs-12">
					<div class="col-xs-6">
						<asp:Label ID="BudgetAmt" runat="server"><p>Amount:</p></asp:Label>
					</div>
					<div class="col-xs-6 textRight">
						<asp:TextBox ID="BudgetAmtField" runat="server"></asp:TextBox>
					</div>
				</div>
				<div class="col-xs-12">
					<div class="col-xs-6">
						<asp:Label ID="StartDateLabel" runat="server"><p>Start Date:</p></asp:Label>
					</div>
					<div class="col-xs-6 textRight">
						<asp:TextBox ID="StartDate" runat="server"></asp:TextBox>
					</div>
				</div>
				<div class="col-xs-12">
					<div class="col-xs-6">
						<asp:Label ID="EndDateLabel" runat="server"><p>End Date:</p></asp:Label>
					</div>
					<div class="col-xs-6 textRight">
						<asp:TextBox ID="EndDate" runat="server"></asp:TextBox>
					</div>
				</div>
				<div class="col-xs-12 textCenter" style="color: #FFF;">
					<!-- Need event handler in code behind. -->
					<asp:Button ID="SubmitButton" CssClass="darkBtn textCenter" CommandArgument="" runat="server" Text="Add Budget"></asp:Button>
				</div>
			</div>
		</div>
	</form>
	<!-- Bootstrap Dependencies -->
	<script src="bootstrap/js/jquery-3.1.1.min.js"></script>
	<script src="bootstrap/js/bootstrap.min.js"></script>
	<!-- Javascript -->
	<script src="plotly/plotly-latest.min.js"></script>
	<script src="js/budget.js"></script>
</body>
</html>
