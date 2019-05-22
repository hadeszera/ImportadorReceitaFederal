using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeFenicio
{
    public class ArquivoReceitaFederalLinhaTerciaria
    {
        public ArquivoReceitaFederalLinhaTerciaria(string Linha)
        {
            //Linha = "6F 00001164000133181130118113024619200476100147610024930201493020252117995811500581310058212005822101582390060101006391700646200070204007311400731900473203008111700823000190019019001902900190390019049001906900199993191010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000             6F 00001164000133181130118113024619200476100147610024930201493020252117995811500581310058212005822101582390060101006391700646200070204007311400731900473203008111700823000190019019001902900190390019049001906900199993191010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000             ";
            string cnaeTodos = Linha.Substring(17, 693);
            CNPJ = Linha.Substring(3, 14);
            CNAE_SECUNDARIA = new List<string>();
            
            for (var i=0;i<98;i++) {

                var cnae = cnaeTodos.Substring(0, 7);
                    if (cnae != "0000000")
                    {
                        CNAE_SECUNDARIA.Add(cnae);
                    }
                    else {
                        break;
                    }
                    cnaeTodos = cnaeTodos.Substring(7);

           


            }
            foreach (var cne in CNAE_SECUNDARIA) {
                inserirDadosLinhaTerciaria(CNPJ,cne);
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
                cmd.Parameters.AddWithValue(@"CnaePrincipal", 0);

                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteScalar();

                }
                catch (Exception e)
                {
                    sql = "INSERT INTO ErrosImportacao (Linha,Erro) VALUES(@Linha,@Erro)";
                   
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue(@"Linha", "Cnae:"+cnae+" CNPJ:"+cnpj);
                    cmd.Parameters.AddWithValue(@"Erro", e.Message);
                    cmd.ExecuteScalar();
                
                }
                finally
                {
                    conn.Close();
                }

            }

        }

        public string CNPJ;
        public List<string> CNAE_SECUNDARIA;

    }
}
