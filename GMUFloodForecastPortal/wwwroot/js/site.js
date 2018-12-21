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

function initMap() {
    var mapProp = {
        center: new google.maps.LatLng(38.8315141, -77.3140937),
        zoom: 15,
        mapTypeId: 'hybrid',
        disableDefaultUI: true
    };
    g_map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

    /*
    var ctaLayer = new google.maps.KmlLayer({
        url: 'https://sites.google.com/site/gmukmls/kmls/MOSAIC_WATER_VIIRS_Prj_SVI_npp_d20170831_t1841_t1844_cspp_dev_105_3855_2966_01.kml',
        map: map
    });
    
    var ctaLayer2 = new google.maps.KmlLayer({
        url: 'https://sites.google.com/site/gmukmls/kmls/KMZ_EXAMPLE.kmz',
        map: map
    });
    */
}