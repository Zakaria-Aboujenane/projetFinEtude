<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/AdminM.Master" AutoEventWireup="true" CodeBehind="RetentionArchives.aspx.cs" Inherits="CalendrierDesArchives.Presentation.RetentionArchives" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="style/TousTypes.css">
    <link href="./style/Loader.css" rel="stylesheet" />
    <link href="./style/AfficherArchive.css" rel="stylesheet" />
    <style>
       .archive{
           float:left;
           width:50%;
           position:relative;
       }
       div#contentTypes > *:nth-child(3n+1) {
   clear: both;
}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
     <div id="simpleModalAjouterA" class="modalAjout">
            
            <div id="ArchiveContent" class="modal-contenu">
               
             </div>
    </div>

    <div id="recherche">
        <div class="moteurRech">
            <div id="searchBarForm">
                <input type="text" name="q" id="search">
                <i onclick=" searchNonRet()" style="transform: scale(1.4,1.4); float: right" class="fas fa-search"></i>
            </div>
        </div>
    </div>
         <div class="container" id="contentTypes">
        <div class="contentTousType">
            <div class="nomTypes">
                Banquere
            </div>
            <div class="dateAjouterTypes">
               <label class="dateAjout">27/03</label>
                2020
            </div>
           
            <div class="mindescriptionTypes">
                <p>La durée de conservation de 5 ans correspond au délai dont vous disposez pour effectuer u</p>
            </div>  
        </div>    

        <div class="contentTousType">
            <div class="nomTypes">
                Banquere
            </div>
            <div class="dateAjouterTypes">
               <label class="dateAjout">27/03</label>
                2020
            </div>
           
            <div class="mindescriptionTypes">
                <p>La durée de conservation de 5 ans correspond au délai dont vous disposez pour effectuer u</p>
            </div>  
        </div>   

        <div class="contentTousType">
            <div class="nomTypes">
                Banquere
            </div>
            <div class="dateAjouterTypes">
               <label class="dateAjout">27/03</label>
                2020
            </div>
           
            <div class="mindescriptionTypes">
                <p>La durée de conservation de 5 ans correspond au délai dont vous disposez pour effectuer u</p>
            </div>  
        </div>   
    </div>
    <div class="wrapper-loading">
        <span class="loader"> <span class="loader-inner">Veuillez patientez</span></span>
    </div>
    <script src="./javascript/AfficherArchive.js" ></script>
    <script>
        $(document).ready(function () {
            getTypes();
          
        });
        $(window).on("load", function () {
            $('.aform').hide();
        });
        function getTypes() {
            $('.wrapper-loading').fadeIn("slow");
            $.ajax({
                type: "POST",
                url: "RetentionArchives.aspx/getTypes",
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    $("#contentTypes").html(msg.d);
                    $('.wrapper-loading').fadeOut("slow");
                },
                error: function (e) {
                    alert("Error : " + e.error);
                }
            });
        }

        function voirArchives(idtype) {
            $('.wrapper-loading').fadeIn("slow");
            $.ajax({
                type: "POST",
                url: "RetentionArchives.aspx/getArchives",
                data: "{idtype: '" + idtype + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#contentTypes").html(msg.d);
                    $('.wrapper-loading').fadeOut("slow");
                    $('.aform').hide();
                },
                error: function (e) {
                    alert("Error : " + e.error);
                }
            });
        }
        function searchNonRet(search) {
            $('.wrapper-loading').fadeIn("slow");
            var searchString = $('#search').val();
            
            $.ajax({
                type: "POST",
                url: "RetentionArchives.aspx/searchArchives",
                data: "{search: '" + searchString + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    alert("atika" + msg.d);
                    $("#contentTypes").html(msg.d);
                    $('.wrapper-loading').fadeOut("slow");
                    $('.aform').hide();
                },
                error: function (e) {
                    alert("Error : " + e.error);
                    $('.wrapper-loading').fadeOut("slow");
                }
            });
        }
    </script>
</asp:Content>
