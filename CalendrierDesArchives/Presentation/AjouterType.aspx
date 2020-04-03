<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/Site1.Master" AutoEventWireup="true" CodeBehind="AjouterType.aspx.cs" Inherits="CalendrierDesArchives.Presentation.AjouterType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
       <form action="#"  runat="server">
            <table cellspacing="0" cellpadding="0" class="addType" 
                style="border:none; margin:60px;float:left">
                <tr>
                    <td>
                        Nom de type 
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" />
                    </td>
                </tr>
                <tr class="hov">
                    <td>
                        Action
                    </td>
                    <td> <asp:TextBox ID="Action" runat="server" /></td>  
                </tr>
                <tr>
                    <td>
                        Duree de type
                    </td>
                    <td> <asp:TextBox ID="txtDuree" runat="server" /></td>  
                </tr>
                <tr>
                    <td>ajouter</td>
                    <td>
                        <asp:Button ID="AddType" runat="server" Text="Ajouter" OnClick="AddType_Click" style="height: 26px" />    
                    </td>
                </tr>
            </table>
            
       </form>  
</asp:Content>