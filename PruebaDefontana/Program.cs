
using PruebaDefontana;
using PruebaDefontana.Models;

public class Program
{
    public static void Main(string[] args)
    {
        // Traemos desde el controlador toda la data que estaremos utilizando en las consultas.
        List<VentaDetalle> datos = Controller.consultaDetalleDeVenta(30);

        //El total de ventas de los últimos 30 días(monto total y Q de ventas).
        int totalVentas = datos.Sum(x => x.TotalLinea);
        Console.WriteLine($"El total de ventas de los ultimos 30 días es: {totalVentas.ToString("N")}");

        // Aqui con esta sencilla consulta seleccionamos toda la data de las siguientes 3 consultas, seleccionando
        // el registro con el mayor monto total de ventas, una vez con este registro ya tenemos lo necesario para desplegar toda la informacion
        var detalleDeVentasPorProductosYTotales = datos.OrderByDescending(x => x.IdVentaNavigation.Total).FirstOrDefault();

        //El día y hora en que se realizó la venta con el monto más alto(y cuál es aquel monto).
        Console.WriteLine($"La fecha y hora en que se realizo la venta mas grande fue " +
        $"{detalleDeVentasPorProductosYTotales?.IdVentaNavigation.Fecha.ToString("yyyy-MM-dd HH:mm:ss")} y su monto es de {detalleDeVentasPorProductosYTotales?.IdVentaNavigation.Total.ToString("N")}");


        //Indicar cuál es el producto con mayor monto total de ventas.
        Console.WriteLine($"El producto con mayor monto total de ventas es: {detalleDeVentasPorProductosYTotales?.IdProductoNavigation.Nombre}");


        //Indicar el local con mayor monto de ventas.
        Console.WriteLine($"El local con mayor monto total de ventas es: {detalleDeVentasPorProductosYTotales?.IdVentaNavigation.IdLocalNavigation.Nombre}");

        Console.ReadKey();


    }
}