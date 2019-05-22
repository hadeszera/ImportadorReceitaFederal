using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackOfficeFenicio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnImportarArquivoCnpj_Click(object sender, EventArgs e)
        {
            int counter = 0;
            char[] str = "".ToCharArray();
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Desenv01\Desktop\Empresas\DADOS_ABERTOS_CNPJ (1)\F.K032001K.D90308");

            //C:\Users\Desenv01\Desktop\Empresas\DADOS_ABERTOS_CNPJ (1)\F.K032001K.D90308 86gb file
            // 
            string line = string.Empty;

            var lista = new List<ArquivoReceitaFederalLinhaPrincipal>();
            var listaSec = new List<ArquivoReceitaFederalLinhaSecundaria>();

            while ((line = file.ReadLine()) != null)
            {

                if (line.Substring(0, 1) == "1")
                {
                    var linhaPrincipal = new ArquivoReceitaFederalLinhaPrincipal(line);
                    inserirDadosLinhaPrincipal(linhaPrincipal);
                }

                if (line.Substring(0, 1) == "2")
                {

                    var linhaSecundaria = new ArquivoReceitaFederalLinhaSecundaria(line);
                    inserirDadosLinhaSecundaria(linhaSecundaria);

                }

                if (line.Substring(0, 1) == "6")
                {
                    var LinhaTerc = new ArquivoReceitaFederalLinhaTerciaria(line);

                }



                counter++;
                txt2.Text = counter.ToString();
                Application.DoEvents();

            }

            MessageBox.Show("Importação Finalizada");




        }








        public bool inserirDadosLinhaSecundaria(ArquivoReceitaFederalLinhaSecundaria Linha2)
        {
            var sql = @"INSERT INTO[dbo].[Socio]
           ([CNPJ]
           ,[IDENTIFICADOR_DE_SOCIO]
           ,[NOME_SOCIO_NO_CASO_PF_OU_RAZÃO_SOCIAL_NO_CASO_PJ]
           ,[CNPJ_CPF_DO_SOCIO]
           ,[CODIGO_QUALIFICACAO_SOCIO]
           ,[PERCENTUAL_CAPITAL_SOCIAL]
           ,[DATA_ENTRADA_SOCIEDADE]
           ,[CODIGO_PAIS]
           ,[NOME_PAIS_SOCIO]
           ,[CPF_REPRESENTANTE_LEGAL]
           ,[NOME_REPRESENTANTE]
           ,[CODIGO_QUALIFICACAO_REPRESENTANTE_LEGAL])
           
           VALUES
           (@CNPJ
           ,@IDENTIFICADOR_DE_SOCIO
           ,@NOME_SOCIO_NO_CASO_PF_OU_RAZÃO_SOCIAL_NO_CASO_PJ
           ,@CNPJ_CPF_DO_SOCIO
           ,@CODIGO_QUALIFICACAO_SOCIO
           ,@PERCENTUAL_CAPITAL_SOCIAL
           ,@DATA_ENTRADA_SOCIEDADE
           ,@CODIGO_PAIS
           ,@NOME_PAIS_SOCIO
           ,@CPF_REPRESENTANTE_LEGAL
           ,@NOME_REPRESENTANTE
           ,@CODIGO_QUALIFICACAO_REPRESENTANTE_LEGAL)";



            //var sqlConnectionString = "Data Source=localhost;Initial Catalog=Fenicio_Receita_Federal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var sqlConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=fenicio_receita_federal;Integrated Security=true;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


            using (var conn = new SqlConnection(sqlConnectionString))
            {
                DateTime DATA_ENTRADA_SOCIEDADE;
                if (!DateTime.TryParse(Linha2.DATA_ENTRADA_SOCIEDADE.Substring(6, 2) + "/" + Linha2.DATA_ENTRADA_SOCIEDADE.Substring(4, 2) + "/" + Linha2.DATA_ENTRADA_SOCIEDADE.Substring(0, 4), out DATA_ENTRADA_SOCIEDADE))
                {
                    sql = sql.Replace("@DATA_OPCAO_PELO_SIMPLES", "NULL");

                }
                //corrigir a data que deu estouro de datetime

                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(@"CNPJ", Linha2.CNPJ);
                cmd.Parameters.AddWithValue(@"IDENTIFICADOR_DE_SOCIO", Linha2.IDENTIFICADOR_DE_SOCIO);
                cmd.Parameters.AddWithValue(@"NOME_SOCIO_NO_CASO_PF_OU_RAZÃO_SOCIAL_NO_CASO_PJ", Linha2.NOME_SOCIO_NO_CASO_PF_OU_RAZÃO_SOCIAL_NO_CASO_PJ);
                cmd.Parameters.AddWithValue(@"CNPJ_CPF_DO_SOCIO", Linha2.CNPJ_CPF_DO_SOCIO);
                cmd.Parameters.AddWithValue(@"CODIGO_QUALIFICACAO_SOCIO", Linha2.CODIGO_QUALIFICACAO_SOCIO);
                cmd.Parameters.AddWithValue(@"PERCENTUAL_CAPITAL_SOCIAL", Linha2.PERCENTUAL_CAPITAL_SOCIAL);
                //cmd.Parameters.AddWithValue(@"DATA_ENTRADA_SOCIEDADE", DATA_ENTRADA_SOCIEDADE);
                cmd.Parameters.Add("@DATA_ENTRADA_SOCIEDADE", System.Data.SqlDbType.DateTime2, 8).Value = DATA_ENTRADA_SOCIEDADE;
                cmd.Parameters.AddWithValue(@"CODIGO_PAIS", Linha2.CODIGO_PAIS);
                cmd.Parameters.AddWithValue(@"NOME_PAIS_SOCIO", Linha2.NOME_PAIS_SOCIO);
                cmd.Parameters.AddWithValue(@"CPF_REPRESENTANTE_LEGAL", Linha2.CPF_REPRESENTANTE_LEGAL);
                cmd.Parameters.AddWithValue(@"NOME_REPRESENTANTE", Linha2.NOME_REPRESENTANTE);
                cmd.Parameters.AddWithValue(@"CODIGO_QUALIFICACAO_REPRESENTANTE_LEGAL", Linha2.CODIGO_QUALIFICACAO_REPRESENTANTE_LEGAL);


                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteScalar();
                    return true;
                }
                catch (Exception e)
                {
                    sql = "INSERT INTO ErrosImportacao (Linha,Erro) VALUES(@Linha,@Erro)";

                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue(@"Linha", Linha2.LinhaCompleta);
                    cmd.Parameters.AddWithValue(@"Erro", e.Message);
                    cmd.ExecuteScalar();
                    return false;
                }
                finally
                {
                    conn.Close();
                }

            }







        }



        private void inserirDadosLinhaTerciaria(string cnpj, string cnae)
        {


            var sql =
                @"INSERT INTO [dbo].[Cnae]
                ([CNPJ],[CNAE],[CnaePrincipal])
                VALUES
               (@CNPJ,@CNAE,@CnaePrincipal)";



            //var sqlConnectionString = "Data Source=localhost;Initial Catalog=Fenicio_Receita_Federal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var sqlConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=fenicio_receita_federal;Integrated Security=true;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";



            using (var conn = new SqlConnection(sqlConnectionString))
            {


                var cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue(@"CNPJ", cnpj);
                cmd.Parameters.AddWithValue(@"CNAE", cnae);
                cmd.Parameters.AddWithValue(@"CnaePrincipal", 1);
                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteScalar();

                }
                catch (Exception e)
                {
                    sql = "INSERT INTO ErrosImportacao (Linha,Erro) VALUES(@Linha,@Erro)";

                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue(@"Linha", "Cnae:" + cnae + " CNPJ:" + cnpj);
                    cmd.Parameters.AddWithValue(@"Erro", e.Message);
                    cmd.ExecuteScalar();

                }
                finally
                {
                    conn.Close();
                }

            }

        }





        public bool inserirDadosLinhaPrincipal(ArquivoReceitaFederalLinhaPrincipal Linha1)
        {
            var sql = @"
            INSERT INTO Empresa
           (
           [CNPJ]
           ,[IDENTIFICADOR_MATRIZ_FILIAL]
           ,[RAZAO_SOCIAL]
           ,[NOME_FANTASIA]
           ,[SITUACAO_CADASTRAL]
           ,[DATA_SITUACAO_CADASTRAL]
           ,[MOTIVO_SITUACAO_CADASTRAL]
           ,[NUMERO_CIDADEEXTERIOR]
           ,[CODIGO_PAIS]
           ,[NUMERO_PAIS]
           ,[CODIGO_NATUREZA_JURIDICA]
           ,[DATA_INICIO_ATIVIDADE]
           ,[DESCRIÇÃO_TIPO_LOGRADOURO]
           ,[LOGRADOURO]
           ,[NUMERO]
           ,[COMPLEMENTO]
           ,[BAIRRO]
           ,[CEP]
           ,[UF]
           ,[CODIGO_MUNICIPIO]
           ,[MUNICIPIO]
           ,[DDD_TELEFONE_1]
           ,[DDD_TELEFONE_2]
           ,[DDD_FAX]
           ,[CORREIO_ELETRONICO]
           ,[QUALIFICACAO_DO_RESPONSAVEL]
           ,[CAPITAL_SOCIAL_DA_EMPRESA]
           ,[PORTE_EMPRESA]
           ,[OPÇÃO_PELO_SIMPLES]
           ,[DATA_OPCAO_PELO_SIMPLES]
           ,[DATA_EXCLUSAO_DO_SIMPLES]
           ,[OPCAO_PELO_MEI])
     VALUES
           (
           @CNPJ,
           @IDENTIFICADOR_MATRIZ_FILIAL,
           @RAZAO_SOCIAL,
           @NOME_FANTASIA,
           @SITUACAO_CADASTRAL,
           @DATA_SITUACAO_CADASTRAL,
           @MOTIVO_SITUACAO_CADASTRAL,
           @NUMERO_CIDADEEXTERIOR,
           @CODIGO_PAIS,
           @NUMERO_PAIS,
           @CODIGO_NATUREZA_JURIDICA,
           @DATA_INICIO_ATIVIDADE,
           @DESCRIÇÃO_TIPO_LOGRADOURO,
           @LOGRADOURO,
           @NUMERO,
           @COMPLEMENTO,
           @BAIRRO,
           @CEP,
           @UF,
           @CODIGO_MUNICIPIO,
           @MUNICIPIO,
           @DDD_TELEFONE_1,
           @DDD_TELEFONE_2,
           @DDD_FAX,
           @CORREIO_ELETRONICO,
           @QUALIFICACAO_DO_RESPONSAVEL,
           @CAPITAL_SOCIAL_DA_EMPRESA,
           @PORTE_EMPRESA,
           @OPÇÃO_PELO_SIMPLES,
           @DATA_OPCAO_PELO_SIMPLES,
           @DATA_EXCLUSAO_DO_SIMPLES,
           @OPCAO_PELO_MEI)";



            var sqlConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=fenicio_receita_federal;Integrated Security=true;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //var sqlConnectionString = "Data Source=localhost;Initial Catalog=Fenicio_Receita_Federal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            DateTime dataOpcaoSimples;
            decimal capitalSocial;
            DateTime DATA_OPCAO_PELO_SIMPLES;
            DateTime DATA_INICIO_ATIVIDADE;
            DateTime DATA_EXCLUSAO_DO_SIMPLES;
            DateTime DATA_SITUACAO_CADASTRAL;
            //DateTime DATA_SITUACAO_ESPECIAL;

            inserirDadosLinhaTerciaria(Linha1.CNPJ, Linha1.CNAE_FISCAL);



            using (var conn = new SqlConnection(sqlConnectionString))
            {
                if (!DateTime.TryParse(Linha1.DATA_OPCAO_PELO_SIMPLES.Substring(6, 2) + "/" + Linha1.DATA_OPCAO_PELO_SIMPLES.Substring(4, 2) + "/" + Linha1.DATA_OPCAO_PELO_SIMPLES.Substring(0, 4), out DATA_OPCAO_PELO_SIMPLES))
                {
                    sql = sql.Replace("@DATA_OPCAO_PELO_SIMPLES", "NULL");

                }

                if (!DateTime.TryParse(Linha1.DATA_INICIO_ATIVIDADE.Substring(6, 2) + "/" + Linha1.DATA_INICIO_ATIVIDADE.Substring(4, 2) + "/" + Linha1.DATA_INICIO_ATIVIDADE.Substring(0, 4), out DATA_INICIO_ATIVIDADE))
                {
                    sql = sql.Replace("@DATA_INICIO_ATIVIDADE", "NULL");

                }


                if (!DateTime.TryParse(Linha1.DATA_EXCLUSAO_DO_SIMPLES.Substring(6, 2) + "/" + Linha1.DATA_EXCLUSAO_DO_SIMPLES.Substring(4, 2) + "/" + Linha1.DATA_EXCLUSAO_DO_SIMPLES.Substring(0, 4), out DATA_EXCLUSAO_DO_SIMPLES))
                {
                    sql = sql.Replace("@DATA_EXCLUSAO_DO_SIMPLES", "NULL");

                }

                if (!DateTime.TryParse(Linha1.DATA_SITUACAO_CADASTRAL.Substring(6, 2) + "/" + Linha1.DATA_SITUACAO_CADASTRAL.Substring(4, 2) + "/" + Linha1.DATA_SITUACAO_CADASTRAL.Substring(0, 4), out DATA_SITUACAO_CADASTRAL))
                {

                    sql = sql.Replace("@DATA_SITUACAO_CADASTRAL", "NULL");
                }



                if (decimal.TryParse(Linha1.CAPITAL_SOCIAL_DA_EMPRESA, out capitalSocial))
                {
                    capitalSocial = capitalSocial / 100;
                }
                else
                {
                    capitalSocial = 0;
                }





                var cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CNPJ", Linha1.CNPJ);


                cmd.Parameters.AddWithValue("@IDENTIFICADOR_MATRIZ_FILIAL", Linha1.IDENTIFICADOR_MATRIZ_FILIAL);

                cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", Linha1.RAZAO_SOCIAL);



                cmd.Parameters.AddWithValue("@MOTIVO_SITUACAO_CADASTRAL", Linha1.MOTIVO_SITUACAO_CADASTRAL);


                cmd.Parameters.AddWithValue("@NOME_FANTASIA", Linha1.NOME_FANTASIA);


                cmd.Parameters.AddWithValue("@SITUACAO_CADASTRAL", Linha1.SITUACAO_CADASTRAL);

                if (DATA_SITUACAO_CADASTRAL != null && DATA_SITUACAO_CADASTRAL != DateTime.MinValue)
                {
                    //cmd.Parameters.AddWithValue("@DATA_SITUACAO_CADASTRAL", DATA_SITUACAO_CADASTRAL);
                    cmd.Parameters.Add("@DATA_SITUACAO_CADASTRAL", System.Data.SqlDbType.DateTime2, 8).Value = DATA_SITUACAO_CADASTRAL;

                }

                cmd.Parameters.AddWithValue("@NUMERO_CIDADEEXTERIOR", Linha1.NUMERO_CIDADEEXTERIOR);

                cmd.Parameters.AddWithValue("@CODIGO_PAIS", Linha1.CODIGO_PAIS);

                cmd.Parameters.AddWithValue("@NUMERO_PAIS", Linha1.NUMERO_PAIS);

                cmd.Parameters.AddWithValue("@CODIGO_NATUREZA_JURIDICA", Linha1.CODIGO_NATUREZA_JURIDICA);
                if (DATA_INICIO_ATIVIDADE != null && DATA_INICIO_ATIVIDADE != DateTime.MinValue)
                {
                    //cmd.Parameters.AddWithValue("@DATA_INICIO_ATIVIDADE", DATA_INICIO_ATIVIDADE);
                    cmd.Parameters.Add("@DATA_INICIO_ATIVIDADE", System.Data.SqlDbType.DateTime2, 8).Value = DATA_INICIO_ATIVIDADE;
                }

                cmd.Parameters.AddWithValue("@DESCRIÇÃO_TIPO_LOGRADOURO", Linha1.DESCRIÇÃO_TIPO_LOGRADOURO);



                cmd.Parameters.AddWithValue("@LOGRADOURO", Linha1.LOGRADOURO);

                cmd.Parameters.AddWithValue("@NUMERO", Linha1.NUMERO);

                cmd.Parameters.AddWithValue("@COMPLEMENTO", Linha1.COMPLEMENTO);

                cmd.Parameters.AddWithValue("@BAIRRO", Linha1.BAIRRO);

                cmd.Parameters.AddWithValue("@CEP", Linha1.CEP);

                cmd.Parameters.AddWithValue("@UF", Linha1.UF);

                cmd.Parameters.AddWithValue("@CODIGO_MUNICIPIO", Linha1.CODIGO_MUNICIPIO);

                cmd.Parameters.AddWithValue("@MUNICIPIO", Linha1.MUNICIPIO);

                cmd.Parameters.AddWithValue("@DDD_TELEFONE_1", Linha1.DDD_TELEFONE_1);

                cmd.Parameters.AddWithValue("@DDD_TELEFONE_2", Linha1.DDD_TELEFONE_2);

                cmd.Parameters.AddWithValue("@DDD_FAX", Linha1.DDD_FAX);

                cmd.Parameters.AddWithValue("@CORREIO_ELETRONICO", Linha1.CORREIO_ELETRONICO);

                cmd.Parameters.AddWithValue("@QUALIFICACAO_DO_RESPONSAVEL", Linha1.QUALIFICACAO_DO_RESPONSAVEL);

                cmd.Parameters.AddWithValue("@CAPITAL_SOCIAL_DA_EMPRESA", capitalSocial);

                cmd.Parameters.AddWithValue("@PORTE_EMPRESA", Linha1.PORTE_EMPRESA);


                cmd.Parameters.AddWithValue("@OPÇÃO_PELO_SIMPLES", Linha1.OPÇÃO_PELO_SIMPLES);



                if (DATA_OPCAO_PELO_SIMPLES != null && DATA_OPCAO_PELO_SIMPLES != DateTime.MinValue)
                {
                    //cmd.Parameters.AddWithValue("@DATA_OPCAO_PELO_SIMPLES", DATA_OPCAO_PELO_SIMPLES);
                    cmd.Parameters.Add("@DATA_OPCAO_PELO_SIMPLES", System.Data.SqlDbType.DateTime2, 8).Value = DATA_OPCAO_PELO_SIMPLES;
                }
                if (DATA_EXCLUSAO_DO_SIMPLES != null && DATA_EXCLUSAO_DO_SIMPLES != DateTime.MinValue)
                {
                    //cmd.Parameters.AddWithValue("@DATA_EXCLUSAO_DO_SIMPLES", DATA_EXCLUSAO_DO_SIMPLES);
                    cmd.Parameters.Add("@DATA_EXCLUSAO_DO_SIMPLES", System.Data.SqlDbType.DateTime2, 8).Value = DATA_EXCLUSAO_DO_SIMPLES;
                }

                cmd.Parameters.AddWithValue("@OPCAO_PELO_MEI", Linha1.OPCAO_PELO_MEI);

                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteScalar();
                    return true;
                }
                catch (Exception e)
                {
                    sql = "INSERT INTO ErrosImportacao (Linha,Erro) VALUES(@Linha,@Erro)";

                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue(@"Linha", Linha1.LinhaCompleta);
                    cmd.Parameters.AddWithValue(@"Erro", e.Message);
                    cmd.ExecuteScalar();
                    return false;
                }
                finally
                {
                    conn.Close();
                }

            }



        }




    }
}
