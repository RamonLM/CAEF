app.controller('MenuController', function ($scope, $http, $location) {

    // Usuario actualmente autenticado
    $scope.usuarioAutenticado = {}
    // Path actual
    $scope.urlActual = "/" + decodeURIComponent($location.absUrl().split('/')[3]);
    console.log($scope.urlActual);

    $scope.obtenerUsuarioAutenticado = function () {
        $http.get("/UsuarioActual")
        .then(function (response) {
            $scope.usuarioAutenticado = response.data;
        });
    }

    $scope.isActive = function (path) {
        return path === $scope.urlActual;
    };

    $scope.obtenerUsuarioAutenticado();
});