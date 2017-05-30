app.controller('LoginController', function ($scope, $location, $window, $http, ModalService) {
    $scope.usuario = {};
    $scope.returnUrl = decodeURIComponent($location.absUrl().split('ReturnUrl=')[1]);
    console.log($scope.returnUrl);
    $scope.login = function () {
        console.log($scope.usuario);
        $http.post("/Login", $scope.usuario)
        .then(function (response) {
            console.log("Success");
            console.log(response);
            $window.location = $scope.returnUrl;
        }, function (error) {
            console.log(error);
            ModalService.showModal({
                templateUrl: "views/mensajeGenerico.html",
                controller: "MensajeController",
                inputs: {
                    mensaje: error.data
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function () {
                    $scope.usuario.Password = "";
                });
            });
        });
    }
});