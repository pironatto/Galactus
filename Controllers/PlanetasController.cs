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

        public ControleTecPlanetas ControleTec = new ControleTecPlanetas();

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
            var numero = await _context.Planeta.FirstOrDefaultAsync(m => m.Nome == nome);

            if (numero.pesMin == 1 || numero.pesNav == 1 || numero.pesRad == 1)
            {
                ViewBag.Mensagem = "Existe uma pesquisa em andamento !";
            }
            if (Convert.ToInt32(TempData["Tempo"]) >= ControleTec.tempoPesquisa)
            {
                numero.pesMin = 2;
                _context.Update(numero);
                await _context.SaveChangesAsync();
            }

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Pesquisa(string id, [Bind("Niobio")] Planeta planeta)
        {
            var nome = HttpContext.Session.GetString("nome");

            var numero = await _context.Planeta.FirstOrDefaultAsync(m => m.Nome == nome);
            if (id == "1" && numero.pesMin == 0)
            {
                if (numero.Niobio < 100)
                {
                    ViewBag.Status = "Você não tem nióbio suficiente para iniciar a pesquisa !";
                }
                else
                {
                    numero.Niobio -= 100;
                    numero.pesMin = 1;
                    ViewBag.Status = "Pesquisa de Mineração iniciada !";
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                    ControleTec.tempoPesquisa = Convert.ToInt32(TempData["Tempo"]) + 1; //INFORMAR TEMPO DE PESQUISA
                }

            }
            else if (id == "2" && numero.pesMin == 2)
            {
                if (numero.Niobio < 200)
                {
                    ViewBag.Status = "Você não tem nióbio suficiente para iniciar a pesquisa !";
                }
                else
                {
                    numero.Niobio -= 200;
                    numero.pesNav = 1;
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                    ViewBag.Status = "Pesquisa de Aeronaves iniciada !";
                }
            }
            else if (id == "3" && numero.pesMin == 2 && numero.pesNav == 2)
            {
                if (numero.Niobio < 500)
                {
                    ViewBag.Status = "Você não tem nióbio suficiente para iniciar a pesquisa !";
                }
                else
                {
                    numero.Niobio -= 500;
                    numero.pesRad = 1;
                    ViewBag.Status = "Pesquisa de Radares iniciada !";
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                }
            }
            else if (id == "4" && numero.pesMin == 2 && numero.pesNav == 2 && numero.pesRad == 2)
            {
                if (numero.Niobio < 1000)
                {
                    ViewBag.Status = "Você não tem nióbio suficiente para iniciar a pesquisa !";
                }
                else
                {
                    numero.Niobio -= 1000;
                    numero.pesGde = 1;
                    ViewBag.Status = "Pesquisa de Grandes Naves iniciada !";
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                }
            }

            else
            {
                ViewBag.Status = "Você não concluiu a pesquisa anterior !";
            }

            return View();

        }

        public async Task<IActionResult> Construcoes(string nome)
        {
            // PARA PEGAR O TICK DO JOGO NA TABELA DO BANCO DE DADOS
            var timeControle = await _context.TimeControle.FirstOrDefaultAsync(m => m.Id == 1);
            TempData["tempo"] = timeControle.Tempo.ToString();

            nome = HttpContext.Session.GetString("nome");
            var numero = await _context.Planeta.FirstOrDefaultAsync(m => m.Nome == nome);

            if (numero.consRefNio == 1 || numero.consRefCar == 1 || numero.consEsta == 1 || numero.consEstaAv == 1 || numero.consRefAvan == 1 || numero.consFabDro == 1 || numero.consEstaOrb == 1)
            {
                ViewBag.Mensagem = "Existe uma construção em andamento !";
            }

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Construcoes(string id, [Bind("Carbono,Niobio")] Planeta planeta)
        {
            var nome = HttpContext.Session.GetString("nome");
            var lista = _context.Planeta.ToList();

                var numero = await _context.Planeta.FirstOrDefaultAsync(m => m.Nome == nome);
            if (id == "1" && numero.consRefNio != 1)
            {
                if (numero.Niobio < 50 && numero.Carbono < 100)
                {
                    ViewBag.Status = "Você não tem recursos suficientes para iniciar a pesquisa !";
                }
                else
                {
                    numero.Niobio -= 50;
                    numero.Carbono -= 100;
                    numero.consRefNio = 1;
                    ViewBag.Status = "Construção de Refinaria iniciada !";
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                }

            }
            else if (id == "2" && numero.consRefNio == 2)
            {
                if (numero.Niobio < 100 && numero.Carbono < 200)
                {
                    ViewBag.Status = "Você não tem recursos suficientes para iniciar a pesquisa !";
                }
                else
                {
                    numero.Niobio -= 100;
                    numero.Carbono -= 200;
                    numero.consRefCar = 1;
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                    ViewBag.Status = "Construção de Refinaria iniciada !";
                }
            }
            else if (id == "3" && numero.consRefNio == 2 && numero.consRefCar == 2)
            {
                if (numero.Niobio < 200 && numero.Carbono < 400)
                {
                    ViewBag.Status = "Você não tem recursos suficientes para iniciar a pesquisa !";
                }
                else
                {
                    numero.Niobio -= 200;
                    numero.Carbono -= 400;
                    numero.consEsta = 1;
                    ViewBag.Status = "Construção de Estaleiros iniciada !";
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                }
            }
            else if (id == "4" && numero.consRefNio == 2 && numero.consRefCar == 2 && numero.consEsta == 2)
            {
                if (numero.Niobio < 400 && numero.Carbono < 800)
                {
                    ViewBag.Status = "Você não tem recursos suficientes para iniciar a pesquisa !";
                }
                else
                {
                    numero.Niobio -= 400;
                    numero.Carbono -= 800;
                    numero.consEstaAv = 1;
                    ViewBag.Status = "Construção de Estaleiros avançados iniciada !";
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                }
            }
            else if (id == "5" && numero.consRefNio == 2 && numero.consRefCar == 2 && numero.consEsta == 2 && numero.consEstaAv == 2)
            {
                if (numero.Niobio < 500 && numero.Carbono < 1000)
                {
                    ViewBag.Status = "Você não tem recursos suficientes para iniciar a pesquisa !";
                }
                else
                {
                    numero.Niobio -= 500;
                    numero.Carbono -= 1000;
                    numero.consRefAvan = 1;
                    ViewBag.Status = "Construção de Refinaria avançada iniciada !";
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                }
            }
            else if (id == "6" && numero.consRefNio == 2 && numero.consRefCar == 2 && numero.consEsta == 2 && numero.consEstaAv == 2 && numero.consRefAvan == 2)
            {
                if (numero.Niobio < 1000 && numero.Carbono < 2000)
                {
                    ViewBag.Status = "Você não tem recursos suficientes para iniciar a pesquisa !";
                }
                else
                {
                    numero.Niobio -= 1000;
                    numero.Carbono -= 2000;
                    numero.consFabDro = 1;
                    ViewBag.Status = "Construção de Fabrica de Drones iniciada !";
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                }
            }
            else if (id == "7" && numero.consRefNio == 2 && numero.consRefCar == 2 && numero.consEsta == 2 && numero.consEstaAv == 2 && numero.consRefAvan == 2 && numero.consFabDro == 2)
            {
                if (numero.Niobio < 2000 && numero.Carbono < 3000)
                {
                    ViewBag.Status = "Você não tem recursos suficientes para iniciar a pesquisa !";
                }
                else
                {
                    numero.Niobio -= 2000;
                    numero.Carbono -= 3000;
                    numero.consEstaOrb = 1;
                    ViewBag.Status = "Construção de Estaleiro Orbital iniciada !";
                    _context.Update(numero);
                    await _context.SaveChangesAsync();
                }

            }
            else
            {
                ViewBag.Status = "Você não concluiu a construção  anterior !";
            }
                       
            return View();

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
