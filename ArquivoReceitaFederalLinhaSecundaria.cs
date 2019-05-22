using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeFenicio
{
    public class ArquivoReceitaFederalLinhaSecundaria
    {
        public ArquivoReceitaFederalLinhaSecundaria(string linha)
        {
            CNPJ = linha.Substring(3, 14).Trim();
            IDENTIFICADOR_DE_SOCIO = linha.Substring(17, 1).Trim();
            NOME_SOCIO_NO_CASO_PF_OU_RAZÃO_SOCIAL_NO_CASO_PJ = linha.Substring(18, 150).Trim();
            CNPJ_CPF_DO_SOCIO = linha.Substring(168, 14).Trim();
            CODIGO_QUALIFICACAO_SOCIO = linha.Substring(182, 2).Trim();
            PERCENTUAL_CAPITAL_SOCIAL = linha.Substring(184, 5).Trim();
            DATA_ENTRADA_SOCIEDADE = linha.Substring(189, 8).Trim();
            CODIGO_PAIS = linha.Substring(197, 3).Trim();
            NOME_PAIS_SOCIO = linha.Substring(200, 70).Trim();
            CPF_REPRESENTANTE_LEGAL = linha.Substring(270, 11).Trim();
            NOME_REPRESENTANTE = linha.Substring(281, 60).Trim();
            CODIGO_QUALIFICACAO_REPRESENTANTE_LEGAL = linha.Substring(341, 2).Trim();
            LinhaCompleta = linha;
        }

        public string CNPJ { get;set;}
        public string IDENTIFICADOR_DE_SOCIO { get; set; }
        public string NOME_SOCIO_NO_CASO_PF_OU_RAZÃO_SOCIAL_NO_CASO_PJ { get; set; }
        public string CNPJ_CPF_DO_SOCIO { get; set; }
        public string CODIGO_QUALIFICACAO_SOCIO { get; set; }
        public string PERCENTUAL_CAPITAL_SOCIAL { get; set; }
        public string DATA_ENTRADA_SOCIEDADE { get; set; }
        public string CODIGO_PAIS { get; set; }
        public string NOME_PAIS_SOCIO { get; set; }
        public string CPF_REPRESENTANTE_LEGAL { get; set; }
        public string NOME_REPRESENTANTE { get; set; }
        public string CODIGO_QUALIFICACAO_REPRESENTANTE_LEGAL { get; set; }
        public string LinhaCompleta { get; set; }



    }
}
