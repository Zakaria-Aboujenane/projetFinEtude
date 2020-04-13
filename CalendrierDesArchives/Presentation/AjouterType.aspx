<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Presentation/AdminM.Master" AutoEventWireup="true" CodeBehind="AjouterType.aspx.cs" Inherits="CalendrierDesArchives.Presentation.AjouterType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
         <link rel="stylesheet" href="./chosen/chosen.css">
    <link rel="stylesheet" href="./style/ajouterArchive.css">
    <script src="//cdn.ckeditor.com/4.14.0/full/ckeditor.js"></script>
    <style>
          .h5TitleInput{
        font-size:20px;
        color:#66FCF1;
    }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
    <div class="container">
         <div class="ajouterArchive-content">
            <h2 class="h2Title">Ajouter un Type de Document </h2>
            
                <div class="containerLeft">
                    <div class="input-div one ">
                        <div class="i">
                            <i class="fas fa-user"></i>
                        </div>
                        <div>
                            <h5>Nom du Type </h5>
                            <input class="input" type="text" runat="server" id="typeN">
                        </div>
                    </div>
                    <div class="input-div two">
                        <div class="i">
                            <i class="fas fa-lock"></i>
                        </div>
                        <div>
                            <h5>duree (DUA)</h5>
                            <input class="input" type="text" runat="server" id="DUA">
                        </div>
                    </div>
                       <h5 class="h5TitleInput">action apres DUA (Sort final)</h5>
                    <div class="input-div tree">
                        <div class="i">
                            <i class="fas fa-lock"></i>
                        </div>
                        <div>
                            <asp:DropDownList class="input" ID="selectAction" runat="server">

                                <asp:ListItem Value="Destruction">Destruction finale</asp:ListItem>
                                <asp:ListItem Value="Conservation">Conservation finale</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <h5 class="h5TitleInput">la DUA de ce type est selon :</h5>
                    <div class="input-div tree">
                        <div class="i">
                            <i class="fas fa-lock"></i>
                        </div>
                       <div>
                           <asp:DropDownList class="input" ID="selectCritere" runat="server">
                               <asp:ListItem Value="DateAjout">Date d&#39;ajout de l&#39;archive</asp:ListItem>
                               <asp:ListItem Value="DateDerniereMod">Date de derniere Modification</asp:ListItem>
                               <asp:ListItem Value="DateDernierAcces">Date du dernier acces</asp:ListItem>
                           </asp:DropDownList>
                       </div>
                    </div>
                <br>
                <br>
               
                    <br>
                </div>
                <div class="containerRight">
                    
                    <div id="textEditor">
                       <textarea name="textArea" id="textArea" runat="server" ></textarea>
                    </div>
                    <p style="color: red;" id="erreur" runat="server"></p>
        </div>
                 
             
                <asp:Button class="btn" ID="BTNADDArch" runat="server" Text="Ajouter Archive" OnClick="BTNADDArch_Click" />
    </div>
      
            <script type="text/javascript" src="javascript/ajouterAchive.js"></script>
    <script src="./chosen/chosen.jquery.js"></script>
        <script>
            CKEDITOR.replace('<%=textArea.ClientID%>')
            $('#<%=selectCritere.ClientID%>').chosen();
         $('#<%=selectAction.ClientID%>').chosen();
        </script>
</asp:Content>






