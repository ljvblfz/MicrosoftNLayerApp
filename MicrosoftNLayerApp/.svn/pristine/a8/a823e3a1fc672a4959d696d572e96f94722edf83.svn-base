<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.IEnumerable>" %>

    <% foreach (var item in Model)
       {
           ViewData["item"] = item;
           string templateName = (string)string.Concat(item.GetType().Name,"Item");
    %>
        <%: Html.Display("item",templateName) %>
    <% } %>
