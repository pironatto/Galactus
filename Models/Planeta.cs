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
