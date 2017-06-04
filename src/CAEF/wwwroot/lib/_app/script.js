var app=angular.module("caef",["angularModalService"]);!function(o){"use strict";o.controller("UsuariosController",["$scope","$location","$window","$http","ModalService",function(o,e,n,t,r){o.usuarios={},o.error="",o.roles={},o.usuarioAutenticado={},o.obtenerUsuarios=function(){t.get("/Usuarios/Usuarios").then(function(e){console.log(e.data),o.usuarios=e.data},function(e){o.error="Error al obtener usuarios: "+e})},o.obtenerRoles=function(){t.get("/CAEF/Roles").then(function(e){o.roles=e.data},function(e){o.error="Error al obtener roles: "+e})},o.editarUsuario=function(e,a){o.error="",r.showModal({templateUrl:"views/editarUsuario.html",controller:"EditarController",inputs:{usuario:e,roles:a}}).then(function(o){o.element.modal(),o.close.then(function(o){e.rol.nombre!=o.nombre&&(console.log(o),e.rolId=o.id,t.post("/Usuarios/Editar",e).then(function(o){r.showModal({templateUrl:"views/mensajeGenerico.html",controller:"MensajeController",inputs:{mensaje:o.data}}).then(function(o){o.element.modal(),o.close.then(function(){n.location="/Usuarios"})})},function(o){r.showModal({templateUrl:"views/mensajeGenerico.html",controller:"MensajeController",inputs:{mensaje:o.data}}).then(function(o){o.element.modal(),o.close.then(function(){n.location="/Usuarios"})})}))})})},o.borrarUsuario=function(e){o.error="",console.log("Quieres borrar usuario:"+e.correo),r.showModal({templateUrl:"views/borrarUsuario.html",controller:"BorrarController"}).then(function(o){o.element.modal(),o.close.then(function(o){o&&t.post("/Usuarios/Borrar",e).then(function(o){r.showModal({templateUrl:"views/mensajeGenerico.html",controller:"MensajeController",inputs:{mensaje:o.data}}).then(function(o){o.element.modal(),o.close.then(function(){n.location="/Usuarios"})})},function(o){r.showModal({templateUrl:"views/mensajeGenerico.html",controller:"MensajeController",inputs:{mensaje:o.data}}).then(function(o){o.element.modal(),o.close.then(function(){n.location="/Usuarios"})})})})})},o.obtenerUsuarioAutenticado=function(){t.get("/UsuarioActual").then(function(e){console.log(e.data),o.usuarioAutenticado=e.data,console.log(o.usuarioAutenticado)})},o.obtenerUsuarios(),o.obtenerRoles(),o.obtenerUsuarioAutenticado()}]),o.controller("BorrarController",["$scope","close",function(o,e){o.close=function(o){e(o,500)}}]),o.controller("EditarController",["$scope","$element","usuario","roles","close",function(o,e,n,t,r){console.log(n),o.usuario=n,o.rolActual=n.rol,o.listaRoles=t,o.rolSeleccionado=o.rolActual,o.accept=function(){console.log("Escogiste rol: "+o.rolSeleccionado.nombre),r(o.rolSeleccionado,500)},o.cancel=function(){console.log("Escogiste rol: "+o.rolSeleccionado.nombre),r(o.rolActual,500)}}]),o.controller("MensajeController",["$scope","mensaje","close",function(o,e,n){o.mensaje=e,o.close=function(){n(!0,500)}}]),o.controller("LoginController",["$scope","$location","$window","$http","ModalService",function(o,e,n,t,r){o.usuario={},o.returnUrl=decodeURIComponent(e.absUrl().split("ReturnUrl=")[1]),console.log(o.returnUrl),o.login=function(){console.log(o.usuario),t.post("/Login",o.usuario).then(function(e){console.log("Success"),console.log(e),n.location=o.returnUrl},function(e){console.log(e),r.showModal({templateUrl:"views/mensajeGenerico.html",controller:"MensajeController",inputs:{mensaje:e.data}}).then(function(e){e.element.modal(),e.close.then(function(){o.usuario.Password=""})})})}}]),o.controller("MenuController",["$scope","$http","$location",function(o,e,n){o.usuarioAutenticado={},o.urlActual="/"+decodeURIComponent(n.absUrl().split("/")[3]),console.log(o.urlActual),o.obtenerUsuarioAutenticado=function(){e.get("/UsuarioActual").then(function(e){o.usuarioAutenticado=e.data})},o.isActive=function(e){return e===o.urlActual},o.obtenerUsuarioAutenticado()}]),o.controller("ActasController",["$scope","$http","$location","$window","$filter","ModalService",function(o,e,n,t,r,a){o.usuarioAutenticado={},o.acta={Periodo:(new Date).getMonth()<5?(new Date).getFullYear()+"-1":(new Date).getFullYear()+"-2"},o.carreras=[],o.materias=[],o.subtipos=[],o.tipos=[],o.fecha=new Date,o.solicitudesAlumno=[{Alumno:{Grupo:""}}],o.grupo="",o.agregarColumna=function(){var e={Alumno:{Grupo:""}};o.solicitudesAlumno.push(e)},o.obtenerUsuarioAutenticado=function(){e.get("/UsuarioActual").then(function(e){o.usuarioAutenticado=e.data,o.acta.IdEmpleado=o.usuarioAutenticado.id})},o.obtenerCarreras=function(){e.get("/CAEF/Carreras").then(function(e){o.carreras=e.data},function(e){o.error="Error al obtener carreras: "+e})},o.obtenerMaterias=function(){e.get("/CAEF/Materias").then(function(e){o.materias=e.data},function(e){o.error="Error al obtener materias: "+e})},o.obtenerSubtipos=function(){e.get("/CAEF/Subtipos").then(function(e){o.subtipos=e.data},function(e){o.error="Error al obtener subtipos: "+e})},o.obtenerTipos=function(){e.get("/CAEF/Tipos").then(function(e){o.tipos=e.data},function(e){o.error="Error al obtener tipos: "+e})},o.calculaClaveMateria=function(){o.acta.Materia?o.acta.IdMateria=JSON.parse(o.acta.Materia).id:o.acta.IdMateria=""},o.calculaClaveCarrera=function(){o.acta.IdCarrera=JSON.parse(o.acta.Carrera).id},o.filtraMaterias=function(e){return!!o.acta.Carrera&&e.carrera===JSON.parse(o.acta.Carrera).nombre},o.grabar=function(){o.acta.IdEstado=1,o.acta.FechaCreacion=new Date,angular.forEach(o.solicitudesAlumno,function(e,n){o.solicitudesAlumno[n].Alumno.Grupo=o.grupo}),console.log(o.solicitudesAlumno),e.post("/CAEF/AgregarActaDocente",o.acta).then(function(n){console.log("Success"),console.log(n),angular.forEach(o.solicitudesAlumno,function(e,t){o.solicitudesAlumno[t].IdSolicitud=n.data}),e.post("/CAEF/AgregarSolicitudAlumno",o.solicitudesAlumno).then(function(o){console.log("Success"),console.log(o),a.showModal({templateUrl:"views/mensajeGenerico.html",controller:"MensajeController",inputs:{mensaje:o.data}}).then(function(o){o.element.modal(),o.close.then(function(){t.location="/Acta"})})},function(o){console.log(o),a.showModal({templateUrl:"views/mensajeGenerico.html",controller:"MensajeController",inputs:{mensaje:o.data}}).then(function(o){o.element.modal(),o.close.then(function(){})})})},function(o){console.log(o),a.showModal({templateUrl:"views/mensajeGenerico.html",controller:"MensajeController",inputs:{mensaje:o.data}}).then(function(o){o.element.modal(),o.close.then(function(){})})})},o.obtenerUsuarioAutenticado(),o.obtenerCarreras(),o.obtenerMaterias(),o.obtenerSubtipos(),o.obtenerTipos()}]),o.controller("AgregarController",["$scope","$http","$window","$location","ModalService",function(o,e,n,t,r){o.roles={},o.usuario={},o.roles={},o.agregar=function(){console.log(o.usuario),e.post("/Usuarios/Agregar",o.usuario).then(function(o){console.log("Success"),console.log(o),r.showModal({templateUrl:"views/mensajeGenerico.html",controller:"MensajeController",inputs:{mensaje:o.data}}).then(function(o){o.element.modal(),o.close.then(function(){n.location="/Usuarios"})})},function(o){console.log(o),r.showModal({templateUrl:"views/mensajeGenerico.html",controller:"MensajeController",inputs:{mensaje:o.data}}).then(function(o){o.element.modal(),o.close.then(function(){})})})},o.obtenerRoles=function(){e.get("/CAEF/Roles").then(function(e){o.roles=e.data},function(e){o.error="Error al obtener roles: "+e})},o.obtenerRoles()}])}(app);