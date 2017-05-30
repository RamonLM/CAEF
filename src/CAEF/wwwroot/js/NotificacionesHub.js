var notificacionesHub;

$().ready(function () {
    notificacionesHub = $.connection.notificacionesHub;

    notificacionesHub.client.reportConnections = function (count) {
        $("#online").text(count);
    }

    $.connection.hub.start();
});