const inputs = document.querySelectorAll('.input');

function focusFunc() {
    let parent = this.parentNode.parentNode;
    parent.classList.add('focus');
}

function blurFunc() {
    let parent = this.parentNode.parentNode;
    if (this.value == "") {
        parent.classList.remove('focus');
    }

}

inputs.forEach(input => {
    input.addEventListener('focus', focusFunc);
    input.addEventListener('blur', blurFunc);
});

var addF = document.getElementById("aDDArch");
if (addF) {
    addF.addEventListener("submit", function (e) {
        var erreur;
        var inputs = document.getElementById("aDDArch").getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            if (!inputs[i].value) {
                erreur = "Vieullez renseigner tous les champ";
            }
        }
        if (erreur) {
            e.preventDefault();
            document.getElementById("erreur").innerHTML = erreur;
            return false;
        } else {
            alert('formulaire envoyé!!');
        }
    });
}


const realFileBtn = document.getElementById("real-file");
const customBtn = document.getElementById("custom-button");
const customTxt = document.getElementById("custom-text");

if (customBtn) {
    customBtn.addEventListener("click", function () {
        realFileBtn.click();
    });
}

if (realFileBtn) {
    realFileBtn.addEventListener("change", function () {
        if (realFileBtn.value) {
            customTxt.innerHTML = realFileBtn.value.match(/[\/\\]([\w\d\s\.\-(\)]+)$/)[1];
        } else {
            customTxt.innerHTML = "No file chosen,yet "
        }
    });
}


var fonts = document.querySelectorAll("select#fontChanger > option");
for (var i = 0; i < fonts.length; i++) {
    fonts[i].style.fontFamily = fonts[i].value;
}

window.addEventListener("load", function () {
    var editor = theWYSIWYG.document;
    editor.designMode = "on";

    boldButton.addEventListener("click", function () {
        editor.execCommand("Bold", false, null);
    }, false);

    italicButton.addEventListener("click", function () {
        editor.execCommand("Italic", false, null);
    }, false);

    supButton.addEventListener("click", function () {
        editor.execCommand("Superscript", false, null);
    }, false);

    subButton.addEventListener("click", function () {
        editor.execCommand("Subscript", false, null);
    }, false);

    strikeButton.addEventListener("click", function () {
        editor.execCommand("Strikethrough", false, null);
    }, false);

    orderedListButton.addEventListener("click", function () {
        editor.execCommand("InsertOrderedList", false, "newOL" + Math.round(Math.random() * 1000));
    }, false);

    unorderedListButton.addEventListener("click", function () {
        editor.execCommand("InsertUnorderedList", false, "newOL" + Math.round(Math.random() * 1000));
    }, false);

    fontColorButton.addEventListener("change", function (event) {
        editor.execCommand("ForeColor", false, event.target.value);
    }, false);

    highlightButton.addEventListener("change", function (event) {
        editor.execCommand("BackColor", false, event.target.value);
    }, false);

    fontChanger.addEventListener("change", function (event) {
        editor.execCommand("FontName", false, event.target.value);
    }, false);

    fontSizeChanger.addEventListener("change", function (event) {
        editor.execCommand("FontSize", false, event.target.value);
    }, false);

    linkButton.addEventListener("click", function () {
        var url = prompt("Entrer a URL", "http://");
        editor.execCommand("CreateLink", false, url);
    }, false);

    unLinkButton.addEventListener("click", function () {
        editor.execCommand("UnLink", false, null);
    }, false);

    undoButton.addEventListener("click", function () {
        editor.execCommand("undo", false, null);
    }, false);

    redoButton.addEventListener("click", function () {
        editor.execCommand("redo", false, null);
    }, false);

}, false);