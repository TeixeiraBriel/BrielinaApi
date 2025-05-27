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
    public class NarrativaMapping : IEntityTypeConfiguration<Narrativa>
    {
        public void Configure(EntityTypeBuilder<Narrativa> builder)
        {
            builder.ToTable("narrativas");

            builder.HasKey(p => p.IdNarrativas);
            builder.Property(p => p.IdNarrativas).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.Titulo).HasDefaultValue("");
            builder.Property(p => p.Descricao).HasDefaultValue("");
            builder.Property(p => p.Texto).HasDefaultValue("");
            builder.Property(p => p.Ramificacoes).HasDefaultValue("");
            builder.Property(p => p.Tipo).HasDefaultValue(0);
        }
    }
}
