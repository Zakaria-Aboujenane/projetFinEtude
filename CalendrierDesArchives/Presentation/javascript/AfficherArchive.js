//get modal element
var model=document.getElementById('simpleModalAjouterA');
//get open model button

//get Close button
var closeBtn =document.getElementsByClassName('closeBtnAjout')[0];

//listen for open click

//listen for otside click;
closeBtn.addEventListener('click',closeModal);

//listen for outside click
window.addEventListener('click',outsideClick);
//function to open modal
function openArchiveModal(idArch, chemain) {

    var url = chemain;
   

    alert("chemain" +url);
    model.style.display = 'block';
    getArchiveInfo(idArch);
    viewPDF(url);
}

//function to close modal
function closeModal(){
    modal.style.display='none ';
} 

//function to close modal if outside click
function outsideClick(e){
    if(e.target==modal){
        modal.style.display='none ';
    
} 
}
function getArchiveInfo(idArch) {
    $('.wrapper-loading').fadeIn("slow");
    $.ajax({
        type: "POST",
        url: "RetentionArchives.aspx/getArchiveInfo",
        data: "{idArch: '" + idArch + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            alert("atika" + msg.d);
            $("#ArchiveContent").html(msg.d);
            $('.wrapper-loading').fadeOut("slow");
        },
        error: function (e) {
            alert("Error : " + e.error);
            $('.wrapper-loading').fadeOut("slow");
        }
    });
}
function viewPDF(chemain) {
    alert("url :"+chemain);
    alert(chemain);
    var viewer = $('#ArchiveContent');
    PDFObject.embed(chemain, viewer);
}

