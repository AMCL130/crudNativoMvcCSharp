using Microsoft.AspNetCore.Mvc;
using tallerCrud.Data;
using tallerCrud.Models;
using Microsoft.EntityFrameworkCore;


namespace tallerCrud.Controllers
{
    public class ProductosController : Controller
    {
        private readonly AplicationDbContext _context;
        public ProductosController(AplicationDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    IEnumerable<Libro> ListLibros = _context.Libro;
        //    return View(ListLibros);
        //}

        public async Task<IActionResult> Index(int buscar)
        {
            var productos = from producto in _context.Producto select producto;

            if (buscar != 0 && buscar > 0)
            {
                productos = productos.Where(a => a.Codigo== buscar);
            }
            return View(await productos.ToListAsync());
        }

        public IActionResult Create(Producto producto)
        {
            //validar el modelo
            if (ModelState.IsValid)
            {
                _context.Producto.Add(producto);
                _context.SaveChanges();  /*guardan los cambiops*/

                TempData["Mensaje"] = "Producto creado exitosamente";

                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var producto = _context.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);


        }


        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            //validar el modelo
            if (ModelState.IsValid)
            {
                _context.Producto.Update(producto);
                _context.SaveChanges();  /*guardan los cambiops*/

                TempData["Mensaje"] = "Producto actulizado exitosamente";

                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var producto = _context.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteProducto(int? id)
        {
            var producto = _context.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Producto.Remove(producto);
            _context.SaveChanges();

            TempData["Mensaje"] = "Producto eliminado exitosamente";
            return RedirectToAction("Index");
        }
    }
}
