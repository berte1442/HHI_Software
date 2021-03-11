
    function ShowDescription(id) {
        document.getElementById(id).hidden = false;
};

    function HideDescription(id) {
        document.getElementById(id).hidden = true;
};

    function ShowAreaDetails(id) {
        if (document.getElementById(id).innerText == "Show Details") {
        document.getElementById(id).innerText = "Hide Details";
}
        else {
        document.getElementById(id).innerText = "Show Details";
}
};
