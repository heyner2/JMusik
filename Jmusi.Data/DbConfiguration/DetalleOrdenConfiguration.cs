using JMusik.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.DbConfiguration
{
    class DetalleOrdenConfiguration: IEntityTypeConfiguration<DetalleOrden>
    {
        public void Configure(EntityTypeBuilder<DetalleOrden> entity)
        {
            entity.ToTable("DetalleOrden", "tienda");

            entity.HasIndex(e => e.OrdenId, "IX_DetalleOrden_OrdenId");

            entity.HasIndex(e => e.ProductoId, "IX_DetalleOrden_ProductoId");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Orden)
                .WithMany(p => p.DetalleOrdenes)
                .HasForeignKey(d => d.OrdenId);

            entity.HasOne(d => d.Producto)
                .WithMany(p => p.DetalleOrdens)
                .HasForeignKey(d => d.ProductoId);
        }

       
    }
}
