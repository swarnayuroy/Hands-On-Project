var subMenuState = false;
var modalConfirmationState = false;

function setSubMenuState() {
    if (!subMenuState) {
        document.getElementById("sub-menu").style.display = "block";
        subMenuState = true;
    } else {
        document.getElementById("sub-menu").style.display = "none";
        subMenuState = false;
    }
};

function setModalConfirmation() {
    if (!modalConfirmationState) {
        document.querySelector('.modal-confirmation').style.display = "flex";
        modalConfirmationState = true;
    } else {
        document.querySelector('.modal-confirmation').style.display = "none";
        modalConfirmationState = false;
    }
};