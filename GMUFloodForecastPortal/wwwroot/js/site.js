// Write your Javascript code.
var g_mapInstance;
var g_viewRegionInstance;
var g_viewProductInstance;
var g_downloadWindowInstance;
var g_downloadGridInstance;

function getViewProductInstance(instance) {
    g_viewProductInstance = instance;
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
    instance["addItem"]('East Asia');
    instance["addItem"]('Europe');
    instance["addItem"]('North America');
    instance["addItem"]('Source America');
    instance["selectIndex"](0);
}

function getImageTypeInstance(instance) {
    instance["addItem"]('GeoTiff');
    instance["addItem"]('HDF4');
    instance["addItem"]('PNG');
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

function getSelectedViewProduct() {
    var product = $('#viewProduct').children(':input').attr('value');
    return product;
}

function getViewTransparency() {
    var trans = $('#viewTransparency')[0].value;
    return trans;
}

function getDownloadImageFormat() {
    var format = $('#downloadImageFormat').children(':input').attr('value');
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
    displayKmls(map, fromDateText, toDateText, viewStep, selectedRegion, selectedProduct);

    /*
    hour = 0;
    var map = createMapInstance("googleMap", 50);

    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_210000_2.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_210000_3.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_210000_4.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_190000_5.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_180000_6.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_160000_8.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_180000_7.kml');
    //displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_230000_9.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_230000_10.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_210000_11.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_210000_12.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_180000_13.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_180000_14.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_160000_15.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_160000_16.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_210000_17.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_190000_18.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_190000_19.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_180000_20.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_160000_21.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_210000_22.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_190000_23.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_190000_24.kml');
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/VIIRS/cspp-viirs-flood-globally_20180815_170000_25.kml');
    */
}

function createMapInstance(mapElement, transparency) {
    var mapProp = {
        center: new google.maps.LatLng({ lat: 0, lng: 0 }),
        maxZoom: 10,
        minZoom: 2,
        zoom: 5,
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
    })

    map.addListener('mousemove', function (e) {
        mouseMoveOnMap(e)
    });

    return map;
}

function displayKmlLayer(map, url) {
    var ctaLayer = new google.maps.KmlLayer({
        preserveViewport: false,
        suppressInfoWindows: false
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
                displayKmlLayer(map, value.fullName);
            });
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
                    fileFullName = value.fullName.replace(".kml", ".tif");
                }
                else if (imageFormat === 'HDF4') {
                    fileFullName = value.fullName.replace(".kml", ".hdf");
                }
                else if (imageFormat === 'PNG') {
                    fileFullName = value.fullName.replace(".kml", ".png");
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

function mouseMoveOnMap(event) {
    $("#currentLatitude").html(event.latLng.lat());
    $("#currentLongitude").html(event.latLng.lng());
    $("#currentDistrict").html('48');
}