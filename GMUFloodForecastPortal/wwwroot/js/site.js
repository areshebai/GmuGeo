// Write your Javascript code.
function getDirectionInstance(instance) {
    instance["addItem"]('Ago');
    instance["addItem"]('Later');
    instance["selectIndex"](0);
}

function getStepInstance(instance) {
    instance["addItem"]('Days');
    instance["addItem"]('Hours');
    instance["addItem"]('Minutes');
    instance["selectIndex"](1);
}

function getStepInstance2(instance) {
    instance["addItem"]('Days');
    instance["addItem"]('Hours');
    instance["addItem"]('Minutes');
    instance["selectIndex"](1);
}

function getProductInstance(instance) {
    instance["addItem"]('VIIRS 375-m');
    instance["addItem"]('ABI 1-km');
    instance["addItem"]('AHI 1-km');
    instance["addItem"]('Joint VIIRS/ABI/AHI');
    instance["selectIndex"](1);
}

function getRegionInstance(instance) {
    instance["addItem"]('East Asia');
    instance["addItem"]('Europe');
    instance["addItem"]('North Ammerica');
    instance["addItem"]('Source Ammerica');
    instance["selectIndex"](1);
}

function getImageTypeInstance(instance) {
    instance["addItem"]('GeoTiff');
    instance["addItem"]('HDF4');
    instance["addItem"]('PNG');
    instance["selectIndex"](1);
}

function getDownloadWindowInstance(instance) {
    instance["bringToFront"]();
}

var hour;
var g_absoluteFromDateTime;
var g_absoluteToDateTime;
var g_mapInstance;
var g_viewFromDateTimeInstance;

function initMap() {
    hour = 0;
    var map = createMapInstance("googleMap");
    displayKmlLayer(map, 'https://jpssflood.gmu.edu/kmls/cspp-viirs-flood-globally_20180815_010000_53.kml');
}

function createMapInstance(mapElement) {
    var mapProp = {
        center: new google.maps.LatLng({ lat: 0, lng: 0 }),
        maxZoom: 10,
        minZoom: 2,
        zoom: 5,
        zoomControl: true,
        mapTypeId: 'hybrid',
        streetViewControl: false,
        mapTypeControl: false,
        zoomControlOptions: {
            position: google.maps.ControlPosition.RIGHT_BOTTOM
        }
    };

    var map = new google.maps.Map(document.getElementById(mapElement), mapProp);

    map.addListener('tilesloaded', function () {
        $("#"+ mapElement).find("img").css("opacity", "0.9");
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

function displayDailyKmls(map) {
    $.ajax({
        type: 'GET',
        url: "api/Kml",
        cache: false,
        success: function (data) {
            $.each(data, function (index, value) {
                alert(value);
                //displayKmlLayer(map, value);
            });
        }
    });
}

function mouseMoveOnMap(event) {
    $("#currentLatitude").html(event.latLng.lat());
    $("#currentLongitude").html(event.latLng.lng());
    $("#currentDistrict").html('48');
}