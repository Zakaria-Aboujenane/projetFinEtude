<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/Site1.Master" AutoEventWireup="true" CodeBehind="ModifierArchive.aspx.cs" Inherits="CalendrierDesArchives.Presentation.ModifierArchive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
       <style>
        input{
            width: 100 %;
        }
        textarea{
            width: 100 %;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">

    <div class="containerAddAr">
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" class="addAr" style="border:none; margin:60px;float:left">
            <tr>
                <td>Titre Archive</td>
                <td> <input id="archiveTitre" type="text" runat="server"/> </td>
            </tr>
             <tr>
                <td>Description de l archive :</td>
                <td> <textarea id="DescriptionArchive" cols="20" rows="2"  runat="server" ></textarea> </td>
            </tr>
             <tr>
                <td>Type Archive:</td>
                 <td>
                     <select  runat="server" style="text-align: left; width:100%;" class="input" id="selectTypeAr" data-placeholder="veuillez choisir un type">
                         <option value="1">Facture</option>
                         <option value="2">Banque</option>
                         <option value="3">CV</option>
                     </select>
                 </td>
            </tr>
             
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                   
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                   
                </td>
            </tr>
           
        </table>
            </form>
    </div>
     <script src="https://code.jquery.com/jquery-3.4.1.js"></script>
    <script src="./chosen/chosen.jquery.js"></script>
    <script>
        $(document).ready(function () {
            $('#selectTypeAr').chosen();
        });
    </script>

</asp:Content>
