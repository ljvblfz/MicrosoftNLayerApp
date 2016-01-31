<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Client
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SubHeaderContent" runat="server">
    <%: Html.DisplayForModel("CustomerBrief") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm("Edit","Customer",FormMethod.Post,new { enctype = "multipart/form-data", @class="editor-form"}))
       { %>
       
        <%: Html.EditorForModel() %>
        <%: Html.SerializedHidden(Model) %>
        <input type="submit" value="Guardar Cambios" class="text-button big-button" />
    <%} %>
</asp:Content>
