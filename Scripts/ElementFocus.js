function LastUsed(id) {
    sessionStorage.setItem('lastUsed', id);
}
function SetFocus() {
    var lastUsed = sessionStorage.getItem('lastUsed');
    if (lastUsed != null) {
        window.location.hash = '#' + lastUsed;
        $(window.location.hash).show();
        var btnID = lastUsed.concat('button'); 
        document.getElementById(btnID).click();
    }

}
