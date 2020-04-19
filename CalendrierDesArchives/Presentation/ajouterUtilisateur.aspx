<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/AdminM.Master" AutoEventWireup="true" CodeBehind="ajouterUtilisateur.aspx.cs" Inherits="CalendrierDesArchives.Presentation.ajouterUtilisateur" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="./style/ajouterArchive.css">
     <link href="https://fonts.googleapis.com/css?family=Lobster&display=swap" 
        rel="stylesheet">
        <script src="https://kit.fontawesome.com/a81368914c.js"></script>
    <style>
          .h5TitleInput{
        font-size:20px;
        color:green;
    }
              .containerUtilisateur .input-div {
        position: relative;
        display: grid;
        grid-template-columns: 7% 93%;
        margin: 25px 0;
        padding: 5px 0;
        border-bottom: 2px solid #d9d9d9;
    }
        .containerRight {
            height:auto;
        }
        .containerLeft {
            height:auto;
        }
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
        .containerGridView{
           float:left;
           position:relative;
           margin:0px 50px;
           align-content:center;

        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">     
    
        <div class="containerAddUser" style="width:70% ; float:left; margin:10px; position:relative;background:#0B0C10;">
            <div class="containerUtilisateur">
                <div class="ajouterUtilisateur-content">
                <div  class="ajouterU" id="ajouterUtilisateur" >
                    <h2>Ajouter un utilisateur </h2>
                    <div class="containerRight">

                        <div class="input-div one ">
                            <div class="i">
                                <i class="fas fa-user"></i>
                            </div>
                            <div>
                                <h5>Nom :</h5>
                                <input class="input" type="text" runat="server" id="nomUtilisateur">
                            </div>
                        </div>
                        <div class="input-div two">
                            <div class="i">
                                <i class="fas fa-user"></i>
                            </div>
                            <div>
                                <h5>Prenom :</h5>
                                <input class="input" type="text" runat="server" id="prenomUtilisateur">
                            </div>
                        </div>

                        <div class="input-div tree">
                            <div class="i">
                                <i class="fas fa-envelope"></i>
                            </div>
                            <div>
                                <h5>Email :</h5>
                                <input class="input" type="text" runat="server" id="emailUtilisateur">
                            </div>
                        </div>
                    </div>
                    <div class="containerLeft">
                        <div class="input-div for">
                            <div class="i">
                                <i class="fas fa-lock"></i>
                            </div>
                            <div>
                                <h5>mot de passe :</h5>
                                <input class="input" type="password" runat="server" id="mdpUtilisateur">
                            </div>
                        </div>

                        <h5 class="h5TitleInput">role :</h5>
                        <div class="input-div five">
                            <div class="i">
                                <i class="fa fa-check-square"></i>
                            </div>
                            <div class="utilisateur">
                                <div class="select" >
                                    <asp:DropDownList ID="selectRole" style="width: 100%; height: -webkit-fill-available;background: #0B0C10;color: #66FCF1;" runat="server">

                                        <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                        <asp:ListItem Value="User">Utilisateur</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br>
                    </div>
                    <p style="color: red;" runat="server" id="erreur"></p>

                    <asp:Button class="btn" ID="btnAjouterUtilisateur" runat="server" Text="Ajouter Utilisateur" OnClick="btnAjouterUtilisateur_Click1" />
                </div>
            </div>
        </div>
        </div>

    <div class="containerGridView">

        <asp:GridView ID="GridView1" DataKeyNames="idUtilisateur" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
                <asp:BoundField DataField="nom" HeaderText="nom" SortExpression="nom" />
                <asp:BoundField DataField="prenom" HeaderText="prenom" SortExpression="prenom" />
                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                <asp:BoundField DataField="motDePasse" HeaderText="motDePasse" SortExpression="motDePasse" />
                <asp:BoundField DataField="privillege" HeaderText="privillege" SortExpression="privillege" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="CalendrierDesArchives.Model.Utilisateur" DeleteMethod="supprimerUtilisateur" SelectMethod="listerTousUtilisateur" TypeName="CalendrierDesArchives.Metiers.ActionsUtilisateur" UpdateMethod="modifierUtilisateur"></asp:ObjectDataSource>

    </div>
    <script type="text/javascript" src="javascript/ajouterAchive.js"></script>
</asp:Content>
