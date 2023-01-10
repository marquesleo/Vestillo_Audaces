
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class InstrucoesDosBoletosRepository : GenericRepository<InstrucoesDosBoletos>
    {
        public InstrucoesDosBoletosRepository() : base(new DapperConnection<InstrucoesDosBoletos>())
        {
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByData(DateTime data)
        {
            var cn = new DapperConnection<InstrucoesDosBoletosView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT *");
            SQL.AppendLine("FROM	instrucoesdosboletos ");
            SQL.AppendLine("WHERE  date(instrucoesdosboletos.DataEmissao) = '" + data.ToString("yyyy-MM-dd") );
            SQL.AppendLine("'        AND " + FiltroEmpresa("instrucoesdosboletos.IdEmpresa"));
            
           

            return cn.ExecuteStringSqlToList(new InstrucoesDosBoletosView(), SQL.ToString());
        }


        public IEnumerable<InstrucoesDosBoletosView> GetViewByDataEBanco(DateTime data, int IdBanco)
        {
            var cn = new DapperConnection<InstrucoesDosBoletosView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT ");
            SQL.AppendLine("  instrucoesdosboletos.id ,instrucoesdosboletos.IdBoleto,CTR.id as IdTitulo,CTR.DataVencimento,instrucoesdosboletos.IdBoleto,instrucoesdosboletos.IdBanco,  ");
            SQL.AppendLine("  instrucoesdosboletos.IdInstrucao,instrucoesdosboletos.RemessaGerada,CTR.NumTitulo,CTR.Parcela,CLI.razaosocial as NomeCliente,CLI.cnpjcpf as CpfCnpj, ");
            SQL.AppendLine("  instrucoesdosboletos.DataEmissao,CTR.ValorParcela as  Valor,BOL.NossoNumero,INSBC.Descricao as DescicaoInstrucao,  ");
            SQL.AppendLine("  IF(instrucoesdosboletos.RemessaGerada = 0,'Não','Sim') as SimNaoRemessaGerada   ");
            SQL.AppendLine("  FROM instrucoesdosboletos ");
            SQL.AppendLine("  INNER JOIN instrucoesporbancos INSBC ON INSBC.IdInstrucao = instrucoesdosboletos.IdInstrucao ");
            SQL.AppendLine("  INNER JOIN boletosgerados BOL ON BOL.id = instrucoesdosboletos.IdBoleto ");
            SQL.AppendLine("  INNER JOIN contasreceber CTR ON CTR.Id = BOL.idTitulo ");
            SQL.AppendLine("  INNER JOIN colaboradores CLI ON CLI.id = CTR.IdCliente ");            
            SQL.AppendLine("WHERE  date(instrucoesdosboletos.DataEmissao) = '" + data.ToString("yyyy-MM-dd") + "'  AND instrucoesdosboletos.IdBanco = " + IdBanco);
            SQL.AppendLine("        AND " + FiltroEmpresa("instrucoesdosboletos.IdEmpresa"));

            return cn.ExecuteStringSqlToList(new InstrucoesDosBoletosView(), SQL.ToString());
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByDataEBoleto(DateTime data, int IdBoleto)
        {
            var cn = new DapperConnection<InstrucoesDosBoletosView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT *");
            SQL.AppendLine("FROM	instrucoesdosboletos ");
            SQL.AppendLine("WHERE  date(instrucoesdosboletos.DataEmissao) = '" + data.ToString("yyyy-MM-dd")   + "'  AND instrucoesdosboletos.IdBoleto = " + IdBoleto);
            SQL.AppendLine("        AND " + FiltroEmpresa("instrucoesdosboletos.IdEmpresa"));

            return cn.ExecuteStringSqlToList(new InstrucoesDosBoletosView(), SQL.ToString());
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByBoletoEInstrucao(int IdBoleto, int IdInstrucao)
        {
            var cn = new DapperConnection<InstrucoesDosBoletosView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT *");
            SQL.AppendLine("FROM	instrucoesdosboletos ");
            SQL.AppendLine("WHERE  instrucoesdosboletos.IdBoleto = " + IdBoleto + " AND instrucoesdosboletos.id = " + IdInstrucao);
            SQL.AppendLine("        AND " + FiltroEmpresa("instrucoesdosboletos.IdEmpresa"));

            return cn.ExecuteStringSqlToList(new InstrucoesDosBoletosView(), SQL.ToString());
        }

        public void UpdateRemessaGerada(List<int> IdInstrucao, List<int> IdTitulo)
        {
            var cn = new DapperConnection<ContasReceber>();
            string SQL = String.Empty;
            foreach (var IdReceber in IdTitulo)
            {
                SQL = String.Empty;
                SQL = "UPDATE contasreceber SET RemessaGerada = 1 WHERE id = " + IdReceber;
                cn.ExecuteNonQuery(SQL.ToString());
            }

            foreach (var IdInstru in IdInstrucao)
            {
                SQL = String.Empty;
                SQL = "UPDATE instrucoesdosboletos SET RemessaGerada = 1 WHERE id = " + IdInstru;
                _cn.ExecuteNonQuery(SQL.ToString());
            }











        }
    }
}
