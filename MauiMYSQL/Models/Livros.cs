using System;
using System.Collections.Generic;
using MySqlConnector;

namespace MauiMYSQL.Models
{
    public class Livros : Conecta
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public bool lido { get; set; }

        public List<Livros> listaLivros = new List<Livros>();

        public Livros() { }

        public bool Livros_Add(string titulo, string autor, bool lido)
        {
            if (!Conexao())
                return false;

            StrQuery = "INSERT INTO livros (titulo, autor, lido) VALUES (@titulo, @autor, @lido)";
            Cmd = new MySqlCommand(StrQuery, Conn);
            Cmd.Parameters.AddWithValue("@titulo", titulo);
            Cmd.Parameters.AddWithValue("@autor", autor);
            Cmd.Parameters.AddWithValue("@lido", lido);

            try
            {
                Cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Livros_Consulta()
        {
            if (!Conexao())
                return false;

            StrQuery = "SELECT * FROM livros ORDER BY titulo";
            MySqlCommand cmd = new MySqlCommand(StrQuery, Conn);
            cmd.CommandType = System.Data.CommandType.Text;
            Dr = cmd.ExecuteReader();
            listaLivros.Clear();

            while (Dr.Read())
            {
                listaLivros.Add(new Livros
                {
                    id = int.Parse(Dr["id"].ToString()),
                    titulo = Dr["titulo"].ToString(),
                    autor = Dr["autor"].ToString(),
                    lido = Convert.ToBoolean(Dr["lido"])
                });
            }

            return true;
        }
    }
}
