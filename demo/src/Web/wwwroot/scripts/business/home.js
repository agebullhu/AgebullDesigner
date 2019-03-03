extend_data({
    col: {
        app: 0,
        reg: 0,
        see: 0,
        wait: 0,
        line_pay: 0,
        line_money: 0.00
    }
});
var vm = new Vue(vue_option);



function on_mq_push(msg) {
    switch (msg.sub) {
        case "Summary":
            vue_option.data.col = msg.data;
            break;
        case "LineUp":
            real_line(msg.data);
            break;
        case "Ten":
            ten_report(msg.data);
            break;
    }
}
var mq_socket = new ws({
    address: "ws://" + location.host + "/mq",
    sub: "real",
    onmessage: on_mq_push
});
mq_socket.open();
var tenChart = null;
function updateChart(data, oldData) {

    for (var i = 0; i < data.length; i++) {
        if (oldData.length <= i)
            oldData.push(data[i]);
        else {
            oldData[i].update(data[i]);
        }
    }
}
function ten_report(data) {
    if (tenChart) {
        updateChart(data.series[0].data, tenChart.series[0].data);
        updateChart(data.series[1].data, tenChart.series[1].data);
        return;
    }
    tenChart = Highcharts.chart('container1', {
        chart: {
            zoomType: 'xy'
        },
        title: {
            text: '近十日就诊人员形势图'
        },
        xAxis: [{
            categories: data.categories,
            crosshair: true
        }],
        yAxis: [{ // Primary yAxis
            labels: {
                format: '{value} 人次',
                style: {
                    color: Highcharts.getOptions().colors[1]
                }
            },
            title: {
                text: '挂号',
                style: {
                    color: Highcharts.getOptions().colors[1]
                }
            },
            allowDecimals: false
        }, { // Secondary yAxis
            title: {
                text: '就诊',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            },
            labels: {
                format: '{value} 人次',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            },
            opposite: true,
            allowDecimals: false
        }],
        tooltip: {
            shared: true
        },
        legend: {
            layout: 'vertical',
            align: 'left',
            x: 120,
            verticalAlign: 'top',
            y: 100,
            floating: true,
            backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
        },
        series: data.series
    });
}
var realChart = null;
function real_line(data) {
    //if (realChart) {
    //    {
    //        realChart = Highcharts.chart('container', {
    //            xAxis: {
    //                categories: data.categories,
    //                crosshair: true
    //            },
    //            series: [{
    //                name: '排队',
    //                data: data.series
    //            }]
    //        });
    //    }
    //    return;
    //}
    realChart = Highcharts.chart('container', {
        chart: {
            type: 'column'
            //,options3d: {
            //    enabled: true,
            //    alpha: 5,
            //    beta: -4,
            //    depth: 100
            //}
        },
        title: {
            text: '各科室实时患者分布'
        },
        xAxis: {
            categories: data.categories,
            crosshair: true
        },
        yAxis: [{
            labels: {
                format: '{value} 人'
            },
            title: {
                text: '排队/诊中'
            },
            allowDecimals: false
        }],
        plotOptions: {
            column: {
                borderWidth: 0
            },
            series: {
                animation: false
            }
        },
        series: [{
            name: '排队/诊中',
            animation: false,
            data: data.series
        }]
    });
}