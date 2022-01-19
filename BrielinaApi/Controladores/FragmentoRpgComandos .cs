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
    public class FragmentoRpgComandos
    {
        private ConnectionString connectionString = new ConnectionString();

        public List<FragmentoRpg> carregarRegistros()
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionStringRpgCSharp(), true))
            {
                var saida = cnn.Query<FragmentoRpg>("select * from Fragmentos", new DynamicParameters());
                return saida.ToList();
            }
        }

        public FragmentoRpg carregarRegistroEspecifico(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionStringRpgCSharp(), true))
            {
                var saida = cnn.Query<FragmentoRpg>("select * from Fragmentos where IdFragmentoHistoria='" + id + "'", new DynamicParameters()).FirstOrDefault();
                return saida;
            }
        }

        public string cadastrarRegistro(FragmentoRpg registro)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionStringRpgCSharp(), true))
            {
                cnn.Execute("insert into Fragmentos" +
                            "(TituloFragmento," +
                            "TextoFragmento," +
                            "IdFragmentoParent) values (" +
                            "@TituloFragmento," +
                            "@TextoFragmento," +
                            "@IdFragmentoParent" +
                            ")"
                            , registro);

                return "Fragmento Cadastrado com Sucesso!";
            }
        }

        public dynamic modificarRegistro(FragmentoRpg registro, string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionStringRpgCSharp(), true))
            {
                cnn.Execute("UPDATE Fragmentos " +
                    "SET TituloFragmento = @TituloFragmento," +
                    "TextoFragmento = @TextoFragmento," +
                    " WHERE IdFragmentoHistoria='" + id + "'", registro);
            }

            return registro;
        }

        public string apagarRegistroEspecifico(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString.carregarConnectionStringRpgCSharp(), true))
            {
                var saida = cnn.Execute("delete from Fragmentos where IdFragmentoHistoria='" + id + "'", new DynamicParameters());
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