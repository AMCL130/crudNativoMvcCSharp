using Microsoft.AspNetCore.Mvc;
using tallerCrud.Data;
using tallerCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace tallerCrud.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AplicationDbContext _context;
        public ClientesController(AplicationDbContext context)
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
            var clientes = from cliente in _context.Cliente select cliente;

            if (buscar != 0 && buscar > 0)
            {
                clientes = clientes.Where(a => a.Cedula == buscar);
            }
            return View(await clientes.ToListAsync());
        }

        public IActionResult Create(Cliente cliente)
        {
            //validar el modelo
            if (ModelState.IsValid)
            {
                _context.Cliente.Add(cliente);
                _context.SaveChanges();  /*guardan los cambiops*/

                TempData["Mensaje"] = "cliente creado exitosamente";

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

            var cliente = _context.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);


        }


        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            //validar el modelo
            if (ModelState.IsValid)
            {
                _context.Cliente.Update(cliente);
                _context.SaveChanges();  /*guardan los cambiops*/

                TempData["Mensaje"] = "Cliente actulizado exitosamente";

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

            var cliente = _context.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteCliente(int? id)
        {
            var cliente = _context.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            _context.SaveChanges();

            TempData["Mensaje"] = "Cliente eliminado exitosamente";
            return RedirectToAction("Index");
        }
    }
}
