using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALProduto
    {
        private DALConexao conexao;

        public DALProduto(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloProduto modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "INSERT INTO produto(pro_nome, pro_descricao, pro_foto, pro_valorpago, pro_valorvenda, pro_qtde, undmed_cod, cat_cod, scat_cod) " +
                "VALUES (@nome, @descricao, @foto, @valorpago, @valorvenda, @qtde, @undmedcod, @catcod, @scatcod); SELECT @@IDENTITY;";
            cmd.Parameters.AddWithValue("@nome", modelo.ProNome);
            cmd.Parameters.AddWithValue("@descricao", modelo.ProDescricao);
            cmd.Parameters.AddWithValue("@foto", System.Data.SqlDbType.Image);

            if (modelo.ProFoto == null)
            {
                cmd.Parameters["@foto"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@foto"].Value = modelo.ProFoto;
            }

            cmd.Parameters.AddWithValue("@valorpago", modelo.ProValorPago);
            cmd.Parameters.AddWithValue("@valorvenda", modelo.ProValorVenda);
            cmd.Parameters.AddWithValue("@qtde", modelo.ProQtde);
            cmd.Parameters.AddWithValue("@undmedcod", modelo.UmedCod);
            cmd.Parameters.AddWithValue("@catcod", modelo.CatCod);
            cmd.Parameters.AddWithValue("@scatco", modelo.SCatCod);
            conexao.Conectar();
            modelo.ProCod = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Excluir(int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "DELETE FROM produto WHERE pro_cod = @codigo";
            cmd.Parameters.AddWithValue("@codigo", codigo);
            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        public void Alterar(ModeloProduto modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "UPDATE produto SET pro_nome = @nome, pro_descricao = @descricao, pro_foto = @foto, pro_valorpago = @valorpago, "+
                "pro_valorvenda = @valorvenda, pro_qtde = @qtde, undmed_cod = @undmed, cat_cod = @catcod, scat_cod = @scatcod WHERE "+
                "pro_cod = @codigo";
            cmd.Parameters.AddWithValue("@nome", modelo.CatNome);
            cmd.Parameters.AddWithValue("@codigo", modelo.CatCod);
            conexao.Conectar();
            // executenonquery: nenhuma informação do banco
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        /*


                public DataTable Localizar(String valor)
                {
                    DataTable tabela = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM categoria WHERE cat_nome LIKE '%" + valor + "%'", conexao.StringConexao);
                    da.Fill(tabela);
                    return tabela;
                }

                public ModeloCategoria CarregaModeloCategoria(int codigo)
                {
                    ModeloCategoria modelo = new ModeloCategoria();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conexao.ObjetoConexao;
                    cmd.CommandText = "SELECT * FROM categoria WHERE cat_cod = @codigo";
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    conexao.Conectar();
                    // executereader: várias informações/registros do banco de dados
                    SqlDataReader registro = cmd.ExecuteReader();
                    if (registro.HasRows)
                    {
                        registro.Read();
                        modelo.CatCod = Convert.ToInt32(registro["cat_cod"]);
                        modelo.CatNome = Convert.ToString(registro["cat_nome"]);
                    }
                    conexao.Desconectar();
                    return modelo;
                }
        */
    }
}
