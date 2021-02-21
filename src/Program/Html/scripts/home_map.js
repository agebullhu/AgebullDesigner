// 百度地图API功能	
var cityName = '福建省沙县';
var map = new BMapGL.Map("allmap");    // 创建Map实例

map.centerAndZoom(cityName, 11);
//map.setTilt(50);
map.enableScrollWheelZoom();

var bd = new BMapGL.Boundary();
bd.get(cityName, function (rs) {
	var count = rs.boundaries.length; //行政区域的点有多少个

	for (var i = 0; i < count; i++) {
		var path = [];
		str = rs.boundaries[i].replace(' ', '');
		points = str.split(';');
		for (var j = 0; j < points.length; j++) {
			var lng = points[j].split(',')[0];
			var lat = points[j].split(',')[1];
			path.push(new BMapGL.Point(lng, lat));
		}
		map.addOverlay(new BMapGL.Prism(path, 5000, {
			topFillColor: '#5679ea',
			topFillOpacity: 0.5,
			sideFillColor: '#5679ea',
			sideFillOpacity: 0.9
		}));
	}
});

var preMarker = null;
function defIcon() {
	return new BMapGL.Icon("/images/def.png", new BMapGL.Size(32, 32), {});
}
function activeIcon() {
	return new BMapGL.Icon("/images/active.png", new BMapGL.Size(32, 32), {});
}
function showDeviceDialog(dev) {
	if (preMarker)
		preMarker.setIcon(defIcon());
	preMarker = dev.marker;
	preMarker.setIcon(activeIcon());
}
function formatTime(time) {
	return !time || time === '0001-01-01T00:00:00' ? '-' : NewDate(time).format("yyyy-MM-dd hh:mm:ss");
}
function valueText(values,key){
	return values && values[key] ? values[key] : '-';
}
function addMapPoint(dev) {
	var pot = dev.point.split(',');
	dev.point = new BMapGL.Point(pot[0], pot[1]);
	dev.marker = new BMapGL.Marker(dev.point, {
		title: dev.organizationName,
		icon: defIcon()
	});  // 创建标注
	map.addOverlay(dev.marker);// 将标注添加到地图中

	dev.marker.addEventListener("click", function (e) {
		// 创建信息窗口对象 
		var infoWindow = new BMapGL.InfoWindow(`
		时　　　间 : ${formatTime(dev.values.recordDate)}<br/>
		酸　碱　度 : ${valueText(dev.values,"value001")}<br/>
		化学需氧量 : ${valueText(dev.values,"value011")}<br/>
		污　　　水 : ${valueText(dev.values,"valueB01")}<br/>
		氨　　　氮 : ${valueText(dev.values.values,"value060")}<br/>
		总　　　磷 : ${valueText(dev.values.values,"value101")}`, {
			width: 250,     // 信息窗口宽度
			height: 180,     // 信息窗口高度
			title: `${dev.values.organizationName}-${dev.values.deviceName}`, // 信息窗口标题
			enableMessage: false//设置允许信息窗发送短息
		});
		map.openInfoWindow(infoWindow, dev.point); //开启信息窗口
	});
}