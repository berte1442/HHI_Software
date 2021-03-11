    function CheckItemVsLimitation(id) {

        //console.log(id);
        var dataTarget = document.getElementById(id).getAttribute('data-target');
    //console.log(dataTarget);
    var limitationBtn = dataTarget.substring(1).concat('limitationBtn');
    var checkItemBtn = dataTarget.substring(2).concat('checkItemBtn');

        if (id == limitationBtn) {
            var checkDataBtnTarget = dataTarget.replace('#', '');
    var checkBtnID = checkDataBtnTarget.concat('checkItemBtn');
    //console.log(checkBtnID);
    var expanded = document.getElementById(checkBtnID).getAttribute('aria-expanded');
    //console.log(expanded);
            if (expanded == 'true') {
        document.getElementById(checkBtnID).click();
}
}
        else if (id == checkItemBtn) {
            var limitDataTarget = dataTarget.replace('#0', '');
    var limitBtnID = limitDataTarget.concat('limitationBtn');
    //console.log(limitBtnID);
    var expanded = document.getElementById(limitBtnID).getAttribute('aria-expanded');
    console.log(expanded);
            if (expanded == 'true') {
        document.getElementById(limitBtnID).click();
}
}
};
