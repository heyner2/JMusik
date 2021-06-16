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
    class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {

        public void Configure(EntityTypeBuilder<Usuario> entity)
        {
            entity.ToTable("Usuario", "tienda");

            entity.HasIndex(e => e.PerfilId, "IX_Usuario_PerfilId");

            entity.Property(e => e.Apellidos).HasMaxLength(256);

            entity.Property(e => e.Email).HasMaxLength(100);

            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.Property(e => e.Password).HasMaxLength(512);

            entity.Property(e => e.Username).HasMaxLength(25);

            entity.HasOne(d => d.Perfil)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PerfilId);
        }
    }
}
