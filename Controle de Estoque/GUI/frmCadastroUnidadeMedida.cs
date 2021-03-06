﻿using BLL;
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
    public partial class frmCadastroUnidadeMedida : GUI.frmModeloFormularioCadastro
    {
        public frmCadastroUnidadeMedida()
        {
            InitializeComponent();
        }

        public void LimpaTela()
        {
            txtCodigo.Clear();
            txtDescricao.Clear();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            this.operacao = "Inserir";
            this.alteraBotoes(2);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            this.operacao = "Alterar";
            this.alteraBotoes(2);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Deseja excluir a unidade de medida '" + txtDescricao.Text + "'?", "Aviso", MessageBoxButtons.YesNo);
                if (d.ToString() == "Yes")
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLUnidadeMedida bll = new BLLUnidadeMedida(cx);
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloUnidadeMedida modelo = new ModeloUnidadeMedida();
                modelo.UmedNome = txtDescricao.Text;

                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLUnidadeMedida bll = new BLLUnidadeMedida(cx);

                if (this.operacao == "Inserir")
                {
                    bll.Incluir(modelo);
                    MessageBox.Show("Unidade de Medida '" + txtDescricao.Text + "' cadastrada com sucesso! Código: " + modelo.UmedCod.ToString());
                }
                else
                {
                    modelo.UmedCod = Convert.ToInt32(txtCodigo.Text);
                    bll.Alterar(modelo);
                    MessageBox.Show("Unidade de Medida '" + txtDescricao.Text + "' alterada com sucesso!");
                }
                this.LimpaTela();
                this.alteraBotoes(1);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.LimpaTela();
            this.alteraBotoes(1);
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            frmConsultaUnidadeMedida f = new frmConsultaUnidadeMedida();
            f.ShowDialog();

            if (f.codigo != 0)
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLUnidadeMedida bll = new BLLUnidadeMedida(cx);

                ModeloUnidadeMedida modelo = bll.CarregaModeloUnidadeMedida(f.codigo);
                txtCodigo.Text = modelo.UmedCod.ToString();
                txtDescricao.Text = modelo.UmedNome;

                alteraBotoes(3);
            }
            else
            {
                this.LimpaTela();
                this.alteraBotoes(1);
            }
            f.Dispose();
        }

        private void txtDescricao_Leave(object sender, EventArgs e)
        {
            if (this.operacao == "Inserir")
            {
                frmConsultaUnidadeMedida f = new frmConsultaUnidadeMedida();
                int r = 0;
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLUnidadeMedida bll = new BLLUnidadeMedida(cx);
                r = bll.VerificaUnidadeMedida(txtDescricao.Text);

                if (r > 0)
                {
                    DialogResult d = MessageBox.Show("Já existe uma Unidade de Medida cadastrada com essa descrição. Deseja alterar?", "Aviso", MessageBoxButtons.YesNo);
                    if (d.ToString() == "Yes")
                    {
                        this.operacao = "Alterar";
                        ModeloUnidadeMedida modelo = bll.CarregaModeloUnidadeMedida(f.codigo);
                        txtCodigo.Text = modelo.UmedCod.ToString();
                        txtDescricao.Text = modelo.UmedNome;
                    }
                }
            }
        }
    }
}