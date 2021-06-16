using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmusik.DTOS
{

    public class ProductoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [Display(Name = "Producto")]
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
        
}
