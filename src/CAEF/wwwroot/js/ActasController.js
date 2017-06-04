app.controller("ActasController", function ($scope, $http, $location, $window, $filter, ModalService) {
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

    $scope.removerColumna = function () {
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

    $scope.grabarActaAdmin = function () {
        $scope.acta.FechaAceptacion = new Date();
        $scope.acta.NumeroAlumnos = $scope.solicitudesAlumno.length;
        $scope.acta.CalificacionLetra = "CIEN";
        $scope.acta.IdEstado = 2;
        $scope.acta.FechaCreacion = new Date();
        $scope.acta.Comentario = $scope.acta.Motivo;

        angular.forEach($scope.solicitudesAlumno, function (value, key) {
            $scope.solicitudesAlumno[key].alumno.grupo = $scope.grupo;
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

    $scope.grabarActaDocente = function () {
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

    $scope.obtenerUsuarioAutenticado();
    $scope.obtenerCarreras();
    $scope.obtenerMaterias();
    $scope.obtenerSubtipos();
    $scope.obtenerTipos();
});