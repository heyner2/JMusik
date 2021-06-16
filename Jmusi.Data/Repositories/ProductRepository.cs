using JMusik.Data.Contracts;
using JMusik.Models;
using JMusik.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.Repositories
{

    public class ProductRepository : IproductosRepository
    {
        private TiendaDbContext _contexto;

        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(TiendaDbContext contexto, ILogger<ProductRepository> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }


        public async Task<bool> Actualizar(Producto producto)
        {
            //extraigo el producto de la base de datos con el id, luego reemplazo los valores que vienen para edicion 
            var nProducto =await ObtenerProductoAsync(producto.Id);
            if (nProducto == null)
            {
                return false;
            }
            nProducto.Nombre = producto.Nombre;
            nProducto.Precio = producto.Precio;
            //_contexto.Productos.Attach(producto);
            //_contexto.Entry(producto).State = EntityState.Modified;
            try
            {
                //el metodo savechangesasync reconoce que la entidad nProductos ha sido modificada y guarda los cambios
                return await _contexto.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(Actualizar)} : {excepcion.Message}");
                ;
            }
            return false;
        }

        public async Task<Producto> Agregar(Producto producto)
        {
            producto.Estatus = EstatusProducto.Activo;
            producto.FechaRegistro = DateTime.UtcNow;
            _contexto.Productos.Add(producto);
            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(Agregar)} : {excepcion.Message}");
                ;
            }

            return producto;
        }

        public async Task<bool> Eliminar(int id)
        {
            //Se realiza una eliminación suave, solamente inactivamos el producto

            var producto = await _contexto.Productos
                                .SingleOrDefaultAsync(c => c.Id == id);
            if (producto == null)
            {
                return false;
            }
            producto.Estatus = EstatusProducto.Inactivo;
            _contexto.Productos.Attach(producto);
            _contexto.Entry(producto).State = EntityState.Modified;

            try
            {
                return (await _contexto.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(Eliminar)} : {excepcion.Message}");
                ;
            }
            return false;

        }

        public async Task<Producto> ObtenerProductoAsync(int id)
        {
            return await _contexto.Productos
                               .SingleOrDefaultAsync(c => c.Id == id && c.Estatus==EstatusProducto.Activo);
        }

        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            return await _contexto.Productos.
                Where(s=> s.Estatus==EstatusProducto.Activo).
                OrderBy(u => u.Nombre)
                                            .ToListAsync();
        }

        public async Task<(int totalDeRegistros, IEnumerable<Producto> registros)>
            ObtenerPaginasProductosAsync(int paginaActual,int registrosPorPagina)
        {
            var totalDeRegistros = await _contexto.Productos.Where(u=>u.Estatus==EstatusProducto.Activo).CountAsync();

            var registros = await _contexto.Productos.Skip((paginaActual - 1) * registrosPorPagina).
                Take(registrosPorPagina).ToListAsync();

            return (totalDeRegistros, registros);
        }
    }

}
