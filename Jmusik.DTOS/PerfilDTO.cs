using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmusik.DTOS
{

    public class PerfilDto
    {
        public int Id { get; set; }
        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "El nombre del perfil es requerido")]
        public string Nombre { get; set; }
    }

}
