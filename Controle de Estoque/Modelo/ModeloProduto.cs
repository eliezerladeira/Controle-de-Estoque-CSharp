using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ModeloProduto
    {
        public ModeloProduto()
        {
            this.ProCod = 0;
            this.ProNome = "";
            this.ProDescricao = "";
            this.ProValorPago = 0;
            this.ProValorVenda = 0;
            this.ProQtde = 0;
            this.UmedCod = 0;
            this.CatCod = 0;
            this.SCatCod = 0;
        }

        public ModeloProduto(int pro_cod, String pro_nome, String pro_descricao, String pro_foto, Double pro_valorpago, Double pro_valorvenda, float pro_qtde, int umed_cod, int cat_cod, int scat_cod)
        {
            this.ProCod = pro_cod;
            this.ProNome = pro_nome;
            this.ProDescricao = pro_descricao;
            this.CarregaImagem(pro_foto);
            this.ProValorPago = pro_valorpago;
            this.ProValorVenda = pro_valorvenda;
            this.ProQtde = pro_qtde;
            this.UmedCod = umed_cod;
            this.CatCod = cat_cod;
            this.SCatCod = scat_cod;
        }

        public ModeloProduto(int pro_cod, String pro_nome, String pro_descricao, Byte[] pro_foto, Double pro_valorpago, Double pro_valorvenda, float pro_qtde, int umed_cod, int cat_cod, int scat_cod)
        {
            this.ProCod = pro_cod;
            this.ProNome = pro_nome;
            this.ProDescricao = pro_descricao;
            this.ProFoto = pro_foto;
            this.ProValorPago = pro_valorpago;
            this.ProValorVenda = pro_valorvenda;
            this.ProQtde = pro_qtde;
            this.UmedCod = umed_cod;
            this.CatCod = cat_cod;
            this.SCatCod = scat_cod;
        }

        private int _pro_cod;
        private String _pro_nome;
        private String _pro_descricao;
        private byte[] _pro_foto;
        private Double _pro_valorpago;
        private Double _pro_valorvenda;
        private float _pro_qtde;
        private int _umed_cod;
        private int _cat_cod;
        private int _scat_cod;

        public int ProCod
        {
            get { return this._pro_cod; }
            set { this._pro_cod = value; }
        }

        public String ProNome
        {
            get { return this._pro_nome; }
            set { this._pro_nome = value; }
        }

        public String ProDescricao
        {
            get { return this._pro_descricao; }
            set { this._pro_descricao = value; }
        }

        public byte[] ProFoto
        {
            get { return this._pro_foto; }
            set { this._pro_foto = value; }
        }

        public Double ProValorPago
        {
            get { return this._pro_valorpago; }
            set { this._pro_valorpago = value; }
        }

        public Double ProValorVenda
        {
            get { return this._pro_valorvenda; }
            set { this._pro_valorvenda = value; }
        }

        public float ProQtde
        {
            get { return this._pro_qtde; }
            set { this._pro_qtde = value; }
        }
 
        public int UmedCod
        {
            get { return this._umed_cod; }
            set { this._umed_cod = value; }
        }

        public int CatCod
        {
            get { return this._cat_cod; }
            set { this._cat_cod = value; }
        }

        public int SCatCod
        {
            get { return this._scat_cod; }
            set { this._scat_cod = value; }
        }

        public void CarregaImagem(String imgCaminho)
        {
            try
            {
                if (string.IsNullOrEmpty(imgCaminho))
                    return;

                FileInfo arqImagem = new FileInfo(imgCaminho);
                FileStream fs = new FileStream(imgCaminho, FileMode.Open, FileAccess.Read, FileShare.Read);
                this.ProFoto = new byte[Convert.ToInt32(arqImagem.Length)];
                int iBytesRead = fs.Read(this.ProFoto, 0, Convert.ToInt32(arqImagem.Length));
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}