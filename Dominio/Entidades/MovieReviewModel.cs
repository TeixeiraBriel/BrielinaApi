using System;

namespace Dominio.Entidades
{
    public class MovieReviewModel
    {
        public long Id { get; set; }
        public int MovieId { get; set; }
        public int UsuarioId { get; set; }
        public double Rating { get; set; }
        public string Review { get; set; } = string.Empty;
        public bool Recommended { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public MovieModel? Movie { get; set; }
        public UsuarioModel? Usuario { get; set; }
    }

    // Single DTO used for create/update from frontend. If Id == 0 or null -> insert; otherwise update.
    public class MovieReviewDto
    {
        public long Id { get; set; }
        public int MovieId { get; set; }
        public int UsuarioId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Review { get; set; } = string.Empty;
        public bool Recommended { get; set; }
    }
}
