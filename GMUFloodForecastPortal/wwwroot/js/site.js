// Write your Javascript code.
var g_mapInstance;
var g_viewRegionInstance;
var g_viewProductInstance;
var g_downloadWindowInstance;
var g_downloadGridInstance;
var g_ViewFromDateInstance;

var g_downloadRegionInstance;
var g_downloadProductInstance;

function getViewProductInstance(instance) {
    g_viewProductInstance = instance;
    instance["addItem"]('VIIRS 375-m');
    instance["addItem"]('ABI 1-km');
    instance["addItem"]('AHI 1-km');
    instance["addItem"]('Joint VIIRS/ABI');
    instance["addItem"]('Joint VIIRS/AHI');
    instance["selectIndex"](0);
}

function getDownloadProductInstance(instance) {
    g_downloadProductInstance = instance;
    instance["addItem"]('VIIRS 375-m');
    instance["addItem"]('ABI 1-km');
    instance["addItem"]('AHI 1-km');
    instance["addItem"]('Joint VIIRS/ABI');
    instance["addItem"]('Joint VIIRS/AHI');
    instance["selectIndex"](0);
}

function getViewRegionInstance(instance) {
    g_viewRegionInstance = instance;
    instance["addItem"]('All');
    instance["addItem"]('North America');
    instance["addItem"]('South America');    
    instance["addItem"]('Europe');
    instance["addItem"]('Asia');
    instance["addItem"]('Africa');
    instance["addItem"]('Australia');
    instance["selectIndex"](1);
}

function getDownloadRegionInstance(instance) {
    g_downloadRegionInstance = instance;
    instance["addItem"]('All');
    instance["addItem"]('North America');
    instance["addItem"]('South America');
    instance["addItem"]('Europe');
    instance["addItem"]('Asia');
    instance["addItem"]('Africa');
    instance["addItem"]('Australia');
    instance["selectIndex"](1);
}

function getImageTypeInstance(instance) {
    instance["addItem"]('GeoTiff');
    instance["addItem"]('HDF4');
    instance["addItem"]('PNG');
    instance["addItem"]('ShapeFile');
    instance["selectIndex"](1);
}

function getDownloadImageTypeInstance(instance) {
    instance["addItem"]('GeoTiff');
    instance["addItem"]('HDF4');
    instance["addItem"]('PNG');
    instance["addItem"]('ShapeFile');
    instance["selectIndex"](1);
}

function getDownloadWindowInstance(instance) {
    g_downloadWindowInstance = instance;
}

function getSelectedViewStep() {
    var isHourly = $('#viewStepHourly').attr('aria-checked');
    var isDaily = $('#viewStepDaily').attr('aria-checked');
    var is5Day = $('#viewStep5Day').attr('aria-checked');

    if (isHourly === 'true') {
        return 1;
    }
    else if (isDaily === 'true') {
        return 2;
    }
    else if (is5Day === 'true') {
        return 3;
    }
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear(),
        hour = '' + d.getHours(),
        minute = '' + d.getMinutes(),
        second = '' + d.getSeconds();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    if (hour.length < 2) hour = '0' + hour;
    if (minute.length < 2) minute = '0' + minute;
    if (second.length < 2) second = '0' + second;

    return [month, day, year].join('/') + ' ' + [hour, minute, second].join(':');
}

function getViewFromDatetimeText() {
    var fromText = $('#viewFromDatetime').attr('aria-valuetext');
    return fromText;
}

function getViewToDatetimeText() {
    var toText = $('#viewToDatetime').attr('aria-valuetext');
    return toText;
}

function getdownloadFromDatetimeText() {
    var fromText = $('#downloadFromDatetime').attr('aria-valuetext');
    return fromText;
}

function getdownloadToDatetimeText() {
    var toText = $('#downloadToDatetime').attr('aria-valuetext');
    return toText;
}

function getViewFromDatetime() {
    var fromDateText = $('#viewFromDatetime').attr('aria-valuetext');
    return new Date(fromDateText);
}

function getViewToDatetime() {
    var toDateText = $('#viewToDatetime').attr('aria-valuetext');
    return new Date(toDateText);
}

function getSelectedViewRegion() {
    var region = $('#viewRegion').children(':input').attr('value');
    return region;
}

function getSelectedDownloadRegion() {
    var region = $('#downloadRegion').children(':input').attr('value');
    return region;
}

function getViewTransparency() {
    var trans = $('#viewTransparency')[0].value;
    return trans;
}

function getSelectedViewProduct() {
    var product = $('#viewProduct').children(':input').attr('value');
    return product;
}

function getSelectedDownloadProduct() {
    var product = $('#downloadProduct').children(':input').attr('value');
    return product;
}

function getDownloadCoordinateNorth() {
    var trans = $('#downloadCoordinateNorth')[0].value;
    return trans;
}

function getDownloadCoordinateSouth() {
    var trans = $('#downloadCoordinateSouth')[0].value;
    return trans;
}

function getDownloadCoordinateWest() {
    var trans = $('#downloadCoordinateWest')[0].value;
    return trans;
}

function getDownloadCoordinateEast() {
    var trans = $('#downloadCoordinateEast')[0].value;
    return trans;
}

function getDownloadImageFormat() {
    var format = $('#downloadImageFormat').children(':input').attr('value');
    return format;
}

function getDownloadImageFormat2() {
    var format = $('#downloadImageFormat2').children(':input').attr('value');
    return format;
}

function initMap() {
    var viewStep = getSelectedViewStep();
    var selectedRegion = getSelectedViewRegion();
    var selectedProduct = getSelectedViewProduct();
    var fromDateText = getViewFromDatetimeText();
    var toDateText = getViewToDatetimeText();
    var currentTrans = getViewTransparency();

    $('#PeriodFrom').html(fromDateText);
    $('#PeriodTo').html(toDateText);

    var map = createMapInstance("googleMap", parseInt(currentTrans));

    var ctaLayer = new google.maps.KmlLayer("https://jpssflood.gmu.edu/kmls/yukon_5_6_15.kmz");
    ctaLayer.setMap(map);

    // displayKmls(map, fromDateText, toDateText, viewStep, selectedRegion, selectedProduct);

}

function createMapInstance(mapElement, transparency) {
    var mapProp = {
        center: new google.maps.LatLng({ lat: 40, lng: 180 }),
        maxZoom: 10,
        minZoom: 2,
        zoom: 3,
        zoomControl: true,
        mapTypeId: 'hybrid',
        streetViewControl: false,
        mapTypeControl: true,
        zoomControlOptions: {
            position: google.maps.ControlPosition.RIGHT_BOTTOM
        },
        mapTypeControlOptions: {
            style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
            position: google.maps.ControlPosition.LEFT_TOP
        }
    };

    var map = new google.maps.Map(document.getElementById(mapElement), mapProp);

    map.addListener('tilesloaded', function () {
        $("#" + mapElement).find("img[src*='googleusercontent']").css("opacity", transparency / 100);
    });

    map.addListener('mousemove', function (e) {
        mouseMoveOnMap(e);
    });

    google.maps.event.addListener(map, 'dragend', function () {
    });

    google.maps.event.addListener(map, 'zoom_changed', function () {
    });

    return map;
}

function mouseMoveOnMap(event) {
    $("#currentLatitude").html(event.latLng.lat());
    $("#currentLongitude").html(event.latLng.lng());
}

function dragOnMap() {
    var bounds = map.getBounds();
    var southWest = bounds.getSouthWest();
    var northEast = bounds.getNorthEast();

    alert(southWest.lat());
    alert(northEast);
}

function zoomChangedOnMap(event) {
    var currentZoom = map.getZoom();
    alert('zoom_changed');
    alert(currentZoom);
}

function displayKmlLayer(map, url) {
    var ctaLayer = new google.maps.KmlLayer({
        preserveViewport: false,
        suppressInfoWindows: false,
        clickable: false
    });

    ctaLayer.setUrl(url);
    ctaLayer.setMap(map);
}

function AddKmlLayer(curhour, map, suppressInfoWindowsEnabled) {
    var apiUrl = "api/Kml/" + curhour.toString();
    $.ajax({
        type: 'GET',
        url: apiUrl,
        cache: false,
        success: function (data) {
            $.each(data, function (index, value) {
                displayKmlLayer(map, value);
            });
        }
    });
}

function displayKmls(map, from, to, step, region, product) {
    $.ajax({
        type: 'GET',
        url: "api/kmls",
        data: {
            from: from,
            to: to,
            step: step,
            region: region,
            product: product
        },
        cache: false,
        success: function (data) {
            $.each(data, function (index, value) {
                if (value.districtId !== 9) {
                    displayKmlLayer(map, value.fullName);
                }
            });
        }
    });
}

function getLatestDataDate() {
    $.ajax({
        type: 'GET',
        url: "api/kmls/LatestDataDate",
        cache: false,
        success: function (data) {
            $('#PeriodFrom').html(data);
        }
    });
}

function GenerateDownloadTask(from, to, step, region, product, imageFormat, north, south, west, east) {
    $.ajax({
        type: 'GET',
        url: "api/kmls/download",
        data: {
            from: from,
            to: to,
            step: step,
            region: region,
            north: north,
            south: south,
            west: west,
            east: east,
            product: product,
            format: imageFormat
        },
        cache: false,
        success: function (data) {
            $('#downloadTaskName').html(data);
            $('#downloadTaskStatus').html("Scheduled");
            $('#downloadTaskPath').html("ftps://jpssflood.gmu.edu/Download/"+data+"/");
            
        }
    });
}


function getKmlFiles(from, to, step, region, product, imageFormat, instance) {
    $.ajax({
        type: 'GET',
        url: "api/kmls",
        data: {
            from: from,
            to: to,
            step: step,
            region: region,
            product: product
        },
        cache: false,
        success: function (data) {
            $.each(data, function (index, value) {
                var fileFullName = "";
                if (imageFormat === 'GeoTiff') {
                    fileFullName = value.fullName.replace(".kml", ".tif.zip");
                }
                else if (imageFormat === 'HDF4') {
                    fileFullName = value.fullName.replace(".kml", ".hdf.zip");
                }
                else if (imageFormat === 'PNG') {
                    fileFullName = value.fullName.replace(".kml", ".kml.zip");
                }
                else if (imageFormat === 'ShapeFile') {
                    fileFullName = value.fullName.replace(".kml", ".zip");
                }
                else {
                    alert('Download: Unsupported file format.');
                }
                instance['addrow'](null, {
                    Index: index + 1, Address: value.shortName, Link: '<a href="' + fileFullName + '">Download</a>'});
            });
        }
    });
}

function displayKmlById(map) {
    alert('Not expected to be call!');
    $.ajax({
        type: 'GET',
        url: "api/kmls/kml",
        data: {
            id: 5
        },
        cache: false,
        success: function (data) {
            $.each(data, function (index, value) {
                alert(value);
                displayKmlLayer(map, value);
            });
        }
    });
}