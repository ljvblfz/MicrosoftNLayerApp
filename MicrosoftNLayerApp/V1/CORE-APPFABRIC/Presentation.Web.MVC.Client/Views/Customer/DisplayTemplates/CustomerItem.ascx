<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.Customer>" %>
<div class="customer-item">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <img src="<%: Url.Action("CustomerPicture",new { customerCode=Model.CustomerCode }) %>"
                alt="<%: Model.ContactName %>" />
            </td>
            <td>
                <h1><%= Html.ActionLink(Model.ContactName, "Details",new RouteValueDictionary(new { customerCode = Model.CustomerCode} )) %></h1>        
                <h2><%: Html.DisplayTextFor(x => x.CompanyName) %></h2>
            </td>
        </tr>
    </table>
</div>
