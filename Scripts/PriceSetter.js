function SetPrice() {
    var min = 1500;
    var price = 315;
    var sqft = document.getElementById("squareFeet").value;

    while (sqft > min) {
        price += 15;
        min += 500;
    }

    document.getElementById("price").value = price;
};