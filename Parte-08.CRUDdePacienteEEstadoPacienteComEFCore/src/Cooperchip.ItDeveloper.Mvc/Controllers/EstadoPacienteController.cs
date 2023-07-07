using Cooperchip.ItDeveloper.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cooperchip.ItDeveloper.Mvc.Controllers
{
    public class EstadoPacienteController : Controller
    {
        private readonly ITDeveloperDbContext _context;

        public EstadoPacienteController(ITDeveloperDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            //var model = await _context.EstadoPaciente.ToListAsync();           
            //return View(model);
            return View(await _context.EstadoPaciente.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            //if(id == null)
            //{
            //    return NotFound("Registro não encontrado!");
            //}

            try
            {
                var estadoPaciente = await _context.EstadoPaciente.FirstOrDefaultAsync(e => e.Id == id);
                return View(estadoPaciente);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                       
        }

        //[HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao")] EstadoPaciente estadoPaciente)
        {
            if (ModelState.IsValid)
            {
                estadoPaciente.Id = Guid.NewGuid();
                _context.Add(estadoPaciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(estadoPaciente);
        }

        //[HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var estadoPaciente = await _context.EstadoPaciente.FindAsync(id);

            return estadoPaciente switch
            {
                null => NotFound(),
                _ => View(estadoPaciente)
            };
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Descricao, Id")] EstadoPaciente estadoPaciente)
        {
            if(id != estadoPaciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoPaciente);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoPacienteExists(estadoPaciente.Id))
                    {
                        return NotFound();
                    }else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estadoPaciente);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var estadoPaciente = await _context.EstadoPaciente
                .FirstOrDefaultAsync(m => m.Id == id);

            return estadoPaciente switch
            {
                null => NotFound(),
                _ => View(estadoPaciente)
            };
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var estadoPaciente = await _context.EstadoPaciente.FindAsync(id);

            try
            {
                _context.EstadoPaciente?.Remove(estadoPaciente);
                await _context.SaveChangesAsync();

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }


        private bool EstadoPacienteExists(Guid id)
        {
            return _context.EstadoPaciente.Any(x => x.Id == id);
        }

    }
}
