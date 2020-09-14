using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_asistentes.Models
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string DNI { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int Sueldo { get; set; }
    }
}
