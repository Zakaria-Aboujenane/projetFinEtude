<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Presentation/AdminM.Master" AutoEventWireup="true" CodeBehind="CalendrierAdmin.aspx.cs" Inherits="CalendrierDesArchives.Presentation.CalendrierAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="style/Calendrier.css" type="text/css">
    <link rel="stylesheet" href="./chosen/chosen.css">
     <link rel="stylesheet" href="style/sectionHead.css" type="text/css">
    <link href="./style/Loader.css" rel="stylesheet" />
     <link href="./style/AfficherArchive.css" rel="stylesheet" />
    <style>
        .Date_cont{
            float:right;
            margin:30px;
        }
        .Voir_cont{
            float:right;
            margin:30px;
        }
        .moteurRech{
            width:30%;
            float:right;
            
        }
 .chosen-container-single .chosen-single {
    position: relative;
    display: block;
    overflow: hidden;
    padding: 0 0 0 8px;
    height: 25px;
    /* border: 1px solid #aaa; */
    /* border-radius: 5px; */
    /* background-color: #fff; */
    /* background: -webkit-gradient(linear, left top, left bottom, color-stop(20%, #fff), color-stop(50%, #f6f6f6), color-stop(52%, #eee), to(#f4f4f4)); */
    /* background: linear-gradient(#fff 20%, #f6f6f6 50%, #eee 52%, #f4f4f4 100%); */
    /* background-clip: padding-box; */
    /* -webkit-box-shadow: 0 0 3px #fff inset, 0 1px 1px rgba(0, 0, 0, 0.1); */
    /* box-shadow: 0 0 3px #fff inset, 0 1px 1px rgba(0, 0, 0, 0.1); */
    /* color: #444; */
    /* text-decoration: none; */
    white-space: nowrap;
    line-height: 24px;
}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
      <%-- Code de l'affichage de l'archive: --%>
    <div id="simpleModalAjouterA" class="modalAjout">
        <div id="ArchiveContent" class="modal-contenu">
        </div>
    </div>

    <%--  --%>

       <div class="box">
                    <div class="notifications">
                        <i class="fas fa-bell"></i>
                        <span id="numNots" class="num">4</span>
                        <ul id="NotifsPlace">
                            <li>
                                <span class="iconeu"><i style="transform:scale(1,1);" class="fas fa-file-archive"></i></span>
                                <span class="textNot">Someone Like Your Post</span>
                            </li>
                        </ul>
                    </div>
                </div>
    <%-- Head : bar de recherche et add: --%>
    
    <div id="recherche">
         <p style="color:green" runat="server" id="createdMSG"></p>
        <h2 style="color:green" runat="server" id="msgIndex"></h2>
        <div class="moteurRech">
        <div  id="searchBarForm">
            <input type="text" name="q" id="search">
            <i onclick="searchAdmin()" id="fasRecherche" class="fas fa-search"></i>
        </div>
            </div>
        <div class="Date_cont">
            <select style="text-align: left; width: 100%;" class="input" id="selectDateC" data-placeholder="veuillez choisir un type">
                <option value="1">Date Ajout</option>
                <option value="2">Date DernierAcces</option>
                <option value="3">Date Modification</option>
            </select>
        </div>
        
        <div class="addDiv">
            <div class="button_cont">
                <a class="example_d" target="_self" href="./AjouterArchiveAdmin.aspx" target="_blank" rel="nofollow noopener">
                    <i class="fas fa-plus-circle"></i>Ajouter un Archive</a>
            </div>
        </div>
    </div>
    <div class="wrapper-loading">
        <span class="loader"> <span class="loader-inner">Veuillez patientez</span></span>
    </div>
    <%-- Calendrier: --%>
    <div class="comble"></div>
    <div class="comble1"></div>
    <div class="comble2"></div>
    <div class="comble3"></div>
    <div class="annonce">
        <div class="msg">
        </div>
    </div>
    <div id="contenu">
    </div>

    <div id="footer">
    </div>
    <div class="calendrie">

        <div class="calendrier">
            <div class="annee">
                <div class="prevAnnee" onclick="moveDate('prevAnnee')">
                    <span>&#10094</span>
                </div>
                <div>
                    <h2 id="annee">2019</h2>
                </div>
                <div class="nextAnnee" onclick="moveDate('nextAnnee')">
                    <span>&#10095</span>
                </div>
            </div>

            <div class="mois">
                <div class="prev" onclick="moveDate('prev')">
                    <span>&#10094</span>
                </div>
                <p id="date_str">Samedi Fevrier 02 2020</p>
                <div>
                    <h2 id="mois">Fevrier</h2>

                </div>
                <div class="next" onclick="moveDate('next')">
                    <span>&#10095</span>
                </div>
            </div>
            <div class="semaine">
                <div>Dimenche</div>
                <div>Lundi</div>
                <div>Mardi</div>
                <div>Mercredi</div>
                <div>Jeudi</div>
                <div>Vendredi</div>
                <div>Samedi</div>
            </div>
            <div class="jour">
            </div>
        </div>
    </div>
    <%-- Archive Place: --%>
    <div class="ListFichers">

        <div id="SimpleModalA" class="modal">

            <div id="ModalContentArchives" class="modal-content">
                <div class="headModal">
                    <div id="closeMe"  class="closeBtn"><i class="far fa-times-circle"></i></div>
                </div>

                <div id="listArchives">
                </div>

            </div>
        </div>

        <%-- NV tags: --%>

        <script src="javascript/Calendrier.js"></script>
        <script src="./chosen/chosen.jquery.js"></script>
         <script src="./javascript/AfficherArchive.js" ></script>

        <%-- Affichage par Date : (AJAX) --%>
        <script type="text/javascript">
            $(window).on("load", function () {
                getNots();
                window.setInterval(function () {
                    getNots();
                }, 1000);
                $('.wrapper-loading').fadeOut("slow");
            });
            $(document).ready(function () {
                
                $('#selectDateC').chosen();
                $('#selectVu').chosen();
                $('#selectTypeRet').chosen();
            });
            function marquerVu(idNotif) {
                $.ajax({
                    type: "POST",
                    url: "CalendrierAdmin.aspx/marquerVuNotif",
                    data: "{idNotif: '" + idNotif + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        
                    },
                    error: function (e) {
                        alert("Error : " + e.error);
                    }
                });
            }
            function ouvrirFichier(idfichier) {
                $('.wrapper-loading').fadeIn("slow");
                $.ajax({
                    type: "POST",
                    url: "CalendrierAdmin.aspx/afficherArchive",
                    data: "{idArch: '" + idfichier + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        openModel();
                        $("#listArchives").html(msg.d);
                        $('.wrapper-loading').fadeOut("slow");
                    },
                    error: function (e) {
                        alert("Error : " + e.error);
                    }
                });
            }
            function searchAdmin(search) {
                var searchString = $('#search').val();
                $.ajax({
                    type: "POST",
                    url: "CalendrierAdmin.aspx/searchArchives",
                    data: "{search: '" + searchString + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        openModel();
                        $("#listArchives").html(msg.d);
                        $('.wrapper-loading').fadeOut("slow");
                    },
                    error: function (e) {
                        alert("Error : " + e.error);
                        $('.wrapper-loading').fadeOut("slow");
                    }
                });
            }
            function deleteArchive(id, date) {
                $('.wrapper-loading').fadeIn("slow");
                $.ajax({
                    type: "POST",
                    url: "CalendrierAdmin.aspx/DeleteArchive",
                    data: "{idArch: '" + id + "' ,date: '" + date + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $("#listArchives").html(msg.d);
                        $('.wrapper-loading').fadeOut("slow");
                    },
                    error: function (e) {
                        alert("Error : " + e.error);
                        $('.wrapper-loading').fadeOut("slow");
                    }
                });
            }
           
            function callCS(dateString) {
               
                $('.wrapper-loading').fadeIn("slow");
                var dateSelection = $('#selectDateC').val();
             
                var type = 1;
               
                $.ajax({
                    type: "POST",
                    url: "CalendrierAdmin.aspx/getArchives",
                    data: "{date:'" + dateString + "',typeD: '" + dateSelection + "',typeS: '" + type + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        openModel();
                        $("#listArchives").html(msg.d);
                        $('.wrapper-loading').fadeOut("slow");
                    },
                    error: function (e) {
                        closeModal();
                        alert("Error : " + e.error);
                    }
                });
            }
        </script>
</asp:Content>
