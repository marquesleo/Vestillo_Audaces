using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;




using System.Data;
using System.Diagnostics;


using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace Vestillo.Business.Repositories
{
    public class EmpresaRepository: GenericRepository<Empresa>
    {
        public EmpresaRepository() : base(new DapperConnection<Empresa>())
        {
        }

        public  IEnumerable<Empresa> GetByUsuarioId(int usuarioId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT E.*");
            SQL.AppendLine("FROM Empresas E");
            SQL.AppendLine("INNER JOIN UsuarioEmpresas UE ON UE.EmpresaId = E.Id");
            SQL.AppendLine("WHERE UsuarioId = " + usuarioId.ToString());
            SQL.AppendLine("ORDER BY Principal DESC, E.Id");

            return _cn.ExecuteStringSqlToList(new Empresa(), SQL.ToString());
        }

        public void UpdateImagemProduto(string Caminho, int IdEmpresaLogada)
        {
            var SQL = new StringBuilder();
            SQL.Append("UPDATE empresas SET  CaminhoImagemProduto = " +  Caminho  + " WHERE id = " + IdEmpresaLogada);
            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public IEnumerable<Empresa> GetBySelecao()
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * ");
            SQL.AppendLine("FROM Empresas");
            SQL.AppendLine("WHERE empresas.RazaoSocial not LIKE '*%' ");
            

            return _cn.ExecuteStringSqlToList(new Empresa(), SQL.ToString());
        }

        public bool VerificaLicenca(string Cnpj)
        {
            bool VerificaLicenca = false;
            try
            {
                String DataAgora = DateTime.Now.ToShortDateString(); //data = 01/12/2014

                

                var SQL = new StringBuilder();
                var cn = new DapperConnection<licenca>();

                string cnpj = VestilloSession.limparCpfCnpj(Cnpj);

                SQL.AppendLine("select * from versao WHERE Cnpj = " + "'" + cnpj + "'");

                licenca ret = new licenca();

                cn.ExecuteToModel(ref ret, SQL.ToString());

                if (ret != null)
                {
                    string DataBanco = ret.Dia;

                    //verifico se a data da empresa é igual a data de hoje,se for não precisa executar novamente
                    //só executo se for o mesmo dia e o cara estiver bloqueado
                    if (DataAgora == DataBanco && ret.Bloqueado == "NAO")
                    {
                        VerificaLicenca = false;
                    }
                    else if (DataAgora == DataBanco && ret.Bloqueado == "SIM")
                    {
                        VerificaLicenca = true;
                    }
                    else
                    {
                        VerificaLicenca = true;
                    }
                }               
                else
                {
                    VerificaLicenca = true;
                }
            }

            catch (Exception ex)
            {
            }
            return VerificaLicenca;
        }

        public void AtualizaLicenca(string Cnpj,string  SimNao)
        {
            String DataAgora = DateTime.Now.ToShortDateString(); //data = 01/12/2014
            string  SQL = String.Empty;
            var cn = new DapperConnection<licenca>();

            string cnpj = VestilloSession.limparCpfCnpj(Cnpj);

            SQL = "UPDATE versao SET dia = " + "'" + DataAgora + "'" +  ",Bloqueado =" + "'" + SimNao + "'" + " WHERE Cnpj = " + cnpj;
            cn.ExecuteNonQuery(SQL);

        }

        public Endereco GetEndereco(int IdEmpresa)
        {
            var cn = new DapperConnection<Endereco>();
            var EndEmpresa = new Endereco();
            string SQL = String.Empty;
            SQL = "SELECT * FROM enderecos WHERE EmpresaId = " + IdEmpresa + " AND TipoEndereco = 1";

            cn.ExecuteToModel(ref EndEmpresa, SQL.ToString());
            return EndEmpresa;


        }

        public bool PossuiArquivoLicenca()
        {
            var SQL = new StringBuilder();
            var cn = new DapperConnection<licenca>();           

            SQL.AppendLine("select * from versao " );

            licenca ret = new licenca();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret == null )
            {
                return false;
            }
            else
            {
                if(String.IsNullOrEmpty(ret.Cnpj))
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }

        }

        public bool IncluirLicenca(string cnpj,string NomeEmpresa)
        {
            var SQL = String.Empty;
            var cn = new DapperConnection<licenca>();

            string cnpjLimpo = VestilloSession.limparCpfCnpj(cnpj);

            try
            {
                String DataAgora = DateTime.Now.ToShortDateString(); //data = 01/12/2014

                SQL = ("insert into versao (Cnpj, Dia, Bloqueado) values ( " + "'" + cnpjLimpo + "'" + "," + "'" + DataAgora + "'" + "," + "'Nao'" + ")");

                cn.ExecuteNonQuery(SQL);


                SQL = ("Update  empresas set empresas.RazaoSocial = " + "'" + NomeEmpresa + "'" + " ,empresas.NomeFantasia = " + "'" + NomeEmpresa + "'" + " , empresas.CNPJ = " + "'" + cnpj + "'");
                cn.ExecuteNonQuery(SQL);


                return true;
            }
            catch(Exception ex)
            {
                return false;
            }           

        }

        public void LeuPdf(int UsuarioLogado)
        {
            var SQL = String.Empty;
            var cn = new DapperConnection<licenca>();            

            try
            {
                
                SQL = ("update usuarios set PdfExibido = 1 WHERE usuarios.id = " + UsuarioLogado);
                cn.ExecuteNonQuery(SQL);
               
            }
            catch (Exception ex)
            {
               
            }

        }

        public bool ExibirTelaPdf(int UsuarioLogado)
        {
            var SQL = new StringBuilder();
            var cn = new DapperConnection<Usuario>();

            SQL.AppendLine("select * from usuarios where PdfExibido = 0 AND usuarios.id = " + UsuarioLogado);

            Usuario ret = new Usuario();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret == null)
            {
                return false;
            }
            else
            {
                return true;

            }

        }

    }
}
