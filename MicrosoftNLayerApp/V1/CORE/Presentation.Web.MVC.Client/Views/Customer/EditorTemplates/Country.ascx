<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.Country>" %>
<li>
    <h1><%: Html.DisplayNameFor(x => x.CountryName) %></h1>
    <%: Html.EditorFor(x => x.CountryName) %>
<div class="validation-field">
    <%: Html.ValidationMessageFor(x => x.CountryName) %></div>
</li>