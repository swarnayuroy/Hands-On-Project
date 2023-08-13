var subMenuState = false;
function setSubMenuState() {
    if (!subMenuState) {
        document.getElementById("sub-menu").style.display = "block";
        subMenuState = true;
    } else {
        document.getElementById("sub-menu").style.display = "none";
        subMenuState = false;
    }
};