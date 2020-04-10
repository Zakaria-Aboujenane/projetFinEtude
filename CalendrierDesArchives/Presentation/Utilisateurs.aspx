<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/AdminM.Master" AutoEventWireup="true" CodeBehind="Utilisateurs.aspx.cs" Inherits="CalendrierDesArchives.Presentation.Utilisateurs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
 <div style="float:right;margin:10px;width:75%;">

     <asp:GridView ID="GridView1" runat="server" DataSourceID="userObject">
     </asp:GridView>

    </div>
</asp:Content>
