
console.log("Site.js");

var map;
var markers = new Array();
var raio;
var nome;

var POA = [-26.723135938859027, -52.73808717727661];

function init(refRaio, refName) {
    raio = refRaio;
    nome = refName;
    map = initOSM();

    L.marker(POA, {
        icon: L.ExtraMarkers.icon({
            icon: 'fa-location-dot',
            shape: 'circle',
            markerColor: 'red',
            prefix: 'fa'
        })
    })
        .addTo(map)
        .bindPopup('Clique em qualquer lugar do mapa para encontrar credneciados');

    var popup = L.popup();

    function onMapClick(e) {
        popup
            .setLatLng(e.latlng)
            .setContent("Lat/Long clicado: " + e.latlng.toString())
            .openOn(map);
    }

    map.on('click', getSpots);
}

function initOSM() {
    var osmUrl = 'http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    var osmAttrib = '&copy; <a href="http://openstreetmap.org/copyright">OpenStreetMap</a> BR Serviços';
    var osm = L.tileLayer(osmUrl, { maxZoom: 18, attribution: osmAttrib });
    return L.map('map').setView(POA, 13).addLayer(osm);
}

function initMapbox() {
    var mbAttr = 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> BR Serviços, ' +
        '<a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
        'Imagery © <a href="http://mapbox.com">Mapbox</a>',
        mbUrl = 'https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpandmbXliNDBjZWd2M2x6bDk3c2ZtOTkifQ._QA7i5Mpkd_m30IGElHziw';

    var streets = L.tileLayer(mbUrl, { id: 'mapbox.streets', attribution: mbAttr });

    return L.map('map').setView(POA, 13).addLayer(streets);
}

function getSpots(e) {

    var baseUrl = "/api/v1/hotspots?latitude=";

    let protocol = $(location).attr('protocol');
    let host = $(location).attr('host');
    let url = `${protocol}//${host}/Credenciados/BuscarCredenciados?`;
    url += "latitude=" + e.latlng.lat + "&longitude=" + e.latlng.lng + "&distance=" + $(raio).val() + "&nome=" + $(nome).val();

    $.getJSON(url,
        function (data) {
            var wifimarker = L.ExtraMarkers.icon({
                icon: 'fa-hand-point-down',
                markerColor: 'black',
                shape: 'circle',
                prefix: 'fa'
            });

            console.log(data);
            // cleanup map
            removeSpots();

            for (var i = 0; i < data.length; i++) {

                console.log("Local: ", data[i]);

                // Locations are stored as long/lat pairs.
                var location = new L.LatLng(data[i].local[0], data[i].local[1]);
                var title = "<div style='font-size: 18px; color: #0078A8;'> <a href='#'>Perfil</a> - TITLE </div>";
                var address = "<div style='font-size: 14px;'> ENDERECO </div>";
                var marker = L.marker(location, { icon: wifimarker });

                // to future cleanup
                markers.push(marker);

                marker.addTo(map);
                marker.bindPopup(
                    "<div style='text-align: center; margin-left: auto; margin-right: auto;'>"
                    + title + address + "LVS </div>",
                    { maxWidth: '400' }
                );
            }
        }
    )
}

function removeSpots() {
    for (i = 0; i < markers.length; i++) {
        map.removeLayer(markers[i]);
    }
}
