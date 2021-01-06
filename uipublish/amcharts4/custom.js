var initPieChart3D = function () {

    $.ajax({
        type: "GET",
        url: "/AmReport/PieChart3D/",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });

    function successFunc(result) {

        am4core.useTheme(am4themes_animated);

        var chart = am4core.create("chartdiv", am4charts.PieChart);

        chart.data = result.data;

     

        var series = chart.series.push(new am4charts.PieSeries());
        series.dataFields.value = "count";
        series.dataFields.category = "name";

        // this creates initial animation
        series.hiddenState.properties.opacity = 1;
        series.hiddenState.properties.endAngle = -90;
        series.hiddenState.properties.startAngle = -90;

        chart.legend = new am4charts.Legend();

    }

    function errorFunc() {

    }

}
var variableheight3DPieChart = function () {
    $.ajax({
        type: "GET",
        url: "/AmReport/Variableheight3DPieChart/",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });

    function successFunc(result) {

        am4core.useTheme(am4themes_animated);

        var chart = am4core.create("chartdivGender", am4charts.PieChart3D);


        chart.data = result.data;

        chart.innerRadius = am4core.percent(40);
        chart.depth = 90;

        chart.legend = new am4charts.Legend();
        chart.legend.position = "right";

        var series = chart.series.push(new am4charts.PieSeries3D());
        series.dataFields.value = "count";
        series.dataFields.depthValue = "count";
        series.dataFields.category = "name";

    }

    function errorFunc() {

    }
}