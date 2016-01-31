<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.BankTransfer>" %>
    <h1>
    <%: Html.DisplayFor(x => x.TransferDate) %></h1>
    <h2>
    <%: Html.DisplayFor(x => x.Amount) %></h2>
    <h2>
    <%: Html.DisplayFor(x => x.BankAccount.BankAccountNumber) %></h2>
