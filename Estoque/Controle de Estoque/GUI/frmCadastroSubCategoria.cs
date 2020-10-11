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
    public partial class frmCadastroSubCategoria : GUI.frmModeloFormularioCadastro
    {
        public frmCadastroSubCategoria()
        {
            InitializeComponent();
        }

        public void LimpaTela()
        {
            txtSCodigo.Clear();
            txtSubCategoria.Clear();
        }

        private void frmCadastroSubCategoria_Load(object sender, EventArgs e)
        {
            this.alteraBotoes(1);

            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLCategoria bll = new BLLCategoria(cx);

            cmbCategoria.DataSource = bll.Localizar("");
            cmbCategoria.DisplayMember = "cat_nome";
            cmbCategoria.ValueMember = "cat_cod";
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            this.alteraBotoes(2);
            this.operacao = "Inserir";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.alteraBotoes(1);
            this.LimpaTela();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            this.alteraBotoes(2);
            this.operacao = "Alterar";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloSubCategoria modelo = new ModeloSubCategoria();
                modelo.ScatNome = txtSubCategoria.Text;
                modelo.CatCod = Convert.ToInt32(cmbCategoria.SelectedValue);

                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLSubCategoria bll = new BLLSubCategoria(cx);

                if (this.operacao == "Inserir")
                {
                    bll.Incluir(modelo);
                    MessageBox.Show("SubCategoria '" + txtSubCategoria.Text + "' cadastrada com sucesso! Código: " + modelo.ScatCod.ToString());
                }
                else
                {
                    modelo.ScatCod = Convert.ToInt32(txtSCodigo.Text);
                    bll.Alterar(modelo);
                    MessageBox.Show("SubCategoria '" + txtSubCategoria.Text + "' alterada com sucesso!");
                }
                this.LimpaTela();
                this.alteraBotoes(1);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Deseja excluir a subcategoria '" + txtSubCategoria.Text + "'?", "Aviso", MessageBoxButtons.YesNo);
                if (d.ToString() == "Yes")
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLSubCategoria bll = new BLLSubCategoria(cx);
                    bll.Excluir(Convert.ToInt32(txtSCodigo.Text));
                    this.LimpaTela();
                    this.alteraBotoes(1);
                }
            }
            catch
            {
                MessageBox.Show("Impossível excluir o registro. \n O registro está sendo utilizado em outro local.");
                this.alteraBotoes(3);
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            frmConsultaSubCategoria f = new frmConsultaSubCategoria();
            f.ShowDialog();

            if (f.codigo != 0)
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLSubCategoria bll = new BLLSubCategoria(cx);

                ModeloSubCategoria modelo = bll.CarregaModeloSubCategoria(f.codigo);
                txtSCodigo.Text = modelo.ScatCod.ToString();
                txtSubCategoria.Text = modelo.ScatNome;
                cmbCategoria.SelectedValue = modelo.CatCod;

                alteraBotoes(3);
            }
            else
            {
                this.LimpaTela();
                this.alteraBotoes(1);
            }
            f.Dispose();
        }
    }
}