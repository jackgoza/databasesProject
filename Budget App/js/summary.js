//for displaying pie chart element

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
    PageMethods.getPieValues(211111110, onSuccess, onError);

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

