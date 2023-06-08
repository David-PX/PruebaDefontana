USE [Prueba]

--¿Cuál es la marca con mayor margen de ganancias?

DECLARE @MargenVentasPorMarca INT;
DECLARE @NombreMarca VARCHAR(50);

DECLARE CursorMargenVentasPorMarca CURSOR FOR
SELECT TOP(1) sum(VD.TotalLinea) MargenVentasPorMarca, M.Nombre
FROM VentaDetalle VD 
INNER JOIN Producto P on P.ID_Producto = VD.ID_Producto 
JOIN Marca M on M.ID_Marca = P.ID_Marca 
Group by M.Nombre
ORDER BY MargenVentasPorMarca DESC

OPEN CursorMargenVentasPorMarca;

FETCH NEXT FROM CursorMargenVentasPorMarca INTO @MargenVentasPorMarca, @NombreMarca

WHILE @@FETCH_STATUS = 0
BEGIN
	PRINT 'La marca con el mayor margen de ventas es: ' + @NombreMarca + ', con un total de ventas de: ' + CAST(@MargenVentasPorMarca AS VARCHAR);
    FETCH NEXT FROM CursorMargenVentasPorMarca INTO @MargenVentasPorMarca, @NombreMarca;
END;

CLOSE CursorMargenVentasPorMarca;
DEALLOCATE CursorMargenVentasPorMarca


--¿Cómo obtendrías cuál es el producto que más se vende en cada local?

DECLARE @VentasPorProducto INT;
DECLARE @NombreProducto VARCHAR(100);

DECLARE CursorVentasPorProducto CURSOR FOR
SELECT TOP(1) sum(VD.Cantidad) AS VentasPorProducto, P.Nombre
FROM VentaDetalle VD 
INNER JOIN Producto P ON P.ID_Producto = VD.ID_Producto 
GROUP BY P.Nombre
ORDER BY VentasPorProducto DESC;

OPEN CursorVentasPorProducto;

FETCH NEXT FROM CursorVentasPorProducto INTO @VentasPorProducto, @NombreProducto;

WHILE @@FETCH_STATUS = 0
BEGIN
	PRINT 'El Producto mas vendido fue: ' + @NombreProducto + ', con una cantidad total de ventas de: ' + CAST(@VentasPorProducto AS VARCHAR);
    FETCH NEXT FROM CursorVentasPorProducto INTO @VentasPorProducto, @NombreProducto;
END;

CLOSE CursorVentasPorProducto;
DEALLOCATE CursorVentasPorProducto;