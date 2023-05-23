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
    public class ProdutoFornecedorPrecoRepository : GenericRepository<ProdutoFornecedorPreco>
    {
        public ProdutoFornecedorPrecoRepository()
            : base(new DapperConnection<ProdutoFornecedorPreco>())
        {
        }

        public ProdutoFornecedorPreco GetByProduto(int ProdutoId)
        {
            var ProdutoFornecedor = new ProdutoFornecedorPreco();
            _cn.ExecuteToModel("IdProduto = " + ProdutoId.ToString(), ref ProdutoFornecedor);
            return ProdutoFornecedor;
        }

        public IEnumerable<ProdutoFornecedorPreco> GetListByProdutoFornecedor(int ProdutoId)
        {
            var SQL = new Select()
                .Campos("produtofornecedorprecos.*, cores.abreviatura AS AbvCor, cores.descricao AS DescCor, " +
                        "tamanhos.abreviatura AS AbvTamanho, tamanhos.descricao AS DescTamanho,colaboradores.referencia, " +
                        "colaboradores.nome")
                .From("produtofornecedorprecos")
                .LeftJoin("cores", "cores.Id = produtofornecedorprecos.idCor")
                .LeftJoin("tamanhos", "tamanhos.Id = produtofornecedorprecos.IdTamanho")
                .InnerJoin("colaboradores", "colaboradores.id = produtofornecedorprecos.IdFornecedor")
                .Where("IdPRoduto =  " + ProdutoId);

            var p = new ProdutoFornecedorPreco();
            return _cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public ProdutoFornecedorPrecoView GetByProdutoView(int ProdutoId)
        {
            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var FornecedorProduto = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel("IdProduto = " + ProdutoId.ToString(), ref FornecedorProduto);
            return FornecedorProduto;
        }

        public IEnumerable<ProdutoFornecedorPrecoView> GetListByFornecedor(int ProdutoId)
        {
            var SQL = new Select()
                .Campos("produtofornecedorprecos.*, cores.abreviatura AS AbvCor, cores.descricao AS NomeCor, " +
                        "tamanhos.abreviatura AS AbvTamanho, tamanhos.descricao AS NomeTamanho,colaboradores.referencia as RefFornecedor, " +
                        "colaboradores.nome as NomeFornecedor")
                .From("produtofornecedorprecos")
                .LeftJoin("cores", "cores.Id = produtofornecedorprecos.idCor")
                .LeftJoin("tamanhos", "tamanhos.Id = produtofornecedorprecos.IdTamanho")
                .InnerJoin("colaboradores", "colaboradores.id = produtofornecedorprecos.IdFornecedor")
                .Where("IdPRoduto IN (" + ProdutoId + ")");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<ProdutoFornecedorPrecoView> GetListByFornecedorFicha(int ProdutoId)
        {
            var SQL = new Select()
                .Campos("avg(NULLIF(produtofornecedorprecos.PrecoFornecedor,0)) as PrecoFornecedor, avg(NULLIF(produtofornecedorprecos.PrecoFornecedor, 0)) as MediaPrecoFornecedor, avg(NULLIF(produtofornecedorprecos.PrecoCor, 0)) as PrecoCor, avg(NULLIF(produtofornecedorprecos.PrecoTamanho, 0)) as PrecoTamanho, cores.abreviatura AS AbvCor, cores.descricao AS NomeCor, " +
                        "tamanhos.abreviatura AS AbvTamanho, tamanhos.descricao AS NomeTamanho,colaboradores.Id as IdFornecedor, colaboradores.referencia as RefFornecedor, " +
                        "colaboradores.nome as NomeFornecedor")
                .From("produtofornecedorprecos")
                .LeftJoin("cores", "cores.Id = produtofornecedorprecos.idCor")
                .LeftJoin("tamanhos", "tamanhos.Id = produtofornecedorprecos.IdTamanho")
                .InnerJoin("colaboradores", "colaboradores.id = produtofornecedorprecos.IdFornecedor")
                .Where("IdProduto = " + ProdutoId )
                .GroupBy("IdFornecedor");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<Colaborador> GetFornecedores(int ProdutoId)
        {
            var SQL = new Select()
                .Campos("colaboradores.*")
                .From("produtofornecedorprecos")
                .InnerJoin("colaboradores", "colaboradores.id = produtofornecedorprecos.IdFornecedor")
                .Where("IdProduto = " + ProdutoId)
                .GroupBy("IdFornecedor");

            var cn = new DapperConnection<Colaborador>();
            var p = new Colaborador();
            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public decimal GetCusto(int ProdutoId, int CorId, int TamanhoId)
        {
            var SQL = new Select()
                .Campos("produtofornecedorprecos.*, produtos.TipoCalculoPreco, produtos.TipoCustoFornecedor")
                .From("produtofornecedorprecos")
                .LeftJoin("cores", "cores.Id = produtofornecedorprecos.idCor")
                .LeftJoin("tamanhos", "tamanhos.Id = produtofornecedorprecos.IdTamanho")
                .InnerJoin("produtos", "produtos.id = produtofornecedorprecos.IdProduto")
                .InnerJoin("produtodetalhes ", "produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " and produtofornecedorprecos.idCor = " + CorId + " and produtofornecedorprecos.IdTamanho = " + TamanhoId + " AND produtodetalhes.Inutilizado = 0");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
           var ret = cn.ExecuteStringSqlToList(p, SQL.ToString());
           decimal resultado = 0;
           if (ret != null && ret.Count() > 0)
           {
               int tipoCaluloCusto = VestilloSession.TipoCalculoCustoFornecedor;

               if (ret.FirstOrDefault().TipoCalculoPreco != null && ret.FirstOrDefault().TipoCalculoPreco != 0)
                   tipoCaluloCusto = int.Parse(ret.FirstOrDefault().TipoCalculoPreco.ToString());
               
               //#ALEX
               if (tipoCaluloCusto == 2) //Pega a media
               {
                   if (ret.FirstOrDefault().TipoCustoFornecedor == 2)// Cor
                   {
                       int count = ret.Where(x => x.PrecoCor > 0).ToList().Count();
                       if (count == 0) count = 1;
                       resultado = (ret.Sum(x => x.PrecoCor) / count);
                   }
                   else if (ret.FirstOrDefault().TipoCustoFornecedor == 3)// Tamanho
                   {
                       int count = ret.Where(x => x.PrecoTamanho > 0).ToList().Count();
                       if (count == 0) count = 1;
                       resultado = (ret.Sum(x => x.PrecoTamanho) / count);
                   }
                   else // Fornecedor
                   {
                       int count = ret.Where(x => x.PrecoFornecedor > 0).ToList().Count();
                       if (count == 0) count = 1;
                       resultado = (ret.Sum(x => x.PrecoFornecedor) / count);
                   }
               }
               else // Pega  o maior valor
               {
                   if (ret.FirstOrDefault().TipoCustoFornecedor == 2)// Cor
                       resultado = ret.Max(x => x.PrecoCor);
                   else if (ret.FirstOrDefault().TipoCustoFornecedor == 3)// Tamanho
                       resultado = ret.Max(x => x.PrecoTamanho);
                   else
                       resultado = ret.Max(x => x.PrecoFornecedor);
               }
           }
                return resultado;
        }

        public decimal GetCusto(int ProdutoId, int? tipoCaluloCusto, int IdProduto, short sequencia)
        {
            //AQUI
            var SQL = new Select()
                .Campos("produtofornecedorprecos.*, produtos.TipoCalculoPreco, produtos.TipoCustoFornecedor")
                .From("produtofornecedorprecos")
                .LeftJoin("cores", "cores.Id = produtofornecedorprecos.idCor")
                .LeftJoin("tamanhos", "tamanhos.Id = produtofornecedorprecos.IdTamanho")
                .InnerJoin("produtos", "produtos.id = produtofornecedorprecos.IdProduto")
                .InnerJoin("produtodetalhes ", "produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND produtodetalhes.Inutilizado = 0");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            var ret = cn.ExecuteStringSqlToList(p, SQL.ToString());

            string SQlFicha = " SELECT * from fichatecnicadomaterial " +
                              " INNER JOIN fichatecnicadomaterialitem ON fichatecnicadomaterialitem.fichatecnicaId = fichatecnicadomaterial.id " +
                              " WHERE fichatecnicadomaterial.ProdutoId = " + IdProduto + " AND fichatecnicadomaterialitem.materiaprimaId = " + ProdutoId + " AND fichatecnicadomaterialitem.sequencia =  " + sequencia;

            var cnFicha = new DapperConnection<FichaTecnicaDoMaterialItem>();
            var fc = new FichaTecnicaDoMaterialItem();
            var DadosFicha = cnFicha.ExecuteStringSqlToList(fc, SQlFicha);

            int idFornecedorDaFicha = 0;
            foreach (var itemMaterial in DadosFicha)
            {
                idFornecedorDaFicha = itemMaterial.idFornecedor;
            }

            decimal resultado = 0;
            if (ret != null && ret.Count() > 0)
            {
                if(tipoCaluloCusto == null)
                {
                    tipoCaluloCusto = VestilloSession.TipoCalculoCustoFornecedor;

                    if (ret.FirstOrDefault().TipoCalculoPreco != null && ret.FirstOrDefault().TipoCalculoPreco != 0)
                        tipoCaluloCusto = int.Parse(ret.FirstOrDefault().TipoCalculoPreco.ToString());
                }



                //#ALEX
                if (idFornecedorDaFicha > 0 && VestilloSession.AtualizaPrecoFichaPorFornecedor)
                {
                    if (tipoCaluloCusto == 2) //Pega a media
                    {
                        if (ret.FirstOrDefault().TipoCustoFornecedor == 2)// Cor
                        {
                            int ContCoresMedia = 0;
                            decimal PrecoCorMedia = 0;
                            foreach (var PrecoCorMediaFc in ret)
                            {
                                if (PrecoCorMediaFc.IdFornecedor == idFornecedorDaFicha && PrecoCorMediaFc.PrecoCor > 0)
                                {
                                    PrecoCorMedia += PrecoCorMediaFc.PrecoCor;
                                    ContCoresMedia += 1;
                                }
                            }
                            if (ContCoresMedia == 0) ContCoresMedia = 1;
                            resultado = PrecoCorMedia / ContCoresMedia;
                        }
                        else if (ret.FirstOrDefault().TipoCustoFornecedor == 3)// Tamanho
                        {
                            int ContTamanMedia = 0;
                            decimal PrecoTamanMedia = 0;
                            foreach (var PrecoTamanMediaFc in ret)
                            {
                                if (PrecoTamanMediaFc.IdFornecedor == idFornecedorDaFicha && PrecoTamanMediaFc.PrecoTamanho > 0)
                                {
                                    PrecoTamanMedia += PrecoTamanMediaFc.PrecoTamanho;
                                    ContTamanMedia += 1;
                                }
                            }
                            if (ContTamanMedia == 0) ContTamanMedia = 1;
                            resultado = PrecoTamanMedia / ContTamanMedia;
                        }
                        else // Fornecedor
                        {
                            int ContFornecMedia = 0;
                            decimal PrecoFornecMedia = 0;
                            foreach (var PrecoFornecMediaFc in ret)
                            {
                                if (PrecoFornecMediaFc.IdFornecedor == idFornecedorDaFicha && PrecoFornecMediaFc.PrecoFornecedor > 0)
                                {
                                    PrecoFornecMedia += PrecoFornecMediaFc.PrecoFornecedor;
                                    ContFornecMedia += 1;
                                }
                            }
                            if (ContFornecMedia == 0) ContFornecMedia = 1;
                            resultado = PrecoFornecMedia / ContFornecMedia;
                        }
                    }
                    else // Pega  o maior valor
                    {
                        if (ret.FirstOrDefault().TipoCustoFornecedor == 2)// Cor        
                        {
                            decimal PrecoCorMaior = 0;
                            foreach (var PrecoCorMaiorFc in ret)
                            {
                                if (PrecoCorMaiorFc.IdFornecedor == idFornecedorDaFicha)
                                {
                                    if (PrecoCorMaiorFc.PrecoCor > PrecoCorMaior)
                                    {
                                        PrecoCorMaior = PrecoCorMaiorFc.PrecoCor;
                                    }
                                }
                            }
                            resultado = PrecoCorMaior;
                        }

                        else if (ret.FirstOrDefault().TipoCustoFornecedor == 3)// Tamanho
                        {
                            decimal PrecoTamanMaior = 0;
                            foreach (var PrecoTamanMaiorFc in ret)
                            {
                                if (PrecoTamanMaiorFc.IdFornecedor == idFornecedorDaFicha)
                                {
                                    if (PrecoTamanMaiorFc.PrecoTamanho > PrecoTamanMaior)
                                    {
                                        PrecoTamanMaior = PrecoTamanMaiorFc.PrecoTamanho;
                                    }
                                }
                            }
                            resultado = PrecoTamanMaior;
                        }
                        else
                        {
                            decimal PrecoFornecMaior = 0;
                            foreach (var PrecoFornecMaiorFc in ret)
                            {
                                if (PrecoFornecMaiorFc.IdFornecedor == idFornecedorDaFicha)
                                {
                                    if (PrecoFornecMaiorFc.PrecoFornecedor > PrecoFornecMaior)
                                    {
                                        PrecoFornecMaior = PrecoFornecMaiorFc.PrecoFornecedor;
                                    }
                                }
                            }
                            resultado = PrecoFornecMaior;
                        }

                    }
                }
                else
                {
                    if (tipoCaluloCusto == 2) //Pega a media
                    {
                        if (ret.FirstOrDefault().TipoCustoFornecedor == 2)// Cor
                        {
                            int count = ret.Where(x => x.PrecoCor > 0).ToList().Count();
                            if (count == 0) count = 1;
                            resultado = (ret.Sum(x => x.PrecoCor) / count);
                        }
                        else if (ret.FirstOrDefault().TipoCustoFornecedor == 3)// Tamanho
                        {
                            int count = ret.Where(x => x.PrecoTamanho > 0).ToList().Count();
                            if (count == 0) count = 1;
                            resultado = (ret.Sum(x => x.PrecoTamanho) / count);
                        }
                        else // Fornecedor
                        {
                            int count = ret.Where(x => x.PrecoFornecedor > 0).ToList().Count();
                            if (count == 0) count = 1;
                            resultado = (ret.Sum(x => x.PrecoFornecedor) / count);
                        }
                    }
                    else // Pega  o maior valor
                    {
                        if (ret.FirstOrDefault().TipoCustoFornecedor == 2)// Cor
                            resultado = ret.Max(x => x.PrecoCor);
                        else if (ret.FirstOrDefault().TipoCustoFornecedor == 3)// Tamanho
                            resultado = ret.Max(x => x.PrecoTamanho);
                        else
                            resultado = ret.Max(x => x.PrecoFornecedor);
                    }
                }

                
            }
            return resultado;
        }

        public decimal GetCustoMaior(int ProdutoId)
        {
            var SQL = new Select()
                .Campos("MAX(produtofornecedorprecos.PrecoFornecedor) AS PrecoFornecedor")
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND IFNULL( ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho),0) = 0");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }

        public decimal GetCustoMedio(int ProdutoId)
        {
            var SQL = new Select()
                .Campos("AVG(NULLIF(produtofornecedorprecos.PrecoFornecedor, 0)) AS PrecoFornecedor")
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND IFNULL( ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho), 0) = 0");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }

        public decimal GetCustoMaiorCor(int ProdutoId) 
        {
            var SQL = new Select()
                .Campos("MAX(produtofornecedorprecos.PrecoCor) AS PrecoFornecedor") // pega maior custo cor, mas mantém o nome por causa do grid único
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho) = 0");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }

        public decimal GetCustoMedioCor(int ProdutoId)
        {
            var SQL = new Select()
                .Campos("AVG(NULLIF(produtofornecedorprecos.PrecoCor, 0)) AS PrecoFornecedor") // pega média custo cor, mas mantém o nome por causa do grid único
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho) = 0");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }


        public decimal GetCustoMaiorTamanho(int ProdutoId)
        {
            var SQL = new Select()
                .Campos("MAX(produtofornecedorprecos.PrecoTamanho) AS PrecoFornecedor") // pega maior custo Tamanho, mas mantém o nome por causa do grid único
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho) = 0");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }

        public decimal GetCustoMedioTamanho(int ProdutoId)
        {
            var SQL = new Select()
                .Campos("AVG(NULLIF(produtofornecedorprecos.PrecoTamanho, 0)) AS PrecoFornecedor") // pega média custo Tamanho, mas mantém o nome por causa do grid único
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho) = 0");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }

        public decimal GetCustoFornecedor(int ProdutoId, int FornecedorId)
        {
            var SQL = new Select()
                .Campos("produtofornecedorprecos.*, produtos.TipoCalculoPreco, produtos.TipoCustoFornecedor")
                .From("produtofornecedorprecos")
                .LeftJoin("cores", "cores.Id = produtofornecedorprecos.idCor")
                .LeftJoin("tamanhos", "tamanhos.Id = produtofornecedorprecos.IdTamanho")
                .InnerJoin("produtos", "produtos.id = produtofornecedorprecos.IdProduto")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND produtofornecedorprecos.IdFornecedor = " + FornecedorId  +
                       " AND IFNULL( ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                     + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho), 0) = 0");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            var ret = cn.ExecuteStringSqlToList(p, SQL.ToString());
            decimal resultado = 0;
            if (ret != null && ret.Count() > 0)
                resultado = ret.Max(x => x.PrecoFornecedor);
            return resultado;
        }


        public IEnumerable<ProdutoFornecedorPreco> GetValoresSemInativos(int ProdutoId)
        {
            var SQL = new Select()
                .Campos("produtofornecedorprecos.Id,produtofornecedorprecos.IdProduto,produtofornecedorprecos.IdFornecedor,produtofornecedorprecos.PrecoFornecedor,produtofornecedorprecos.IdCor,produtofornecedorprecos.IdTamanho,produtofornecedorprecos.PrecoCor,produtofornecedorprecos.PrecoTamanho")
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND  ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho) = 0");
                

            var cn = new DapperConnection<ProdutoFornecedorPreco>();
            var p = new ProdutoFornecedorPreco();
            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<ProdutoFornecedorPreco> GetValoresByExcecaoFicha(int ProdutoId, int FichaItemId)
        {
            var SQL = new Select()
                .Campos("produtofornecedorprecos.Id,produtofornecedorprecos.IdProduto,produtofornecedorprecos.IdFornecedor,produtofornecedorprecos.PrecoFornecedor,produtofornecedorprecos.IdCor,produtofornecedorprecos.IdTamanho,produtofornecedorprecos.PrecoCor,produtofornecedorprecos.PrecoTamanho")
                .From("produtofornecedorprecos")
                .InnerJoin("fichatecnicadomaterialrelacao", "fichatecnicadomaterialrelacao.materiaprimaId = produtofornecedorprecos.IdProduto AND fichatecnicadomaterialrelacao.fichatecnicaitemId = " + FichaItemId.ToString() + 
                            " AND fichatecnicadomaterialrelacao.cor_materiaprima_Id = produtofornecedorprecos.IdCor AND fichatecnicadomaterialrelacao.tamanho_materiaprima_Id = produtofornecedorprecos.IdTamanho ")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND  ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho) = 0")
                .GroupBy("produtofornecedorprecos.Id");


            var cn = new DapperConnection<ProdutoFornecedorPreco>();
            var p = new ProdutoFornecedorPreco();
            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public decimal GetCustoMaiorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            var SQL = new Select()
                .Campos("MAX(produtofornecedorprecos.PrecoFornecedor) AS PrecoFornecedor")
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND IFNULL( ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho),0) = 0");
            if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.idCor IN (" + string.Join(", ", idCor) + ") ");

            if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.IdTamanho IN(" + string.Join(", ", idTamanho) + ")");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }

        public decimal GetCustoMedioByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            var SQL = new Select()
                .Campos("AVG(NULLIF(produtofornecedorprecos.PrecoFornecedor, 0)) AS PrecoFornecedor")
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND IFNULL( ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho), 0) = 0");
             if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.idCor IN (" + string.Join(", ", idCor) + ") ");

             if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.IdTamanho IN(" + string.Join(", ", idTamanho) + ")");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }

        public decimal GetCustoMaiorCorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            var SQL = new Select()
                .Campos("MAX(produtofornecedorprecos.PrecoCor) AS PrecoFornecedor") // pega maior custo cor, mas mantém o nome por causa do grid único
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho) = 0");
            if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.idCor IN (" + string.Join(", ", idCor) + ") ");

            if (idCor != null && idCor.Count > 0)
                SQL
                .Where("produtofornecedorprecos.IdTamanho IN(" + string.Join(", ", idTamanho) + ")");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }

        public decimal GetCustoMedioCorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            var SQL = new Select()
                .Campos("AVG(NULLIF(produtofornecedorprecos.PrecoCor, 0)) AS PrecoFornecedor") // pega média custo cor, mas mantém o nome por causa do grid único
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho) = 0");
            if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.idCor IN (" + string.Join(", ", idCor) + ") ");

            if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.IdTamanho IN(" + string.Join(", ", idTamanho) + ")");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }


        public decimal GetCustoMaiorTamanhoByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            var SQL = new Select()
                .Campos("MAX(produtofornecedorprecos.PrecoTamanho) AS PrecoFornecedor") // pega maior custo Tamanho, mas mantém o nome por causa do grid único
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho) = 0");
            if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.idCor IN (" + string.Join(", ", idCor) + ") ");

            if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.IdTamanho IN(" + string.Join(", ", idTamanho) + ")");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }

        public decimal GetCustoMedioTamanhoByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            var SQL = new Select()
                .Campos("AVG(NULLIF(produtofornecedorprecos.PrecoTamanho, 0)) AS PrecoFornecedor") // pega média custo Tamanho, mas mantém o nome por causa do grid único
                .From("produtofornecedorprecos")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND ( SELECT produtodetalhes.Inutilizado from produtodetalhes WHERE produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto "
                        + " AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho) = 0");
            if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.idCor IN (" + string.Join(", ", idCor) + ") ");

            if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.IdTamanho IN(" + string.Join(", ", idTamanho) + ")");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            cn.ExecuteToModel(ref p, SQL.ToString());

            return p.PrecoFornecedor;
        }

        public decimal GetCustoByExcecao(int ProdutoId, int? tipoCaluloCusto, List<int> idCor, List<int> idTamanho, int IdProduto,short sequencia)
        {
            //AQUI TAMBÉM
            var SQL = new Select()
                .Campos("produtofornecedorprecos.*, produtos.TipoCalculoPreco, produtos.TipoCustoFornecedor")
                .From("produtofornecedorprecos")
                .LeftJoin("cores", "cores.Id = produtofornecedorprecos.idCor")
                .LeftJoin("tamanhos", "tamanhos.Id = produtofornecedorprecos.IdTamanho")
                .InnerJoin("produtos", "produtos.id = produtofornecedorprecos.IdProduto")
                .InnerJoin("produtodetalhes ", "produtodetalhes.IdProduto = produtofornecedorprecos.IdProduto AND produtodetalhes.Idcor = produtofornecedorprecos.IdCor AND produtodetalhes.IdTamanho = produtofornecedorprecos.IdTamanho")
                .Where("produtofornecedorprecos.IdProduto = " + ProdutoId + " AND produtodetalhes.Inutilizado = 0");
            if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.idCor IN (" + string.Join(", ", idCor) + ") ");

            if (idCor != null && idCor.Count > 0)
                SQL.Where("produtofornecedorprecos.IdTamanho IN(" + string.Join(", ", idTamanho) + ")");

            var cn = new DapperConnection<ProdutoFornecedorPrecoView>();
            var p = new ProdutoFornecedorPrecoView();
            var ret = cn.ExecuteStringSqlToList(p, SQL.ToString());

            string SQlFicha = " SELECT * from fichatecnicadomaterial " +
                              " INNER JOIN fichatecnicadomaterialitem ON fichatecnicadomaterialitem.fichatecnicaId = fichatecnicadomaterial.id " +
                              " WHERE fichatecnicadomaterial.ProdutoId = " + IdProduto + " AND fichatecnicadomaterialitem.materiaprimaId = " + ProdutoId + " AND fichatecnicadomaterialitem.sequencia =  " + sequencia;

            var cnFicha = new DapperConnection<FichaTecnicaDoMaterialItem>();
            var fc = new FichaTecnicaDoMaterialItem();
            var DadosFicha = cnFicha.ExecuteStringSqlToList(fc, SQlFicha);

            int idFornecedorDaFicha = 0;
            foreach (var itemMaterial in DadosFicha)
            {
                idFornecedorDaFicha = itemMaterial.idFornecedor;
            }

            decimal resultado = 0;
            if (ret != null && ret.Count() > 0)
            {
                if (tipoCaluloCusto == null)
                {
                    tipoCaluloCusto = VestilloSession.TipoCalculoCustoFornecedor;

                    if (ret.FirstOrDefault().TipoCalculoPreco != null && ret.FirstOrDefault().TipoCalculoPreco != 0)
                        tipoCaluloCusto = int.Parse(ret.FirstOrDefault().TipoCalculoPreco.ToString());
                }



                //#ALEX
                if (idFornecedorDaFicha > 0 && VestilloSession.AtualizaPrecoFichaPorFornecedor)
                {
                    if (tipoCaluloCusto == 2) //Pega a media
                    {
                        if (ret.FirstOrDefault().TipoCustoFornecedor == 2)// Cor
                        {
                            int ContCoresMedia = 0;
                            decimal PrecoCorMedia = 0;
                            foreach (var PrecoCorMediaFc in ret)
                            {
                                if (PrecoCorMediaFc.IdFornecedor == idFornecedorDaFicha && PrecoCorMediaFc.PrecoCor > 0)
                                {
                                    PrecoCorMedia += PrecoCorMediaFc.PrecoCor;
                                    ContCoresMedia += 1;
                                }
                            }
                            if (ContCoresMedia == 0) ContCoresMedia = 1;
                            resultado = PrecoCorMedia / ContCoresMedia;
                        }
                        else if (ret.FirstOrDefault().TipoCustoFornecedor == 3)// Tamanho
                        {
                            int ContTamanMedia = 0;
                            decimal PrecoTamanMedia = 0;
                            foreach (var PrecoTamanMediaFc in ret)
                            {
                                if (PrecoTamanMediaFc.IdFornecedor == idFornecedorDaFicha && PrecoTamanMediaFc.PrecoTamanho > 0)
                                {
                                    PrecoTamanMedia += PrecoTamanMediaFc.PrecoTamanho;
                                    ContTamanMedia += 1;
                                }
                            }
                            if (ContTamanMedia == 0) ContTamanMedia = 1;
                            resultado = PrecoTamanMedia / ContTamanMedia;
                        }
                        else // Fornecedor
                        {
                            int ContFornecMedia = 0;
                            decimal PrecoFornecMedia = 0;
                            foreach (var PrecoFornecMediaFc in ret)
                            {
                                if (PrecoFornecMediaFc.IdFornecedor == idFornecedorDaFicha && PrecoFornecMediaFc.PrecoFornecedor > 0)
                                {
                                    PrecoFornecMedia += PrecoFornecMediaFc.PrecoFornecedor;
                                    ContFornecMedia += 1;
                                }
                            }
                            if (ContFornecMedia == 0) ContFornecMedia = 1;
                            resultado = PrecoFornecMedia / ContFornecMedia;
                        }
                    }
                    else // Pega  o maior valor
                    {
                        if (ret.FirstOrDefault().TipoCustoFornecedor == 2)// Cor        
                        {
                            decimal PrecoCorMaior = 0;
                            foreach (var PrecoCorMaiorFc in ret)
                            {
                                if (PrecoCorMaiorFc.IdFornecedor == idFornecedorDaFicha)
                                {
                                    if (PrecoCorMaiorFc.PrecoCor > PrecoCorMaior)
                                    {
                                        PrecoCorMaior = PrecoCorMaiorFc.PrecoCor;
                                    }
                                }
                            }
                            resultado = PrecoCorMaior;
                        }

                        else if (ret.FirstOrDefault().TipoCustoFornecedor == 3)// Tamanho
                        {
                            decimal PrecoTamanMaior = 0;
                            foreach (var PrecoTamanMaiorFc in ret)
                            {
                                if (PrecoTamanMaiorFc.IdFornecedor == idFornecedorDaFicha)
                                {
                                    if (PrecoTamanMaiorFc.PrecoTamanho > PrecoTamanMaior)
                                    {
                                        PrecoTamanMaior = PrecoTamanMaiorFc.PrecoTamanho;
                                    }
                                }
                            }
                            resultado = PrecoTamanMaior;
                        }
                        else
                        {
                            decimal PrecoFornecMaior = 0;
                            foreach (var PrecoFornecMaiorFc in ret)
                            {
                                if (PrecoFornecMaiorFc.IdFornecedor == idFornecedorDaFicha)
                                {
                                    if (PrecoFornecMaiorFc.PrecoFornecedor > PrecoFornecMaior)
                                    {
                                        PrecoFornecMaior = PrecoFornecMaiorFc.PrecoFornecedor;
                                    }
                                }
                            }
                            resultado = PrecoFornecMaior;
                        }

                    }
                }
                else
                {
                    if (tipoCaluloCusto == 2) //Pega a media
                    {
                        if (ret.FirstOrDefault().TipoCustoFornecedor == 2)// Cor
                        {
                            int count = ret.Where(x => x.PrecoCor > 0).ToList().Count();
                            if (count == 0) count = 1;
                            resultado = (ret.Sum(x => x.PrecoCor) / count);
                        }
                        else if (ret.FirstOrDefault().TipoCustoFornecedor == 3)// Tamanho
                        {
                            int count = ret.Where(x => x.PrecoTamanho > 0).ToList().Count();
                            if (count == 0) count = 1;
                            resultado = (ret.Sum(x => x.PrecoTamanho) / count);
                        }
                        else // Fornecedor
                        {
                            int count = ret.Where(x => x.PrecoFornecedor > 0).ToList().Count();
                            if (count == 0) count = 1;
                            resultado = (ret.Sum(x => x.PrecoFornecedor) / count);
                        }
                    }
                    else // Pega  o maior valor
                    {
                        if (ret.FirstOrDefault().TipoCustoFornecedor == 2)// Cor
                            resultado = ret.Max(x => x.PrecoCor);
                        else if (ret.FirstOrDefault().TipoCustoFornecedor == 3)// Tamanho
                            resultado = ret.Max(x => x.PrecoTamanho);
                        else
                            resultado = ret.Max(x => x.PrecoFornecedor);
                    }
                }
               
            }
            return resultado;
        }
    }
}
