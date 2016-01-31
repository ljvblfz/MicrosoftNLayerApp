<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Microsoft.Samples.NLayerApp.Domain.MainModule.Entities.CustomerPicture>" %>
<li>
    <h1><%: Html.DisplayNameFor(x => x.Photo) %></h1>
    <input type="file" name="<%: string.Concat(ViewData.TemplateInfo.HtmlFieldPrefix,".Photo") %>" />
    <div class="validation-field">
        <%: Html.ValidationMessageFor(x => x.Photo) %></div>
</li>
