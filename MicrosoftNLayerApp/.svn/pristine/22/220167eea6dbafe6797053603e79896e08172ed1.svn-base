<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.BankAccount>" %>
<li><span class="account-item">
    <%: Html.DisplayFor(x => x.BankAccountNumber) %>
</span>
    <br />
    <span class="account-item">
        <%: Html.DisplayFor(x => x.Balance) %>
    </span>
    <br />
    <span class="<%: Model.Locked ? "account-locked" : "account-unlocked" %>">
        <%: Model.Locked ? "Locked" : "Unlocked" %>
    </span></li>
