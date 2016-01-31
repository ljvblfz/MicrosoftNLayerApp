<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels.IPagedView>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Client List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page-frame">
        <%= Html.DisplayFor(x => x.PageItems) %>
    </div>
    <div id="pager" class="container-button">
    <% Html.RenderPartial("Pager",Tuple.Create("Index","Customer",Model)); %>
    </div>
</asp:Content>
