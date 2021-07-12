using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALSubCategoria
    {
        private DALConexao conexao;

        public DALSubCategoria(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloSubCategoria modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "INSERT INTO subcategoria(cat_cod, scat_nome) VALUES (@catcod, @nome); SELECT @@IDENTITY;";
                cmd.Parameters.AddWithValue("@catcod", modelo.CatCod);
                cmd.Parameters.AddWithValue("@nome", modelo.ScatNome);
                conexao.Conectar();
                modelo.ScatCod = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        public void Alterar(ModeloSubCategoria modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "UPDATE subcategoria SET scat_nome = @nome, cat_cod = @catcod WHERE scat_cod = @scatcod";
                cmd.Parameters.AddWithValue("@nome", modelo.ScatNome);
                cmd.Parameters.AddWithValue("@catcod", modelo.CatCod);
                cmd.Parameters.AddWithValue("@scatcod", modelo.ScatCod);
                conexao.Conectar();
                // executenonquery: nenhuma informação do banco
                cmd.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        public void Excluir(int codigo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.ObjetoConexao;
                cmd.CommandText = "DELETE FROM subcategoria WHERE scat_cod = @codigo";
                cmd.Parameters.AddWithValue("@codigo", codigo);
                conexao.Conectar();
                cmd.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        public DataTable Localizar(String valor)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT sc.scat_cod, sc.scat_nome, sc.cat_cod, c.cat_nome FROM subcategoria sc " +
                "INNER JOIN categoria c ON sc.cat_cod = c.cat_cod " +
                "WHERE scat_nome LIKE '%" + valor + "%'", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public DataTable LocalizarPorCategoria(int categoria)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT sc.scat_cod, sc.scat_nome, sc.cat_cod, c.cat_nome FROM subcategoria sc " +
                "INNER JOIN categoria c ON sc.cat_cod = c.cat_cod " +
                "WHERE sc.cat_cod = " + categoria.ToString(), conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }
        public ModeloSubCategoria CarregaModeloSubCategoria(int codigo)
        {
            ModeloSubCategoria modelo = new ModeloSubCategoria();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "SELECT * FROM subcategoria WHERE scat_cod = @codigo";
            cmd.Parameters.AddWithValue("@codigo", codigo);
            conexao.Conectar();
            // executereader: várias informações/registros do banco de dados
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.CatCod = Convert.ToInt32(registro["cat_cod"]);
                modelo.ScatCod = Convert.ToInt32(registro["scat_cod"]);
                modelo.ScatNome = Convert.ToString(registro["scat_nome"]);
            }
            conexao.Desconectar();
            return modelo;
        }
    }
}