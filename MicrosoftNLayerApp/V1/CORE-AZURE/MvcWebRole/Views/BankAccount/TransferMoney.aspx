<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels.BankAccountListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    TransferMoney
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="transfer-money-section container-button">
        <% using (Html.BeginForm())
           { %>
        <h1>
            Source Account</h1>
        <%: Html.DropDownList("sourceAccount", Model.Select(x => new SelectListItem(){ Text = x.BankAccountNumber, Value= x.BankAccountNumber })) %>
        <h1>
            Destiny Account</h1>
        <%: Html.DropDownList("destinationAccount", Model.Select(x => new SelectListItem(){ Text = x.BankAccountNumber, Value= x.BankAccountNumber })) %>
        <h1>
            Amount</h1>
        <%: Html.TextBox("amount") %>
        <input type="submit" value="Transfer" class="text-button medium-button" />
        <% } %>
    </div>
    <div class="accounts-section container-button">
        <h1>
            Accounts Information</h1>
        <ul>
            <%: Html.DisplayFor(x => x.BankAccounts) %>
        </ul>
        <% using (Html.BeginForm("LockAccount", "BankAccount"))
           { %>
        <h1>
            Account to block</h1>
        <%: Html.DropDownList("accountNumber", Model.Select(x => new SelectListItem(){ Text = x.BankAccountNumber, Value= x.BankAccountNumber })) %>
        <input type="submit" value="Lock/Unlock" class="text-button big-button" />
        <% } %>
    </div>
    <div class="middle">
    </div>
</asp:Content>
