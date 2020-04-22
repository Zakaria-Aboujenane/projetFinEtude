<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/AdminM.Master" AutoEventWireup="true" CodeBehind="AjouterHistorique.aspx.cs" Inherits="CalendrierDesArchives.Presentation.AjouterHistorique" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
         tr,td{
            color:#66FCF1;
              font-family: Merriweather,monospace;
        }
        a{
            color:#66FCF1;
              font-family: Merriweather,monospace;
              
        }
        table{
            background:#0B0C10;
            border:2px solid #5CDB95;
          
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
    <div class="containerAddUser" style="            width: 70%;
            float: left;
            margin: 10px;
            position: relative;
    ">
         <asp:GridView ID="GridView1" DataKeyNames="IdHistorique" runat="server" AutoGenerateColumns="False"   DataSourceID="obj1" >
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
            <asp:BoundField DataField="textHistorique" HeaderText="textHistorique" SortExpression="textHistorique" />
            <asp:BoundField DataField="date" HeaderText="date" SortExpression="date" />
        </Columns>
      
    </asp:GridView>
         <asp:ObjectDataSource ID="obj1" runat="server" DataObjectTypeName="CalendrierDesArchives.Model.Historique" DeleteMethod="supprimerHistorique" SelectMethod="listerTousHistoriques" TypeName="CalendrierDesArchives.Metiers.ActionsHistorique"></asp:ObjectDataSource>

    </div>
   </asp:Content>
