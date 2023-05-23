
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class FatNfeServiceWeb : GenericServiceWeb<FatNfe , FatNfeRepository , FatNfeController >, IFatNfeService
    {
        private string modificaWhere = "";

        public FatNfeServiceWeb(string requestUri): base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<FatNfeView> GetCamposEspecificos(string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<FatNfeView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FatNfeView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FatNfeView> GetListagemNfeRelatorio(FiltroFatNfeListagem filtro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FechamentoDoDiaView> GetFechamentoDoDia(DateTime DataInicio, DateTime DataFim, int Tipo)
        {

            var cn = new ConnectionWebAPI<IEnumerable<FechamentoDoDiaView>>(VestilloSession.UrlWebAPI);

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";



            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT nfe.referencia as Referencia,nfe.numero as Numero, nfe.DataInclusao as Inclusao, nfe.DataEmissao as Emissao,nfe.StatusNota as Status, ");
            SQL.AppendLine("nfe.totalnota as total, 1 as TP from nfe WHERE nfe.DataInclusao BETWEEN " + Valor + " AND nfe.StatusNota = " + Tipo + " AND nfe.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            SQL.AppendLine("   UNION ");
            SQL.AppendLine(" SELECT nfce.referencia as Referencia,nfce.numeronfce as Numero, nfce.dataemissao as Inclusao, nfce.datafinalizacao as Emissao,nfce.StatusNota as Status, ");
            SQL.AppendLine("nfce.totaloriginal as total, 2 as TP from nfce WHERE nfce.dataemissao BETWEEN " + Valor + " AND nfce.StatusNota = " + Tipo + " AND nfce.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            SQL.AppendLine("    ORDER BY TP ");
            
            return cn.Get(this.RequestUri, SQL.ToString());

        }


        public IEnumerable<RepXVendaView> GetRepXVenda(string Ano, int Uf)
        {
            var SQL = new StringBuilder();
            var cn = new ConnectionWebAPI<IEnumerable<RepXVendaView>>(VestilloSession.UrlWebAPI);


            var DataInicioTotal = Ano + "0101";
            var DataFimTotal = int.Parse(Ano) + 1 + "0101";

            SQL.AppendLine("SELECT V.referencia,v.Nome");

            //loop para cada mês do ano
            for (int i = 1; i <= 12; i++)
            {
                var DataInicio = Ano + "-" + i.ToString("d2") + "-" + "01";
                string DataFim = "";
                if (i == 12)
                {
                    DataFim = int.Parse(Ano) + 1 + "-" + "01-01";

                }
                else
                {
                    DataFim = Ano + "-" + (i + 1) + "01-01";
                }


                SQL.AppendLine(",SUM(IF(SUBSTRING(NFE.DataInclusao,1,10) >=" + DataInicio + "AND SUBSTRING(NFE.DataInclusao,1,10) < " + DataFim + ",nfeitens.quantidade - nfeitens.Qtddevolvida,0)) as QtdTotalPecasM" + i);
                SQL.AppendLine(",ROUND(SUM(IF(SUBSTRING(NFE.DataInclusao,1,10) >=" + DataInicio + "AND SUBSTRING(NFE.DataInclusao,1,10) < " + DataFim + ",(nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ),0)),2) as ValorTotalPecasM" + i);
                SQL.AppendLine(",ROUND(SUM(IF(SUBSTRING(NFE.DataInclusao,1,10) >=" + DataInicio + "AND SUBSTRING(NFE.DataInclusao,1,10) < " + DataFim + ",(nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ),0)) / SUM(IF(SUBSTRING(NFE.DataInclusao,1,10) >=" + DataInicio + "AND SUBSTRING(NFE.DataInclusao,1,10) <" + DataFim + ",nfeitens.quantidade - nfeitens.Qtddevolvida,0)),2) as PrecoMedioTotalPecasM" + i);
            }

            SQL.AppendLine(" from nfeitens");
            SQL.AppendLine(" INNER JOIN NFE ON NFE.Id = nfeitens.IdNfe");
            SQL.AppendLine(" LEFT OUTER JOIN colaboradores V on V.id = NFE.idvendedor");
            SQL.AppendLine(" INNER JOIN colaboradores C on C.id = NFE.IdColaborador");
            SQL.AppendLine(" INNER JOIN estados E on E.id = C.idestado");
            SQL.AppendLine(" WHERE NFE.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            if (Uf > 0)
            {
                SQL.AppendLine(" AND  E.id = " + Uf);
            }
            SQL.AppendLine(" GROUP BY v.id ");
            return cn.Get(this.RequestUri, SQL.ToString());
        }


        public IEnumerable<ListaFatVendaView> GetListaFatXVenda(DateTime DataInicio, DateTime DataFim, List<int> Vendedor, bool SomenteNFCe, bool SomenteTipoVendaNFCe, bool DataDatNfce)
        {
            var SQL = new StringBuilder();
            var cn = new ConnectionWebAPI<IEnumerable<ListaFatVendaView>>(VestilloSession.UrlWebAPI);

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";


            SQL.AppendLine("SELECT  Round(IFNULL(IF(SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))<0,0.00,SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))),0),2) as ValorTotal, ");
            SQL.AppendLine("ROUND(SUM(nfeitens.quantidade - nfeitens.Qtddevolvida),0) QtdTotalPecas, colaboradores.nome as NomeVendedor,colaboradores.id as IdVendedor,colaboradores.referencia as RefVendedor ");
            SQL.AppendLine("(select COUNT(nfe.id) as QtdFaturamento from nfe WHERE  IFNULL(NFE.statusnota = 1,0) AND  NFE.Tipo = 0 AND NFE.recebidasefaz = 1 AND NFE.StatusNota = 1 AND SUBSTRING(NFE.datainclusao,1,10) BETWEEN  '2019-01-01' AND '2019-01-31' AND NFE.Idempresa = 1 and nfe.idvendedor IN(" + Vendedor + ")) as QtdTotalFaturamentos");
            SQL.AppendLine("FROM nfeitens");
            SQL.AppendLine("INNER JOIN produtos ON produtos.id = nfeitens.iditem");
            SQL.AppendLine("INNER JOIN nfe on nfe.id = nfeitens.IdNfe");
            SQL.AppendLine("INNER JOIN colaboradores ON colaboradores.id = NFE.idvendedor");
            SQL.AppendLine("WHERE  IFNULL(NFE.statusnota = 1,0) AND  NFE.Tipo = 0 AND NFE.recebidasefaz = 1 AND NFE.StatusNota = 1 AND ");
            SQL.AppendLine(" SUBSTRING(NFE.DataInclusao,1,10) BETWEEN " + Valor);
            SQL.AppendLine(" AND NFE.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            SQL.AppendLine(" AND NFE.idvendedor IN (" + Vendedor + ")");
            SQL.AppendLine(" GROUP BY NFE.idvendedor order by colaboradores.nome");
            return cn.Get(this.RequestUri, SQL.ToString());
        }


        public int TotalFaturamentos(DateTime DataInicio, DateTime DataFim, int Vendedor, bool SomenteNFCe, bool DataDatNfce)
        {
            int Total = 0;

            var SQL = new StringBuilder();            
            var cn = new ConnectionWebAPI<IEnumerable<ListaFatVendaView>>(VestilloSession.UrlWebAPI);

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            SQL.AppendLine("select COUNT(*) as QtdTotalFaturamentos from nfe WHERE  IFNULL(NFE.statusnota = 1,0) AND  NFE.Tipo = 0 AND NFE.recebidasefaz = 1 AND NFE.StatusNota = 1 AND SUBSTRING(NFE.DataInclusao,1,10) BETWEEN " + Valor + " AND NFE.Idempresa = 1 and nfe.idvendedor IN(" + Vendedor + ") ");


            ListaFatVendaView ret = new ListaFatVendaView();


            //return cn.Get(ref ret, SQL.ToString());
           // cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                Total = ret.QtdTotalFaturamentos;
            }

            return Total;

        }

        public IEnumerable<FechamentoDoDiaPagView> GetFechamentoDoDiaPorPagamento(DateTime DataInicio, DateTime DataFim)
        {

            var SQL = new StringBuilder();
            var cn = new ConnectionWebAPI<IEnumerable<FechamentoDoDiaPagView>>(VestilloSession.UrlWebAPI);


            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

           
            SQL.AppendLine("SELECT nfe.id as IdNfe,nfe.referencia as ReferenciaFat,nfe.numero as NumNota, nfe.DataInclusao as Inclusao, ");
            SQL.AppendLine("IFNULL(nfe.DataEmissao,'') as Emissao,nfe.valdesconto as Desconto,nfe.frete as Frete,nfe.despesa as Despesas, ");
            SQL.AppendLine("nfe.totalnota as Total, ctr.id as IdTitulo,ctr.numtitulo as RefTitulo,ctr.parcela as ParcelaTitulo,ctr.dataVencimento as VencTitulo, ");
            SQL.AppendLine("ctr.ValorParcela as ValorParcela,IFNULL(ctr.SisCob,'') as SisCob, c.referencia as RefCliente,c.nome as NomeCliente from nfe ");
            SQL.AppendLine("INNER JOIN colaboradores c on c.id = nfe.IdColaborador ");
            SQL.AppendLine("INNER JOIN contasreceber ctr on ctr.IdFatNfe = nfe.id  ");
            SQL.AppendLine(" WHERE SUBSTRING(nfe.DataInclusao,1,10) BETWEEN " + Valor + " AND not isnull(ctr.IdCliente) AND nfe.tipo = 0 ");
            SQL.AppendLine(" AND nfe.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            SQL.AppendLine("    nfe.id,ctr.parcela ");
            return cn.Get(this.RequestUri, SQL.ToString());

        }

        public FatNfeEtiquetaView EtiquetaEnderecamento(int Faturamento, int Tipo)
        {

            var SQL = new StringBuilder();
            var cn = new ConnectionWebAPI<IEnumerable<FatNfeEtiquetaView>>(VestilloSession.UrlWebAPI);
      


            if (Tipo == 0)
            {
                SQL.AppendLine(" SELECT 'DESTINATARIO' as Linha1,cli.razaosocial as Linha2, CONCAT(cli.endereco,',',cli.numero,',',IFNULL(cli.complemento,'')) as Linha3, ");
                SQL.AppendLine(" CONCAT(cli.bairro, ' - ', mcli.municipio, ' - ',mcli.uf ) as Linha4, CONCAT('CEP:',IFNULL(cli.cep,'')) as Linha5, 'TRANSPORTADORA' as Linha6, ");
                SQL.AppendLine(" CONCAT('TRANSPORTADO POR: ', tra.razaosocial) as Linha7, CONCAT('VOLUME:___ DE ',nfe.volumes) as Linha8, CONCAT('NOTA FISCAL: ', nfe.numero) as Linha9,'' as Linha10, '' as Linha11 from nfe  ");
                SQL.AppendLine(" INNER JOIN colaboradores cli ON cli.id = nfe.IdColaborador ");
                SQL.AppendLine(" INNER JOIN municipiosibge as mcli ON mcli.id = cli.idmunicipio ");
                SQL.AppendLine(" INNER JOIN colaboradores tra ON tra.id = nfe.idtransportadora ");
                SQL.AppendLine(" WHERE nfe.Id = " + Faturamento);
                SQL.AppendLine(" AND nfe.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            }
            else
            {

                SQL.AppendLine(" SELECT 'REMETENTE' as Linha1,emp.razaosocial as Linha2, CONCAT(enderecos.Logradouro,',',enderecos.Numero) as Linha3, ");
                SQL.AppendLine(" CONCAT(enderecos.bairro, ' - ', memp.municipio, ' - ',memp.uf )as Linha4, CONCAT('CEP:',IFNULL(enderecos.cep,'')) as Linha5, 'DESTINATÁRIO' as Linha6, ");
                SQL.AppendLine(" cli.razaosocial as Linha7, CONCAT(cli.endereco,',',cli.numero,',',IFNULL(cli.complemento,'')) as Linha8, CONCAT(cli.bairro, ' - ', mcli.municipio, ' - ',mcli.uf ) as Linha9, ");
                SQL.AppendLine(" CONCAT('CEP:',cli.cep) as Linha10, CONCAT('TRANSPORTADO POR: EMP. BRASILEIRA DE CORREIOS: ',tra.nome) as Linha11 from nfe  ");
                SQL.AppendLine(" INNER JOIN colaboradores cli ON cli.id = nfe.IdColaborador ");
                SQL.AppendLine(" INNER JOIN municipiosibge as mcli ON mcli.id = cli.idmunicipio ");
                SQL.AppendLine(" INNER JOIN empresas emp ON emp.id = nfe.Idempresa ");
                SQL.AppendLine(" INNER JOIN enderecos ON enderecos.EmpresaId = nfe.Idempresa ");
                SQL.AppendLine(" INNER JOIN municipiosibge as memp ON memp.id = enderecos.MunicipioId ");
                SQL.AppendLine(" INNER JOIN colaboradores tra ON tra.id = nfe.idtransportadora ");
                SQL.AppendLine(" WHERE nfe.Id = " + Faturamento);
                SQL.AppendLine(" AND nfe.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            }


            FatNfeEtiquetaView ret = new FatNfeEtiquetaView();
           // return cn.Get(this.RequestUri, SQL.ToString());
           

            return ret;

        }

        public decimal TotalNcc(DateTime DataInicio, DateTime DataFim, int IdVendedor)
        {
            decimal Total = 0;

            var SQL = new StringBuilder();
            var cn = new ConnectionWebAPI<IEnumerable<ListaFatVendaView>>(VestilloSession.UrlWebAPI);

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            SQL.AppendLine("select SUM(contasreceber.ValorParcela) as ValorNcc from contasreceber where contasreceber.IdNotaconsumidor = " + IdVendedor + " AND contasreceber.IdTipoDocumento = 4");



            ListaFatVendaView ret = new ListaFatVendaView();


            //return cn.Get(ref ret, SQL.ToString());
            // cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                Total = ret.ValorNcc;
            }

            return Total;

        }

        public void GravarMovimentacaoCaixa(string referenciaNota, int idNota, IEnumerable<ContasReceber> parcelas, bool debito = false)
        {
            throw new NotImplementedException();
        }

        public FatNfe GetByNfce(int idNFCe)
        {
            throw new NotImplementedException();
        }


    }
}