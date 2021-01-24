using Microsoft.AspNetCore.Mvc;
using System.Timers;


namespace Galactus.Models
{
    public class TimeControle 
    {
        public int Id { get; set; }
        public int Tempo { get; set; }

        public TimeControle(int id, int tempo)
        {
            Id = id;
            Tempo = tempo;
       }

        public TimeControle()
        {
        }
    }
}
