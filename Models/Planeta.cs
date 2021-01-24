using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galactus.Models
{
    public class Planeta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
       
        public int Carbono { get; set; }
        public int Niobio { get; set; }
        public int Plutonio { get; set; }
        public int AstCarbono { get; set; }
        public int AstNiobio { get; set; }
        public int AstPlutonio { get; set; }
        public int AstLivre { get; set; }
        public int pesMin { get; set; }
        public int pesNav { get; set; }
        public int pesRad { get; set; }
        public int pesGde { get; set; }

        public int consRefNio { get; set; }
        public int consRefCar { get; set; }
        public int consEsta { get; set; }
        public int consEstaAv { get; set; }
        public int consRefAvan { get; set; }
        public int consFabDro{ get; set; }
        public int consEstaOrb { get; set; }



        public Planeta()
        {

        }

        public Planeta(int id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }
    }
}
