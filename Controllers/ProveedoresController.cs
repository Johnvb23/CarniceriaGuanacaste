using Microsoft.AspNetCore.Mvc;
using CarniceriaGuanacaste.Models;
using CarniceriaGuanacaste.Data;
using System;
using System.Linq;

namespace CarniceriaGuanacaste.Web.Controllers
{
    public class ProveedoresController : Controller
    {
        // GET: Proveedores
        public IActionResult Index()
        {
            var proveedores = AlmacenDatos.Proveedores;
            return View(proveedores);
        }

        // GET: Proveedores/Registrar
        public IActionResult Registrar()
        {
            return View();
        }

        // POST: Proveedores/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                proveedor.Id = Guid.NewGuid();
                AlmacenDatos.Proveedores.Add(proveedor);
                TempData["Mensaje"] = "Proveedor registrado con éxito.";
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        // GET: Proveedores/Buscar
        public IActionResult Buscar(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                return View(null);
            }

            var proveedor = AlmacenDatos.Proveedores
                .FirstOrDefault(p => p.Identificacion.Equals(identificacion, StringComparison.OrdinalIgnoreCase));

            if (proveedor == null)
            {
                ViewBag.Mensaje = "No se encontró ningún proveedor con la identificación proporcionada.";
            }

            return View(proveedor);
        }

        // GET: Proveedores/Actualizar/5
        public IActionResult Actualizar(Guid id)
        {
            var proveedor = AlmacenDatos.Proveedores.FirstOrDefault(p => p.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedores/Actualizar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Actualizar(Proveedor proveedorModificado)
        {
            if (ModelState.IsValid)
            {
                var proveedorExistente = AlmacenDatos.Proveedores.FirstOrDefault(p => p.Id == proveedorModificado.Id);
                if (proveedorExistente != null)
                {
                    proveedorExistente.Identificacion = proveedorModificado.Identificacion;
                    proveedorExistente.NombreComercial = proveedorModificado.NombreComercial;
                    proveedorExistente.Contacto = proveedorModificado.Contacto;
                    proveedorExistente.Telefono = proveedorModificado.Telefono;
                    proveedorExistente.Correo = proveedorModificado.Correo;
                    proveedorExistente.Direccion = proveedorModificado.Direccion;

                    TempData["Mensaje"] = "Proveedor actualizado con éxito.";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(proveedorModificado);
        }

        // GET: Proveedores/Eliminar/5
        public IActionResult Eliminar(Guid id)
        {
            var proveedor = AlmacenDatos.Proveedores.FirstOrDefault(p => p.Id == id);
            if (proveedor != null)
            {
                AlmacenDatos.Proveedores.Remove(proveedor);
                TempData["Mensaje"] = "Proveedor eliminado con éxito.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}