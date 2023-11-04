using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TurnosRotativos.DTOs;
using TurnosRotativos.Exceptions;
using TurnosRotativos.Models;

namespace TurnosRotativos.Validations
{
    public static class Validadaciones
    {

        /*Validaciones para los campos de Empleado*/
        public static void ValidarEdad(string fecha)
        {
            DateTime fechaNacimiento = DateTime.ParseExact(fecha, "dd/MM/yyyy", null);

            if (fechaNacimiento.AddYears(18) >= DateTime.Now)
            {
                throw new CustomException("La edad del empleado no puede ser menor a 18 años.", 400);
            }
        }

        public static void ValidarEmail(string email)
        {
            if (!email.Contains('@') || !email.Contains(".com"))
            {
                throw new CustomException("El email ingresado no es correcto.", 400);
            }
        }

        public static void ValidarFechaDeIngreso(string Fecha)
        {
            DateTime fecha = DateTime.ParseExact(Fecha, "dd/MM/yyyy", null);

            if (fecha > DateTime.Now)
            {
                throw new CustomException("La fecha de ingreso no puede ser posterior al día de la fecha.", 400);
            }
        }

        public static void ValidarFechaNacimiento(string Fecha)
        {
            DateTime fecha = DateTime.ParseExact(Fecha, "dd/MM/yyyy", null);

            if (fecha > DateTime.Now)
            {
                throw new CustomException("La fecha de nacimiento no puede ser posterior al día de la fecha.", 400);
            }
        }

        public static void ValidarNombres(string dato, string nombreDato)
        {
            if (!Regex.IsMatch(dato, @"^[a-zA-Z]+$"))
            {
                throw new CustomException($"Solo se permiten letras en el campo: {nombreDato} ", 400);
            }

            if (!Regex.IsMatch(dato, @"^[a-zA-Z]+$"))
            {
                throw new CustomException($"Solo se permiten letras en el campo: {nombreDato}", 400);
            }
        }
        public static void ValidarDNI(int nroDocumento)
        {
            if (nroDocumento.GetType() != typeof(int) || nroDocumento <= 0)
            {
                throw new CustomException("El DNI ingresado no es correcto.", 400);
            }
        }

        public static void ValidarEmpleado(EmpleadoCreacionDTO empleado)
        {
            ValidarEdad(empleado.FechaNacimiento);
            ValidarEmail(empleado.Email);
            ValidarFechaDeIngreso(empleado.FechaIngreso);
            ValidarNombres(empleado.Nombre, "nombre");
            ValidarNombres(empleado.Apellido, "apellido");
            ValidarDNI(empleado.NroDocumento);
        }

        /*Validaciones para los campos de Jornada*/

        public static void ValidarHsSemanales(Empleado empleado, Jornada nueva)
        {
            int? hsDeTrabajo = 0;
            foreach (var jornada in empleado.Jornadas)
            {
                if (hsDeTrabajo <= 48)
                {
                    hsDeTrabajo += jornada.HorasTrabajadas;
                }
            }

            if (hsDeTrabajo + nueva.HorasTrabajadas > 48)
            {
                throw new CustomException("El empleado ingresado supera las 48 horas semanales.", 400);
            }

        }

        public static void ValidarTurnos(Empleado empleado, int idConcepto)
        {
            int contadorConceptos = 0;

            foreach (var jornada in empleado.Jornadas)
            {
                if (jornada.IdConcepto == idConcepto)
                {
                    contadorConceptos += 1;
                }
            }

            if (idConcepto == 1 && contadorConceptos == 5)
            {
                throw new CustomException("El empleado ingresado ya cuenta con 5 turnos normales esta semana.", 400);
            }

            if (idConcepto == 2 && contadorConceptos == 3)
            {
                throw new CustomException("El empleado ingresado ya cuenta con 3 turnos extra esta semana.", 400);
            }

            if (idConcepto == 3 && contadorConceptos == 2)
            {
                throw new CustomException("El empleado no cuenta con más días libres esta semana.", 400);
            }
        }
    }
}
