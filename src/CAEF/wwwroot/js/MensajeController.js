app.controller('MensajeController', function ($scope, mensaje, close) {
    $scope.mensaje = mensaje;

    $scope.close = function () {
        close(true, 500);
    };
});