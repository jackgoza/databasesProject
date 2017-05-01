var desktopModeToggle = false;

window.onload = function () {
    
    var width = document.documentElement.clientWidth;
   
    respondToSize(width);
    
    get_pieData();
    
}

window.onresize = function () {
    if (desktopModeToggle) {
        var width = 1024;
        respondToSize(width);
    } else {
        var width = document.documentElement.clientWidth;
        respondToSize(width);
    }

}

var desktopMode = document.getElementById('desktopMode');

desktopMode.onclick = function () {
    if (desktopModeToggle) {
        var width = document.documentElement.clientWidth;
        respondToSize(width);
        desktopModeToggle = false;
        desktopMode.setAttribute('style', 'color: #006847;');
    } else {
        var width = 1024;
        respondToSize(width);
        desktopModeToggle = true;
        desktopMode.setAttribute('style', 'color: #00ab75;');
    }
}

var gotoBudget = document.getElementById('gotoBudgetBtn');
var gotoGoals = document.getElementById('gotoGoalsBtn');

gotoBudget.onclick = function () {
    window.location.href = 'Budget.aspx';
}



function respondToSize(screenWidth) {

    if (screenWidth < 1024) {
        var navigation = document.getElementById('navigation');
        navigation.setAttribute('style', 'display: none;');

        var content = document.getElementById('content');
        content.className = 'col-xs-12';

        var mobileNavTop = document.getElementById('mobileNavTop');
        mobileNavTop.setAttribute('style', 'display: block;')
        var mobileNavBottom = document.getElementById('mobileNavBottom');
        mobileNavBottom.setAttribute('style', 'display: block;');

    } else {

        var mobileNavTop = document.getElementById('mobileNavTop');
        mobileNavTop.setAttribute('style', 'display: none;')

        var mobileNavBottom = document.getElementById('mobileNavBottom');
        mobileNavBottom.setAttribute('style', 'display: none;');

        var navigation = document.getElementById('navigation');
        navigation.setAttribute('style', 'display: block;');

        var content = document.getElementById('content');
        content.className = 'col-xs-10';
    }

    
}

//Navigation
var collapsed = false;

document.getElementById('summary').onclick = function () {
    window.location.href = 'Summary.aspx';
}

document.getElementById('budget').onclick = function () {
    window.location.href = 'Budget.aspx';
}



document.getElementById('wallet').onclick = function () {
    window.location.href = 'Wallet.aspx';
}



document.getElementById('summary_m').onclick = function () {
    if (collapsed) {
        document.getElementById('content').setAttribute('style', 'display: block;');
        collapsed = false;
    } else {
        document.getElementById('content').setAttribute('style', 'display: none;');
        collapsed = true;
    }
}

document.getElementById('budget_m').onclick = function () {
    window.location.href = 'Budget.aspx';
}



document.getElementById('wallet_m').onclick = function () {
    window.location.href = 'Wallet.aspx';
}





var get_color = {
    0: '#2882a0',
    3: '#6537ff',
    6: '#639664',
    9: '#871f87',
    12: '#a1a526',
    15: '#b21e13',
    18: '#ffff00',
    21: '#ec9393',
    24: '#FF8C00',
    27: '#228B22',
    30: '#00FF00',
    'extra': '#6be06d'
};

function get_pieData() {
    
    var acctNum = $("#hfAcctNum");
    PageMethods.getPieValues(acctNum.val(), onSuccess, onError);

    var values = [];
    var total = 360;
    var filled = 0;
    function onSuccess(result) {
        for (var i = 0; i < result.length; i += 3) {
            filled += parseInt(result[i + 2]);
            //alert("iter: " + i);
            values.push({
                value: parseInt(result[i + 1]),
                color: get_color[i],
                label: result[i]

            });
        }

        var extra = total - filled;
        if (extra != 0) {

            values.push({
                value: extra,
                color: get_color['extra']
            });
        }

        var chart = new Chart(document.getElementById("graphcanvas").getContext("2d")).Pie(values);
        
        return values;
    }
    function onError() {

    }

}

