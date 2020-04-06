<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/NonConn.Master" AutoEventWireup="true" CodeBehind="Authentification.aspx.cs" Inherits="CalendrierDesArchives.Presentation.Authentification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    connectez vous
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="./style/login.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
    <div class="container">
            <div class="img">
                <img src="./images/phone.png">
            </div>
            <div class="login-content">
                <form id="login" runat="server">
                    <img class="profil" src="./images/profil.png">
                    <h2>Login</h2>
                    <div class="input-div one " >
                        <div class="i">
                            <i class="fas fa-envelope"></i>                          
                        </div>
                        <div>
                            <h5>Email</h5>
                            <input runat="server" class="input" type="email" id="cne">
                        </div>
                    </div>
                    
                    <div class="input-div two">
                        <div class="i">
                            <i class="fas fa-lock"></i>
                           <!-- <i class="fas fa-envelope"></i> --> 
                            
                        </div>
                        <div>
                            <h5>Mot_de_passe</h5>
                            <input runat="server" class="input" type="password" id="password">
                        </div>
                    </div>
                    <a href="a">Mot de passe oublié</a>
                    <p runat="server" style="color: red;" id="erreur"></p>
                    <input type="submit" class="btn" value="se connecter">
                    
                </form>
            </div>
       
        </div>
        <script type="text/javascript" src="./javascript/login.js"></script>
</asp:Content>
