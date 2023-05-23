using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class AtividadeRepository : GenericRepository<Atividade>
    {
        public IEnumerable<AtividadeView> ListByColaboradorETipoAtividadeCliente(int colaborador, Atividade.EnumTipoAtividadeCliente tipoAtividadeCliente)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	CLI.Nome AS NomeCliente");
            sql.AppendLine("		,VEN.Nome AS VendedorNome");
            sql.AppendLine("		,UI.Nome AS NomeUsuarioCriacao");
            sql.AppendLine("		,T.Descricao AS DescricaoTipoAtividade");
            sql.AppendLine("		,UA.Nome AS NomeUsuarioAlerta");
            sql.AppendLine("		,A.*");
            sql.AppendLine("FROM 	Atividades A");
            sql.AppendLine("	INNER JOIN Usuarios     UI  ON UI.Id = A.UsuarioCriacaoAtividadeId");
            sql.AppendLine("    INNER JOIN TipoAtividade T  ON T.Id = A.TipoAtividadeId	");
            sql.AppendLine("    LEFT JOIN Usuarios      UA  ON UA.Id = A.UsuarioAlertaAtividadeId");
            sql.AppendLine("	LEFT JOIN Colaboradores CLI ON CLI.Id = A.ClienteId");
            sql.AppendLine("	LEFT JOIN Colaboradores	VEN ON VEN.Id = A.VendedorId");
            sql.AppendLine("WHERE 	A.ClienteId = " + colaborador.ToString());
            sql.AppendLine("		AND A.TipoAtividadeCliente = " + ((int)tipoAtividadeCliente).ToString());
            sql.AppendLine("ORDER BY A.DataCriacao DESC");

            return VestilloConnection.ExecSQLToListWithNewConnection<AtividadeView>(sql.ToString());
        }

        public IEnumerable<AtividadeView> ListByVendedor(int vendedorId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	CLI.Nome AS NomeCliente");
            sql.AppendLine("		,VEN.Nome AS VendedorNome");
            sql.AppendLine("		,UI.Nome AS NomeUsuarioCriacao");
            sql.AppendLine("		,T.Descricao AS DescricaoTipoAtividade");
            sql.AppendLine("		,UA.Nome AS NomeUsuarioAlerta");
            sql.AppendLine("		,A.*");
            sql.AppendLine("FROM 	Atividades A");
            sql.AppendLine("	INNER JOIN Usuarios     UI  ON UI.Id = A.UsuarioCriacaoAtividadeId");
            sql.AppendLine("    INNER JOIN TipoAtividade T  ON T.Id = A.TipoAtividadeId	");
            sql.AppendLine("    LEFT JOIN Usuarios      UA  ON UA.Id = A.UsuarioAlertaAtividadeId");
            sql.AppendLine("	LEFT JOIN Colaboradores CLI ON CLI.Id = A.ClienteId");
            sql.AppendLine("	LEFT JOIN Colaboradores	VEN ON VEN.Id = A.VendedorId");
            sql.AppendLine("WHERE 	A.VendedorId = " + vendedorId.ToString());
            sql.AppendLine("ORDER BY A.DataCriacao DESC");

            return VestilloConnection.ExecSQLToListWithNewConnection<AtividadeView>(sql.ToString());
        }

        public IEnumerable<AtividadeView> ListByUsuarioVendedor(int usuarioId, bool somenteComAlertaPendente = false)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	CLI.Nome AS NomeCliente");
            sql.AppendLine("		,VEN.Nome AS VendedorNome");
            sql.AppendLine("		,UI.Nome AS NomeUsuarioCriacao");
            sql.AppendLine("		,T.Descricao AS DescricaoTipoAtividade");
            sql.AppendLine("		,UA.Nome AS NomeUsuarioAlerta");
            sql.AppendLine("		,A.*");
            sql.AppendLine("FROM 	Atividades A");
            sql.AppendLine("	INNER JOIN Usuarios     UI  ON UI.Id = A.UsuarioCriacaoAtividadeId");
            sql.AppendLine("    INNER JOIN TipoAtividade T  ON T.Id = A.TipoAtividadeId	");
            sql.AppendLine("    LEFT JOIN Usuarios      UA  ON UA.Id = A.UsuarioAlertaAtividadeId");
            sql.AppendLine("	LEFT JOIN Colaboradores CLI ON CLI.Id = A.ClienteId");
            sql.AppendLine("	LEFT JOIN Colaboradores	VEN ON VEN.Id = A.VendedorId");
            sql.AppendLine("WHERE 	(VEN.UsuarioId = " + usuarioId.ToString() + " OR A.UsuarioAlertaAtividadeId = " + usuarioId.ToString() + ")");

            if (somenteComAlertaPendente)
            {
                sql.AppendLine("AND A.AlertarUsuario = 1");
                sql.AppendLine("AND A.DataExibicaoAlerta IS NULL");
            }
            else
            {
                sql.AppendLine("ORDER BY A.DataCriacao DESC");
            }

            return VestilloConnection.ExecSQLToListWithNewConnection<AtividadeView>(sql.ToString());
        }


        public IEnumerable<AtividadeView> ListByVendedores(int[] vendedores, bool somenteComAlertaPendente = false)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	CLI.razaosocial AS NomeCliente");
            sql.AppendLine("		,IF(VEN.ID IS NULL, UA.Nome, VEN.Nome) AS NomeVendedor");
            sql.AppendLine("       ,CLI.id as IdCliente");
            sql.AppendLine("		,UI.Nome AS NomeUsuarioCriacao");
            sql.AppendLine("		,T.Descricao AS DescricaoTipoAtividade");
            sql.AppendLine("		,UA.Nome AS NomeUsuarioAlerta");
            sql.AppendLine("		,A.*");
            sql.AppendLine("FROM 	Atividades A");
            sql.AppendLine("	INNER JOIN Usuarios     UI  ON UI.Id = A.UsuarioCriacaoAtividadeId");
            sql.AppendLine("    INNER JOIN TipoAtividade T  ON T.Id = A.TipoAtividadeId	");
            sql.AppendLine("    LEFT JOIN Usuarios      UA  ON UA.Id = A.UsuarioAlertaAtividadeId");
            sql.AppendLine("	LEFT JOIN Colaboradores CLI ON CLI.Id = A.ClienteId");
            sql.AppendLine("	LEFT JOIN Colaboradores	VEN ON IF(A.UsuarioAlertaAtividadeId IS NOT NULL, VEN.UsuarioId, VEN.Id) = IF(A.UsuarioAlertaAtividadeId IS NOT NULL, UA.Id, CLI.IdVEndedor)");
            sql.AppendLine("WHERE VEN.Id IN (" + string.Join(", ", vendedores) + ") ");


            if (somenteComAlertaPendente)
            {
                sql.AppendLine("AND A.AlertarUsuario = 1");
                sql.AppendLine("AND A.DataExibicaoAlerta IS NULL");
            }
            else
            {
                sql.AppendLine("ORDER BY A.DataCriacao DESC");
            }

            return VestilloConnection.ExecSQLToListWithNewConnection<AtividadeView>(sql.ToString());
        }


        public IEnumerable<AtividadeView> ListByVendedoresCobranca(int[] vendedores)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	CLI.razaosocial AS NomeCliente");
            sql.AppendLine("		,IF(VEN.ID IS NULL, UA.Nome, VEN.Nome) AS NomeVendedor");
            sql.AppendLine("       ,CLI.id as IdCliente");
            sql.AppendLine("		,UI.Nome AS NomeUsuarioCriacao");
            sql.AppendLine("		,T.Descricao AS DescricaoTipoAtividade");
            sql.AppendLine("		,UA.Nome AS NomeUsuarioAlerta");
            sql.AppendLine("		,A.*");
            sql.AppendLine("FROM 	Atividades A");
            sql.AppendLine("	INNER JOIN Usuarios     UI  ON UI.Id = A.UsuarioCriacaoAtividadeId");
            sql.AppendLine("    INNER JOIN TipoAtividade T  ON T.Id = A.TipoAtividadeId	");
            sql.AppendLine("    LEFT JOIN Usuarios      UA  ON UA.Id = A.UsuarioAlertaAtividadeId");
            sql.AppendLine("	LEFT JOIN Colaboradores CLI ON CLI.Id = A.ClienteId");
            sql.AppendLine("	LEFT JOIN Colaboradores	VEN ON IF(A.UsuarioAlertaAtividadeId IS NOT NULL, VEN.UsuarioId, VEN.Id) = IF(A.UsuarioAlertaAtividadeId IS NOT NULL, UA.Id, CLI.IdVEndedor)");
            sql.AppendLine("WHERE VEN.Id IN (" + string.Join(", ", vendedores) + ") AND T.id IN(11,12,13)");


            
           sql.AppendLine("ORDER BY A.DataCriacao DESC");
           

            return VestilloConnection.ExecSQLToListWithNewConnection<AtividadeView>(sql.ToString());
        }


        public IEnumerable<AtividadeView> ListAlertas(int usuarioId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	DISTINCT CLI.Nome AS NomeCliente");
            sql.AppendLine("		,VEN.Nome AS VendedorNome");
            sql.AppendLine("		,UI.Nome AS NomeUsuarioCriacao");
            sql.AppendLine("		,T.Descricao AS DescricaoTipoAtividade");
            sql.AppendLine("		,UA.Nome AS NomeUsuarioAlerta");
            sql.AppendLine("		,A.*");
            sql.AppendLine("FROM 	Atividades A");
            sql.AppendLine("	INNER JOIN Usuarios     UI  ON UI.Id = A.UsuarioCriacaoAtividadeId");
            sql.AppendLine("    INNER JOIN TipoAtividade T  ON T.Id = A.TipoAtividadeId	");
            sql.AppendLine("    LEFT JOIN Usuarios      UA  ON UA.Id = A.UsuarioAlertaAtividadeId");
            sql.AppendLine("	LEFT JOIN Colaboradores CLI ON CLI.Id = A.ClienteId");
            sql.AppendLine("	LEFT JOIN Colaboradores	VEN ON VEN.Id = A.VendedorId");
            sql.AppendLine("WHERE 	(VEN.UsuarioId = " + usuarioId.ToString() + " OR A.UsuarioAlertaAtividadeId = " + usuarioId.ToString() +")");
            sql.AppendLine("AND A.AlertarUsuario = 1");
            sql.AppendLine("AND A.DataExibicaoAlerta IS NULL");
            sql.AppendLine("ORDER BY A.DataAlerta ASC");

            return VestilloConnection.ExecSQLToListWithNewConnection<AtividadeView>(sql.ToString());
        }


        public IEnumerable<AtividadeView> ListByVendedoresAgendaDash(int[] vendedores, bool somenteComAlertaPendente = false)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	CLI.razaosocial AS NomeCliente");
            sql.AppendLine("		,IF(VEN.ID IS NULL, UA.Nome, VEN.Nome) AS NomeVendedor");
            sql.AppendLine("       ,CLI.id as IdCliente");
            sql.AppendLine("		,UI.Nome AS NomeUsuarioCriacao");
            sql.AppendLine("		,T.Descricao AS DescricaoTipoAtividade");
            sql.AppendLine("		,UA.Nome AS NomeUsuarioAlerta");
            sql.AppendLine("		,A.*");
            sql.AppendLine("FROM 	Atividades A");
            sql.AppendLine("	INNER JOIN Usuarios     UI  ON UI.Id = A.UsuarioCriacaoAtividadeId");
            sql.AppendLine("    INNER JOIN TipoAtividade T  ON T.Id = A.TipoAtividadeId	");
            sql.AppendLine("    LEFT JOIN Usuarios      UA  ON UA.Id = A.UsuarioAlertaAtividadeId");
            sql.AppendLine("	LEFT JOIN Colaboradores CLI ON CLI.Id = A.ClienteId");
            sql.AppendLine("	LEFT JOIN Colaboradores	VEN ON IF(A.UsuarioAlertaAtividadeId IS NOT NULL, VEN.UsuarioId, VEN.Id) = IF(A.UsuarioAlertaAtividadeId IS NOT NULL, UA.Id, CLI.IdVEndedor)");
            sql.AppendLine("WHERE VEN.Id IN (" + string.Join(", ", vendedores) + ") AND A.TipoAtividadeId NOT IN(1,11,12,13) ");


            if (somenteComAlertaPendente)
            {
                sql.AppendLine("AND A.AlertarUsuario = 1");
                sql.AppendLine("AND A.DataExibicaoAlerta IS NULL");
            }
            else
            {
                sql.AppendLine("ORDER BY A.DataAlerta");
            }

            return VestilloConnection.ExecSQLToListWithNewConnection<AtividadeView>(sql.ToString());
        }


        public IEnumerable<AtividadeView> ListByVendedoresCobrancaDash(int[] vendedores)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	CLI.razaosocial AS NomeCliente");
            sql.AppendLine("		,IF(VEN.ID IS NULL, UA.Nome, VEN.Nome) AS NomeVendedor");
            sql.AppendLine("       ,CLI.id as IdCliente");
            sql.AppendLine("		,UI.Nome AS NomeUsuarioCriacao");
            sql.AppendLine("		,T.Descricao AS DescricaoTipoAtividade");
            sql.AppendLine("		,UA.Nome AS NomeUsuarioAlerta");
            sql.AppendLine("		,A.*");
            sql.AppendLine("FROM 	Atividades A");
            sql.AppendLine("	INNER JOIN Usuarios     UI  ON UI.Id = A.UsuarioCriacaoAtividadeId");
            sql.AppendLine("    INNER JOIN TipoAtividade T  ON T.Id = A.TipoAtividadeId	");
            sql.AppendLine("    LEFT JOIN Usuarios      UA  ON UA.Id = A.UsuarioAlertaAtividadeId");
            sql.AppendLine("	LEFT JOIN Colaboradores CLI ON CLI.Id = A.ClienteId");
            sql.AppendLine("	LEFT JOIN Colaboradores	VEN ON IF(A.UsuarioAlertaAtividadeId IS NOT NULL, VEN.UsuarioId, VEN.Id) = IF(A.UsuarioAlertaAtividadeId IS NOT NULL, UA.Id, CLI.IdVEndedor)");
            sql.AppendLine("WHERE ( VEN.Id IN (" + string.Join(", ", vendedores) + ") OR ISNULL(A.vendedorid) ) AND T.id IN(11,12,13)");



            sql.AppendLine("ORDER BY A.DataAlerta ");


            return VestilloConnection.ExecSQLToListWithNewConnection<AtividadeView>(sql.ToString());
        }


        public IEnumerable<ListagemAtividadesView> ListByAtividadesCobranca(DateTime DataInicio, DateTime DataFim)
        {
            StringBuilder sql = new StringBuilder();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            sql.AppendLine("select colaboradores.id as IdCliente, colaboradores.referencia as RefCliente,colaboradores.nome as NomeCliente,colaboradores.DebitoAntigo, ");
            sql.AppendLine("atividades.DataCriacao as DataCriacaoAtividade,tipoatividade.Descricao as DescricaoTipoAtividade, ");
            sql.AppendLine("IF(atividades.StatusAtividade = 1,'Incluído','Concluído') as StatusAtividade, ");
            sql.AppendLine("USUCRI.Nome as UsuarioCriacaoAtividade,USUALT.Nome as UsuarioAlertaAtividade,atividades.DataAlerta, ");
            sql.AppendLine("IF(ISNULL(atividades.DataExibicaoAlerta),'Pendente','Exibido') StatusAlerta,atividades.Observacao as ObsAlerta, ");

            sql.AppendLine("contasreceber.NumTitulo,contasreceber.Parcela,contasreceber.Prefixo,contasreceber.NotaFiscal,contasreceber.DataEmissao, ");
            sql.AppendLine("contasreceber.dataVencimento,contasreceber.DataPagamento,contasreceber.ValorParcela,contasreceber.Desconto,contasreceber.Juros, ");
            sql.AppendLine("contasreceber.ValorPago,contasreceber.Saldo,contasreceber.Obs as ObsTitulo FROM atividades ");
            sql.AppendLine("LEFT JOIN contasreceber on contasreceber.Id = atividades.IdTitulo ");
            sql.AppendLine("INNER JOIN colaboradores on  colaboradores.id = atividades.ClienteId	");
            sql.AppendLine("INNER JOIN tipoatividade ON tipoatividade.Id = atividades.TipoAtividadeId ");
            sql.AppendLine("INNER JOIN usuarios USUCRI ON USUCRI.Id = atividades.UsuarioCriacaoAtividadeId ");
            sql.AppendLine("INNER JOIN usuarios USUALT ON USUALT.Id = atividades.UsuarioAlertaAtividadeId ");
            sql.AppendLine(" WHERE atividades.TipoAtividadeCliente = 2 AND SUBSTRING(atividades.DataCriacao, 1, 10) BETWEEN " + Valor + " ORDER by atividades.DataCriacao DESC");


            return VestilloConnection.ExecSQLToListWithNewConnection<ListagemAtividadesView>(sql.ToString());
        }

        public IEnumerable<ListagemItensBling> ListaItensBling(List<int> Produtos)
        {
            StringBuilder sql = new StringBuilder();

            List<ListagemItensBling> ListaTotal = new List<ListagemItensBling>();
            
            sql.AppendLine("SELECT 	id as Id,referencia as Referencia,descricao as DescricaoVestillo,DescricaoMarketPlace as NomeBling,CodigoBarrarEcommerce as EANPAI");           
            sql.AppendLine("FROM 	Produtos");            
            sql.AppendLine("WHERE Produtos.Id IN(" + string.Join(", ", Produtos) + ") ");
            sql.AppendLine("ORDER BY Produtos.Id ");

           
            var dados = VestilloConnection.ExecSQLToListWithNewConnection<ListagemItensBling>(sql.ToString());

            foreach (var itemPapai in dados)
            {
                var ListaPreliminar = new ListagemItensBling();

                ListaPreliminar.DescricaoVestillo = itemPapai.DescricaoVestillo;
                ListaPreliminar.Id = itemPapai.Id;
                ListaPreliminar.Referencia = itemPapai.Referencia;
                ListaPreliminar.NomeBling = itemPapai.NomeBling;
                ListaPreliminar.EANPAI = itemPapai.EANPAI;
                ListaTotal.Add(ListaPreliminar);

                StringBuilder sqlGrade = new StringBuilder();
                sqlGrade.AppendLine(" SELECT cores.Id as IdCor,cores.Descricao as Cor,tamanhos.Id as IdTamanho,tamanhos.Abreviatura as Tamanho, codbarras as EANFILHO from produtodetalhes ");
                sqlGrade.AppendLine("INNER JOIN cores ON cores.Id = produtodetalhes.Idcor");
                sqlGrade.AppendLine("INNER JOIN tamanhos ON tamanhos.Id = produtodetalhes.IdTamanho");
                sqlGrade.AppendLine("where produtodetalhes.IdProduto = " + itemPapai.Id + " AND produtodetalhes.Inutilizado = 0");

                var dadosGrade = VestilloConnection.ExecSQLToListWithNewConnection<ListagemItensBling>(sqlGrade.ToString());

                foreach (var itemGrade in dadosGrade)
                {
                    ListaPreliminar = new ListagemItensBling();
                    ListaPreliminar.IdCor = itemGrade.IdCor;
                    ListaPreliminar.Cor = itemGrade.Cor;
                    ListaPreliminar.IdTamanho = itemGrade.IdTamanho;
                    ListaPreliminar.Tamanho = itemGrade.Tamanho;
                    ListaPreliminar.EANFILHO = itemGrade.EANFILHO;
                    ListaTotal.Add(ListaPreliminar);
                }
            }

            return ListaTotal;
        }
    }
}
