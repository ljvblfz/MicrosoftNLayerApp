<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    New Client
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm("Create","Customer",FormMethod.Post,new { enctype = "multipart/form-data", @class="editor-form"}))
       { %>
        <%: Html.EditorForModel() %>
        <input type="submit" value="Crear" class="text-button small-button" />
    <%} %>
</asp:Content>
