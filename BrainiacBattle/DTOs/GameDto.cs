using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainiacBattle.DTOs
{
    public class GameDto
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string ImgSrc { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
