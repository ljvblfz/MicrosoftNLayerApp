<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.Customer>" %>
<ul>
    <li>
        <h1><%: Html.DisplayNameFor(x => x.CustomerCode) %></h1>
        <%: Html.EditorFor(x => x.CustomerCode) %>
        <div class="validation-field">
            <%: Html.ValidationMessageFor(x => x.CustomerCode) %></div>
    </li>
    <li>
        <h1><%: Html.DisplayNameFor(x => x.ContactName) %></h1>
        <%: Html.EditorFor(x => x.ContactName) %>
        <div class="validation-field">
            <%: Html.ValidationMessageFor(x => x.ContactName) %></div>
    </li>
    <li>
        <h1><%: Html.DisplayNameFor(x => x.ContactTitle) %></h1>
        <%: Html.EditorFor(x => x.ContactTitle) %>
        <div class="validation-field">
            <%: Html.ValidationMessageFor(x => x.ContactTitle) %></div>
    </li>
    <li>
        <h1><%: Html.DisplayNameFor(x => x.CompanyName) %></h1>
        <%: Html.EditorFor(x => x.CompanyName) %>
        <div class="validation-field">
            <%: Html.ValidationMessageFor(x => x.CompanyName) %></div>
    </li>
    <li>
        <h1><%: Html.DisplayNameFor(x => x.IsEnabled) %></h1>
        <%: Html.EditorFor(x => x.IsEnabled)%>
        <div class="validation-field">
            <%: Html.ValidationMessageFor(x => x.IsEnabled) %></div>
    </li>
        <%: Html.EditorFor(x => x.Country) %>
        <%: Html.EditorFor(x => x.Address) %>
        <%: Html.EditorFor(x => x.CustomerPicture) %>
</ul>
