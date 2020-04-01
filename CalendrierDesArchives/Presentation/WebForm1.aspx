<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CalendrierDesArchives.Presentation.WebForm1" %>


<html runat="server">
    <head>
        <title>Calendrier des Archives</title>
          <!-- include calendrier.css-->
        <link rel="stylesheet" href="style/Calendrier.css" type="text/css">
        <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
        <script src="https://kit.fontawesome.com/6c9ba7741a.js" crossorigin="anonymous"></script>
    </head>
    <body onload="RenderDate()">
        
        <form id="form1" runat="server">
        
        <div id="head1">
        </div>
        <div id="menu">
            <ul>
                <li><a href="acceuil.html">Acceuil</a></li>
                <li><a href="apropos.html">A propos</a></li>
                <li><a href="contactez_nous.html">Contactez_nous</a></li>
            </ul>
            <div class="cercle">
            </div>
        </div>
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
                  <div>
                      <h2 id="mois">Fevrier</h2>
                      <p id="date_str">Samedi Fevrier 02 2020</p>
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
            
        <div class="ListFichers">
            <div id="simpleModal" class="modal">
               
            <div id="ModalContent" class="modal-content">
              
        </div>
    </div>
            
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
        </form>
    </body>
    
    <!--  include Calendrier.js  -->
    <script src="javascript/Calendrier.js"></script>
  <script
  src="https://code.jquery.com/jquery-3.4.1.js"
 ></script>
    <script
  src="https://code.jquery.com/jquery-3.4.1.min.js" ></script>
    <script type-="text/javascript">
        function callCS(dateString) {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/getDate",
                data: "{'date':'" + dateString + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    openModel();
                    alert("hello");
                    alert("hi"+msg.d);
                    $("#ModalContent").html(msg.d);
                },
                error: function (e) {
                    alert("noo" + e.error);
                }
            });
        }
    </script>
</html>