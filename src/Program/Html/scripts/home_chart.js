globalOptions.appId = "10V6WMADM";
//ajax_ex.baseURL = 'http://localhost';
function toData(factor, data) {

    let categories = [];
    let series = [{
        name: '实时值',
        type: data.chart_type,
        data: []
    }];
    if (factor.chart_type == 'pie') {
        for (var key in data) {
            series[0].data.push({ name: key, y: data[key] });
        }
    } else {
        for (var key in data) {
            categories.push(key);
        }
        categories = categories.sort();
        for (var idx = 0; idx < categories.length; idx++) {
            series[0].data.push(data[categories[idx]]);
        }
    }
    return {
        categories: categories,
        series: series
    }
}
var piePlotOptions = {
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
};
var columPlotOptions = {
    series: {
        depth: 0,
        colorByPoint: true
    }
};
var myChart = {
    collect_chart(factor, data) {
        let chartData = toData(factor, data);
        let opt = {
            chart: {
                type: factor.chart_type,
                zoomType: 'x'// 水平缩放
            },
            title: {
                text: factor.title
            },
            tooltip: {
                shared: true,
                valueSuffix: factor.suf
            },
            plotOptions: factor.plotOptions,
            series: chartData.series
        };
        if (factor.chart_type == 'pie') {
            opt.chart.options3d = {
                enabled: true,
                alpha: 45
            };
            opt.series[0].showInLegend = true;
            opt.series[0].dataLabels = {
                enabled: false
            };
            opt.legend = {
                enabled: true
            };
            factor.chart = Highcharts.chart(factor.element, opt);
        } else {
            opt.chart.options3d = {
                enabled: true,
                alpha: 6,
                beta: factor.beta,
                depth: 0,
                beta: 0,
                viewDistance: 25
            };
            opt.yAxis = {
                min: 0,
                //max: factor.max,
                title: {
                    text: factor.suf
                }
            };
            opt.xAxis = {
                categories: chartData.categories,
                crosshairs: true
            };
            opt.legend = {
                enabled: false
            };
            try{
                factor.chart = Highcharts.chart(factor.element, opt);
            }catch(e){
                console.error(e);
            }
        }
    },
    factors: {
        '101': {
            title: '总磷',
            beta: -6,
            code: '101',
            max: 1000,
            element: 'c101',
            suf: 'mg/L',
            plotOptions: columPlotOptions,
            chart_type: 'column'
        },
        '001': {
            title: 'PH值',
            code: '001',
            beta: -6,
            max: 14,
            value: 0,
            element: 'c001',
            suf: '',
            plotOptions: columPlotOptions,
            chart_type: 'column'
        },
        '011': {
            title: 'COD',
            beta: 6,
            code: '011',
            max: 800,
            value: 0,
            element: 'c011',
            suf: 'mg/L',
            plotOptions: columPlotOptions,
            chart_type: 'column'
        },
        '060': {
            title: '氨氮',
            beta: -6,
            code: '060',
            max: 1000,
            value: 0,
            element: 'c060',
            suf: 'mg/L',
            plotOptions: columPlotOptions,
            chart_type: 'column'
        },
        'B01': {
            title: '污水',
            beta: 6,
            code: 'B01',
            max: 1000,
            value: 0,
            element: 'cB01',
            suf: 'L/s',
            plotOptions: columPlotOptions,
            chart_type: 'column'
        },
        'B21': {
            title: '累计流量',
            beta: 6,
            code: 'B21',
            max: 1000,
            value: 0,
            element: 'cB21',
            suf: 'm³',
            plotOptions: columPlotOptions,
            chart_type: 'column'
        },
        'Alarm': {
            title: '报警企业',
            beta: 0,
            code: 'Alarm',
            max: 1000,
            value: 0,
            element: 'cAlarm',
            suf: '次',
            plotOptions: piePlotOptions,
            chart_type: 'pie'
        },
        'FactorAlarm': {
            title: '因子报警',
            beta: 0,
            code: 'FactorAlarm',
            max: 1000,
            value: 0,
            element: 'cFactor',
            suf: '次',
            plotOptions: piePlotOptions,
            chart_type: 'pie'
        },
        'HourAlarm': {
            title: '报警走势',
            beta: 0,
            code: 'HourAlarm',
            max: 100,
            value: 0,
            element: 'cHour',
            suf: '次',
            plotOptions: {
            },
            chart_type: 'area'
        },
        'Inline': {
            title: '企业在线率',
            beta: 0,
            code: 'Inline',
            max: 100,
            value: 0,
            element: 'cInline',
            suf: '个',
            plotOptions: piePlotOptions,
            chart_type: 'pie'
        },
        'State': {
            title: '企业报警率',
            beta: 0,
            code: 'State',
            max: 100,
            value: 0,
            element: 'cState',
            suf: '个',
            plotOptions: piePlotOptions,
            chart_type: 'pie'
        }
    },
    showChart(type, data) {
        var that = this;
        let factor = this.factors[type];
        if (!factor) {
            console.log(`因子${type}不存在`);
            return;
        }
        if (!factor.chart) {
            that.collect_chart(factor, data);
            return;
        }
        let chartData = toData(factor, data);

        factor.chart.update({
            xAxis: {
                categories: chartData.categories,
                crosshairs: true
            },
            series: chartData.series
        });
    }
};

extend_data({
    list: {
        map: {},
        filter: {},
        rows: []
    }
});
 
var ws = new MessageWebsocket({
    host: 'ws://www.zeroteam.com.cn:1808',//
    rooms: ['realCollect', 'real', 'device'],
    onSocketMessage(room, type, msg) {
        //console.log(`${room} - ${type}`);
        switch (room) {
            case 'device':
                showDevice(msg);
                break;
            case 'realCollect':
                showDeviceLatest(msg);
                break;
            case 'real':
                myChart.showChart(type, msg);
                break;
        }
    }
});
var devices = {};


vue_option.ready(v => {
    ajax_load('导航数据', `/iot/monitorDevice/v1/edit/list`, {}, d => {
        for (var i = 0; i < d.rows.length; i++) {
            try {
                let dev = d.rows[i];
                if (!dev.point)
                    continue;
                devices[dev.deviceCode] = dev;
                dev.values = {
                    recordDate: new Date().format("yyyy-MM-dd hh:mm:ss")
                };
                if (dev.point)
                    addMapPoint(dev);
            } catch (error) {
                console.log(error);
            }
        }
        ws.open();
    });
});


function showDeviceLatest(msg) {
    if (msg.deviceCode)
        return;
    var dev = devices[msg.deviceCode];
    if (!dev)
        return;
    dev.recordDate = NewDate(msg.collectionTime).format("yyyy-MM-dd hh:mm:ss");
    if (dev.point)
        showDeviceDialog(dev);
    return devices;
}

function copy(src, row) {
    if (typeof row.value001 !== 'undefined')
        src.value001 = row.value001;
    if (typeof row.value011 !== 'undefined')
        src.value011 = row.value011;
    if (typeof row.valueB01 !== 'undefined')
        src.valueB01 = row.valueB01;
    if (typeof row.valueB21 !== 'undefined')
        src.valueB21 = row.valueB21;
    if (typeof row.values !== 'undefined')
        src.values = row.values;
    if (typeof row.recordDate !== 'undefined')
        src.recordDate = row.recordDate;
}
function showDevice(data) {
    if (!data.deviceCode)
        return;
    if (vueObject.list.now)
        vueObject.list.now.class = 'data';
    if (!vueObject.list.map[data.deviceCode]) {
        checkListData(data);
        vueObject.list.map[data.deviceCode] = data;
        vueObject.list.rows.push(data);
    }
    else {
        copy(vueObject.list.map[data.deviceCode], data);
    }
    vueObject.list.now = vueObject.list.map[data.deviceCode];
    vueObject.list.now.class = 'now';

    var dev = devices[data.deviceCode];
    if (!dev)
        return;
    dev.values = data;

    if (dev.point)
        showDeviceDialog(dev);
}

function checkListData(row) {
    if (typeof row.id == 'undefined')
        row.id = row.id;
    if (typeof row.deviceId == 'undefined')
        row.deviceId = row.deviceId;
    if (typeof row.dataType == 'undefined')
        row.dataType = row.dataType;
    if (typeof row.deviceCode == 'undefined')
        row.deviceCode = row.deviceCode;
    if (typeof row.deviceName == 'undefined')
        row.deviceName = '';
    if (typeof row.organizationId == 'undefined')
        row.organizationId = 0;
    if (typeof row.organizationCode == 'undefined')
        row.organizationCode = '';
    if (typeof row.organizationName == 'undefined')
        row.organizationName = '';
    if (typeof row.recordCode == 'undefined')
        row.recordCode = '';
    if (typeof row.value001 == 'undefined')
        row.value001 = 0;
    if (typeof row.value011 == 'undefined')
        row.value011 = 0;
    if (typeof row.valueB01 == 'undefined')
        row.valueB01 = 0;
    if (typeof row.valueB21 == 'undefined')
        row.valueB21 = 0;
    if (typeof row.values == 'undefined')
        row.values = '';
    if (typeof row.recordDate == 'undefined')
        row.recordDate = '';
}