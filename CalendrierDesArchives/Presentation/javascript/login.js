const inputs=document.querySelectorAll('.input');

function focusFunc(){
    let parent =this.parentNode.parentNode;
    parent.classList.add('focus');
}

function blurFunc(){
    let parent =this.parentNode.parentNode;
    if(this.value == ""){
        parent.classList.remove('focus');
    }
    
}

inputs.forEach(input =>{
    input.addEventListener('focus',focusFunc);
    input.addEventListener('blur',blurFunc);
});

document.getElementById("login").addEventListener("submit",function(e){
    var erreur;
    var inputs=document.getElementById("login").getElementsByTagName("input");
    for(var i=0;i<inputs.length ;i++){
        if(!inputs[i].value){
            erreur="Vieullez renseigner tous les champ";
        }
    }
    if(erreur){
        e.preventDefault();
        document.getElementById("erreur").innerHTML=erreur;
        return false;
    } else {
        alert('formulaire envoyé!!');
    }
});
