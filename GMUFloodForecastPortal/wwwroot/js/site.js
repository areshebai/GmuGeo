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

var g_map;
var hour;

function initMap() {
    var mapProp = {
        center: new google.maps.LatLng(0, 0),
        zoom: 2,
        mapTypeId: 'hybrid',
        disableDefaultUI: true
    };
    g_map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
    hour = 0;

    displayKmlLayer(g_map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_17.kml');
    displayKmlLayer(g_map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_18.kml');
    displayKmlLayer(g_map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_19.kml');
    displayKmlLayer(g_map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_20.kml');
    
    /*
    var ctaLayer2 = new google.maps.KmlLayer({
        url: 'https://sites.google.com/site/gmukmls/kmls/KMZ_EXAMPLE.kmz',
        map: map
    });
    */
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

    if (curhour === 1) {
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_42.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_43.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_44.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_45.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_46.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_47.kml');

        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_53.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_54.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_55.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_56.kml');

        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_66.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_67.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_68.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_69.kml');

        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_81.kml');
    }

    if (curhour === 2) {
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_49.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_50.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_51.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_52.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_53.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_54.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_55.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_56.kml');

        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_64.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_65.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_66.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_67.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_68.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_69.kml');

        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_79.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_80.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_81.kml');

        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_112.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_120.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_126.kml');
    }

    if (curhour === 3) {
        alert("No Image");
    }

    if (curhour === 4) {
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_47.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_48.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_49.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_50.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_51.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_52.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_53.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_54.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_55.kml');

        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_63.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_64.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_65.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_66.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_67.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_68.kml');

        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_77.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_78.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_79.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_80.kml');

        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_91.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_92.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_102.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_103.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_111.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_112.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_118.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_119.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_120.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_124.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_125.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_126.kml');
    }

    if (curhour === 21) {
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_1.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_2.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_3.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_4.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_5.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_6.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_9.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_10.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_11.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_12.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_13.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_17.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_18.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_22.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_26.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_39.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_44.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_45.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_46.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_47.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_48.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_49.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_50.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_51.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_52.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_53.kml');
    }

    if (curhour === 22) {
        alert("No Image");
    }

    if (curhour === 23) {
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_1.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_2.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_3.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_4.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_9.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_10.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_11.kml');
        displayKmlLayer(map, 'http://13.78.141.105/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_17.kml');
    }
}