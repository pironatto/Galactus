using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Galactus.Data;
using Galactus.Models;
using Microsoft.AspNetCore.Http;

namespace Galactus.Controllers
{    
    public class PreferenciasController : Controller
    {


        private readonly GalactusContext _context;

        public PreferenciasController(GalactusContext context)
        {
            _context = context;
        }

        // GET: Preferencias
        public async Task<IActionResult> Index(string nome)
        {
            // PARA PEGAR O TICK DO JOGO NA TABELA DO BANCO DE DADOS
            var timeControle = await _context.TimeControle.FirstOrDefaultAsync(m => m.Id == 1);
            TempData["tempo"] = timeControle.Tempo.ToString();

            nome = HttpContext.Session.GetString("nome");
            var planeta =  _context.Planeta.Where(m => m.Nome == nome).ToList();
            return View(planeta);
        }

        // GET: Preferencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // PARA PEGAR O TICK DO JOGO NA TABELA DO BANCO DE DADOS
            var timeControle = await _context.TimeControle.FirstOrDefaultAsync(m => m.Id == 1);
            TempData["tempo"] = timeControle.Tempo.ToString();

            if (id == null)
            {
                return NotFound();
            }

            var planeta = await _context.Planeta.FindAsync(id);
            if (planeta == null)
            {
                return View(TempData["mensagem"]= "Dados da conta incorretos");
            }
            return View(planeta);
        }

        // POST: Preferencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email")] Planeta planeta)
        {
            if (id != planeta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planeta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanetaExists(planeta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(planeta);
        }

        // GET: Preferencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // PARA PEGAR O TICK DO JOGO NA TABELA DO BANCO DE DADOS
            var timeControle = await _context.TimeControle.FirstOrDefaultAsync(m => m.Id == 1);
            TempData["tempo"] = timeControle.Tempo.ToString(); 

            if (id == null)
            {
                return NotFound();
            }

            var planeta = await _context.Planeta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planeta == null)
            {
                return NotFound();
            }

            return View(planeta);
        }

        // POST: Preferencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planeta = await _context.Planeta.FindAsync(id);
            _context.Planeta.Remove(planeta);
            await _context.SaveChangesAsync();
            HttpContext.Session.Clear();
            return View("~/Views/Home/Index.cshtml");
        }

        private bool PlanetaExists(int id)
        {
            return _context.Planeta.Any(e => e.Id == id);
        }
    }
}
