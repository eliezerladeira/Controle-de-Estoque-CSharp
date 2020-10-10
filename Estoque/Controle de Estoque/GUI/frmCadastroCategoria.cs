using BLL;
using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmCadastroCategoria : GUI.frmModeloFormularioCadastro
    {
        public frmCadastroCategoria()
        {
            InitializeComponent();
        }

        public void LimpaTela()
        {
            txtCodigo.Clear();
            txtCategoria.Clear();
        }

        private void frmCadastroCategoria_Load(object sender, EventArgs e)
        {
            this.alteraBotoes(1);
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            this.operacao = "Inserir";
            this.alteraBotoes(2);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.LimpaTela();
            this.alteraBotoes(1);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloCategoria modelo = new ModeloCategoria();
                modelo.CatNome = txtCategoria.Text;

                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLCategoria bll = new BLLCategoria(cx);

                if (this.operacao == "Inserir")
                {
                    bll.Incluir(modelo);
                    MessageBox.Show("Categoria " + txtCategoria.Text + " cadastrada com sucesso! Código: " + modelo.CatCod.ToString());
                }
                else
                {
                    modelo.CatCod = Convert.ToInt32(txtCodigo.Text);
                    bll.Alterar(modelo);
                    MessageBox.Show("Categoria " + txtCategoria.Text + " alterada com sucesso!");
                }
                this.LimpaTela();
                this.alteraBotoes(1);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
    }
}