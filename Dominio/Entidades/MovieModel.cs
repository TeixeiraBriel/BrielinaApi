using System;

namespace Dominio.Entidades
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Year { get; set; }
        public double Rating { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string DirectedBy { get; set; } = string.Empty;
        public string Sinopse { get; set; } = string.Empty;
    }

    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Year { get; set; }
        public double Rating { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string DirectedBy { get; set; } = string.Empty;
        public string Sinopse { get; set; } = string.Empty;
    }
}
