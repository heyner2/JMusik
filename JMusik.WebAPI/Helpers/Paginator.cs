using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMusik.WebAPI.Helpers
{
    public class Paginator<T> where T : class
    {
        public int PaginaActual { get; set; }

        public int RegistrosPorPagina { get; set; }

        public int TotalRegistros { get ; set; }

        public IEnumerable<T> Registros { get; set; }

        public Paginator(IEnumerable<T> Registros, int TotalRegistros, int RegistrosPorPagina, int PaginaActual)
        {
            this.PaginaActual = PaginaActual;
            this.Registros = Registros;
            this.RegistrosPorPagina = RegistrosPorPagina;
            this.TotalRegistros = TotalRegistros;
        
        }

        public int TotalPaginas { get { return (int)Math.Ceiling(this.TotalRegistros / (double)RegistrosPorPagina);} }  

        public bool TienePaginaAnterior { get { return (PaginaActual > 1 ); } }

        public bool TienePaginaSiguiente { get { return (PaginaActual < TotalPaginas); } }
    }
}
