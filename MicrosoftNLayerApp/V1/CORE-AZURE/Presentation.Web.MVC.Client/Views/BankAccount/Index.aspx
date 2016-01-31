<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels.IPagedView>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Transferencias
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("TransferSearch"); %>
    <%: Html.DisplayFor(x => x.PageItems) %>

</asp:Content>
