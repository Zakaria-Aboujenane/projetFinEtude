<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/Site1.Master" AutoEventWireup="true" CodeBehind="Calendrier.aspx.cs" Inherits="CalendrierDesArchives.Presentation.Calendrier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="style/Calendrier.css" type="text/css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
    <%-- Head : bar de recherche et add: --%>
      <div id="recherche">
                <form action="#">
                    <input type="text" name="q" id="search">
                </form>
                <div class="addDiv">
                    <div class="button_cont">
                       <a class="example_d" href="add-website-here" target="_blank" rel="nofollow noopener">
                        <i class="fas fa-plus-circle"></i>Ajouter un Archive</a>
                    </div>
                </div>
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
           <form id="form1" runat="server">
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
               </form>
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
        <script src="https://code.jquery.com/jquery-3.4.1.js"></script>
        <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>

        <%-- Affichage par Date : (AJAX) --%>
        <script type="text/javascript">
            function callCS(dateString) {
                $.ajax({
                    type: "POST",
                    url: "Calendrier.aspx/getArchives",
                    data: "{'date':'" + dateString + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        openModel();
                        alert("hi" + msg.d);
                        $("#listArchives").html(msg.d);
                    },
                    error: function (e) {
                        alert("Error : " + e.error);
                    }
                });
            }
        </script>
</asp:Content>
