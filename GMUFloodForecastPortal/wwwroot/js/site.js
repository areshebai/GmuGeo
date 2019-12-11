// Write your Javascript code.
var g_mapInstance;
var g_viewRegionInstance;
var g_viewNWSDomainInstance;
var g_viewCountryInstance;
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

function getViewNWSDomainInstance(instance) {
    g_viewNWSDomainInstance = instance;
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

function getViewCountryInstance(instance) {
    g_viewCountryInstance = instance;
    instance["addItem"]('Afghanistan');
    instance["addItem"]('Albania');
    instance["addItem"]('Algeria');
    instance["addItem"]('Andorra');
    instance["addItem"]('Angola');
    instance["addItem"]('Argentina');
    instance["addItem"]('Armenia');
    instance["addItem"]('Australia');
    instance["addItem"]('Austria');
    instance["addItem"]('Azerbaijan');
    instance["addItem"]('Azores');
    instance["addItem"]('Bahrain');
    instance["addItem"]('Bangladesh');
    instance["addItem"]('Belarus');
    instance["addItem"]('Belgium');
    instance["addItem"]('Benin');
    instance["addItem"]('Bermuda');
    instance["addItem"]('Bhutan');
    instance["addItem"]('Bolivia');
    instance["addItem"]('Bosnia and Herzegovina');
    instance["addItem"]('Botswana');
    instance["addItem"]('Brazil');
    instance["addItem"]('Brunei Darussalam');
    instance["addItem"]('Bulgaria');
    instance["addItem"]('Burkina Faso');
    instance["addItem"]('Burundi');
    instance["addItem"]('Cabo Verde');
    instance["addItem"]('Cambodia');
    instance["addItem"]('Cameroon');
    instance["addItem"]('Canada');
    instance["addItem"]('Central African Republic');
    instance["addItem"]('Central America');
    instance["addItem"]('Chad');
    instance["addItem"]('Chile');
    instance["addItem"]('China');
    instance["addItem"]('Christmas Island');
    instance["addItem"]('Clipperton');
    instance["addItem"]('Cocos Islands');
    instance["addItem"]('Colombia');
    instance["addItem"]('Comoros');
    instance["addItem"]('Congo');
    instance["addItem"]('Congo DRC');
    instance["addItem"]('Croatia');
    instance["addItem"]('Cyprus');
    instance["addItem"]('Czech Republic');
    instance["addItem"]('Denmark');
    instance["addItem"]('Djibouti');
    instance["addItem"]('Ecuador');
    instance["addItem"]('Egypt');
    instance["addItem"]('Equatorial Guinea');
    instance["addItem"]('Eritrea');
    instance["addItem"]('Estonia');
    instance["addItem"]('Ethiopia');
    instance["addItem"]('Falkland Islands');
    instance["addItem"]('Faroe Islands');
    instance["addItem"]('Fiji');
    instance["addItem"]('Finland');
    instance["addItem"]('Gabon');
    instance["addItem"]('Gambia');
    instance["addItem"]('Georgia');
    instance["addItem"]('Germany');
    instance["addItem"]('Ghana');
    instance["addItem"]('Gibraltar');
    instance["addItem"]('Glorioso Islands');
    instance["addItem"]('Greece');
    instance["addItem"]('Greenland');
    instance["addItem"]('Guernsey');
    instance["addItem"]('Guinea');
    instance["addItem"]('Guinea-Bissau');
    instance["addItem"]('Hungary');
    instance["addItem"]('Iceland');
    instance["addItem"]('India');
    instance["addItem"]('Indonesia');
    instance["addItem"]('Iran');
    instance["addItem"]('Iraq');
    instance["addItem"]('Ireland');
    instance["addItem"]('Isle of Man');
    instance["addItem"]('Israel');
    instance["addItem"]('Italy');
    instance["addItem"]('Ivory Coast');
    instance["addItem"]('Jan Mayen');
    instance["addItem"]('Japan');
    instance["addItem"]('Jersey');
    instance["addItem"]('Jordan');
    instance["addItem"]('Juan De Nova Island');
    instance["addItem"]('Kazakhstan');
    instance["addItem"]('Kenya');
    instance["addItem"]('Kuwait');
    instance["addItem"]('Kyrgyzstan');
    instance["addItem"]('Laos');
    instance["addItem"]('Latvia');
    instance["addItem"]('Lebanon');
    instance["addItem"]('Lesotho');
    instance["addItem"]('Liberia');
    instance["addItem"]('Libya');
    instance["addItem"]('Liechtenstein');
    instance["addItem"]('Lithuania');
    instance["addItem"]('Luxembourg');
    instance["addItem"]('Madagascar');
    instance["addItem"]('Malawi');
    instance["addItem"]('Malaysia');
    instance["addItem"]('Maldives');
    instance["addItem"]('Mali');
    instance["addItem"]('Malta');
    instance["addItem"]('Mauritania');
    instance["addItem"]('Mauritius');
    instance["addItem"]('Mayotte');
    instance["addItem"]('Mexico');
    instance["addItem"]('Moldova');
    instance["addItem"]('Monaco');
    instance["addItem"]('Mongolia');
    instance["addItem"]('Montenegro');
    instance["addItem"]('Morocco');
    instance["addItem"]('Mozambique');
    instance["addItem"]('Myanmar');
    instance["addItem"]('Namibia');
    instance["addItem"]('Nepal');
    instance["addItem"]('Netherlands');
    instance["addItem"]('New Caledonia');
    instance["addItem"]('New Zealand');
    instance["addItem"]('Niger');
    instance["addItem"]('Nigeria');
    instance["addItem"]('Norfolk Island');
    instance["addItem"]('North Korea');
    instance["addItem"]('Norway');
    instance["addItem"]('Oman');
    instance["addItem"]('Pacific Islands');
    instance["addItem"]('Pakistan');
    instance["addItem"]('Palau');
    instance["addItem"]('Palestinian Territory');
    instance["addItem"]('Papua New Guinea');
    instance["addItem"]('Paraguay');
    instance["addItem"]('Peru');
    instance["addItem"]('Philippines');
    instance["addItem"]('Poland');
    instance["addItem"]('Qatar');
    instance["addItem"]('Reuunion');
    instance["addItem"]('Romania');
    instance["addItem"]('Russian Federation');
    instance["addItem"]('Rwanda');
    instance["addItem"]('Saint Pierre and Miquelon');
    instance["addItem"]('San Marino');
    instance["addItem"]('Sao Tome and Principe');
    instance["addItem"]('Saudi Arabia');
    instance["addItem"]('Senegal');
    instance["addItem"]('Serbia');
    instance["addItem"]('Seychelles');
    instance["addItem"]('Sierra Leone');
    instance["addItem"]('Singapore');
    instance["addItem"]('Slovakia');
    instance["addItem"]('Slovenia');
    instance["addItem"]('Solomon Islands');
    instance["addItem"]('Somalia');
    instance["addItem"]('South Africa');
    instance["addItem"]('South Korea');
    instance["addItem"]('South Sudan');
    instance["addItem"]('Spain');
    instance["addItem"]('Sri Lanka');
    instance["addItem"]('Sudan');
    instance["addItem"]('Swaziland');
    instance["addItem"]('Sweden');
    instance["addItem"]('Switzerland');
    instance["addItem"]('Syria');
    instance["addItem"]('Tajikistan');
    instance["addItem"]('Tanzania');
    instance["addItem"]('Thailand');
    instance["addItem"]('The Former Yugoslav Republic of Macedonia');
    instance["addItem"]('Timor-Leste');
    instance["addItem"]('Togo');
    instance["addItem"]('Tunisia');
    instance["addItem"]('Turkey');
    instance["addItem"]('Turkmenistan');
    instance["addItem"]('Uganda');
    instance["addItem"]('Ukraine');
    instance["addItem"]('United Arab Emirates');
    instance["addItem"]('United Kingdom');
    instance["addItem"]('Uruguay');
    instance["addItem"]('USA-Alaska');
    instance["addItem"]('USA-CONUS');
    instance["addItem"]('USA-Hawaii');
    instance["addItem"]('Uzbekistan');
    instance["addItem"]('Vanuatu');
    instance["addItem"]('Vatican City');
    instance["addItem"]('Vietnam');
    instance["addItem"]('Yemen');
    instance["addItem"]('Zambia');
    instance["addItem"]('Zimbabwe');
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

function getSelectedViewNWSDomain() {
    var region = $('#viewNWSDomain').children(':input').attr('value');
    return region;
}

function getSelectedViewRegion() {
    var region = $('#viewRegion').children(':input').attr('value');
    return region;
}

function getSelectedViewCountry() {
    var region = $('#viewCountry').children(':input').attr('value');
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