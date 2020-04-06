<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/AdminM.Master" AutoEventWireup="true" CodeBehind="Types.aspx.cs" Inherits="CalendrierDesArchives.Presentation.Types" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
     
        <div id="recherche">
            &nbsp;<div class="addDiv">
            <div class="button_cont">
                <a class="example_d" href="./AjouterType.aspx" target="_blank" rel="nofollow noopener">
                    <i class="fas fa-plus-circle"></i>Ajouter Un type</a>
            </div>
        </div>
            <input type="text" name="q" id="search"></div>
           <div style="float:right;margin:10px;width:75%;">
             <div>
         <asp:GridView ID="GridView1" runat="server" AllowPaging="True" DataKeyNames="idType" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" DataSourceID="TypeObj" ForeColor="Black" GridLines="None">
             <AlternatingRowStyle BackColor="PaleGoldenrod" />
             <Columns>
                 <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
                 <asp:BoundField DataField="idType" HeaderText="idType" SortExpression="idType" />
                 <asp:BoundField DataField="nomType" HeaderText="nomType" SortExpression="nomType" />
                 <asp:BoundField DataField="duree" HeaderText="duree" SortExpression="duree" />
                 <asp:BoundField DataField="action" HeaderText="action" SortExpression="action" />
             </Columns>
             <FooterStyle BackColor="Tan" />
             <HeaderStyle BackColor="Tan" Font-Bold="True" />
             <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
             <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
             <SortedAscendingCellStyle BackColor="#FAFAE7" />
             <SortedAscendingHeaderStyle BackColor="#DAC09E" />
             <SortedDescendingCellStyle BackColor="#E1DB9C" />
             <SortedDescendingHeaderStyle BackColor="#C2A47B" />
         </asp:GridView>
         <asp:ObjectDataSource ID="TypeObj" runat="server" DeleteMethod="supprimerType" SelectMethod="ListerTypes" TypeName="CalendrierDesArchives.Metiers.ActionsType" DataObjectTypeName="CalendrierDesArchives.Model.Type" UpdateMethod="modifierType">
               <DeleteParameters>
                         <asp:Parameter Name="idType" Type="Int32" />
                     </DeleteParameters>
         </asp:ObjectDataSource>
    </div>
               </div>
</asp:Content>
