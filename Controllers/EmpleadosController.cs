using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CarniceriaGuanacaste.Data;
using CarniceriaGuanacaste.Models;

namespace CarniceriaGuanacaste.Controllers
{
    public class EmpleadosController : Controller
    {
        // GET: Empleados (Listado / Consultar)
        public IActionResult Index()
        {
            return View(AlmacenDatos.Empleados);
        }

        // GET: Empleados/Registrar
        public IActionResult Registrar()
        {
            return View();
        }

        // POST: Empleados/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(Empleado empleado)
        {
            // Validación que la identificación ingresada no esté duplicada para registrar un nuevo empleado
            if (AlmacenDatos.Empleados.Any(e => e.Identificacion == empleado.Identificacion))
            {
                ModelState.AddModelError("Identificacion", "Ya existe un empleado registrado con esta identificación.");
            }

            if (ModelState.IsValid)
            {
                if (empleado.Id == Guid.Empty)
                {
                    empleado.Id = Guid.NewGuid();
                }

                AlmacenDatos.Empleados.Add(empleado);
                TempData["Mensaje"] = "Persona empleada registrada satisfactoriamente.";
                return RedirectToAction(nameof(Index));
            }

            // Si hay errores, se conserva la información ingresada en el formulario
            return View(empleado);
        }

        // GET: Empleados/Buscar (Vista independiente requerida por la rúbrica)
        public IActionResult Buscar(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
            {
                return View(null); // Vista inicial vacía
            }

            var empleado = AlmacenDatos.Empleados
                .FirstOrDefault(e => e.Identificacion.Contains(identificacion, StringComparison.OrdinalIgnoreCase));

            if (empleado == null)
            {
                ViewBag.Mensaje = "No se encontró ninguna persona empleada con la identificación ingresada.";
            }

            return View(empleado);
        }

        // GET: Empleados/Actualizar/5
        public IActionResult Actualizar(Guid id)
        {
            var empleado = AlmacenDatos.Empleados.FirstOrDefault(e => e.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Actualizar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Actualizar(Empleado empleadoModificado)
        {
            // Validar que la identificación no pertenezca a otro empleado diferente
            if (AlmacenDatos.Empleados.Any(e => e.Identificacion == empleadoModificado.Identificacion && e.Id != empleadoModificado.Id))
            {
                ModelState.AddModelError("Identificacion", "Ya existe otro empleado registrado con esta identificación.");
            }

            if (ModelState.IsValid)
            {
                var empleadoOriginal = AlmacenDatos.Empleados.FirstOrDefault(e => e.Id == empleadoModificado.Id);
                if (empleadoOriginal == null)
                {
                    return NotFound();
                }

                // Actualizar propiedades
                empleadoOriginal.TipoIdentificacion = empleadoModificado.TipoIdentificacion;
                empleadoOriginal.Identificacion = empleadoModificado.Identificacion;
                empleadoOriginal.Nombre = empleadoModificado.Nombre;
                empleadoOriginal.PrimerApellido = empleadoModificado.PrimerApellido;
                empleadoOriginal.SegundoApellido = empleadoModificado.SegundoApellido;
                empleadoOriginal.FechaNacimiento = empleadoModificado.FechaNacimiento;
                empleadoOriginal.Salario = empleadoModificado.Salario;
                empleadoOriginal.FechaIngreso = empleadoModificado.FechaIngreso;
                empleadoOriginal.Area = empleadoModificado.Area;
                empleadoOriginal.Puesto = empleadoModificado.Puesto;
                empleadoOriginal.Turno = empleadoModificado.Turno;

                TempData["Mensaje"] = "Persona empleada actualizada satisfactoriamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(empleadoModificado);
        }

        // POST: Empleados/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Guid id)
        {
            var empleado = AlmacenDatos.Empleados.FirstOrDefault(e => e.Id == id);
            if (empleado != null)
            {
                AlmacenDatos.Empleados.Remove(empleado);
                TempData["Mensaje"] = "Persona empleada eliminada satisfactoriamente.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}