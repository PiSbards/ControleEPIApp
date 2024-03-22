using ControleEPIApp.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ControleEPIApp.Controller
{
    public class MySQLCon
    {
        static string conn = @"server=sql.freedb.tech;port=3306;database=freedb_ControleEPI;user=freedb_MuriloPietro;password=8*FcGMa*bxUb2Nj";

        public static List<Funcionario> ListaFuncionario()
        {
            List<Funcionario> listafuncionarios = new List<Funcionario>();
            string sql = "SELECT * FROM funcionario";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Funcionario func = new Funcionario()
                            {
                                id = reader.GetInt32(0),
                                matricula = reader.GetInt32(1),
                                nome = reader.GetString(2),
                                epi = reader.GetString(3),
                                data_entrega = reader.GetDateTime(4),
                                data_vencimento = reader.GetDateTime(5),
                            };
                            listafuncionarios.Add(func);
                        }
                    }
                }
                con.Close();
                return listafuncionarios;
            }
        }

        public static List<Funcionario> ListarEPI()
        {
            List<Funcionario> listaepi = new List<Funcionario>();
            string sql = "SELECT * FROM funcionario WHERE data_vencimento BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 3 DAY)";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using(MySqlCommand cmd = new MySqlCommand( sql, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Funcionario func = new Funcionario()
                            {
                                id = reader.GetInt32(0),
                                matricula = reader.GetInt32(1),
                                epi = reader.GetString(3),
                                nome = reader.GetString(2),
                                data_entrega = reader.GetDateTime(4),
                                data_vencimento = reader.GetDateTime(5),
                            };
                            listaepi.Add(func);
                        }
                    }
                }
                con.Close();
                return listaepi;
            }
        }

        public static List<Funcionario> ListarEPIVencida()
        {
            List<Funcionario> listaepivencida = new List<Funcionario>();
            string sql = "SELECT * FROM funcionario WHERE data_vencimento < CURDATE()";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Funcionario func = new Funcionario()
                            {
                                id = reader.GetInt32(0),
                                matricula = reader.GetInt32(1),
                                epi = reader.GetString(3),
                                nome = reader.GetString(2),
                                data_entrega = reader.GetDateTime(4),
                                data_vencimento = reader.GetDateTime(5),
                            };
                            listaepivencida.Add(func);
                        }
                    }
                }
                con.Close();
                return listaepivencida;
            }
        }

        public static void InserirFuncionario(int matricula, string nome, string epi, DateTime data_entrega, DateTime data_vencimento)
        {
            string sql = "INSERT INTO funcionario(matricula, nome, epi, data_entrega, data_vencimento) VALUES (@matricula,@nome,@epi,@data_entrega,@data_vencimento)";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    data_entrega = DateTime.Today;
                    cmd.Parameters.Add("@matricula", MySqlDbType.Int32).Value = matricula;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;
                    cmd.Parameters.Add("@epi", MySqlDbType.VarChar).Value = epi;
                    cmd.Parameters.Add("@data_entrega", MySqlDbType.DateTime).Value = data_entrega;
                    cmd.Parameters.Add("@data_vencimento",MySqlDbType.DateTime).Value = data_vencimento;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public static void AtualizarFuncionario(Funcionario funcionario)
        {
            string sql = "UPDATE funcionario SET matricula=@matricula, nome=@nome, epi=@epi, data_entrega=@data_entrega, data_vencimento=@data_vencimento WHERE id=@id";
            try
            {
                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        funcionario.data_entrega = DateTime.Today;
                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = funcionario.id;
                        cmd.Parameters.Add("@matricula", MySqlDbType.Int32).Value = funcionario.matricula;
                        cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = funcionario.nome;
                        cmd.Parameters.Add("@epi", MySqlDbType.VarChar).Value = funcionario.epi;
                        cmd.Parameters.Add("@data_entrega", MySqlDbType.DateTime).Value = funcionario.data_entrega;
                        cmd.Parameters.Add("@data_vencimento", MySqlDbType.DateTime).Value = funcionario.data_vencimento;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void ExcluirFuncionario(Funcionario funcionario)
        {
            string sql = "DELETE FROM funcionario WHERE id=@id";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = funcionario.id;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}
