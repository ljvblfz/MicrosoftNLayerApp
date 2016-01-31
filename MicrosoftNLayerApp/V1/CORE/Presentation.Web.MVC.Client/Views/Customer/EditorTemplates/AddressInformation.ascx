<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.AddressInformation>" %>
<li>
    <h1><%: Html.DisplayNameFor(x => x.City) %></h1>
    <%: Html.EditorFor(x => x.City) %>
    <div class="validation-field">
        <%: Html.ValidationMessageFor(x => x.City) %></div>
</li>
<li>
    <h1><%: Html.DisplayNameFor(x => x.PostalCode) %></h1>
    <%: Html.EditorFor(x => x.PostalCode) %>
    <div class="validation-field">
        <%: Html.ValidationMessageFor(x => x.PostalCode) %></div>
</li>
<li>
    <h1><%: Html.DisplayNameFor(x => x.Address) %></h1>
    <%: Html.EditorFor(x => x.Address) %>
    <div class="validation-field">
        <%: Html.ValidationMessageFor(x => x.Address) %></div>
</li>
<li>
    <h1><%: Html.DisplayNameFor(x => x.Telephone) %></h1>
    <%: Html.EditorFor(x => x.Telephone) %>
    <div class="validation-field">
        <%: Html.ValidationMessageFor(x => x.Telephone) %></div>
</li>
<li>
    <h1><%: Html.DisplayNameFor(x => x.Fax) %></h1>
    <%: Html.EditorFor(x => x.Fax) %>
    <div class="validation-field">
        <%: Html.ValidationMessageFor(x => x.Fax) %></div>
</li>
