using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping
{
    public class MovieMapping : IEntityTypeConfiguration<MovieModel>
    {
        public void Configure(EntityTypeBuilder<MovieModel> builder)
        {
            builder.ToTable("movie");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                   .HasMaxLength(250)
                   .IsRequired()
                   .HasColumnName("title");

            builder.Property(p => p.Genre)
                   .HasMaxLength(100)
                   .IsRequired()
                   .HasColumnName("genre");

            builder.Property(p => p.Year)
                   .IsRequired()
                   .HasColumnName("release_year");

            builder.Property(p => p.Rating)
                   .IsRequired()
                   .HasColumnName("rating");

            builder.Property(p => p.PosterUrl)
                   .HasMaxLength(500)
                   .IsRequired(false)
                   .HasColumnName("poster_url");

            builder.Property(p => p.DirectedBy)
                   .HasMaxLength(200)
                   .IsRequired(false)
                   .HasColumnName("directed_by");

            builder.Property(p => p.Sinopse)
                   .HasColumnName("sinopse")
                   .IsRequired(false);
        }
    }
}
