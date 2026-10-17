using Microsoft.AspNetCore.Mvc;
using CarniceriaGuanacaste.Models;
using CarniceriaGuanacaste.Data;
using System;
using System.Linq;

namespace CarniceriaGuanacaste.Web.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos
        public IActionResult Index()
        {
            var productos = AlmacenDatos.Productos;
            return View(productos);
        }

        // GET: Productos/Registrar
        public IActionResult Registrar()
        {
            return View();
        }

        // POST: Productos/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.Id = Guid.NewGuid();
                AlmacenDatos.Productos.Add(producto);
                TempData["Mensaje"] = "Producto registrado con éxito.";
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Buscar
        public IActionResult Buscar(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
            {
                return View(null);
            }

            var producto = AlmacenDatos.Productos
                .FirstOrDefault(p => p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));

            if (producto == null)
            {
                ViewBag.Mensaje = "No se encontró ningún producto con el código ingresado.";
            }

            return View(producto);
        }

        // GET: Productos/Actualizar/5
        public IActionResult Actualizar(Guid id)
        {
            var producto = AlmacenDatos.Productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Actualizar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Actualizar(Producto productoModificado)
        {
            if (ModelState.IsValid)
            {
                var productoExistente = AlmacenDatos.Productos.FirstOrDefault(p => p.Id == productoModificado.Id);
                if (productoExistente != null)
                {
                    productoExistente.Codigo = productoModificado.Codigo;
                    productoExistente.Nombre = productoModificado.Nombre;
                    productoExistente.Descripcion = productoModificado.Descripcion;
                    productoExistente.Precio = productoModificado.Precio;
                    productoExistente.Stock = productoModificado.Stock;
                    productoExistente.Categoria = productoModificado.Categoria;

                    TempData["Mensaje"] = "Producto actualizado con éxito.";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(productoModificado);
        }

        // GET: Productos/Eliminar/5
        public IActionResult Eliminar(Guid id)
        {
            var producto = AlmacenDatos.Productos.FirstOrDefault(p => p.Id == id);
            if (producto != null)
            {
                AlmacenDatos.Productos.Remove(producto);
                TempData["Mensaje"] = "Producto eliminado con éxito.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}