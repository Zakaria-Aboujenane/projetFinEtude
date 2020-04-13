<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/AdminM.Master" AutoEventWireup="true" CodeBehind="Types.aspx.cs" Inherits="CalendrierDesArchives.Presentation.Types" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>

        tr,td{
            color:#66FCF1;
              font-family: 'Lobster', cursive;
        }
        a{
            color:#66FCF1;
              font-family: 'Lobster', cursive;
              
        }
        table{
            background:#0B0C10;
            border:2px solid #5CDB95;
          
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">

    <div id="recherche">
        &nbsp;<div class="addDiv">
            <div class="button_cont">
                <a class="example_d" href="./AjouterType.aspx" target="_blank" rel="nofollow noopener">
                    <i class="fas fa-plus-circle"></i>Ajouter Un type</a>
            </div>
        </div>

    </div>
    <div style="float: right; margin: 10px; width: 75%;">
        <div>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" DataKeyNames="idType" AutoGenerateColumns="False" DataSourceID="TypeObj">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
                    <asp:BoundField DataField="idType" HeaderText="idType" SortExpression="idType" />
                    <asp:BoundField DataField="nomType" HeaderText="nomType" SortExpression="nomType" />
                    <asp:BoundField DataField="duree" HeaderText="duree" SortExpression="duree" />
                    <asp:BoundField DataField="action" HeaderText="action" SortExpression="action" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="TypeObj" runat="server" DeleteMethod="supprimerType" SelectMethod="ListerTypes" TypeName="CalendrierDesArchives.Metiers.ActionsType" DataObjectTypeName="CalendrierDesArchives.Model.Type" UpdateMethod="modifierType">
                <DeleteParameters>
                    <asp:Parameter Name="idType" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
