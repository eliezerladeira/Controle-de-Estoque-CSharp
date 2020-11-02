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
    public partial class frmModeloFormularioCadastro : Form
    {
        public String operacao;

        public frmModeloFormularioCadastro()
        {
            InitializeComponent();
        }

        public void alteraBotoes(int op)
        {
            // op - operações que serão feitas com os botões
            // 1  = prepara os botões para inserir/localizar
            // 2  = prepara os botões para inserir/alterar um registro
            // 3  = prepara os botões para excluir/alterar

            panDados.Enabled = false;
            btnInserir.Enabled = false;
            btnAlterar.Enabled = false;
            btnLocalizar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;

            if (op == 1)
            {
                btnInserir.Enabled = true;
                btnLocalizar.Enabled = true;
            }
            if (op == 2)
            {
                panDados.Enabled = true;
                btnSalvar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            if (op ==3)
            {
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = true;
                btnCancelar.Enabled = true;
            }
        }

        private void frmModeloFormularioCadastro_Load(object sender, EventArgs e)
        {
            this.alteraBotoes(1);
        }
    }
}