<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Presentation/AdminM.Master" AutoEventWireup="true" CodeBehind="ModifierArchiveAdmin.aspx.cs" Inherits="CalendrierDesArchives.Presentation.ModifierArchiveAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="./chosen/chosen.css">
     <link rel="stylesheet" href="./style/ajouterArchive.css">
    <script src="//cdn.ckeditor.com/4.14.0/full/ckeditor.js"></script>
   <style>
    .h5TitleInput{
        font-size:20px;
        color:green;
    }
       </style>
  

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
    <div class="container">

        <div class="ajouterArchive-content">
            <h2 class="h2Title">Modifier un archive </h2>

            <div class="containerLeft">
                <div class="input-div one ">
                    <div class="i">
                        <i class="fas fa-user"></i>
                    </div>
                    <div>
                        <h5>Titre de l'archive </h5>
                        <input class="input" type="text" runat="server" id="TitreArch">
                    </div>
                </div>
                <div class="input-div two">
                    <div class="i">
                        <i class="fas fa-lock"></i>
                    </div>
                    <div>
                        <h5>Emplacement physique</h5>
                        <input class="input" type="text" runat="server" id="EmpPc">
                    </div>
                </div>

                <div class="input-div tree">
                    <div class="i">
                        <i class="fas fa-lock"></i>
                    </div>
                    <div>
                        <h5>Index ou mot-clés pour faciliter votre recherche:</h5>
                        <input class="input" runat="server" type="text" id="index">
                    </div>
                </div>
                <h5 class="h5TitleInput">Choisissez un type pour votre archive:</h5>
                <div class="input-div tree">
                    <div class="i">
                        <i class="fas fa-lock"></i>
                    </div>
                    <div>
                        <asp:DropDownList class="input" ID="selectTypeAroo" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <h5 class="h5TitleInput">impossible de changer le fichier que vous avez ajoute a l'archive</h5>
                <div class="input-div tree">
                    <div class="i">
                        <i class="fas fa-lock"></i>
                    </div>
                    <div>
                        <asp:FileUpload class="input" ID="ArchiveUpload" runat="server" />
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


          
        </div>
          <asp:Button class="btn" ID="BTNADDArch" runat="server" Text="Modifier Archive" OnClick="BTNADDArch_Click" />
    </div>

    <script type="text/javascript" src="javascript/ajouterAchive.js"></script>
    <script src="./chosen/chosen.jquery.js"></script>
    <script>
        CKEDITOR.replace('<%=textArea.ClientID%>')
        $('#<%=selectTypeAroo.ClientID%>').chosen();
    </script>
</asp:Content>
