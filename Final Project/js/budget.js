var desktopModeToggle = false;

window.onload = function () {
    
    var width = document.documentElement.clientWidth;
    respondToSize(width);
    displayHistogram();
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
    window.location.href = 'Summary.aspx';
}

document.getElementById('budget_m').onclick = function () {
    if (collapsed) {
        document.getElementById('content').setAttribute('style', 'display: block;');
        collapsed = false;
    } else {
        document.getElementById('content').setAttribute('style', 'display: none;');
        collapsed = true;
    }
}



document.getElementById('wallet_m').onclick = function () {
    window.location.href = 'Wallet.aspx';
}


//Add Budget Modal

// Get the modal
var addBudgetmodal = document.getElementById('addBudget');

// Get the button that opens the modal
var addBudgetbtn = document.getElementById("addBudgetBtn");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks the button, open the modal 
addBudgetbtn.onclick = function () {
    addBudgetmodal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    addBudgetmodal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == addBudgetmodal) {
        addBudgetmodal.style.display = "none";
    }
}


//Histogram

function displayHistogram() {

    PageMethods.getbudgetData(onSuccess, onError);


    function onSuccess(result) {

        var hist = document.getElementById('histogram');


        var trace1 = {
            x: [],
            y: [],
            type: 'bar',
            name: 'Budget Limit',
            marker: {
                color: 'rgb(0,171,117)',
                opacity: 0.7,
            }
        };
        var trace2 = {
            x: [],
            y: [],
            type: 'bar',
            name: 'Amount Spent',
            marker: {
                color: 'rgb(153,50,204)',
                opacity: 0.5
            }
        };
        for (var i = 0; i < result.length; i += 9) {

            trace1.x.push(result[i + 1]);

            trace2.x.push(result[i + 1]);
            trace1.y.push(result[i + 5]);

            trace2.y.push(result[i + 8]);

        }




        var values = [trace1, trace2];


        var layout = {
            width: 900,
            height: 400,
            title: 'Spending Overview',
            xaxis: {
                tickangle: -45
            },
            barmode: 'group'
        };

        Plotly.newPlot(hist, values, layout);

        return values;
    }
    function onError() {

    }
}
