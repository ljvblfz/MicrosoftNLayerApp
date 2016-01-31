<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Client Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubHeaderContent" runat="server">
    <%: Html.DisplayForModel("CustomerBrief") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model != null)
       { %>
    <%: Html.DisplayForModel() %>
    <% }
       else
       { %>
    No existe el cliente solicitado.
    <% } %>
    <div class="container-button">
        <ul>
            <li><a href="<%= Request.UrlReferrer %>">
                <img src="<%: Url.Content("~/Content/Images/exit.png") %>" alt="Back" /></a></li>
            <li><a href="<%: Url.Action("Edit", (object) new { customerCode = Model.CustomerCode }) %>">
                <img src="<%: Url.Content("~/Content/Images/edit.png") %>" alt="Edit" /></a>
            </li>
            <li><a href="#" onclick="document.Delete.submit();">
                <img src="<%: Url.Content("~/Content/Images/delete.png") %>" alt="Delete" /></a>
                <% using (Html.BeginForm("Delete", "Customer", new { customerCode = Model.CustomerCode }, FormMethod.Post, new { name = "Delete" }))
                   { %>
                <%: Html.HttpMethodOverride(HttpVerbs.Delete) %>
            </li>
        </ul>
    </div>
    <% } %>
</asp:Content>
