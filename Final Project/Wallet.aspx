<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Wallet.aspx.cs" Inherits="Wallet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8"/>
		<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
		<meta name="viewport" content="width=device-width, initial-scale=1"/>
		<title>Budgeteer - Wallet</title>
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
						<h4>Your Wallet</h4>
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
						<div id="wallet_m" class="navBtn"><h3>Wallet</h3></div>
					</div>
					<div id="navigation" class="col-xs-2">
						<div id="summary" class="navBtn"><h3>Summary</h3></div>
						<div id="budget" class="navBtn"><h3>Budget</h3></div>
						<div id="wallet" class="navBtn"><h3>Wallet</h3></div>
						
					</div>
					<div id="content" class="col-xs-10">
						<div id="contentHeader" class="col-xs-12">
							<div class="col-xs-8 textLeft">
								<h4><b>View transactions relevant to your budgets.</b></h4>
							</div>
							<div class="col-xs-4 textRight">
								<asp:TextBox ID="Search" runat="server" Text="Search"></asp:TextBox>
								<asp:Button ID="SearchBtn" CssClass="darkBtn textCenter" runat="server" Text="Search" OnClick="searchTransactions" />
							</div>
						</div>
						<div class="col-xs-12">
							<!--Need to be able to click these for filtering.-->
							<div class="col-xs-3">Description</div>
							<div class="col-xs-2">Amount</div>
							<div class="col-xs-2">Category</div>
							<div class="col-xs-3">Date</div>
							<div class="col-xs-2">Type</div>
						</div>
					<asp:ListView ID="TransactionsList"  runat="server">
						<LayoutTemplate>
							<div style="overflow:scroll; height: 400px;">
								<div id="itemPlaceholder" runat="server"></div>
								
							</div>
						</LayoutTemplate>
						<AlternatingItemTemplate>
							<div class="col-xs-12" style="background-color: #e1e1e1;">
								<div class="col-xs-3">
									<asp:Label ID="Desc" CssClass="aspLabel" runat="server" Text='<%#Eval("Description") %>' />
								</div>
								<div class="col-xs-2">
									<asp:Label ID="Amount" CssClass="aspLabel" runat="server" Text='<%#Eval("Deposit", "{0:c}") %>' />
								</div>
								<div class="col-xs-2">
									<asp:Label ID="Category" CssClass="aspLabel" runat="server" Text='<%#Eval("Name") %>' />
								</div>
								<div class="col-xs-3">
									<asp:Label ID="Date" CssClass="aspLabel" runat="server" Text='<%#Eval("TransDate", "{0:d}") %>' />
								</div>
								<div class="col-xs-2">
									<asp:Label ID="Type" CssClass="aspLabel" runat="server" Text='<%#Eval("TransType") %>' />
								</div>
							</div>
						</AlternatingItemTemplate>
						<ItemTemplate>
							<div class="col-xs-12" style="background-color: #FFF;">
								<div class="col-xs-3">
									<asp:Label ID="Desc" CssClass="aspLabel" runat="server" Text='<%#Eval("Description") %>' />
								</div>
								<div class="col-xs-2">
									<asp:Label ID="Amount" CssClass="aspLabel" runat="server" Text='<%#Eval("Deposit", "{0:c}") %>' />
								</div>
								<div class="col-xs-2">
									<asp:Label ID="Category" CssClass="aspLabel" runat="server" Text='<%#Eval("Name") %>' />
								</div>
								<div class="col-xs-3">
									<asp:Label ID="Date" CssClass="aspLabel" runat="server" Text='<%#Eval("TransDate", "{0:d}") %>' />
								</div>
								<div class="col-xs-2">
									<asp:Label ID="Type" CssClass="aspLabel" runat="server" Text='<%#Eval("TransType") %>' />
								</div>
							</div>
						</ItemTemplate>
						<EditItemTemplate>
							<div class="col-xs-12">
								<div class="col-xs-3">
									<asp:Label ID="Desc" CssClass="aspLabel" runat="server" Text='<%#Eval("Description") %>' />
								</div>
								<div class="col-xs-2">
									<asp:Label ID="Amount" CssClass="aspLabel" runat="server" Text='<%#Eval("Deposit", "{0:c}") %>' />
								</div>
								<div class="col-xs-2">
									<asp:DropDownList ID="ChooseCategory" runat="server">
									</asp:DropDownList>
								</div>
								<div class="col-xs-3">
									<asp:Label ID="Date" CssClass="aspLabel" runat="server" Text='<%#Eval("TransDate", "{0:d}") %>' />
								</div>
								<div class="col-xs-2">
									<asp:Label ID="Type" CssClass="aspLabel" runat="server" Text='<%#Eval("TransType") %>' />
								</div>
							</div>
						</EditItemTemplate>
					</asp:ListView>
					<div class="col-xs-12 textRight">
						<div class="col-xs-10"></div>
						<div id="desktopMode" class="col-xs-2">Desktop Mode</div>
					</div>
					</div>
					<div id="mobileNavBottom" class="col-xs-12">
						
					</div>
				</div>
				
		</div>       
	</form>
	<!-- Bootstrap Dependencies -->
	<script src="bootstrap/js/jquery-3.1.1.min.js"></script>
	<script src="bootstrap/js/bootstrap.min.js"></script>
	<!-- Javascript -->
	<script src="js/wallet.js"></script>
</body>
</html>
