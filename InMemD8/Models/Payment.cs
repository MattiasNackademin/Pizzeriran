using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InMemD8.Models
{
    public class Payment
    {

        [Required]
        public string Namn { get; set; }
        [Required]
        public string Tel { get; set; }
        [Required]
        public string Gata { get; set; }
        [Required]
        public string Postnr { get; set; }
        [Required, MinLength(16)]
        public string Kortnr { get; set; }
        [Required, MinLength(3)]
        public string Cvc { get; set; }
    }
}
