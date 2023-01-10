using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    public class Where
    {
        private string _Campo;
        private string _Valor;
        private string _Completo;

        public Where(string pWhereCompleto)
        {
            _Completo = pWhereCompleto;
        }

        public Where(string pCampo, string pValor)
        {
            _Campo = pCampo;
            _Valor = pValor;
            _Completo = pCampo + " =  '" + pValor + "'";
        }

        public Where(string pCampo, string pSinal, int pValor)
        {
            _Campo = pCampo;
            _Valor = pValor.ToString();
            _Completo = pCampo + pSinal + pValor;
        }

        public Where(string pCampo, DateTime pData1, DateTime pData2)
        {
            _Campo = pCampo;
            _Valor = "'" + pData1.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + pData2.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            _Completo = pCampo + " BETWEEN " + _Valor;
        }

        public override string ToString()
        {
            return _Completo;
        }

    }

    public class Join
    {
        private string _TipoJoin;
        private string _Tabela;
        private string _Definicao;
        public Join(string pTipoJoin, string pTabela, string pDefinicao)
        {
            _TipoJoin = pTipoJoin; _Tabela = pTabela; _Definicao = pDefinicao;
        }
        public override string ToString()
        {
            return string.Format(" {0} JOIN {1} ON {2} ", _TipoJoin, _Tabela, _Definicao);
        }
    }

    public class Select
    {
        private string _Campos = string.Empty;
        private string _Tabela = string.Empty;
        private string _Alias = string.Empty;
        private List<Where> _Where = new List<Where>();
        private List<Join> _Joins = new List<Join>();
        private string _SQL = string.Empty;
        private string _OrderBy = string.Empty;
        private int _Limit = 0;
        private string _DistinctRow = string.Empty;
        private string _GroupBy = string.Empty;

        public Select DistinctRow()
        {
            _DistinctRow = " DISTINCT ";

            return this;
        }

        public Select Limit(int pLimit)
        {
            _Limit = pLimit;

            return this;
        }

        public Select OrderBy(string pOrderBy)
        {
            if (_OrderBy.Equals(string.Empty))
                _OrderBy = pOrderBy;
            else
                _OrderBy += ", " + pOrderBy;

            return this;
        }

        public Select From(string pTabela)
        {
            _Tabela = pTabela;

            return this;
        }

        public Select GroupBy(string pGroupBy)
        {
            _GroupBy = pGroupBy;

            return this;
        }

        public Select From(string pTabela, string pAlias)
        {
            _Tabela = pTabela;
            _Alias = pAlias;

            return this;
        }

        public Select() { }

        public Select(string pCampos)
        {
            _Campos = pCampos;
        }

        public Select(string pCampos, string pTabela)
        {
            _Tabela = pTabela;
            _Campos = pCampos;
        }

        public Select(string pCampos, string pTabela, string pWhere)
        {
            _Campos = pCampos;
            _Tabela = pTabela;
            _Where.Add(new Where(pWhere));
        }

        public Select(string pCampos, string pTabela, string pWhere, string pOrderBy)
        {
            _Campos = pCampos;
            _Tabela = pTabela;
            _Where.Add(new Where(pWhere));
            _OrderBy = pOrderBy;
        }

        public Select LeftJoin(string pTabela, string pDefinicao)
        {
            _Joins.Add(new Join("LEFT", pTabela, pDefinicao));

            return this;
        }

        public Select InnerJoin(string pTabela, string pDefinicao)
        {
            _Joins.Add(new Join("INNER", pTabela, pDefinicao));

            return this;
        }

        public Select RightJoin(string pTabela, string pDefinicao)
        {
            _Joins.Add(new Join("RIGHT", pTabela, pDefinicao));

            return this;
        }

        public Select FullJoin(string pTabela, string pDefinicao)
        {
            _Joins.Add(new Join("FULL", pTabela, pDefinicao));

            return this;
        }

        public Select WhereBooleanTrue(bool pVerificacao, string pCampo)
        {
            if (pVerificacao)
                return Where(pCampo + " = 1");
            else
                return this;
        }

        public Select WhereValorMaiorOuIgualAZero(int pValor, string pCampo)
        {
            if (pValor >= 0)
            {
                if (pCampo.IndexOf(".") == -1)
                    return Where(pCampo + " = @" + pCampo);
                else
                {
                    string CampoSemPonto = pCampo.Substring(pCampo.IndexOf(".") + 1);
                    return Where(pCampo + " = @" + CampoSemPonto);
                }

            }
            else
                return this;
        }

        public Select WhereValorMaiorZero(int pValor, string pCampo, string pParametro)
        {
            if (pValor > 0)
                return Where(pCampo + " = @" + pParametro);
            else
                return this;
        }

        public Select WhereLike(string pValor, string pCampo, string pParametro)
        {
            if (!string.IsNullOrEmpty(pValor))

                return Where(pCampo + " LIKE @" + pParametro);
            else
                return this;
        }

        public Select WhereLike(string pValor, string pCampo)
        {
            if (!string.IsNullOrEmpty(pValor))
            {
                if (pCampo.IndexOf(".") == -1)
                    return Where(pCampo + " LIKE @" + pCampo);
                else
                {
                    string CampoSemPonto = pCampo.Substring(pCampo.IndexOf(".") + 1);
                    return Where(pCampo + " LIKE @" + CampoSemPonto);
                }
            }
            else
                return this;
        }

        public Select WhereFaixaValores(decimal pValorInicial, decimal pValorFinal, string pCampo, string pParametroDe, string pParametroAte)
        {
            if ((pValorFinal > 0) && (pValorFinal > pValorInicial))
                return Where(pCampo + " BETWEEN @" + pParametroDe + " AND @" + pParametroAte);
            else
                return this;
        }

        public Select WhereValorMaiorZero(int pValor, string pCampo)
        {
            if (pValor > 0)
            {
                if (pCampo.IndexOf(".") == -1)
                    return Where(pCampo + " = @" + pCampo);
                else
                {
                    string CampoSemPonto = pCampo.Substring(pCampo.IndexOf(".") + 1);
                    return Where(pCampo + " = @" + CampoSemPonto);
                }

            }
            else
                return this;
        }

        /// <summary>
        /// Monta o WHERE com uma lista de itens passados.  Não faz SELECT IN pois é mais lento. Faz com OR.
        /// </summary>
        /// <param name="pCampo"></param>
        /// <param name="pLista"></param>
        /// <returns></returns>
        public Select Where(string pCampo, List<int> pLista)
        {
            if (pLista.Count == 0)
                return this;

            string Str = "(";

            foreach (var Item in pLista)
            {
                if (pLista.IndexOf(Item) == pLista.Count() - 1)
                    Str += pCampo + " = " + Item.ToString();
                else
                    Str += pCampo + " = " + Item.ToString() + " OR ";
            }

            Str += ")";

            _Where.Add(new Where(Str));

            return this;
        }

        public Select Where(string pValor)
        {
            if (string.IsNullOrEmpty(pValor))
                return this;

            _Where.Add(new Where(pValor));

            return this;
        }

        public Select Where(string pCampo, int pValor)
        {
            _Where.Add(new Where(pCampo, pValor.ToString()));

            return this;
        }

        public Select Where(string pCampo, string pSinal, int pValor)
        {
            _Where.Add(new Where(pCampo, pSinal, pValor));

            return this;
        }

        public Select Where(string pCampo, DateTime pData1, DateTime pData2)
        {
            _Where.Add(new Where(pCampo, pData1, pData2));

            return this;
        }

        public Select Campos()
        {
            _Campos = "*";

            return this;
        }

        public Select Campos(string pCampos)
        {
            _Campos = pCampos;

            return this;
        }

        public Select Campos(string pCampos, string pTabela)
        {
            _Campos = pCampos;
            _Tabela = pTabela;

            return this;
        }

        private string GerarSQL()
        {
            string SQL = "SELECT ";

            if (_Limit > 0)
                SQL += " TOP " + _Limit.ToString();

            SQL += " " + _DistinctRow + " " + _Campos + " FROM " + _Tabela;

            if (!string.IsNullOrEmpty(_Alias))
                SQL += " AS " + _Alias;

            SQL += "  ";

            foreach (Join J in _Joins)
                SQL += J.ToString();

            if (_Where.Count > 0)
            {
                SQL += " WHERE ";

                int ContWhere = 0;

                foreach (Where W in _Where)
                {
                    if (ContWhere == _Where.Count - 1) // ÚLTIMO
                        SQL += W.ToString();
                    else
                        SQL += W.ToString() + " AND ";

                    ContWhere++;
                }
            }

            if (_GroupBy.Length > 0)
                SQL += " GROUP BY " + _GroupBy;

            if (_OrderBy.Length > 0)
                SQL += " ORDER BY " + _OrderBy;

            return SQL;
        }

        public override string ToString()
        {
            return GerarSQL();
        }

        public string ToStringUnionAll(Select pSQL2)
        {
            string SQL1 = GerarSQL();
            string SQL2 = pSQL2.GerarSQL();

            return SQL1 + " UNION ALL " + SQL2;
        }

        public string ToStringUnionAll(Select pSQL2, Select pSQL3)
        {
            string SQL1 = GerarSQL();
            string SQL2 = pSQL2.GerarSQL();
            string SQL3 = pSQL3.GerarSQL();

            return SQL1 + " UNION ALL " + SQL2 + " UNION ALL " + SQL3;
        }

        public string ToStringUnionAll(Select pSQL2, Select pSQL3, Select pSQL4)
        {
            string SQL1 = GerarSQL();
            string SQL2 = pSQL2.GerarSQL();
            string SQL3 = pSQL3.GerarSQL();
            string SQL4 = pSQL4.GerarSQL();

            return SQL1 + " UNION ALL " + SQL2 + " UNION ALL " + SQL3 + " UNION ALL " + SQL4;
        }
    }
}
