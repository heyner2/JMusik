using JMusik.Models.Enum;
using System;
using System.Collections.Generic;

#nullable disable

namespace JMusik.Models
{
    public partial class Orden
    {
        public Orden()
        {
            DetalleOrdenes = new HashSet<DetalleOrden>();
        }

        public int Id { get; set; }
        public decimal CantidadArticulos { get; set; }
        public decimal Importe { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int UsuarioId { get; set; }
        public EstatusOrden EstatusOrden { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<DetalleOrden> DetalleOrdenes { get; set; }
    }
}
