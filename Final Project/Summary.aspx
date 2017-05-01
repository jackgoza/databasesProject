<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Summary.aspx.cs" Inherits="Summary" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<meta charset="utf-8"/>
		<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
		<meta name="viewport" content="width=device-width, initial-scale=1"/>
		<title>Budgeteer - Summary</title>
		<link href="bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
		<link href="css/styles.css" rel="stylesheet"/>
		<link href="css/fonts.css" rel="stylesheet"/>
		<script src="js/summary.js"></script>
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
						<h4>Financial Summary</h4>
					</div>
					<div class="textRight col-xs-4">
						<asp:Button ID="logOutButton" onClick="logoutClick" CssClass="aspButton" style="background-color: #e1e1e1; color: #006847;" runat="server" Text="Logout" />
					</div>
				</div>
				<!-- Page Content -->
				<div id="pageContent" class="row">
					<div id="mobileNavTop" class="col-xs-12">
						<div id="summary_m" class="navBtn"><h3>Summary</h3></div>
					</div>
					<div id="navigation" class="col-xs-2">
						<div id="summary" class="navBtn"><h3>Summary</h3></div>
						<div id="budget" class="navBtn"><h3>Budget</h3></div>
						
						<div id="wallet" class="navBtn"><h3>Wallet</h3></div>
						
					</div>
					<div id="content" class="col-xs-10">
						<div id="contentHeader" class="col-xs-12">
							<div class="col-xs-12 textLeft">
								<h4><b>View your spending and important budgets.</b></h4>
							</div>
						</div>
						<h3 class="col-xs-12 textCenter"> Monthly Spending </h3>
						<canvas id="graphcanvas" height ="256" width ="256" style="display: block; margin: 0 auto;"></canvas>
						<br/>
								
					
						<div class="col-xs-12" style="margin-bottom: 1em;">
						<div class="col-xs-12">
							<h3>Favorite Budgets</h3>
						</div>
						<div class="col-xs-6" style="padding-top: 1em;">
							<h5>Manage your budgets on the Budget page.</h5>
						</div>
						<div class="col-xs-6" style="padding-top: 1em;">
							<div id="gotoBudgetBtn" class="darkBtn textCenter"><h5>Go to Budget</h5></div>
						</div>
					</div>
					<asp:ListView ID="FaveBudgetsList" runat="server">
						<LayoutTemplate>
							<div id="itemPlaceholder" runat="server"></div>
						</LayoutTemplate>
						<ItemTemplate>
							<!-- Numerical values associated with dollar amounts need to be truncated. -->
							<div class="col-xs-12 itemHeader" runat="server">
								<div class="col-xs-4 textLeft">
									<asp:Label ID="CategoryName" CssClass="aspLabel" runat="server" Text='<%#Eval("Name") %>' />
								</div>
								<div class="col-xs-4 textCenter">
								</div>
								<div class="col-xs-4 textRight">
									<p>Spend no more than <asp:Label ID="MaxbudgetAmt" runat="server" Text='<%#Eval("MaxAmt", "{0:c}") %>' />
									by <asp:Label ID="BudgetInterval" CssClass="aspLabel" runat="server" Text='<%#Eval("EndDate", "{0:MMM dd, yyyy}") %>' /></p>
								</div>
							</div>
							<div class="col-xs-12 itemContent" runat="server">
								<div class="col-xs-12">
									<div class="progress">
										<!-- It might be beneficial to derive this value elsewhere. Data binding cannot work with a CSS property. It may require a PageMethod and Javascript to manipulate the width of the progress bar.-->
										<!-- The value should be truncated. -->
										<div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow='<%#:getPercent(Convert.ToDouble(Eval("CurrentAmt")),Convert.ToDouble(Eval("MaxAmt"))).ToString() %>' aria-valuemin="0" aria-valuemax="100" style='<%#:widthString(Convert.ToDouble(Eval("CurrentAmt")) , Convert.ToDouble(Eval("MaxAmt")))%>'>
										</div>
									</div>
										You've spent <asp:Label ID="CurrentAmt" runat="server" Text='<%#Eval("CurrentAmt", "{0:c}") %>' />. (<%#:getPercent(Convert.ToDouble(Eval("CurrentAmt")),Convert.ToDouble(Eval("MaxAmt"))).ToString() %>% of Limit.)
								</div>
							</div>
						</ItemTemplate>
					</asp:ListView>
							</div>
						
						
					   
					<div class="col-xs-12 textRight">
						<div class="col-xs-10"></div>
						<div id="desktopMode" class="col-xs-2">Desktop Mode</div>
					</div>
					
					<div id="mobileNavBottom" class="col-xs-12">
						<div id="budget_m" class="navBtn"><h3>Budget</h3></div>
						
						<div id="wallet_m" class="navBtn"><h3>Wallet</h3></div>
						
					</div>
				</div>
				</div>

		
			<asp:HiddenField ID="hfAcctNum" runat="server" Value="" ClientIDMode="Static" />
		</form>
		
		
		
		<!-- Bootstrap Dependencies -->
		<script src="bootstrap/js/jquery-3.1.1.min.js"></script>
		<script src="bootstrap/js/bootstrap.min.js"></script>
		<!-- Javascript -->
		<script src="js/summary.js"></script>
		<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/1.0.2/Chart.min.js"></script>
	</body>
</html>
