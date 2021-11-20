using Dapper;
using Infraestrutura;
using Infraestrutura.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimesController
{
    public class AnimesComandos
    {
        private ConnectionString connectionString = new ConnectionString();

        public List<Anime> carregarAlunos()
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionString(), true))
            {
                var saida = cnn.Query<Anime>("select * from Animes", new DynamicParameters());
                return saida.ToList();
            }
        }

        public Anime carregarAlunoEspecifico(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionString(), true))
            {
                var saida = cnn.Query<Anime>("select * from Animes where Id='" + id + "'", new DynamicParameters()).FirstOrDefault();
                return saida;
            }
        }

        public string cadastrarAluno(Anime anime)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionString(), true))
            {
                var saida = cnn.Query<int>("select Id from Animes order by Id desc  LIMIT 1", new DynamicParameters()).FirstOrDefault();
                anime.Id = 
                cnn.Execute("insert into Animes" +
                            "(Nome," +
                            "Episodios," +
                            "Generos," +
                            "Completo," +
                            "Link," +
                            "LinkImage," +
                            "Finalizada," +
                            "DiaLancamento) values (" +
                            "@Nome," +
                            "@Episodios," +
                            "@Generos," +
                            "@Completo," +
                            "@Link," +
                            "@LinkImage," +
                            "@Finalizada," +
                            "@DiaLancamento" +
                            ")"
                            , anime);

                return "Gerado com o ID: " + saida + 1;
            }
        }

        public Anime modificarAnime(Anime anime, string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionString(), true))
            {
                cnn.Execute("UPDATE Animes " +
                    "SET Nome = @Nome," +
                    "Episodios = @Episodios," +
                    "Generos = @Generos," +
                    "Completo = @Completo," +
                    "Link = @Link," +
                    "LinkImage = @LinkImage," +
                    "DiaLancamento = @DiaLancamento," +
                    "Finalizada = @Finalizada" +
                    " WHERE Id='" + id + "'", anime);
            }

            return anime;
        }

        public string apagarAlunoEspecifico(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionString(), true))
            {
                var saida = cnn.Execute("delete from Animes where Id='" + id + "'", new DynamicParameters());
                if (saida == 0)
                {
                    return "not found";
                }
                else
                {
                    return "success";
                }
            }
        }
    }

}
