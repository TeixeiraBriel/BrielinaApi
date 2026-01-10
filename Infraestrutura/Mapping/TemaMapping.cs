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
    public class TemaMapping : IEntityTypeConfiguration<TemaModel>
    {
        public void Configure(EntityTypeBuilder<TemaModel> builder)
        {
            builder.ToTable("tema");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.Livro)
                   .HasMaxLength(150)
                   .IsRequired()
                   .HasColumnName("livro");  // ← EXPLÍCITO

            // ← CORREÇÃO AQUI
            builder.Property(p => p.ResponsavelId)
                   .HasColumnName("responsavel_id")  // nome real da coluna
                   .IsRequired(false);

            builder.Property(p => p.DataApresentacao)
                   .HasColumnName("data_apresentacao")
                   .IsRequired(false);

            builder.Property(p => p.CriadoPorId)
                   .HasColumnName("criado_por_id")
                   .IsRequired();

            builder.Property(p => p.CriadoEm)
                   .HasColumnName("criado_em")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(p => p.AtualizadoEm)
                   .HasColumnName("atualizado_em")
                   .IsRequired(false);

            // Relacionamentos
            builder.HasOne(p => p.Responsavel)
                   .WithMany()
                   .HasForeignKey(p => p.ResponsavelId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.CriadoPor)
                   .WithMany()
                   .HasForeignKey(p => p.CriadoPorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.ResponsavelId);
            builder.HasIndex(p => p.CriadoPorId);
        }
    }
}
