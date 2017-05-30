using CAEF.Models.Contexts;
using CAEF.Models.Entities.CAEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Models.Seed
{
    public class SemillaCAEF
    {
        private CAEFContext _context;

        public SemillaCAEF(CAEFContext context)
        {
            _context = context;
        }

        /*
         * Genera datos automáticamente dentro de la base de
         * datos (UsuariosUABC) en caso de encontrarse vacía
         */
        public async Task GeneraDatosSemilla()
        {
            // Checa si las tablas de la base de dato se encuentran vacía
            if (!_context.Roles.Any())
            {
                var rolAmin = new Rol()
                {
                     Nombre = "Administrador"
                };

                var rolUsuario = new Rol()
                {
                    Nombre = "Usuario"
                };

                _context.Roles.Add(rolAmin);
                _context.Roles.Add(rolUsuario);
                await _context.SaveChangesAsync();
            }

            if (!_context.Carreras.Any())
            {
                var Civil = new Carrera()
                {
                    Nombre = "Ingeniería Civil"
                };

                var Electronica = new Carrera()
                {
                    Nombre = "Ingeniería Electrónica"
                };

                var Computacion = new Carrera()
                {
                    Nombre = "Ingeniería en Computación"
                };

                var Industrial = new Carrera()
                {
                    Nombre = "Ingeniería Industrial"
                };

                var Bioingenieria = new Carrera()
                {
                    Nombre = "Bioingeniería"
                };

                var Nanotecnologia = new Carrera()
                {
                    Nombre = "Nanotecnología"
                };

                var Arquitectura = new Carrera()
                {
                    Nombre = "Arquitectura"
                };

                _context.Carreras.Add(Civil);
                _context.Carreras.Add(Electronica);
                _context.Carreras.Add(Computacion);
                _context.Carreras.Add(Industrial);
                _context.Carreras.Add(Bioingenieria);
                _context.Carreras.Add(Nanotecnologia);
                _context.Carreras.Add(Arquitectura);
                await _context.SaveChangesAsync();
            }

            if (!_context.Materias.Any())
            {
                var EstucIso = new Materia()
                {
                    Id = 11934,
                    Nombre = "Estructuras Isostáticas",
                    Carrera = "Ingeniería Civil"
                };

                var Dinamica = new Materia()
                {
                    Id = 11347,
                    Nombre = "Dinámica",
                    Carrera = "Ingeniería Civil"
                };

                var CircElec = new Materia()
                {
                    Id = 11675,
                    Nombre = "Circuitos Electrónicos",
                    Carrera = "Ingeniería Electrónica"
                };

                var Mecanismos = new Materia()
                {
                    Id = 11896,
                    Nombre = "Mecanismos",
                    Carrera = "Ingeniería Electrónica"
                };

                var IngSoft = new Materia()
                {
                    Id = 12119,
                    Nombre = "Ingeniería de Software",
                    Carrera = "Ingeniería en Computación"
                };

                var AdmRedes = new Materia()
                {
                    Id = 12117,
                    Nombre = "Admon. y Seguridad en Redes",
                    Carrera = "Ingeniería en Computación"
                };

                var ApWeb = new Materia()
                {
                    Id = 12146,
                    Nombre = "Desarrollo de Aplicaciones Web",
                    Carrera = "Ingeniería en Computación"
                };

                var TermoCienc = new Materia()
                {
                    Id = 4357,
                    Nombre = "Termociencia",
                    Carrera = "Ingeniería Industrial"
                };

                var MetodNum = new Materia()
                {
                    Id = 5311,
                    Nombre = "Métodos Numéricos",
                    Carrera = "Ingeniería Industrial"
                };

                var CircLineal = new Materia()
                {
                    Id = 11789,
                    Nombre = "Circuitos Lineales",
                    Carrera = "Bioingeniería"
                };

                var QuimOrg = new Materia()
                {
                    Id = 11788,
                    Nombre = "Química Orgánica",
                    Carrera = "Bioingeniería"
                };

                var TermDin = new Materia()
                {
                    Id = 13177,
                    Nombre = "Termodinámica",
                    Carrera = "Nanotecnología"
                };

                var BioGen = new Materia()
                {
                    Id = 13179,
                    Nombre = "Biología General",
                    Carrera = "Nanotecnología"
                };

                var MatSis = new Materia()
                {
                    Id = 9761,
                    Nombre = "Materiales y Sistemas Constructivos",
                    Carrera = "Arquitectura"
                };

                var Estructuras = new Materia()
                {
                    Id = 9757,
                    Nombre = "Estructuras",
                    Carrera = "Arquitectura"
                };

                _context.Materias.Add(EstucIso);
                _context.Materias.Add(Dinamica);
                _context.Materias.Add(CircElec);
                _context.Materias.Add(Mecanismos);
                _context.Materias.Add(IngSoft);
                _context.Materias.Add(AdmRedes);
                _context.Materias.Add(ApWeb);
                _context.Materias.Add(TermoCienc);
                _context.Materias.Add(MetodNum);
                _context.Materias.Add(CircLineal);
                _context.Materias.Add(QuimOrg);
                _context.Materias.Add(TermDin);
                _context.Materias.Add(BioGen);
                _context.Materias.Add(MatSis);
                _context.Materias.Add(Estructuras);
                await _context.SaveChangesAsync();
            }

            if (!_context.SubtiposExamen.Any())
            {
                var Complementaria = new SubtipoExamen()
                {
                    Nombre = "Complementaria"
                };

                var Equivalencia = new SubtipoExamen()
                {
                    Nombre = "Equivalencia"
                };

                var Correccion = new SubtipoExamen()
                {
                    Nombre = "Corrección"
                };

                _context.SubtiposExamen.Add(Complementaria);
                _context.SubtiposExamen.Add(Equivalencia);
                _context.SubtiposExamen.Add(Correccion);
                await _context.SaveChangesAsync();
            }

            if (!_context.TiposExamen.Any())
            {
                var Ordinario = new TipoExamen()
                {
                    Nombre = "Ordinario"
                };

                var Extraordinario = new TipoExamen()
                {
                    Nombre = "Extraordinario"
                };

                var Regularizacion = new TipoExamen()
                {
                    Nombre = "Regularizacion"
                };

                var OrdinarioInter = new TipoExamen()
                {
                    Nombre = "Ordinario Intersemestral"
                };

                var ExtraInter = new TipoExamen()
                {
                    Nombre = "Extraordinario intersemestral"
                };

                var RegInter = new TipoExamen()
                {
                    Nombre = "Regularización Intersemestral"
                };

                var EvalPerm = new TipoExamen()
                {
                    Nombre = "Evaluación Permanente"
                };

                _context.TiposExamen.Add(Ordinario);
                _context.TiposExamen.Add(Extraordinario);
                _context.TiposExamen.Add(Regularizacion);
                _context.TiposExamen.Add(OrdinarioInter);
                _context.TiposExamen.Add(ExtraInter);
                _context.TiposExamen.Add(RegInter);
                _context.TiposExamen.Add(EvalPerm);
                await _context.SaveChangesAsync();
            }

            if (!_context.Estados.Any())
            {
                var Pendiente = new Estado()
                {
                    FechaModificacion = DateTime.Now,
                    Nombre = "Pendiente"
                };

                var Aceptado = new Estado()
                {
                    FechaModificacion = DateTime.Now,
                    Nombre = "Aceptado"
                };

                var Rechazado = new Estado()
                {
                    FechaModificacion = DateTime.Now,
                    Nombre = "Rechazado"
                };

                _context.Estados.Add(Pendiente);
                _context.Estados.Add(Aceptado);
                _context.Estados.Add(Rechazado);
                await _context.SaveChangesAsync();
            }

            if (!_context.Usuarios.Any())
            {
                var usuarioA = new Usuario()
                {
                    Id = 338323,
                    Nombre = "José Ramón",
                    ApellidoP = "López",
                    ApellidoM = "Madueño",
                    Correo = "rlopez1@uabc.edu.mx",
                    RolId = 1
                };

                var usuarioB = new Usuario()
                {
                    Id = 335127,
                    Nombre = "César Samuel",
                    ApellidoP = "Parra",
                    ApellidoM = "Salas",
                    Correo = "samuel.parra@uabc.edu.mx",
                    RolId = 1
                };

                var usuarioC = new Usuario()
                {
                    Id = 331364,
                    Nombre = "Celso",
                    ApellidoP = "Figueroa",
                    ApellidoM = "Jacinto",
                    Correo = "celso.figueroa@uabc.edu.mx",
                    RolId = 1
                };

                _context.Usuarios.Add(usuarioA);
                _context.Usuarios.Add(usuarioB);
                _context.Usuarios.Add(usuarioC);
                await _context.SaveChangesAsync();
            }
        }
    }
}
