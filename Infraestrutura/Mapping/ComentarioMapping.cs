using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Mapping
{
    public class ComentarioMapping : IEntityTypeConfiguration<ComentarioModel>
    {
        public void Configure(EntityTypeBuilder<ComentarioModel> builder)
        {
            builder.ToTable("tema_comentario");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.TemaId)
                   .IsRequired();

            builder.Property(p => p.UsuarioId)
                   .IsRequired();

            builder.Property(p => p.Texto)
                   .IsRequired();

            builder.Property(p => p.CriadoEm)
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relacionamentos
            builder.HasOne(p => p.Tema)
                   .WithMany(t => t.Comentarios!)  // se adicionar ICollection no TemaModel
                   .HasForeignKey(p => p.TemaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Usuario)
                   .WithMany()
                   .HasForeignKey(p => p.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.TemaId);
            builder.HasIndex(p => p.UsuarioId);
        }
    }
}
