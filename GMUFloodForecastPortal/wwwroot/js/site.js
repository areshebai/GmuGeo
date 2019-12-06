// Write your Javascript code.
var g_mapInstance;
var g_viewRegionInstance;
var g_viewProductInstance;
var g_downloadWindowInstance;
var g_downloadConfirmationWindowInstance;
var g_downloadGridInstance;
var g_ViewFromDateInstance;

var g_downloadRegionInstance;
var g_downloadProductInstance;

function getViewProductInstance(instance) {
    g_viewProductInstance = instance;
    instance["addItem"]('VIIRS 375-m');
    instance["addItem"]('VIIRS 375-m 5-Day');
    instance["addItem"]('ABI 1-km');
    instance["addItem"]('AHI 1-km');
    instance["addItem"]('Joint VIIRS/ABI');
    instance["addItem"]('Joint VIIRS/AHI');
    instance["selectIndex"](0);
}

function getDownloadProductInstance(instance) {
    g_downloadProductInstance = instance;
    instance["addItem"]('VIIRS 375-m');
    instance["addItem"]('VIIRS 375-m 5-Day');
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
    instance["addItem"]('NWS Alaska');
    instance["addItem"]('NWS North East');
    instance["addItem"]('NWS North Central');
    instance["addItem"]('NWS South East');
    instance["addItem"]('NWS Missouri Basin');
    instance["addItem"]('NWS West Gulf');
    instance["addItem"]('NWS North West');
    instance["addItem"]('NWS South West');
    instance["selectIndex"](0);
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
    instance["addItem"]('NWS Alaska');
    instance["addItem"]('NWS North East');
    instance["addItem"]('NWS North Central');
    instance["addItem"]('NWS South East');
    instance["addItem"]('NWS Missouri Basin');
    instance["addItem"]('NWS West Gulf');
    instance["addItem"]('NWS North West');
    instance["addItem"]('NWS South West');
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

function getDownloadConfirmationWindowInstance(instance) {
    g_downloadConfirmationWindowInstance = instance;
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
    var fromDateText = getViewFromDatetimeText();
    var toDateText = getViewToDatetimeText();
    var currentTrans = getViewTransparency();

    $('#PeriodFrom').html(fromDateText);
    $('#PeriodTo').html(toDateText);

    g_mapInstance = createMapInstance("googleMap", parseInt(currentTrans), null, null, null);
}

function createMapInstance(mapElement, transparency, lat, lng, zoom) {
    if (lat === null || lng === null) {
        lat = 0;
        lng = 0;
    }

    if (zoom === null) {
        zoom = 3;
    }

    var mapProp = {
        center: new google.maps.LatLng({ lat: parseFloat(lat), lng: parseFloat(lng) }),
        // maxZoom: 10,
        minZoom: 2,
        zoom: parseInt(zoom),
        zoomControl: true,
        mapTypeId: 'hybrid',
        streetViewControl: false,
        mapTypeControl: true,
        fullscreenControl: false,
        zoomControlOptions: {
            position: google.maps.ControlPosition.RIGHT_BOTOM
        },
        mapTypeControlOptions: {
            style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
            position: google.maps.ControlPosition.RIGHT_TOP
        }
    };

    var map = new google.maps.Map(document.getElementById(mapElement), mapProp);

    map.addListener('tilesloaded', function () {
        var transparency = getViewTransparency();
        $("#" + mapElement).find("img[src*='googleusercontent']").css("opacity", (100 - transparency) / 100);
    });

    map.addListener('mousemove', function (e) {
        mouseMoveOnMap(e);
    });

    google.maps.event.addListener(map, 'dragend', function () {
        var latlng = map.getCenter();
        sessionStorage.setItem("navigation_center_lat", latlng.lat());
        sessionStorage.setItem("navigation_center_lng", latlng.lng());
        sessionStorage.setItem("navigation_zoom", map.getZoom());

        var latLngBounds = map.getBounds();
        var northEast = latLngBounds.getNorthEast();
        var southWest = latLngBounds.getSouthWest();
        sessionStorage.setItem("bounds_north", northEast.lat().toFixed());
        sessionStorage.setItem("bounds_east", northEast.lng().toFixed());
        sessionStorage.setItem("bounds_south", southWest.lat().toFixed());
        sessionStorage.setItem("bounds_west", southWest.lng().toFixed());
    });

    google.maps.event.addListener(map, 'zoom_changed', function () {
        var latlng = map.getCenter();
        sessionStorage.setItem("navigation_center_lat", latlng.lat());
        sessionStorage.setItem("navigation_center_lng", latlng.lng());
        sessionStorage.setItem("navigation_zoom", map.getZoom());
    });

    return map;
}

function mouseMoveOnMap(event) {
    $("#currentLatitude").html(event.latLng.lat().toFixed(3));
    $("#currentLongitude").html(event.latLng.lng().toFixed(3));
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
        preserveViewport: true,
        suppressInfoWindows: false,
        clickable: false
    });

    ctaLayer.setUrl(url);
    ctaLayer.setMap(map);
}

function displayKmls(map, from, to, region, product) {
    $.ajax({
        type: 'GET',
        url: "api/kmls",
        data: {
            from: from,
            to: to,
            region: region,
            product: product
        },
        cache: false,
        success: function (data) {
            $.each(data, function (index, value) {
                if ((value.districtId === 1 || value.districtId === 9 || value.districtId === 136) &&
                    (product === 'VIIRS 375-m' || product === 'VIIRS 375-m 5-Day')) {
                    return true;
                }

                displayKmlLayer(map, value.fullName);
            });
        },
        complete: function () {
            sessionStorage.setItem("navigation_kmlloaded", 1);
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

function GenerateDownloadTask(from, to, region, product, imageFormat, north, south, west, east) {
    $.ajax({
        type: 'GET',
        url: "api/kmls/download",
        data: {
            from: from,
            to: to,
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
            sessionStorage.setItem("download_taskname", data);
            $('#downloadTaskName').html(data);
            sessionStorage.setItem("download_taskstatus", "Scheduled");
            $('#downloadTaskStatus').html("Scheduled");
            // $('#downloadTaskPath').html("ftp://jpssflood.gmu.edu/"+data+"/");
        }
    });
}

function GetDownloadTaskStatus(taskName) {
    $.ajax({
        type: 'GET',
        url: "api/kmls/downloadstatus",
        data: {
            taskName: taskName
        },
        cache: false,
        success: function (data) {
            if (data === 1) {
                $('#downloadTaskStatus').html("Scheduled");
                sessionStorage.setItem("download_taskstatus", "Scheduled");
            } else if (data === 2) {
                $('#downloadTaskStatus').html("Processing");
                sessionStorage.setItem("download_taskstatus", "Processing");
            } else if (data === 3) {
                $('#downloadTaskStatus').html("Completed");
                sessionStorage.setItem("download_taskstatus", "Completed");
            } else {
                $('#downloadTaskStatus').html("Deleted");
                sessionStorage.setItem("download_taskstatus", "Deleted");
            }
        }
    });
}

function getKmlFiles(from, to, region, product, imageFormat, instance, north, south, west, east, zoom) {
    $.ajax({
        type: 'GET',
        url: "api/kmls/block",
        data: {
            from: from,
            to: to,
            region: region,
            product: product,
            north: north,
            south: south,
            west: west,
            east: east,
            zoom: zoom
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