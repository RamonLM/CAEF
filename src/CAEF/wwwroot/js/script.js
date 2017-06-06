var app = angular.module("caef", ['angularModalService']);

(function (app) {
    "use strict";

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
            $http.get("/Usuarios/Usuarios")
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

    app.controller('MensajeController', function ($scope, mensaje, close) {
        $scope.mensaje = mensaje;

        $scope.close = function () {
            close(true, 500);
        };
    });

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
                    templateUrl: "https://caefuabc.azurewebsites.net/views/mensajeGenerico.html",
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

    app.controller("ActasController", function ($scope, $http, $location, $window, $filter, ModalService) {
        // Usuario actualmente autenticado
        $scope.usuarioAutenticado = {}
        // Acta que se va a grabar
        $scope.acta = {
            Periodo: new Date().getMonth() < 5 ? new Date().getFullYear() + '-1' : new Date().getFullYear() + '-2'
        }
        // Carreras de la facultad
        $scope.carreras = []
        // Materias de la facultad
        $scope.materias = []
        // Subtipos de examen
        $scope.subtipos = []
        // Tipos de examen
        $scope.tipos = []
        // Fecha actual
        $scope.fecha = new Date();
        // Solicitudes de cada alumno
        $scope.solicitudesAlumno = [{
            Alumno: {
                Grupo: ""
            }
        }]
        // Grupo de la solicitud
        $scope.grupo = ""

        $scope.agregarColumna = function () {
            var alumnoNuevo = {
                Alumno: {
                    Grupo: ""
                }
            }
            $scope.solicitudesAlumno.push(alumnoNuevo);
        }

        $scope.obtenerUsuarioAutenticado = function () {
            $http.get("/UsuarioActual")
            .then(function (response) {
                $scope.usuarioAutenticado = response.data;
                //$scope.acta.Usuario = $scope.usuarioAutenticado;
                $scope.acta.IdEmpleado = $scope.usuarioAutenticado.id;
            });
        }

        $scope.obtenerCarreras = function () {
            $http.get("/CAEF/Carreras")
            .then(function (response) {
                $scope.carreras = response.data;
            }, function (error) {
                $scope.error = "Error al obtener carreras: " + error;
            });
        }

        $scope.obtenerMaterias = function () {
            $http.get("/CAEF/Materias")
            .then(function (response) {
                $scope.materias = response.data;
            }, function (error) {
                $scope.error = "Error al obtener materias: " + error;
            });
        }

        $scope.obtenerSubtipos = function () {
            $http.get("/CAEF/Subtipos")
            .then(function (response) {
                $scope.subtipos = response.data;
            }, function (error) {
                $scope.error = "Error al obtener subtipos: " + error;
            });
        }

        $scope.obtenerTipos = function () {
            $http.get("/CAEF/Tipos")
            .then(function (response) {
                $scope.tipos = response.data;
            }, function (error) {
                $scope.error = "Error al obtener tipos: " + error;
            });
        }

        $scope.calculaClaveMateria = function () {
            if (!$scope.acta.Materia) {
                $scope.acta.IdMateria = "";
            } else {
                $scope.acta.IdMateria = JSON.parse($scope.acta.Materia).id;
            }

        }

        $scope.calculaClaveCarrera = function () {

            $scope.acta.IdCarrera = JSON.parse($scope.acta.Carrera).id;
        }

        $scope.filtraMaterias = function (materia) {
            if (!$scope.acta.Carrera) return false;

            if (materia.carrera === JSON.parse($scope.acta.Carrera).nombre)
                return true
            else
                return false;
        }

        $scope.grabar = function () {
            $scope.acta.IdEstado = 1;
            $scope.acta.FechaCreacion = new Date();

            angular.forEach($scope.solicitudesAlumno, function (value, key) {
                $scope.solicitudesAlumno[key].Alumno.Grupo = $scope.grupo;
            });

            console.log($scope.solicitudesAlumno);
            //console.log($scope.usuarioAutenticado);
            //console.log($scope.acta);

            $http.post("/CAEF/AgregarActaDocente", $scope.acta)
            .then(function (response) {
                console.log("Success");
                console.log(response);

                angular.forEach($scope.solicitudesAlumno, function (value, key) {
                    $scope.solicitudesAlumno[key].IdSolicitud = response.data;
                });

                $http.post("/CAEF/AgregarSolicitudAlumno", $scope.solicitudesAlumno)
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
                        $window.location = "/Acta";
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

        $scope.obtenerUsuarioAutenticado();
        $scope.obtenerCarreras();
        $scope.obtenerMaterias();
        $scope.obtenerSubtipos();
        $scope.obtenerTipos();
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

})(app);
