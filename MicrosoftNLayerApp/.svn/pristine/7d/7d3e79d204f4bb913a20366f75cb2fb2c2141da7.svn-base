<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels.MenuStateViewModel>" %>
<ul class="CustomerMenu">
    <li><span class="Title">Customer</span></li>
    <li>
        <ul class="options">
            <li class="CustomerList">
                <%: Html.ActionLink("List","Index",(string)"Customer",new { page = 0, pageSize = 5},null) %></li>
            <li class="CustomerAddNew">
                <%: Html.ActionLink("Add new","Create",(string)"Customer") %></li>
        </ul>
    </li>
</ul>
<div class="separator"></div>
<ul class="BankingMenu">
    <li><span class="Title">Banking</span></li>
    <li>
        <ul class="options">
            <li class="BankingTransferList">
                <%: Html.ActionLink("Transfer List", "Index", (string)"BankAccount", new { page = 0, pageSize = 5 }, null)%></li>
            <li class="BankingPerformTransfer">
                <%: Html.ActionLink("Perform Transfer","TransferMoney",(string)"BankAccount") %></li>
        </ul>
    </li>
</ul>
<div class="separator"></div>
<ul class="OrdersMenu">
    <li><span class="Title">Orders</span></li>
    <li>
        <ul class="options">
            <li class="OrdersList">List</li>
            <li class="OrdersPerformOrder">Perform Order</li>
        </ul>
    </li>
</ul>
