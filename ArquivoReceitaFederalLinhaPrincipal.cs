using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeFenicio
{
    public class ArquivoReceitaFederalLinhaPrincipal
    {
        public  string TIPO_REGISTRO { get; set; }
        public string CNPJ { get; set; }
        public string IDENTIFICADOR_MATRIZ_FILIAL {get;set;}
        public string RAZAO_SOCIAL { get; set; }
        public string NOME_FANTASIA {get;set; }
        public string SITUACAO_CADASTRAL { get; set; }
        public string DATA_SITUACAO_CADASTRAL { get; set; }
        public string MOTIVO_SITUACAO_CADASTRAL { get; set; }
        public string NUMERO_CIDADEEXTERIOR { get; set; }
        public string CODIGO_PAIS  { get; set; }
        public string NUMERO_PAIS { get; set; }
        public string CODIGO_NATUREZA_JURIDICA { get; set; }
        public string DATA_INICIO_ATIVIDADE { get; set; }
        public string CNAE_FISCAL { get; set; }
        public string DESCRIÇÃO_TIPO_LOGRADOURO { get; set; }
        public string LOGRADOURO { get; set; }
        public string NUMERO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string BAIRRO { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string CODIGO_MUNICIPIO { get; set; }
        public string MUNICIPIO { get; set; }
        public string DDD_TELEFONE_1 { get; set; }
        public string DDD_1 { get; set; }
        public string TELEFONE_1  { get; set; }
        public string DDD_TELEFONE_2 { get; set; }
        public string DDD_2  { get; set; }
        public string TELEFONE_2 { get; set; }
        public string DDD_FAX { get; set; }
        public string NU_DDD_FAX  { get; set; }
        public string NU_FAX { get; set; }
        public string CORREIO_ELETRONICO { get; set; }
        public string QUALIFICACAO_DO_RESPONSAVEL { get; set; }
        public string CAPITAL_SOCIAL_DA_EMPRESA { get; set; }
        public string PORTE_EMPRESA { get; set; }
        public string OPÇÃO_PELO_SIMPLES { get; set; }
        public string DATA_OPCAO_PELO_SIMPLES { get; set; }
        public string DATA_EXCLUSAO_DO_SIMPLES { get; set; }
        public string OPCAO_PELO_MEI { get; set; }
        public string SITUACAO_ESPECIAL { get; set; }
        public string DATA_SITUACAO_ESPECIAL { get; set; }
        public string LinhaCompleta { get; set; }

        public ArquivoReceitaFederalLinhaPrincipal(string linha)
        {
            CNPJ = linha.Substring(3, 14).Trim();
            IDENTIFICADOR_MATRIZ_FILIAL = linha.Substring(17, 1).Trim();
            RAZAO_SOCIAL = linha.Substring(18, 150).Trim();
            NOME_FANTASIA = linha.Substring(168, 55).Trim();
            SITUACAO_CADASTRAL = linha.Substring(223, 2).Trim();
            DATA_SITUACAO_CADASTRAL = linha.Substring(225,8).Trim();
            MOTIVO_SITUACAO_CADASTRAL = linha.Substring(233,2).Trim();
            NUMERO_CIDADEEXTERIOR = linha.Substring(235,55).Trim();
            CODIGO_PAIS = linha.Substring(290,3).Trim();
            NUMERO_PAIS = linha.Substring(363,4).Trim();
            CODIGO_NATUREZA_JURIDICA = linha.Substring(367,8).Trim();
            DATA_INICIO_ATIVIDADE = linha.Substring(367,8).Trim();
            CNAE_FISCAL = linha.Substring(375,7);
            DESCRIÇÃO_TIPO_LOGRADOURO = linha.Substring(382,20).Trim();
            LOGRADOURO = linha.Substring(402,60).Trim();
            NUMERO = linha.Substring(462,6).Trim();
            COMPLEMENTO = linha.Substring(468,156).Trim();
            BAIRRO = linha.Substring(624, 50).Trim();
            CEP = linha.Substring(674,8).Trim();
            UF = linha.Substring(682,2).Trim();
            CODIGO_MUNICIPIO = linha.Substring(684,4).Trim();
            MUNICIPIO = linha.Substring(688,50).Trim();
            DDD_TELEFONE_1 = linha.Substring(738,12).Trim();
            DDD_1 = linha.Substring(738,4).Trim();
            TELEFONE_1 = linha.Substring(738,8).Trim();
            DDD_TELEFONE_2 = linha.Substring(750,12).Trim();
            DDD_2 = linha.Substring(750,04).Trim();
            TELEFONE_2 = linha.Substring(750,8).Trim();
            DDD_FAX = linha.Substring(762,12).Trim();
            NU_DDD_FAX = linha.Substring(762,4).Trim();
            NU_FAX = linha.Substring(762,8).Trim();
            CORREIO_ELETRONICO = linha.Substring(774,115).Trim();
            QUALIFICACAO_DO_RESPONSAVEL = linha.Substring(889,2).Trim();
            CAPITAL_SOCIAL_DA_EMPRESA = linha.Substring(891,14).Trim();
            PORTE_EMPRESA = linha.Substring(905,2).Trim();
            OPÇÃO_PELO_SIMPLES = linha.Substring(907,1).Trim();
            DATA_OPCAO_PELO_SIMPLES = linha.Substring(908,8).Trim();
            DATA_EXCLUSAO_DO_SIMPLES = linha.Substring(916,8).Trim();
            OPCAO_PELO_MEI = linha.Substring(924,1).Trim();
            SITUACAO_ESPECIAL = linha.Substring(925,23).Trim();
            DATA_SITUACAO_ESPECIAL = linha.Substring(948,8).Trim();
            LinhaCompleta = linha;

        }


    }
}
