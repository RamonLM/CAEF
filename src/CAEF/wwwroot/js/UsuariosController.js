app.controller("UsuariosController", function ($scope, $location, $window, $http, ModalService) {
    // Lista de usuarios en el sistema
    $scope.usuarios = {};
    // Mensaje de error
    $scope.error = "";
    // Roles de usuario
    $scope.roles = {}
    // Usuario actualmente autenticado
    $scope.usuarioAutenticado = {}

    $scope.obtenerUsuarios = function () {
        $http.get("/Usuarios/Listar")
        .then(function (response) {
            console.log(response.data);
            $scope.usuarios = response.data;
        }, function (error) {
            $scope.error = "Error al obtener usuarios: " + error;
        });
    }

    $scope.obtenerRoles = function () {
        $http.get("/CAEF/Roles")
        .then(function (response) {
            $scope.roles = response.data;
        }, function (error) {
            $scope.error = "Error al obtener roles: " + error;
        });
    }

    $scope.editarUsuario = function (usuario, listaRoles) {
        $scope.error = "";
        ModalService.showModal({
            templateUrl: "https://caefuabc.azurewebsites.net/views/editarUsuario.html",
            controller: "EditarController",
            inputs: {
                usuario: usuario,
                roles: listaRoles
            }
        }).then(function (modal) {
            modal.element.modal();
            modal.close.then(function (result) {
                if (usuario.rol.nombre != result.nombre) {
                    console.log(result);
                    usuario.rolId = result.id;
                    $http.post("/Usuarios/Editar", usuario)
                    .then(function (response) {
                        ModalService.showModal({
                            templateUrl: "https://caefuabc.azurewebsites.net/views/mensajeGenerico.html",
                            controller: "MensajeController",
                            inputs: {
                                mensaje: response.data
                            }
                        }).then(function (modal) {
                            modal.element.modal();
                            modal.close.then(function () {
                                $window.location = "/Usuarios";
                            });
                        });

                    }, function (error) {
                        ModalService.showModal({
                            templateUrl: "https://caefuabc.azurewebsites.net/views/mensajeGenerico.html",
                            controller: "MensajeController",
                            inputs: {
                                mensaje: error.data
                            }
                        }).then(function (modal) {
                            modal.element.modal();
                            modal.close.then(function () {
                                $window.location = "/Usuarios";
                            });
                        });
                    });
                }
            });
        });
    }

    $scope.borrarUsuario = function (usuario) {
        $scope.error = "";
        console.log("Quieres borrar usuario:" + usuario.correo);
        ModalService.showModal({
            templateUrl: "https://caefuabc.azurewebsites.net/views/borrarUsuario.html",
            controller: "BorrarController"
        }).then(function (modal) {
            modal.element.modal();
            modal.close.then(function (result) {
                if (result) {
                    $http.post("/Usuarios/Borrar", usuario)
                        .then(function (response) {
                            ModalService.showModal({
                                templateUrl: "https://caefuabc.azurewebsites.net/views/mensajeGenerico.html",
                                controller: "MensajeController",
                                inputs: {
                                    mensaje: response.data
                                }
                            }).then(function (modal) {
                                modal.element.modal();
                                modal.close.then(function () {
                                    $window.location = "/Usuarios";
                                });
                            });
                        }, function (error) {
                            ModalService.showModal({
                                templateUrl: "https://caefuabc.azurewebsites.net/views/mensajeGenerico.html",
                                controller: "MensajeController",
                                inputs: {
                                    mensaje: error.data
                                }
                            }).then(function (modal) {
                                modal.element.modal();
                                modal.close.then(function () {
                                    $window.location = "/Usuarios";
                                });
                            });
                        });
                }
            });
        });
    }

    $scope.obtenerUsuarioAutenticado = function () {
        $http.get("/UsuarioActual")
        .then(function (response) {
            console.log(response.data);
            $scope.usuarioAutenticado = response.data;
            console.log($scope.usuarioAutenticado);
        });
    }

    $scope.obtenerUsuarios();
    $scope.obtenerRoles();
    $scope.obtenerUsuarioAutenticado();
});

app.controller('AgregarController', function ($scope, $http, $window, $location, ModalService) {

    // Roles de usuario
    $scope.roles = {}
    // Usuario a agregar
    $scope.usuario = {};
    // Roles de usuario
    $scope.roles = {}

    $scope.agregar = function () {
        console.log($scope.usuario);
        $http.post("/Usuarios/Agregar", $scope.usuario)
        .then(function (response) {
            console.log("Success");
            console.log(response);
            ModalService.showModal({
                templateUrl: "https://caefuabc.azurewebsites.net/views/mensajeGenerico.html",
                controller: "MensajeController",
                inputs: {
                    mensaje: response.data
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function () {
                    $window.location = "/Usuarios";
                });
            });
        }, function (error) {
            console.log(error);
            ModalService.showModal({
                templateUrl: "https://caefuabc.azurewebsites.net/views/mensajeGenerico.html",
                controller: "MensajeController",
                inputs: {
                    mensaje: error.data
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function () {
                    //$window.location = "/AgregarUsuario";
                });
            });
        });
    }

    $scope.obtenerRoles = function () {
        $http.get("/CAEF/Roles")
        .then(function (response) {
            $scope.roles = response.data;
        }, function (error) {
            $scope.error = "Error al obtener roles: " + error;
        });
    }

    $scope.obtenerRoles();
});

app.controller("BorrarController", function ($scope, close) {

    $scope.close = function (result) {
        close(result, 500);
    };
});

app.controller("EditarController", function ($scope, $element, usuario, roles, close) {
    console.log(usuario);

    $scope.usuario = usuario;
    $scope.rolActual = usuario.rol;
    $scope.listaRoles = roles;
    $scope.rolSeleccionado = $scope.rolActual;

    $scope.accept = function () {
        console.log("Escogiste rol: " + $scope.rolSeleccionado.nombre);
        close($scope.rolSeleccionado, 500);
    };

    $scope.cancel = function () {
        console.log("Escogiste rol: " + $scope.rolSeleccionado.nombre);
        close($scope.rolActual, 500);
    };
});