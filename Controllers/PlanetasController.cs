using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Galactus.Data;
using Galactus.Models;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System;

namespace Galactus.Controllers
{
    public class PlanetasController : Controller
    {
        private readonly GalactusContext _context;

        public PlanetasController(GalactusContext context)
        {
            _context = context;
        }

        // GET: Planetas
        public async Task<IActionResult> Index()
        {
            // PARA PEGAR O TICK DO JOGO NA TABELA DO BANCO DE DADOS
            var timeControle = await _context.TimeControle.FirstOrDefaultAsync(m => m.Id == 1);
            TempData["tempo"] = timeControle.Tempo.ToString();
            return View(await _context.Planeta.ToListAsync());

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string nome, string email)
        {
            var planeta = await _context.Planeta.FirstOrDefaultAsync(m => m.Nome == nome && m.Email == email);

            if (planeta == null)
            {
                ModelState.AddModelError("", "Usuário ou email inválidos");
                return View();
            }
            var name = Request.Form["nome"];
            HttpContext.Session.SetString("nome", planeta.Nome);

            // PARA PEGAR O TICK DO JOGO NA TABELA DO BANCO DE DADOS
            var timeControle = await _context.TimeControle.FirstOrDefaultAsync(m => m.Id == 1);
            TempData["tempo"] = timeControle.Tempo.ToString();
            return View("Index");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("~/Views/Home/Index.cshtml");
        }

        public async Task<IActionResult> Universo()
        {
            // PARA PEGAR O TICK DO JOGO NA TABELA DO BANCO DE DADOS
            var timeControle = await _context.TimeControle.FirstOrDefaultAsync(m => m.Id == 1);
            TempData["tempo"] = timeControle.Tempo.ToString();

            var planetas = _context.Planeta.ToList();
            return View(planetas);
        }

        public async Task<IActionResult> Recursos(string nome)
        {
            // PARA PEGAR O TICK DO JOGO NA TABELA DO BANCO DE DADOS
            var timeControle = await _context.TimeControle.FirstOrDefaultAsync(m => m.Id == 1);
            TempData["tempo"] = timeControle.Tempo.ToString();

            nome = HttpContext.Session.GetString("nome");
            var planeta = _context.Planeta.Where(m => m.Nome == nome).ToList();
            return View(planeta);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Recursos(string iniAst, string qtdAst, [Bind("Carbono,Niobio,Plutonio,AstCarbono,AstNiobio,AstPlutonio,AstLivre")] Planeta planeta)
        {
            var nome = HttpContext.Session.GetString("nome");
            var lista = await _context.Planeta.ToListAsync();
            foreach (var item in lista)
            {
                var numero = await _context.Planeta.FirstOrDefaultAsync(m => m.Nome == nome);
                if (iniAst == null)
                {
                    ViewBag.Status = "Escolha o tipo de asteróide para inicializar!";
                }

                else if (iniAst != null && item.AstLivre < Convert.ToInt32(qtdAst))
                {
                    ViewBag.Status = "Você  não tem asteróides livres suficientes !";

                }
                else
                {                    
                    switch (iniAst)
                    {
                        case "AstCarbono":
                            item.AstCarbono += Convert.ToInt32(qtdAst);
                            item.AstLivre -= Convert.ToInt32(qtdAst);
                            item.Niobio -= 100 * Convert.ToInt32(qtdAst);
                            _context.Update(numero);
                            await _context.SaveChangesAsync();
                            ViewBag.Status = "Asteroides Iniciados !";
                            break;

                        case "AstNiobio":
                            item.AstNiobio += Convert.ToInt32(qtdAst);
                            item.AstLivre -= Convert.ToInt32(qtdAst);
                            item.Niobio -= 100;
                            _context.Update(numero);
                            await _context.SaveChangesAsync();
                            ViewBag.Status = "Asteroides Iniciados !";
                            break;

                        case "AstPlutonio":
                            item.AstPlutonio += Convert.ToInt32(qtdAst);
                            item.AstLivre -= Convert.ToInt32(qtdAst);
                            item.Niobio -= 100;
                            _context.Update(numero);
                            await _context.SaveChangesAsync();
                            ViewBag.Status = "Asteroides Iniciados !";
                            break;

                    }
                }                                            
             
            }
            return View(lista.Where(m => m.Nome == nome));

        }

        // GET: Planetas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

        public async Task<IActionResult> Pesquisa(string nome)
        {
            // PARA PEGAR O TICK DO JOGO NA TABELA DO BANCO DE DADOS
            var timeControle = await _context.TimeControle.FirstOrDefaultAsync(m => m.Id == 1);
            TempData["tempo"] = timeControle.Tempo.ToString();

            nome = HttpContext.Session.GetString("nome");
            var lista = _context.Planeta.ToList();
            foreach (var item in lista)
            {
                if (item.pesMin == 1 || item.pesGde == 1 || item.pesNav == 1 || item.pesRad == 1)
                {
                    ViewBag.Mensagem = "Existe uma pesquisa em andamento !";
                }
            }
            return View(lista.Where(m => m.Nome == nome));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Pesquisa(string id, [Bind("Niobio")] Planeta planeta)
        {
            var nome = HttpContext.Session.GetString("nome");
            var lista = _context.Planeta.ToList();

            foreach (var item in lista)
            {
                var numero = await _context.Planeta.FirstOrDefaultAsync(m => m.Nome == nome);
                if (id == "1" && item.pesMin != 1)
                {
                    if (item.Niobio < 100)
                    {
                        ViewBag.Status = "Você não tem nióbio suficiente para iniciar a pesquisa !";
                    }
                    else
                    {
                        item.Niobio -= 100;
                        item.pesMin = 1;
                        ViewBag.Status = "Pesquisa de Mineração iniciada !";
                        _context.Update(numero);
                        await _context.SaveChangesAsync();
                    }

                }
                else if (id == "2" && item.pesMin == 2)
                {
                    if (item.Niobio < 200)
                    {
                        ViewBag.Status = "Você não tem nióbio suficiente para iniciar a pesquisa !";
                    }
                    else
                    {
                        item.Niobio -= 200;
                        item.pesNav = 1;
                        _context.Update(numero);
                        await _context.SaveChangesAsync();
                        ViewBag.Status = "Pesquisa de Aeronaves iniciada !";
                    }
                }
                else if (id == "3" && item.pesMin == 2 && item.pesNav == 2)
                {
                    if (item.Niobio < 500)
                    {
                        ViewBag.Status = "Você não tem nióbio suficiente para iniciar a pesquisa !";
                    }
                    else
                    {
                        item.Niobio -= 500;
                        item.pesRad = 1;
                        ViewBag.Status = "Pesquisa de Radares iniciada !";
                        _context.Update(numero);
                        await _context.SaveChangesAsync();
                    }
                }
                else if (id == "4" && item.pesMin == 2 && item.pesNav == 2 && item.pesRad == 2)
                {
                    if (item.Niobio < 1000)
                    {
                        ViewBag.Status = "Você não tem nióbio suficiente para iniciar a pesquisa !";
                    }
                    else
                    {
                        item.Niobio -= 1000;
                        item.pesGde = 1;
                        ViewBag.Status = "Pesquisa de Grandes Naves iniciada !";
                        _context.Update(numero);
                        await _context.SaveChangesAsync();
                    }
                }

                else
                {
                    ViewBag.Status = "Você não concluiu a pesquisa anterior !";
                }



            }
            return View(lista.Where(m => m.Nome == nome));

        }


        public async Task<IActionResult> Ondas(string qtdAst, string procurar)
        {
            // PARA PEGAR O TICK DO JOGO NA TABELA DO BANCO DE DADOS
            var timeControle = await _context.TimeControle.FirstOrDefaultAsync(m => m.Id == 1);
            TempData["tempo"] = timeControle.Tempo.ToString();

            var nome = HttpContext.Session.GetString("nome");
            var lista = _context.Planeta.ToList();
            foreach (var item in lista)
            {
                var planeta = _context.Planeta.FirstOrDefault(m => m.Nome == nome);

                if (procurar == null)
                {
                    return View();
                }
                else
                {
                    Random random = new Random();
                    int ast = random.Next(0, Convert.ToInt32(qtdAst));
                    item.AstLivre += ast;
                    item.Niobio -= 100 * ast;
                    _context.Update(planeta);
                    await _context.SaveChangesAsync();
                    ViewBag.Mensagem = "Foram encontrados " + ast.ToString() + " asteroides";
                    ViewBag.Mensagem2 = "Foram gastos " + ((ast * 100).ToString()) + " de Niobio";

                }
            }

            return View();
        }



        // GET: Planetas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Planetas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email")] Planeta planeta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planeta);
                planeta.AstLivre = 3;
                planeta.Carbono = 1000;
                planeta.Niobio = 1000;
                planeta.Plutonio = 1000;
                await _context.SaveChangesAsync();
                ViewBag.Status = "Conta criada ! Faça o Login!";
                return View();
            }
            return View(planeta);
        }

        // GET: Planetas/Edit/5
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
                return NotFound();
            }
            return View(planeta);
        }

        // POST: Planetas/Edit/5
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

        public async Task<PartialViewResult> Comunicacoes()
        {
            // PARA PEGAR O TICK DO JOGO NA TABELA DO BANCO DE DADOS
            var timeControle = await _context.TimeControle.FirstOrDefaultAsync(m => m.Id == 1);
            TempData["tempo"] = timeControle.Tempo.ToString();

            return PartialView(timeControle);

        }


        private bool PlanetaExists(int id)
        {
            return _context.Planeta.Any(e => e.Id == id);
        }


    }
}
