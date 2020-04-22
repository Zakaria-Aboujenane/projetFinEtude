var annonce=document.querySelector('.annonce')


var comble=document.querySelector('.comble')
var comble1=document.querySelector('.comble1');
var comble2=document.querySelector('.comble2');
var comble3=document.querySelector('.comble3');

comble.style.display="none"
comble1.style.display="none"
comble2.style.display="none"
comble3.style.display="none"

var affiche=function(){
    comble.style.display="block"
    comble1.style.display="block"
    comble2.style.display="block"
    comble3.style.display="block"
    annonce.classList.toggle('active')
}

var supprimer=function(){
    annonce.classList.toggle('active')
    comble.style.display="none"
    comble1.style.display="none"
    comble2.style.display="none"
    comble3.style.display="none"
}


comble.addEventListener('click',supprimer)
comble1.addEventListener('click',supprimer)
comble2.addEventListener('click',supprimer)
comble3.addEventListener('click',supprimer)

var dt=new Date();
function RenderDate(){

var day=dt.getDay();
dt.setDate(1);
console.log(dt.getDay());
var endDate=new Date(
    dt.getFullYear(),
    dt.getMonth() + 1,
    0
).getDate();

var prevDate=new Date(
    dt.getFullYear(),
    dt.getMonth(),
    0
).getDate();

var today=new Date();
console.log(today);
var mois=["JANVIER","FEVRIER","MARS","AVRIL","MAI",
"JUIN","JUILLET","AOUT","SEPTEMBRE","OCTOBRE",
"NOVEMBRE","DECEMBRE"];

document.getElementById("annee").innerHTML=dt.getFullYear();
document.getElementById("date_str").innerHTML=dt.toDateString();
document.getElementById("mois").innerHTML=mois[dt.getMonth()];


var cells="";

for(x=day; x>0;x--){
    cells+="<div class='prev_date'>"+(prevDate -x+1)+"</div>";
}

for(i=1;i<=endDate;i++){
    if(i==today.getDate() && dt.getMonth()==today.getMonth() &&dt.getFullYear()==today.getFullYear()){
        cells += "<div class='today' onclick='getCurrentDay("+i+")'>" + i + "</div>"; 
    }
    else{
        cells += "<div runat='server' onclick='getCurrentDay(" + i +")' >" + i + "</div>"; 
    }
}

document.getElementsByClassName("jour")[0].innerHTML =cells; 
}
function getCurrentDay(x) {
    var dateString = "";// ici on va mettre la date complete 
    var day = x;// le jour qui est x 
    var month = dt.getMonth() + 1;// dt.getMonth  -- mois
    var imonth = dt.getMonth()+1;
    if (x <= 9) {
        day = "0" + x;// 02
        if (imonth <= 9)
            month = "0" + imonth;// la meme chose si mois par ex 2 : 02==>
    }
    else if (imonth <= 9) {
        month = "0" + imonth;
        if (x <= 9)
            day = "0" + x;
    } else {
        day = x;
        month = imonth;
    }
    dateString = dt.getFullYear() + "-" + month + "-" + day;
    callCS(dateString);// ex : 02/02/2000

}

function moveDate(para){
  if(para=='prev'){
      dt.setMonth(dt.getMonth()-1);
      RenderDate();
  }
  if(para=='next'){
      dt.setMonth(dt.getMonth()+1);
      RenderDate();
  }
  if(para=='prevAnnee'){
    dt.setFullYear(dt.getFullYear()-1);
     RenderDate();
 }
 if(para=='nextAnnee'){
     dt.setFullYear(dt.getFullYear()+1);
     RenderDate();
 }
}
//get modal element


//get Close button


//listen for open click

//listen for otside click;


//listen for outside click
//var closeBtn = document.getElementsByClassName('closeBtn')[0];
var modalA = document.getElementById('SimpleModalA');
var closeB = document.getElementById('closeMe');
//closeBtn.addEventListener('click', closeModal);
window.addEventListener('click', outsideClick);
closeB.addEventListener('click', closeModal);
//function to close modal
function closeModal() {
   
    modalA.style.display = 'none ';
}
//function to close modal if outside click
function outsideClick(e) {
    if (e.target == modalA) {
        modal.style.display = 'none ';
    }
}
function openModel() {
    modalA.style.display = 'block';
}

