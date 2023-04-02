
window.onload = function () {

    var chart = new CanvasJS.Chart("chartContainer", {
        animationEnabled: true,
        theme: "light3", // "light1", "light2", "dark1", "dark2"
        title: {
            text: "Top Most Ticket Viewed "
        },
        axisY: {
            title: "Growth Rate (in %)",
            suffix: "%"
        },
        axisX: {
            title: "Tickets"
        },
        data: [{
            type: "column",
            yValueFormatString: "#,##0.0#\"%\"",
            dataPoints: [
                { label: "Java", y: 7.1 },
                { label: "Devops", y: 6.70 },
                { label: "Salesforce", y: 5.00 },
                { label: "Asp .Net", y: 2.50 },
                { label: "Frontend", y: 2.30 },

                { label: "Backend", y: 1.80 },
                { label: "FullStack", y: 1.60 },
                { label: "Testing", y: 1.60 }

            ]
        }]
    });
    chart.render();

}
