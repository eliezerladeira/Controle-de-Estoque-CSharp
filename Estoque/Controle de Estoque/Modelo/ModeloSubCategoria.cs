using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ModeloSubCategoria
    {
        public ModeloSubCategoria()
        {
            this.CatCod = 0;
            this.ScatCod = 0;
            this.ScatNome = "";
        }

        public ModeloSubCategoria(int scatcod, int catcod, String snome)
        {
            this.CatCod = catcod;
            this.ScatCod = scatcod;
            this.ScatNome = snome;
        }

        private int cat_cod;
        private int scat_cod;
        private String scat_nome;

        public int CatCod
        {
            get { return this.cat_cod; }
            set { this.cat_cod = value; }
        }

        public int ScatCod
        {
            get { return this.scat_cod; }
            set { this.scat_cod = value; }
        }

        public String ScatNome
        {
            get { return this.scat_nome; }
            set { this.scat_nome = value; }
        }
    }
}