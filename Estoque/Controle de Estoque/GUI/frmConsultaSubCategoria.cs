﻿using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmConsultaSubCategoria : Form
    {
        public int codigo = 0;

        public frmConsultaSubCategoria()
        {
            InitializeComponent();
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLSubCategoria bll = new BLLSubCategoria(cx);

            dgvDados.DataSource = bll.Localizar(txtValor.Text);
        }

        private void frmConsultaSubCategoria_Load(object sender, EventArgs e)
        {
            btnLocalizar_Click(sender, e);

            dgvDados.Columns[0].HeaderText = "Código";
            dgvDados.Columns[0].Width = 50;
            dgvDados.Columns[1].HeaderText = "SubCategoria";
            dgvDados.Columns[1].Width = 500;
            dgvDados.Columns[2].HeaderText = "Categoria";
            dgvDados.Columns[2].Width = 50;
        }

        private void dgvDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.codigo = Convert.ToInt32(dgvDados.Rows[e.RowIndex].Cells[0].Value);
                this.Close();
            }
        }
    }
}