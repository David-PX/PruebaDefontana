using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaDefontana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDefontana
{
    internal static class Controller
    {
        public static List<VentaDetalle> consultaDetalleDeVenta(int UltimosDias)
        {
            // Aqui agregamos el archivo de configuracion, para poder usarlo en el DbContext llamando al connectionString
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Instanciamos el DbContext y le pasamos nuestra configuracion
            PruebaContext _context = new PruebaContext(configuration);

            // Realizamos una unica consulta a la bases de datos que incluye las ventas, el detalle de las ventas, los productos y los locales
            List<VentaDetalle> ventas = _context.VentaDetalles
                .Include(x => x.IdVentaNavigation)
                .Include(x => x.IdVentaNavigation.IdLocalNavigation)
                .Include(x => x.IdProductoNavigation)
                .Include(x => x.IdProductoNavigation.IdMarcaNavigation)
                // Aqui agregamos una condicion para filtrar solamente los registros de los días que se indiquen mediante la variable UltimosDias
                .Where(x => x.IdVentaNavigation.Fecha >= DateTime.Now.AddDays(-UltimosDias))
                .ToList();

            return ventas;
        }
    }
}
