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

    displayKmlLayer(g_map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_17.kml');
    displayKmlLayer(g_map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_18.kml');
    displayKmlLayer(g_map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_19.kml');
    displayKmlLayer(g_map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_20.kml');
    
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
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_42.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_43.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_44.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_45.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_46.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_47.kml');

        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_53.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_54.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_55.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_56.kml');

        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_66.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_67.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_68.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_69.kml');

        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_81.kml');
    }

    if (curhour === 2) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_49.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_50.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_51.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_52.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_53.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_54.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_55.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_56.kml');

        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_64.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_65.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_66.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_67.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_68.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_69.kml');

        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_79.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_80.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_81.kml');

        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_112.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_120.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_126.kml');
    }

    if (curhour === 3) {
        alert("No Image");
    }

    if (curhour === 4) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_47.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_48.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_49.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_50.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_51.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_52.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_53.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_54.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_55.kml');

        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_63.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_64.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_65.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_66.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_67.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_68.kml');

        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_77.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_78.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_79.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_80.kml');

        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_91.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_92.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_102.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_103.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_111.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_112.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_118.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_119.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_120.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_124.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_125.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_126.kml');
    }

    if (curhour === 5) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_101.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_102.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_103.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_109.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_110.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_111.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_117.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_118.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_119.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_123.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_124.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_125.kml');
    }

    if (curhour === 6) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_101.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_102.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_103.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_109.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_110.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_111.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_42.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_46.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_47.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_48.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_49.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_50.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_51.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_52.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_53.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_6.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_61.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_62.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_63.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_64.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_65.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_66.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_7.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_76.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_77.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_78.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_79.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_8.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_89.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_90.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_91.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_92.kml');
    }

    if (curhour === 7) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_100.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_101.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_108.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_109.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_110.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_117.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_123.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_124.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_45.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_46.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_47.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_48.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_49.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_60.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_61.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_62.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_63.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_64.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_74.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_75.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_76.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_77.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_88.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_89.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_90.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_99.kml');
    }

    if (curhour === 8) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_43.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_44.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_45.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_46.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_47.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_48.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_49.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_50.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_51.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_52.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_59.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_60.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_61.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_62.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_63.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_64.kml');
    }

    if (curhour === 9) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_100.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_107.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_108.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_116.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_42.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_43.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_44.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_45.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_46.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_47.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_48.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_58.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_59.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_60.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_61.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_62.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_72.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_73.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_74.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_75.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_86.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_87.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_88.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_97.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_98.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_99.kml');
    }

    if (curhour === 10) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_105.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_106.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_107.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_114.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_115.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_116.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_121.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_122.kml');
    }

    if (curhour === 11) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_1.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_105.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_106.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_107.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_114.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_115.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_116.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_2.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_3.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_4.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_42.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_43.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_44.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_45.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_46.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_5.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_57.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_58.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_59.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_6.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_60.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_61.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_7.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_71.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_72.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_73.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_84.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_85.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_86.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_87.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_96.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_97.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_98.kml');
    }

    if (curhour === 12) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_104.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_105.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_113.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_114.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_115.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_121.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_122.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_57.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_58.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_59.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_70.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_71.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_72.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_82.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_83.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_84.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_85.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_94.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_95.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_96.kml');
    }

    if (curhour === 13) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_16.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_42.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_43.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_44.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_45.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_57.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_58.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_59.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_70.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_71.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_72.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_8.kml');
    }

    if (curhour === 14) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_104.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_113.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_121.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_15.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_16.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_33.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_42.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_43.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_57.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_7.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_70.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_8.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_82.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_83.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_93.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_94.kml');
    }

    if (curhour === 15) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_150000_38.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_150000_41.kml');
    }

    if (curhour === 16) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_13.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_14.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_15.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_16.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_20.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_21.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_25.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_29.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_32.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_33.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_35.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_36.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_38.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_49.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_50.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_51.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_52.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_53.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_54.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_55.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_56.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_6.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_7.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_8.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_82.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_93.kml');
    }

    if (curhour === 17) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_20.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_21.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_24.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_25.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_27.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_28.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_29.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_30.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_31.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_32.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_34.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_35.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_36.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_37.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_38.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_39.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_40.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_41.kml');
    }

    if (curhour === 18) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_11.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_12.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_13.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_14.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_15.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_16.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_19.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_20.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_21.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_23.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_24.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_25.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_3.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_4.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_5.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_6.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_7.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_8.kml');
    }

    if (curhour === 19) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_11.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_12.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_13.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_14.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_17.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_18.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_19.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_2.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_20.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_22.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_23.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_24.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_26.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_27.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_3.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_30.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_31.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_34.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_37.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_38.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_39.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_4.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_40.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_41.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_5.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_6.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_7.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_8.kml');
    }

    if (curhour === 20) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_200000_39.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_200000_40.kml');
    }

    if (curhour === 21) {
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_1.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_2.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_3.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_4.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_5.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_6.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_9.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_10.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_11.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_12.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_13.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_17.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_18.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_22.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_26.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_39.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_44.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_45.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_46.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_47.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_48.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_49.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_50.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_51.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_52.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_53.kml');
    }

    if (curhour === 22) {
        alert("No Image");
    }

    if (curhour === 23) {
        // $items = Get-ChildItem C:\GmuTemp\2018\08\15\dis | Where {$_.Name.Contains(".kml") -and $_.Name.Contains("_230000_") }
        // $items | ForEach-Object { Write-Host ("displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/uni/" + $_.Name + "`');")}
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_1.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_2.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_3.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_4.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_9.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_10.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_11.kml');
        displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_230000_17.kml');
    }
}

function displayDailyKmls(map) {
    // Get-ChildItem C:\GmuTemp\2018\08\15\uni | Where {$_.Name.Contains(".kml")}
    // $items | ForEach-Object { Write-Host ("displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/uni/" + $_.Name + "`');")}
    alert("Daily");
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_42.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_43.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_44.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_45.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_46.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_47.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_53.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_54.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_55.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_56.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_66.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_67.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_68.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_69.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_010000_81.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_112.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_120.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_126.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_49.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_50.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_51.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_52.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_64.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_65.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_79.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_020000_80.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_102.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_103.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_111.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_118.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_119.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_124.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_125.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_48.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_63.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_77.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_78.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_91.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_040000_92.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_101.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_109.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_110.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_117.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_050000_123.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_6.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_61.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_62.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_7.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_76.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_8.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_89.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_060000_90.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_100.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_108.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_60.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_74.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_75.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_88.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_070000_99.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_080000_59.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_107.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_116.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_58.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_72.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_73.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_86.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_87.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_97.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_090000_98.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_105.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_106.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_114.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_115.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_121.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_100000_122.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_1.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_2.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_3.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_4.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_5.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_57.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_71.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_84.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_85.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_110000_96.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_104.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_113.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_70.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_82.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_83.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_94.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_120000_95.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_130000_16.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_15.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_33.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_140000_93.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_150000_38.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_150000_41.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_13.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_14.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_20.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_21.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_25.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_29.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_32.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_35.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_160000_36.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_24.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_27.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_28.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_30.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_31.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_34.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_37.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_39.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_170000_40.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_11.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_12.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_19.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_180000_23.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_17.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_18.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_22.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_190000_26.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_10.kml');
    displayKmlLayer(map, 'http://13.78.138.134/kmls/2018/08/15/cspp-viirs-flood-globally_20180815_210000_9.kml');
}