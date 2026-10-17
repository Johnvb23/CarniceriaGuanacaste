using Microsoft.AspNetCore.Mvc;
using CarniceriaGuanacaste.Models;
using CarniceriaGuanacaste.Data;
using System;
using System.Linq;

namespace CarniceriaGuanacaste.Web.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public IActionResult Index()
        {
            var clientes = AlmacenDatos.Clientes;
            return View(clientes);
        }

        // GET: Clientes/Registrar
        public IActionResult Registrar()
        {
            return View();
        }

        // POST: Clientes/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Id = Guid.NewGuid();
                AlmacenDatos.Clientes.Add(cliente);
                TempData["Mensaje"] = "Cliente registrado con éxito.";
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Buscar
        public IActionResult Buscar(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                return View(null);
            }

            var cliente = AlmacenDatos.Clientes
                .FirstOrDefault(c => c.Identificacion.Equals(identificacion, StringComparison.OrdinalIgnoreCase));

            if (cliente == null)
            {
                ViewBag.Mensaje = "No se encontró ningún cliente con el número de identificación proporcionado.";
            }

            return View(cliente);
        }

        // GET: Clientes/Actualizar/5
        public IActionResult Actualizar(Guid id)
        {
            var cliente = AlmacenDatos.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Actualizar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Actualizar(Cliente clienteModificado)
        {
            if (ModelState.IsValid)
            {
                var clienteExistente = AlmacenDatos.Clientes.FirstOrDefault(c => c.Id == clienteModificado.Id);
                if (clienteExistente != null)
                {
                    clienteExistente.TipoIdentificacion = clienteModificado.TipoIdentificacion;
                    clienteExistente.Identificacion = clienteModificado.Identificacion;
                    clienteExistente.Nombre = clienteModificado.Nombre;
                    clienteExistente.PrimerApellido = clienteModificado.PrimerApellido;
                    clienteExistente.SegundoApellido = clienteModificado.SegundoApellido;
                    clienteExistente.FechaNacimiento = clienteModificado.FechaNacimiento;
                    clienteExistente.Telefono = clienteModificado.Telefono;
                    clienteExistente.Correo = clienteModificado.Correo;
                    clienteExistente.Direccion = clienteModificado.Direccion;

                    TempData["Mensaje"] = "Cliente actualizado con éxito.";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(clienteModificado);
        }

        // GET: Clientes/Eliminar/5
        public IActionResult Eliminar(Guid id)
        {
            var cliente = AlmacenDatos.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente != null)
            {
                AlmacenDatos.Clientes.Remove(cliente);
                TempData["Mensaje"] = "Cliente eliminado con éxito.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}