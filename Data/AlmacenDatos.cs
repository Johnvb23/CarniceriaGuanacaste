using System.Collections.Generic;
using CarniceriaGuanacaste.Models;

namespace CarniceriaGuanacaste.Data
{
    public static class AlmacenDatos
    {
        public static List<Empleado> Empleados { get; set; } = new List<Empleado>();
        public static List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public static List<Producto> Productos { get; set; } = new List<Producto>();
        public static List<Proveedor> Proveedores { get; set; } = new List<Proveedor>();
    }
}