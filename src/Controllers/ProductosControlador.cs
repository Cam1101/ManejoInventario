using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductosController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        //LISTAR TODOS LOS PRODUCTOS//
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Listar()
        {
            return await _context.Productos.ToListAsync();
        }

        //LISTAR PRODUCTO POR ID//
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> ListarPorId(int id)
        {
            var p = await _context.Productos.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            return p;
        }

        //EDITAR PRODUCTO//
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarProducto(int id, Producto p)
        {
            p.Id = id;
            if(p.Stock <= p.MaxStock*0.2){
                p.Stock = p.MaxStock;
            }
            _context.Entry(p).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! _context.Productos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new {message = "Producto modificado con éxito"});
        }

        //GUARDAR PRODUCTO//
        [HttpPost]
        public async Task<ActionResult<Producto>> GuardarProducto(Producto p)
        {
            _context.Productos.Add(p);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = p.Id }, p);
        }

        //BORRAR PRODUCTO//
        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarProducto(int id)
        {
            var p = await _context.Productos.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(p);
            await _context.SaveChangesAsync();

            return Ok(new {message = "Producto "+ id +" eliminado con éxito"});
        }

        //FUNCION REABASTECER AUTOMATICA//
        [HttpPut("{id}/reabastecer")]
        public async Task<IActionResult> Reabastecer(int id)
        {
            var p = await _context.Productos.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            // para solo reestablecer el stock si el stock es menor al límite
            if (p.Stock >= p.MaxStock)
            {
                return BadRequest(new { message = "Las existencias del producto están al máximo" });
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("http://localhost:5005/proveedor", null);
            if (response.IsSuccessStatusCode)
            {
                p.Stock = p.MaxStock;
                _context.Entry(p).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new { message = "El stock fue asignado a su máximo valor" });
            }
            else
            {
                return StatusCode(500, new { message = "Error al reponer existencias" });
            }
        }

    }
}