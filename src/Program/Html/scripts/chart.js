
Highcharts.setOptions({
    global: {
        useUTC: false
    },
    exporting: {
        enabled: false
    },
    credits: {
        enabled: false
    }
});


function activeLastPointToolip(chart) {
    var points = chart.series[0].points;
    chart.tooltip.refresh(points[points.length - 1]);
}


function update_chart(chart, value) {
    var point = chart.series[0].points[0];
    point.update(value);
}

function gauge_chart_option(max, option) {
    var op = extend(option, gauge_chart_def);
    op.yAxis.max = max;
    op.yAxis.plotBands = [
        {
            from: 0,
            to: max * 0.4,
            color: '#55BF3B' // green
        }, {
            from: max * 0.4,
            to: max * 0.8,
            color: '#DDDF0D' // yellow
        }, {
            from: max * 0.8,
            to: max,
            color: '#DF5353' // red
        }
    ]
    return op;
}
var gauge_chart_def = {
    credits: {
        enabled: false
    },
    chart: {
        type: 'gauge',
        plotBackgroundColor: null,
        plotBackgroundImage: null,
        plotBorderWidth: 0,
        plotShadow: false
    },
    pane: {
        startAngle: -150,
        endAngle: 150,
        background: [
            {
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#FFF'],
                        [1, '#333']
                    ]
                },
                borderWidth: 0,
                outerRadius: '109%'
            }, {
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#333'],
                        [1, '#FFF']
                    ]
                },
                borderWidth: 1,
                outerRadius: '107%'
            }, {
                // default background

            }, {
                backgroundColor: '#DDD',
                borderWidth: 0,
                outerRadius: '105%',
                innerRadius: '103%'
            }
        ]
    },
    yAxis: {
        min: 0,
        max: 0,
        minorTickInterval: 'auto',
        minorTickWidth: 1,
        minorTickLength: 20,
        minorTickPosition: 'inside',
        minorTickColor: '#666',
        tickPixelInterval: 20,
        tickWidth: 2,
        tickPosition: 'inside',
        tickLength: 20,
        tickColor: '#666',
        labels: {
            step: 2,
            rotation: 'auto'
        }
    }
};
function solidgauge_chart_option(max, option) {
    var op = extend(option, solidgauge_chart_def);
    op.yAxis.max = max;
    return op;
}
var solidgauge_chart_def = {
    credits: {
        enabled: false
    },
    chart: {
        type: 'solidgauge'
    },
    pane: {
        center: ['50%', '85%'],
        size: '140%',
        startAngle: -90,
        endAngle: 90,
        background: {
            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || '#EEE',
            innerRadius: '60%',
            outerRadius: '100%',
            shape: 'arc'
        }
    },
    yAxis: {
        min: 0,
        max: 0,
        stops: [
            [0.5, '#55BF3B'], // green
            [0.75, '#DDDF0D'], // yellow
            [0.9, '#DF5353'] // red
        ],
        lineWidth: 0,
        minorTickInterval: null,
        tickPixelInterval: 400,
        tickWidth: 0,
        formatter: function () {
            return this.value.toFixed(1);
        }
    },
    xAxis: {
        formatter: function () {
            return this.value.toFixed(1);
        }
    },
    plotOptions: {
        solidgauge: {
            dataLabels: {
                y: 5,
                borderWidth: 0,
                useHTML: true
            }
        }
    }
};

function line_chart_option(option) {

    return extend(option, line_chart_def);
}
var line_chart_def = {
    credits: {
        enabled: false
    },
    chart: {
        type: 'spline',
        marginRight: 10
    },
    xAxis: {
        type: 'datetime',
        tickPixelInterval: 150
    },
    plotOptions: {
        spline: {
            lineWidth: 3,
            states: {
                hover: {
                    lineWidth: 5
                }
            },
            marker: {
                enabled: false
            }
        }
    },
    tooltip: {
        formatter: function () {
            return '<b>' +
                this.series.name +
                '</b><br/>' +
                Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) +
                '<br/>' +
                Highcharts.numberFormat(this.y, 2);
        }
    },
    legend: {
        enabled: false
    }
};



var myChart = {
    datas: {},
    collectItems: {},
    onSericeClick(type,category,name){

    },
    formatLegend(serice){

    },
    redraw() {
        this.collectItems = {};
        for (var type in this.datas) {
            this.showChart(type, this.datas[type]);
        }
    },
    showChart(type, data) {
        var that = this;
        let collectItem = this.collectItems[type];
        if (!collectItem) {
            collectItem = this.collectItems[type] = {
                title: type,
                beta: 0,
                code: type,
                max: 100,
                value: 0,
                element: type,
                suf: '次',
                chart_type: 'column'
            }
        }
        let chartData = this.toData(collectItem, data);
        if (!chartData)
            return;
        if (!collectItem.chart) {
            that.createChart(collectItem, chartData);
        } else {
            collectItem.chart.update({
                xAxis: {
                    categories: chartData.categories
                },
                series: chartData.series
            });
        }
    },
    createChart(collectItem, data) {
        let div = document.getElementById(collectItem.element);
        div.innerHTML = '';
        var that = this;
        try {
            collectItem.chart = Highcharts.chart(collectItem.element, {
                credits: {
                    enabled: false // 禁用版权信息
                },
                chart: {
                    type: collectItem.chart_type,
                    marginRight: 100,
                    marginLeft: 80,
                    options3d: {
                        enabled: true,
                        alpha: 6,
                        beta: 0,
                        depth: 0,
                        beta: 0,
                        viewDistance: 0
                    }
                },
                plotOptions: {
                    column: {
                        depth: 25
                    }
                },
                legend: {
                    align: 'right', //水平方向位置
                    verticalAlign: 'top', //垂直方向位置
                    layout: 'vertical',
                    floating: true,
                    x: 0, //距离x轴的距离
                    y: 0, //距离Y轴的距离
                    labelFormatter: function () {
                        return that.formatLegend(this);
                    }
                },
                title: {
                    style: { display: 'none' }
                },
                tooltip: {
                    shared: true,
                    valueSuffix: collectItem.suf
                },
                yAxis: {
                    min: 0,
                    allowDecimals: false, //是否允许刻度有小数
                    title: {
                        text: collectItem.suf
                    }
                },
                xAxis: {
                    categories: data.categories,
                },
                series: data.series
            });
        } catch (e) {
            console.error(e);
        }
    },
    toData(collectItem, data) {
        if (!data.categories || !document.getElementById(collectItem.element))
            return null;
        let series = [];
        var that = this;
        for (var idx = 0; idx < data.series.length; idx++) {
            let serie = data.series[idx];
            if (!serie.name)
                continue;
            series.push(serie);
            serie.legend = { enabled: true };
            serie.showInLegend = true;
            serie.events = {
                click: function (event) {
                    that.onSericeClick(collectItem.code, event.point.category, this.name);
                }
            };
        }
        this.datas[collectItem.code] = {
            categories: data.categories,
            series: series
        };
        return this.datas[collectItem.code];

    },
    piePlotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            innerSize: 80,
            depth: 45,
            dataLabels: {
                enabled: true,
                format: '{point.name}'
            }
        }
    },
    columPlotOptions: {
        series: {
            depth: 0,
            colorByPoint: true
        }
    }
};

