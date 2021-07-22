using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmCadastroProduto : GUI.frmModeloFormularioCadastro
    {
        public string foto = "";

        public frmCadastroProduto()
        {
            InitializeComponent();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            this.operacao = "Inserir";
            alteraBotoes(2);
        }

        private void frmCadastroProduto_Load(object sender, EventArgs e)
        {
            this.alteraBotoes(1);

            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLCategoria bll = new BLLCategoria(cx);

            cmbCategoria.DataSource = bll.Localizar("");
            cmbCategoria.DisplayMember = "cat_nome";
            cmbCategoria.ValueMember = "cat_cod";

            try
            {
                BLLSubCategoria sbll = new BLLSubCategoria(cx);

                cmbSubCategoria.DataSource = sbll.LocalizarPorCategoria((int)cmbCategoria.SelectedValue);
                cmbSubCategoria.DisplayMember = "scat_nome";
                cmbSubCategoria.ValueMember = "scat_cod";
            }
            catch
            {
                //MessageBox.Show("Cadastre uma categoria.");
            }

            BLLUnidadeMedida ubll = new BLLUnidadeMedida(cx);

            cmbUndMedida.DataSource = ubll.Localizar("");
            cmbUndMedida.DisplayMember = "umed_nome";
            cmbUndMedida.ValueMember = "umed_cod";
        }

        private void txtValorVenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != ',' && e.KeyChar != '.')
                e.Handled = true;

            if (e.KeyChar == ',' || e.KeyChar == '.')
            {
                if (!txtValorVenda.Text.Contains("."))
                {
                    e.KeyChar = '.';
                }
                else e.Handled = true;
            }
        }

        private void txtValorPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != ',' && e.KeyChar != '.')
                e.Handled = true;

            if (e.KeyChar == ',' || e.KeyChar =='.')
            {
                if (!txtValorPago.Text.Contains("."))
                {
                    e.KeyChar = '.';
                }
                else e.Handled = true;
            }
        }

        private void txtValorPago_Leave(object sender, EventArgs e)
        {
            if (txtValorPago.Text.Contains(".") == false)
            {
                txtValorPago.Text += ".00";
            }
            else
            {
                if (txtValorPago.Text.IndexOf(".") == txtValorPago.Text.Length - 1)
                {
                    txtValorPago.Text += "00";
                }
            }
            try
            {
                Double d = Convert.ToDouble(txtValorPago.Text);
            }
            catch
            {
                txtValorPago.Text = "0.00";
            }
        }

        private void txtValorVenda_Leave(object sender, EventArgs e)
        {
            if (txtValorVenda.Text.Contains(".") == false)
            {
                txtValorVenda.Text += ".00";
            }
            else
            {
                if (txtValorVenda.Text.IndexOf(".") == txtValorVenda.Text.Length - 1)
                {
                    txtValorVenda.Text += "00";
                }
            }
            try
            {
                Double d = Convert.ToDouble(txtValorVenda.Text);
            }
            catch
            {
                txtValorVenda.Text = "0.00";
            }
        }

        private void txtQtd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != ',' && e.KeyChar != '.')
                e.Handled = true;

            // se quantidade for inteira, tirar esse if e o leave
            if (e.KeyChar == ',' || e.KeyChar == '.')
            {
                if (!txtQtd.Text.Contains("."))
                {
                    e.KeyChar = '.';
                }
                else e.Handled = true;
            }
        }

        private void txtQtd_Leave(object sender, EventArgs e)
        {
            // não usar se quantidade for inteira
            if (txtQtd.Text.Contains(".") == false)
            {
                txtQtd.Text += ".00";
            }
            else
            {
                if (txtQtd.Text.IndexOf(".") == txtQtd.Text.Length - 1)
                {
                    txtQtd.Text += "00";
                }
            }
            try
            {
                Double d = Convert.ToDouble(txtQtd.Text);
            }
            catch
            {
                txtQtd.Text = "0.00";
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            this.operacao = "Alterar";
            this.alteraBotoes(2);
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);

            try
            {
                cmbSubCategoria.Text = "";
                BLLSubCategoria sbll = new BLLSubCategoria(cx);

                cmbSubCategoria.DataSource = sbll.LocalizarPorCategoria((int)cmbCategoria.SelectedValue);
                cmbSubCategoria.DisplayMember = "scat_nome";
                cmbSubCategoria.ValueMember = "scat_cod";
            }
            catch
            {
                //MessageBox.Show("Cadastre uma categoria.");
            }
        }

        private void cmdLoadFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.ShowDialog();
            
            if (!string.IsNullOrEmpty(od.FileName))
            {
                this.foto = od.FileName;
                picFoto.Load(this.foto);
            }
        }

        private void cmdRemoveFoto_Click(object sender, EventArgs e)
        {
            this.foto = "";
            picFoto.Image = null;
        }
    }
}



/*

        public void LimpaTela()
        {
            txtCodigo.Clear();
            txtCategoria.Clear();
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
                    MessageBox.Show("Categoria '" + txtCategoria.Text + "' cadastrada com sucesso! Código: " + modelo.CatCod.ToString());
                }
                else
                {
                    modelo.CatCod = Convert.ToInt32(txtCodigo.Text);
                    bll.Alterar(modelo);
                    MessageBox.Show("Categoria '" + txtCategoria.Text + "' alterada com sucesso!");
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
                DialogResult d = MessageBox.Show("Deseja excluir a categoria '" + txtCategoria.Text + "'?", "Aviso", MessageBoxButtons.YesNo);
                if (d.ToString() == "Yes")
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLCategoria bll = new BLLCategoria(cx);
                    bll.Excluir(Convert.ToInt32(txtCodigo.Text));
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
            frmConsultaCategoria f = new frmConsultaCategoria();
            f.ShowDialog();

            if (f.codigo != 0)
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLCategoria bll = new BLLCategoria(cx);

                ModeloCategoria modelo = bll.CarregaModeloCategoria(f.codigo);
                txtCodigo.Text = modelo.CatCod.ToString();
                txtCategoria.Text = modelo.CatNome;

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
 */