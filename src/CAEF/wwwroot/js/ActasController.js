app.controller("ActasController", function ($rootScope, $scope, $http, $location, $window, $filter, ModalService) {
    // Usuario actualmente autenticado
    $scope.usuarioAutenticado = {}
    // Acta que se va a grabar
    $scope.acta = {
        Periodo: new Date().getMonth() < 5 ? new Date().getFullYear() + '-1' : new Date().getFullYear() + '-2',
        CicloEscolar: new Date().getMonth() < 5 ? new Date().getFullYear() + '-1' : new Date().getFullYear() + '-2',
        ClaveUnidad: 290,
        UnidadAcademica: "Fac. de Ingeniería, Arq. y Diseño"
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
    $scope.alumnoActual = []

    $scope.fecha = new Date();
    // Solicitudes de cada alumno

    //ID de solicitud Actual 
    $scope.idSolicitudActual = decodeURIComponent($location.absUrl().split('/')[4]);
    console.log($scope.idSolicitudActual);

    //ID de solicitud de Alumno Actual
    $scope.idSolicitudActualAlumno = []

    $scope.solicitudesAlumno = []
    // Grupo de la solicitud
    $scope.grupo = ""
    //Grupo de la solicitud Actual
    $scope.grupoA = ""

    $scope.obtenerActas = function () {
        $http.get("/Usuarios/NotificacionesDocente")
            .then(function (response) {
                //console.log(response.data);
                $scope.actas = response.data;
            }, function (error) {
                $scope.error = "Error al obtener usuarios: " + error;
            });
    }

    ///////////////////////////////////////////////////////////////////////////
    $scope.revisarActaAdmin = function () {
        console.log($scope.idSolicitudActual)
        $http.get("/CAEF/RevisarActa/" + $scope.idSolicitudActual)
            .then(function (response) {
                //console.log(response.data);
                $scope.actaActual = response.data;
                console.log($scope.actaActual);
            }, function (error) {
                $scope.error = "Error al obtener la acta: " + error;
            });
    }

    $scope.obtenerAlumnosDeActas = function () {
        $http.get("/CAEF/ObtenerAlumno/" + $scope.idSolicitudActual)
            .then(function (response) {
                $scope.alumnoActual = response.data;
                console.log($scope.alumnoActual);

                angular.forEach($scope.alumnoActual, function (value, key) {
                    $scope.solicitudesAlumno.push($scope.alumnoActual[key]);
                });

                angular.forEach($scope.solicitudesAlumno, function (value, key) {

                    $scope.idSolicitudActualAlumno.push($scope.solicitudesAlumno[key].id);
                });

                $scope.grupoA = $scope.alumnoActual[0].alumno.grupo

            }, function (error) {
                $scope.error = "Error al obtener alumnos: " + error;
            });
    }

    //$scope.obtenerDocenteDeActas = function () {
    //    $http.get("/CAEF/ObtenerDocente/")
    //    .then(function (response) {
    //        //console.log(response.data);
    //        $scope.docenteActual = response.data;
    //        console.log($scope.docenteActual);
    //    }, function (error) {
    //        $scope.error = "Error al obtener alumnos: " + error;
    //    });
    //}

    $scope.revisarActaDocente = function () {
        $http.get("/Usuarios/NotificacionesDocente")
            .then(function (response) {
                //console.log(response.data);
                $scope.actas = response.data;
            }, function (error) {
                $scope.error = "Error al obtener la acta: " + error;
            });
    }
    ////////////////////////////////////////////////////////////////////////////


    $scope.inicializarAlumnosEnActa = function () {


    }



    $scope.agregarColumna = function () {
        var alumnoNuevo = {
            alumno: {
                grupo: ""
            }
        }
        $scope.solicitudesAlumno.push(alumnoNuevo);
    }

    $scope.removerColumna = function () {

        //    $http.post("/CAEF/RemoverSolcitudAlumno")
        //.then(function (response) {
        //    console.log("Success")
        //}, function (error) {
        //    console.log(error);
        //});

        if ($scope.solicitudesAlumno.length > 1) {
            $scope.solicitudesAlumno.splice(-1, 1);
        }
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

    $scope.calculaClaveMateriaActual = function () {
        if (!$scope.actaActual.materia) {
            $scope.actaActual.idMateria = "";
        } else {
            $scope.actaActual.idMateria = JSON.parse($scope.acta.Materia).id;
        }

    }

    $scope.calculaClaveCarrera = function () {

        $scope.acta.IdCarrera = JSON.parse($scope.acta.Carrera).id;
    }

    $scope.calculaClaveCarreraActual = function () {

        $scope.actaActual.idCarrera = JSON.parse($scope.acta.Carrera).id;
    }

    $scope.filtraMaterias = function (materia) {
        if (!$scope.acta.Carrera) return false;

        if (materia.carrera === JSON.parse($scope.acta.Carrera).nombre)
            return true
        else
            return false;
    }



    //////////////////////////////
    $scope.grabarActaAdminActual = function () {
        $scope.acta.FechaAceptacion = new Date();
        $scope.acta.FechaCreacion = new Date();
        $scope.acta.NumeroAlumnos = $scope.solicitudesAlumno.length;
        $scope.acta.CalificacionLetra = "CIEN";
        $scope.actaActual.IdEstado = 2;
        $scope.acta.Comentario = $scope.acta.Motivo;

        console.log($scope.actaActual)
        console.log($scope.acta)

        // $scope.solicitudesAlumno.splice(0, 1);
        console.log($scope.solicitudesAlumno);
        //console.log($scope.usuarioAutenticado);
        //console.log($scope.acta);

        angular.forEach($scope.solicitudesAlumno, function (value, key) {
            delete $scope.solicitudesAlumno[key].id
        });

        angular.forEach($scope.solicitudesAlumno, function (value, key) {
            $scope.solicitudesAlumno[key].alumno.grupo = $scope.alumnoActual[0].alumno.grupo;
        });

        $http.post("/CAEF/ObtenerIdSolicitud", $scope.idSolicitudActualAlumno)
            .then(function (response) {
                console.log("Success")
            }, function (error) {
                console.log(error);
                ModalService.showModal({
                    templateUrl: "http://localhost:52244/views/mensajegenerico.html",
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

        $http.post("/CAEF/EditarActaDocente", $scope.actaActual)
            .then(function (response) {
                console.log("Success");
                console.log(response);

                angular.forEach($scope.solicitudesAlumno, function (value, key) {
                    $scope.solicitudesAlumno[key].IdSolicitud = response.data;
                });

                $scope.acta.IdSolicitud = response.data;
                $scope.acta.URLDocumento = "~/Acta/" + response.data;

                $http.post("/CAEF/AgregarActaAdmin", $scope.acta)
                    .then(function (response) {
                        console.log("Success");
                        console.log(response);

                        $http.post("/CAEF/AgregarSolicitudAlumno", $scope.solicitudesAlumno)
                            .then(function (response) {
                                console.log("Success");
                                console.log(response);


                                ModalService.showModal({
                                    templateUrl: "http://localhost:52244/views/mensajeGenerico.html",
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
                                    templateUrl: "http://localhost:52244/views/mensajeGenerico.html",
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
                            templateUrl: "http://localhost:52244/views/mensajeGenerico.html",
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
                    templateUrl: "http://localhost:52244/views/mensajeGenerico.html",
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


    /////////////////////////////


    ////////////////////////////
    $scope.grabarActaAdmin = function () {
        $scope.acta.FechaAceptacion = new Date();
        $scope.acta.NumeroAlumnos = $scope.solicitudesAlumno.length;
        $scope.acta.CalificacionLetra = "CIEN";
        $scope.acta.IdEstado = 2;
        $scope.acta.FechaCreacion = new Date();
        $scope.acta.Comentario = $scope.acta.Motivo;

        angular.forEach($scope.solicitudesAlumno, function (value, key) {
            $scope.solicitudesAlumno[key].Alumno.Grupo = $scope.grupo;
        });

        console.log($scope.acta);
        console.log($scope.solicitudesAlumno);

        $http.post("/CAEF/AgregarActaDocente", $scope.acta)
            .then(function (response) {
                console.log("Success");
                console.log(response);

                angular.forEach($scope.solicitudesAlumno, function (value, key) {
                    $scope.solicitudesAlumno[key].IdSolicitud = response.data;
                });

                $scope.acta.IdSolicitud = response.data;
                $scope.acta.URLDocumento = "~/Acta/" + response.data;

                $http.post("/CAEF/AgregarActaAdmin", $scope.acta)
                    .then(function (response) {
                        console.log("Success");
                        console.log(response);

                        $http.post("/CAEF/AgregarSolicitudAlumno", $scope.solicitudesAlumno)
                            .then(function (response) {
                                console.log("Success");
                                console.log(response);


                                ModalService.showModal({
                                    templateUrl: "views/mensajeGenerico.html",
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
                                    templateUrl: "views/mensajeGenerico.html",
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
                            templateUrl: "views/mensajeGenerico.html",
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
                    templateUrl: "views/mensajeGenerico.html",
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
    //////////////////////////////////////

    $scope.rechazarActaAdminActual = function () {


        ModalService.showModal({
            templateUrl: "http://localhost:52244/views/rechazarActa.html",
            controller: "RechazarController",
            inputs: {
                actaActual: $scope.actaActual
            }
        }).then(function (modal) {
            modal.element.modal();
            modal.close.then(function (result) {

                $scope.actaModificada = result;
                $scope.actaModificada.IdEstado = 3;
                if (result != null) {

                    $http.post("/CAEF/EditarActaDocente", $scope.actaModificada)
                        .then(function (response) {
                            console.log("Success");
                            console.log(response);
                            ModalService.showModal({
                                templateUrl: "views/mensajeGenerico.html",
                                controller: "MensajeController",
                                inputs: {
                                    mensaje: response.data
                                }
                            }).then(function (modal) {
                                modal.element.modal();
                                modal.close.then(function () {
                                    //$window.location = "/AgregarUsuario";
                                });
                            });


                        }, function (error) {
                            console.log(error);
                            ModalService.showModal({
                                templateUrl: "http://localhost:52244/views/mensajeGenerico.html",
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

            });
        });
    }

    $scope.grabarActaDocente = function () {
        $scope.acta.IdEstado = 1;
        $scope.acta.FechaCreacion = new Date();
        // $scope.acta = $scope.actaActual;

        angular.forEach($scope.solicitudesAlumno, function (value, key) {
            $scope.solicitudesAlumno[key].alumno.grupo = $scope.grupo;
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
                            templateUrl: "views/mensajeGenerico.html",
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
                            templateUrl: "views/mensajeGenerico.html",
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
                    templateUrl: "views/mensajeGenerico.html",
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

    $scope.grabarActaDocenteActual = function () {
        $scope.acta.FechaCreacion = new Date();
        $scope.acta = $scope.actaActual;
        $scope.acta.IdEstado = 1;

        console.log($scope.actaActual)
        console.log($scope.acta)


        // $scope.solicitudesAlumno.splice(0, 1);
        console.log($scope.solicitudesAlumno);
        //console.log($scope.usuarioAutenticado);
        //console.log($scope.acta);

        angular.forEach($scope.solicitudesAlumno, function (value, key) {
            delete $scope.solicitudesAlumno[key].id
        });

        angular.forEach($scope.solicitudesAlumno, function (value, key) {
            $scope.solicitudesAlumno[key].alumno.grupo = $scope.grupoA;
        });

        $http.post("/CAEF/ObtenerIdSolicitud", $scope.idSolicitudActualAlumno)
            .then(function (response) {
                console.log("Success")
            }, function (error) {
                console.log(error);
                ModalService.showModal({
                    templateUrl: "http://localhost:52244/views/mensajegenerico.html",
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

        $http.post("/CAEF/EditarActaDocente", $scope.actaActual)
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
                            templateUrl: "http://localhost:52244/views/mensajegenerico.html",
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
                            templateUrl: "http://localhost:52244/views/mensajegenerico.html",
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
                    templateUrl: "http://localhost:52244/views/mensajegenerico.html",
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


    $scope.revisarActaAdmin();
    $scope.obtenerAlumnosDeActas();
    $scope.inicializarAlumnosEnActa();
    $scope.revisarActaDocente();
    $scope.obtenerActas();
    $scope.obtenerUsuarioAutenticado();
    $scope.obtenerCarreras();
    $scope.obtenerMaterias();
    $scope.obtenerSubtipos();
    $scope.obtenerTipos();
});

app.controller("RechazarController", function ($scope, $element, actaActual, close) {
    $scope.actaActual = actaActual;

    $scope.accept = function () {
        close($scope.actaActual, 500);
    };

    $scope.cancel = function () {
        close(null, 500);
    };
});