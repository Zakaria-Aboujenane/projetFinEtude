<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/AdminM.Master" AutoEventWireup="true" CodeBehind="RetentionArchives.aspx.cs" Inherits="CalendrierDesArchives.Presentation.RetentionArchives" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="style/TousTypes.css">
    <link href="./style/Loader.css" rel="stylesheet" />
    <style>
       .archive{
           float:left;
           width:50%;
           position:relative;
       }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
     
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

        function voirArchives(idArchive) {
            $('.wrapper-loading').fadeIn("slow");
            $.ajax({
                type: "POST",
                url: "RetentionArchives.aspx/getArchives",
                contentType: "application/json; charset=utf-8",
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
    </script>
</asp:Content>
