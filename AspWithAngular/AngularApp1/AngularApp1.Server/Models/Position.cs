using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularApp1.Server.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Remove or comment out the collection of Players
        // public ICollection<Player> Players { get; set; }
    }
}
