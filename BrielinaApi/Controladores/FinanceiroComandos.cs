using Dapper;
using Infraestrutura;
using Infraestrutura.Entidades;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace BrielinaApi.Controladores
{
    public class FinanceiroComandos
    {
        private ConnectionString connectionString = new ConnectionString();

        public List<Registro> carregarRegistros()
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionStringFinanceiro(), true))
            {
                var saida = cnn.Query<Registro>("select * from Registros", new DynamicParameters());
                return saida.ToList();
            }
        }

        public Registro carregarRegistroEspecifico(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionStringFinanceiro(), true))
            {
                var saida = cnn.Query<Registro>("select * from Registros where Id='" + id + "'", new DynamicParameters()).FirstOrDefault();
                return saida;
            }
        }

        public string cadastrarRegistro(Registro registro)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionStringFinanceiro(), true))
            {
                var saida = cnn.Query<int>("select Id from Registros order by Id desc  LIMIT 1", new DynamicParameters()).FirstOrDefault();
                registro.Id = saida + 1;
                cnn.Execute("insert into Registros" +
                            "(Valor," +
                            "Data," +
                            "Grupo," +
                            "Credito," +
                            "Tipo," +
                            "Descricao," +
                            "Fixa," +
                            "DataVencimento) values (" +
                            "@Valor," +
                            "@Data," +
                            "@Grupo," +
                            "@Credito," +
                            "@Tipo," +
                            "@Descricao," +
                            "@Fixa," +
                            "@DataVencimento" +
                            ")"
                            , registro);

                return "Gerado com o ID: " + registro.Id;
            }
        }

        public dynamic modificarRegistro(Registro registro, string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionStringFinanceiro(), true))
            {
                cnn.Execute("UPDATE Registros " +
                    "SET Valor = @Valor," +
                    "Data = @Data," +
                    "Grupo = @Grupo," +
                    "Credito = @Credito," +
                    "Tipo = @Link," +
                    "Descricao = @Descricao," +
                    "Fixa = @Fixa," +
                    "DataVencimento = @DataVencimento," +
                    " WHERE Id='" + id + "'", registro);
            }

            return registro;
        }

        public string apagarRegistroEspecifico(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionStringFinanceiro(), true))
            {
                var saida = cnn.Execute("delete from Registros where Id='" + id + "'", new DynamicParameters());
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