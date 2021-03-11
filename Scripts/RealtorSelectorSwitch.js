
function CloseNewRealtor() {
    var check = document.getElementById("newBtn").getAttribute("aria-expanded");

    if (check == "true") {
        document.getElementById("newBtn").click();
    }
};

function CloseExistingRealtor() {
    var check = document.getElementById("existBtn").getAttribute("aria-expanded");

    if (check == "true") {
        document.getElementById("existBtn").click();
    }
};