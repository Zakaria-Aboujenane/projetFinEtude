<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/Site1.Master" AutoEventWireup="true" CodeBehind="Calendrier.aspx.cs" Inherits="CalendrierDesArchives.Presentation.Calendrier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="style/Calendrier.css" type="text/css">
    <link rel="stylesheet" href="./chosen/chosen.css">
     <link href="./style/AfficherArchive.css" rel="stylesheet" />
    <link rel="stylesheet" href="style/sectionHead.css" type="text/css">
    <link href="./style/Loader.css" rel="stylesheet" />
    <style>
        .Date_cont {
            float: right;
            margin: 30px;
        }

        .Voir_cont {
            float: right;
            margin: 30px;
        }

        .moteurRech {
            width: 30%;
            float: right;
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
                    <span class="iconeu"><i style="transform: scale(1,1);" class="fas fa-file-archive"></i></span>
                    <span class="textNot">Someone Like Your Post</span>
                </li>
            </ul>
        </div>
    </div>
    <%-- Head : bar de recherche et add: --%>

    <div id="recherche">
        <p style="color: green" runat="server" id="createdMSG"></p>
        <h2 style="color: green" runat="server" id="msgIndex"></h2>
        <div class="moteurRech">
            <div id="searchBarForm">
                <input type="text" name="q" id="search">
                <i onclick="searchUser()" style="transform: scale(1.4,1.4); float: right" class="fas fa-search"></i>
            </div>
        </div>
        <div class="Date_cont">
            <select style="text-align: left; width: 100%;" class="input" id="selectDateC" data-placeholder="veuillez choisir un type">
                <option value="1">Date Ajout</option>
                <option value="2">Date DernierAcces</option>
                <option value="3">Date Modification</option>
            </select>
        </div>
        <div class="Voir_cont">
            <select style="text-align: left; width: 100%;" class="input" id="selectVu" data-placeholder="veuillez choisir un type">
                <option value="1">Voir Tous les archives</option>
                <option value="2">Voir les archives que j'ai ajouté seulement</option>
            </select>
        </div>
        <div class="addDiv">
            <div class="button_cont">
                <a class="example_d" target="_self" href="./AjouterArchive.aspx" target="_blank" rel="nofollow noopener">
                    <i class="fas fa-plus-circle"></i>Ajouter un Archive</a>
            </div>


        </div>

    </div>
    <div class="wrapper-loading">
        <span class="loader"><span class="loader-inner">Veuillez patientez</span></span>
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
                    <div id="closeMe" class="closeBtn"><i class="far fa-times-circle"></i></div>
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
                }, 30000);
                $('.wrapper-loading').fadeOut("slow");
                $('#selectDateC').chosen();
                $('#selectVu').chosen();
            });
            function ouvrirFichier(idfichier) {
                $('.wrapper-loading').fadeIn("slow");
                $.ajax({
                    type: "POST",
                    url: "Calendrier.aspx/afficherArchive",
                    data: "{idArch: '" + id + "'}",
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
            function deleteArchive(id, date) {
                $('.wrapper-loading').fadeIn("slow");
                var dateSelection = $('#selectDateC').val();
                var type = $('#selectVu').val();
                $.ajax({
                    type: "POST",
                    url: "Calendrier.aspx/DeleteArchive",
                    data: "{idArch: '" + id + "' ,date: '" + date + "',typeD: '" + dateSelection + "',typeS: '" + type + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $("#listArchives").html(msg.d);
                        $('.wrapper-loading').fadeOut("slow");
                    },
                    error: function (e) {
                        alert("Error : " + e.error);
                    }
                });
            }
            function searchUser() {
                var searchString = $('#search').val();
                var type = $('#selectVu').val();
                $.ajax({
                    type: "POST",
                    url: "Calendrier.aspx/searchArchives",
                    data: "{search: '" + searchString + "',typeS: '" + type + "'}",
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
            function callCS(dateString) {

                $('.wrapper-loading').fadeIn("slow");
                var dateSelection = $('#selectDateC').val();
                var type = $('#selectVu').val();

                $.ajax({
                    type: "POST",
                    url: "Calendrier.aspx/getArchives",
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
