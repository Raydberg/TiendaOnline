using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Datos;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment entorno;

        public ProductosController(ApplicationDbContext context,IWebHostEnvironment entorno)
        {
            _context = context;
            this.entorno = entorno;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var productos = _context.Productos
                .Include(p => p.Modelo)
                .ThenInclude(m => m.Marca);
            return View(await productos.ToListAsync());
        }

       

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoDTO productoDTO)
        {
            if (productoDTO.ArchivoImagen==null || productoDTO.ArchivoImagen.Length==0)
            {
                ModelState.AddModelError("ArchivoImagen", "El archivo de imagen es obligatorio");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA");
                return View(productoDTO);
            }

            string rutaCarpetaImagenes = Path.Combine(entorno.WebRootPath, "imagenes");
            string nombreArchivo = $"{DateTime.Now:yyyyMMddHHmmssfff}{Path.GetExtension(productoDTO.ArchivoImagen.FileName)}";
            string rutaCompletaImagen = Path.Combine(rutaCarpetaImagenes, nombreArchivo);

            using (var stream = new FileStream(rutaCompletaImagen, FileMode.Create))
            {
                await productoDTO.ArchivoImagen.CopyToAsync(stream);
            }
            Producto producto = new Producto
            {
                Precio=productoDTO.Precio,
                año=productoDTO.año,
                Color=productoDTO.Color,
                ModeloIDMODELO=productoDTO.ModeloIDMODELO,
                Imagen=nombreArchivo,
                FechaCreacion=DateTime.Now
            };
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var v = _context.Productos.Find(id);
            if (v == null)
            {
                return RedirectToAction("Index");
            }

            var producto = await _context.Productos
                .Include(v => v.Modelo)
                .ThenInclude(m => m.Marca)
                .FirstOrDefaultAsync(v => v.IdProducto == id);
            var productoDTO = new ProductoDTO()
            {
                Precio = v.Precio,
                año = v.año,
                Color = v.Color,
                ModeloIDMODELO = v.ModeloIDMODELO,
                Modelo=v.Modelo,
            };
            ViewData["IdProducto"] = v.IdProducto;
            ViewData["Imagen"] = v.Imagen;
            ViewData["FechaCreacion"] = v.FechaCreacion.ToString("MM/dd/yyyy");



            if (producto == null)
            {
                return NotFound();
            }

            ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA", producto.Modelo.MarcaIDMARCA);
            ViewBag.Modelos = new SelectList(_context.Modelos.Where(m => m.MarcaIDMARCA == producto.Modelo.MarcaIDMARCA), "IDMODELO", "NOM_MODELO", producto.ModeloIDMODELO);
            return View(productoDTO);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ProductoDTO productoDTO)
        {
            var v = _context.Productos.Find(id);

            if (v == null)
            {
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                ViewData["IdProducto"] = v.IdProducto;
                ViewData["Imagen"] = v.Imagen;
                ViewData["FechaCreacion"] = v.FechaCreacion.ToString("MM/dd/yyyy");
                return View(productoDTO);
            }

            string nuevoNombreArchivo = v.Imagen;
            if (productoDTO.ArchivoImagen != null)
            {
                nuevoNombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                nuevoNombreArchivo += Path.GetExtension(productoDTO.ArchivoImagen.FileName);
                string rutaCompletaImagen = entorno.WebRootPath + "/imagenes/" + nuevoNombreArchivo;
                using (var stream = System.IO.File.Create(rutaCompletaImagen))
                {
                    productoDTO.ArchivoImagen.CopyTo(stream);
                }
                string rutaCompletaImagenAntigua = entorno.WebRootPath + "/imagenes/" + v.Imagen;
                System.IO.File.Delete(rutaCompletaImagenAntigua);
            }

            ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA", productoDTO.MarcaID);
            ViewBag.Modelos = new SelectList(_context.Modelos.Where(m => m.MarcaIDMARCA == productoDTO.MarcaID), "IDMODELO", "NOM_MODELO", productoDTO.ModeloIDMODELO);


            v.Precio = productoDTO.Precio;
            v.año = productoDTO.año;
            v.Color = productoDTO.Color;
            v.ModeloIDMODELO = productoDTO.ModeloIDMODELO;
            v.Imagen = nuevoNombreArchivo;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var v = _context.Productos.Find(id);

            if (v == null)
            {
                return RedirectToAction("Index");
            }

            string rutaCompletaImagen = entorno.WebRootPath + "/imagenes/" + v.Imagen;
            System.IO.File.Delete(rutaCompletaImagen);
            _context.Productos.Remove(v);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        private bool VehiculoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
        public JsonResult ObtenerModelos(int idMarca)
        {
            var modelos = _context.Modelos
                .Where(m => m.MarcaIDMARCA == idMarca)
                .Select(m => new { idModelo = m.IdModelo, noM_MODELO = m.Nom_Modelo })
                .ToList();
            return Json(modelos);
        }
    }
}
