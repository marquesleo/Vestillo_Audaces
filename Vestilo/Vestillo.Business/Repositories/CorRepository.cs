using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;


namespace Vestillo.Business.Repositories
{
    public class CorRepository: GenericRepository<Cor>
    {
        public CorRepository() : base(new DapperConnection<Cor>())
        {
        }

        public IEnumerable<Cor> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Ativo ")
                .From("cores ")
                .Where(" Ativo = " + AtivoInativo )
                .OrderBy(" Descricao");

            var cr = new Cor ();
            return _cn.ExecuteStringSqlToList(cr, SQL.ToString());
        }

        public IEnumerable<Cor> GetListPorDescricao(string Descricao)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Ativo ")
                .From("cores ")
                .Where(" descricao like '%" + Descricao + "%' And ativo = 1");

            var cr = new Cor();
            return _cn.ExecuteStringSqlToList(cr, SQL.ToString());
        }

        public IEnumerable<Cor> GetCoresProduto(int produto)
        {
            var SQL = new Select()
                .Campos("c.Id,c.Abreviatura,c.Descricao,c.Ativo ")
                .From("cores c")
                .InnerJoin(" produtodetalhes pd ", " pd.idcor = c.id")
                .Where(" pd.idproduto = " + produto + " and pd.Inutilizado = 0")
                .GroupBy(" c.id");

            var cr = new Cor();
            return _cn.ExecuteStringSqlToList(cr, SQL.ToString());
        }

        public IEnumerable<Cor> GetCoresByProdutoTamanho(int produto, int tamanho)
        {
            var SQL = new Select()
                .Campos("c.Id,c.Abreviatura,c.Descricao,c.Ativo ")
                .From("cores c")
                .InnerJoin(" produtodetalhes pd ", " pd.idcor = c.id")
                .Where(" pd.idproduto = " + produto + " and pd.idtamanho = " + tamanho + " and pd.Inutilizado = 0")
                .GroupBy(" c.id");

            var cr = new Cor();
            return _cn.ExecuteStringSqlToList(cr, SQL.ToString());
        }
        public int RetornaIdCor(string NomeCor)
        {
            int IdCor = 0;
            string Sql = String.Empty;

            Sql = "select id from cores where cores.Descricao = " + "'" + NomeCor + "'";
            var cr = new Cor();
            var Dados =  _cn.ExecuteStringSqlToList(cr, Sql).ToList();

            if(Dados != null && Dados.Count() > 0)
            {
                IdCor = Dados[0].Id;
            }
            return IdCor;
        }

        public int RetornaIdCorAbrev(string Abrev)
        {
            int IdCor = 0;
            string Sql = String.Empty;

            Sql = "select id from cores where cores.Abreviatura = " + "'" + Abrev + "'";
            var cr = new Cor();
            var Dados = _cn.ExecuteStringSqlToList(cr, Sql).ToList();

            if (Dados != null && Dados.Count() > 0)
            {
                IdCor = Dados[0].Id;
            }
            return IdCor;
        }
    }
}
