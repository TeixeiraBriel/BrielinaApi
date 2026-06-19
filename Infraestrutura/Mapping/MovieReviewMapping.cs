using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping
{
    public class MovieReviewMapping : IEntityTypeConfiguration<MovieReviewModel>
    {
        public void Configure(EntityTypeBuilder<MovieReviewModel> builder)
        {
            builder.ToTable("movie_reviews");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.MovieId)
                   .IsRequired()
                   .HasColumnName("movie_id");

            builder.Property(p => p.UsuarioId)
                   .IsRequired()
                   .HasColumnName("usuario_id")
                   .HasColumnType("int");

            builder.Property(p => p.Rating)
                   .IsRequired()
                   .HasColumnName("rating");

            builder.Property(p => p.Review)
                   .HasMaxLength(300)
                   .IsRequired()
                   .HasColumnName("review");

            builder.Property(p => p.Recommended)
                   .IsRequired()
                   .HasColumnName("recommended");

            builder.Property(p => p.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(p => p.Movie)
                   .WithMany()
                   .HasForeignKey(p => p.MovieId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Usuario)
                   .WithMany()
                   .HasForeignKey(p => p.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
