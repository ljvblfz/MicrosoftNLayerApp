<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.Customer>" %>
<div class="customer-brief">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <img src="<%: Url.Action("CustomerPicture",new { customerCode=Model.CustomerCode }) %>"
                alt="<%: Model.ContactName %>" />
            </td>
            <td>
                <h1><%: Html.DisplayTextFor( x => x.ContactName) %></h1>        
                <h2><%: Html.DisplayTextFor(x => x.CompanyName) %></h2>
            </td>
        </tr>
    </table>
</div>


