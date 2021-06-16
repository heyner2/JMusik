using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JMusik.Data;
using JMusik.Models;
using JMusik.Data.Contracts;
using Jmusik.DTOS;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace JMusik.WebAPI.Controllers
{
    [Authorize(Roles ="Administrador,Vendedor")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IproductosRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(IproductosRepository repository, IMapper mapper, ILogger<ProductosController> logger)
        {
            _repository = repository;
            _mapper = mapper;
           _logger = logger;
        }

        // GET: api/Productos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
        {
           
               var productos= await _repository.ObtenerProductosAsync();
               
            if (productos.Count > 0)
            {
                return _mapper.Map<List<ProductoDto>>(productos);
            }
            else
            {
                return NotFound();
            }
               
            
        }


        //// GET: api/Productos/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Get(int id)
        {
            _logger.LogError($"Error al eliminar el producto, mensaje de error:");
            var producto = await _repository.ObtenerProductoAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            else
            {

                return _mapper.Map<ProductoDto>(producto);
            }



        }
        //// POST: api/Productos
       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Post([FromBody]ProductoDto productoDto)
        {
            
            try
            {
                //utilizo mapper para hacer la reversion del ProductoDTO que recibo a la clase Producto
                var producto = _mapper.Map<Producto>(productoDto);
                //envio el producto a completar sus campos en el repositorio, en el repositorio agrego los campos restantes
                 var Nproducto=await _repository.Agregar(producto);
                if (Nproducto == null)
                {
                    return BadRequest();
                }
                else
                {
                    //mapeo el producto que cree a un Dto para enviar a la vista 
                    var nuevoProductoDto = _mapper.Map<ProductoDto>(Nproducto);
                    return CreatedAtAction(nameof(Post), new {id=nuevoProductoDto.Id}, producto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el producto, mensaje de error: {ex.Message}");
                return BadRequest();
            }
        }

        //// PUT: api/Productos/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Update(int id,[FromBody]ProductoDto productoDto)
        {
            try
            {
                if (productoDto == null)
                {
                    return NotFound();
                }

                var producto = _mapper.Map<Producto>(productoDto);
                var resultado = await _repository.Actualizar(producto);
                if (!resultado)
                {
                    return BadRequest();
                }              
                return productoDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el producto en {nameof(Update)}, mensaje de error: {ex.Message}");
                return BadRequest();
            }
        }

        //// DELETE: api/Productos/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _repository.Eliminar(id);
                if (!resultado)
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el producto, mensaje de error: {ex.Message}");
                return NoContent();
            }

        }
    }
}