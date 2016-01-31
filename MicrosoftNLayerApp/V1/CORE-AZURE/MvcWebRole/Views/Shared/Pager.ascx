<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Tuple<string,string,Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.ViewModels.IPagedView>>" %>
<% if (Model.Item3.HasPreviousPage)
    { %>
<a href="<%: Url.Action(Model.Item1,Model.Item2,new { page=Model.Item3.PreviousPage, pageSize=Model.Item3.PageSize }) %>">
    <img src="<%: Url.Content("~/Content/Images/previus.png") %>" alt="Previous Page" />
</a>
<%	} %>
<% if (Model.Item3.HasNextPage)
    { %>
<a href="<%: Url.Action(Model.Item1,Model.Item2,new { page=Model.Item3.NextPage, pageSize=Model.Item3.PageSize }) %>">
    <img src="<%: Url.Content("~/Content/Images/next.png") %>" alt="Previous Page" />
</a>
<%	} %>
