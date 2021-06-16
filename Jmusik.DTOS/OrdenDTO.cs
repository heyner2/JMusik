using JMusik.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmusik.DTOS
{

    public class OrdenDto
    {
        public OrdenDto()
        {
            DetalleOrdenes = new List<DetalleOrdenDto>();
        }

        public int Id { get; set; }
        public decimal CantidadArticulos { get; set; }
        public decimal Importe { get; set; }
        //[Required]
        public DateTime? FechaRegistro { get; set; }
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
      
        //  public EstatusOrden EstatusOrden { get; set; }

        //es una tabla relacionada asi que creamos una lista 
        public List<DetalleOrdenDto> DetalleOrdenes { get; set; }
    }

}
