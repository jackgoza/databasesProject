

//Add Budget Modal -- popup that is created in the html file, used to create a new budget with given inputs

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


//Histogram for budgets if needed

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
                color: 'rgb(49,130,189)',
                opacity: 0.7,
            }
        };
        var trace2 = {
            x: [],
            y: [],
            type: 'bar',
            name: 'Amount Spent',
            marker: {
                color: 'rgb(204,204,204)',
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
