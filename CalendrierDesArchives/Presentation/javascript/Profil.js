//get modal element
var modal = document.getElementById('simpleModal');
//get open model button
var modalBtn = document.getElementById('modaleBtn');
//get Close button
var closeBtn = document.getElementsByClassName('closeBtnDeconnecte')[0];
//listen for open click
modalBtn.addEventListener('click', openModal);
//listen for otside click;
closeBtn.addEventListener('click', closeModal);

//listen for outside click
window.addEventListener('click', outsideClick);
//function to open modal
function openModal() {
    modal.style.display = 'block';
}

//function to close modal
function closeModal() {
    modal.style.display = 'none ';
}

//function to close modal if outside click
function outsideClick(e) {
    if (e.target == modal) {
        modal.style.display = 'none ';

    }
}