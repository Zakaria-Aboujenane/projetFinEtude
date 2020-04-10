//get modal element
var model=document.getElementById('simpleModalAjouterA');
//get open model button

//get Close button
var closeBtn =document.getElementsByClassName('closeBtnAjout')[0];

//listen for open click

//listen for otside click;

//listen for outside click
window.addEventListener('click',outsideClick);
//function to open modal
function openArchiveModal(idArch, chemain) {
    getArchiveInfo(idArch);
    model.style.display = 'block';
    
}

//function to close modal
function closeArchiveModal(){
    model.style.display='none ';
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
            $("#ArchiveContent").html(msg.d);
            $('.wrapper-loading').fadeOut("slow");
        },
        error: function (e) {
            alert("Error : " + e.error);
            $('.wrapper-loading').fadeOut("slow");
        }
    });
}


