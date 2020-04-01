<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/Site1.Master" AutoEventWireup="true" CodeBehind="Types.aspx.cs" Inherits="CalendrierDesArchives.Presentation.Types" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
    <div id="recherche">
        <form action="#">
            <input type="text" name="q" id="search">
        </form>
        <div class="addDiv">
            <div class="button_cont">
                <a class="example_d" href="add-website-here" target="_blank" rel="nofollow noopener">
                    <i class="fas fa-plus-circle"></i>Add Call to Action</a>
            </div>
        </div>
    </div>
    <table class="tableTypes">
        <tr>
            <th>Type</th>
            <th>DUA(jours)</th>
            <th>Action</th>

        </tr>
        <tr class="hov">
            <td>Facture</td>
            <td>4</td>
            <td>Supprimer</td>
            <td>
                <button type="button" class="btn-edit">Modifier</button>
                <button type="button" class="btn-delete">Supprimer</button>
            </td>
        </tr>
    </table>
</asp:Content>
