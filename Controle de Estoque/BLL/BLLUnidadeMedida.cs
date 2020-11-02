using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLUnidadeMedida
    {
        private DALConexao conexao;

        public BLLUnidadeMedida(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloUnidadeMedida modelo)
        {
            if (modelo.UmedNome.Trim().Length == 0)
                throw new Exception("A descrição da unidade de medida é obrigatória!");

            DALUnidadeMedida DALobj = new DALUnidadeMedida(conexao);
            DALobj.Incluir(modelo);
        }

        public void Alterar(ModeloUnidadeMedida modelo)
        {
            if (modelo.UmedNome.Trim().Length == 0)
                throw new Exception("A descrição da unidade de medida é obrigatória!");

            if (modelo.UmedCod <= 0)
                throw new Exception("A unidade de medida é obrigatória!");

            DALUnidadeMedida DALobj = new DALUnidadeMedida(conexao);
            DALobj.Incluir(modelo);
        }

        public void Excluir(int codigo)
        {
            DALUnidadeMedida DALobj = new DALUnidadeMedida(conexao);
            DALobj.Excluir(codigo);
        }

        public DataTable Localizar(String valor)
        {
            DALUnidadeMedida DALobj = new DALUnidadeMedida(conexao);
            return DALobj.Localizar(valor);
        }

        public ModeloUnidadeMedida CarregaModeloUnidadeMedida(int codigo)
        {
            DALUnidadeMedida DALobj = new DALUnidadeMedida(conexao);
            return DALobj.CarregaModeloUnidadeMedida(codigo);
        }
    }
}