using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;

namespace Vestillo.Business.Controllers
{
    public class FichaTecnicaDoMaterialRelacaoController : GenericController<FichaTecnicaDoMaterialRelacao, FichaTecnicaDoMaterialRelacaoRepository>
    {
            
          
        private ProdutoDetalheService _produtoDetalhesService;
        private ProdutoDetalheService produtoDetalhesService
        {
            get
            {
                if (_produtoDetalhesService == null)
                    _produtoDetalhesService = new ProdutoDetalheService();
                return _produtoDetalhesService;
            }
        }
             

        private TamanhoService _TamanhoService;
        private TamanhoService TamanhoService
        {
            get
            {
                if (_TamanhoService == null)
                    _TamanhoService = new TamanhoService();
                return _TamanhoService;
            }
        }

        private IEnumerable<Tamanho>_lstTamanho;
        private IEnumerable<Tamanho> lstTamanho
        {
            get
            {
                if (_lstTamanho == null)
                {

                    _lstTamanho = TamanhoService.GetServiceFactory().GetAll();
                }
                return _lstTamanho;
            }
        }

           

        private IEnumerable<Cor> _lstCor;
        private IEnumerable<Cor> lstCor
        {
            get
            {
                if (_lstCor == null)
                {
                    _lstCor = new System.Collections.Generic.List<Cor>();
                    _lstCor = CorService.GetServiceFactory().GetAll();
                }
                return _lstCor;
            }
        }

        private CorService _CorService;
        private CorService CorService
        {
            get
            {
                if (_CorService == null)
                    _CorService = new CorService();
                return _CorService;
            }
        }


        //public List<MontagemFichaTecnicaDoMaterialCorView> getMontagemFichaTecnicaDoMaterialCor(int produtoId, int materiaPrimaId)
        //{
        //    var produtoDetalheService = produtoDetalhesService.GetServiceFactory();


        //    IEnumerable<ProdutoDetalhe> gradeProduto = produtoDetalheService.GetListByProduto(produtoId);
        //    IEnumerable<ProdutoDetalhe> gradeMateriaPrima = produtoDetalheService.GetListByProduto(materiaPrimaId);

        //    int[] cores = gradeProduto.Select(x => x.Idcor).ToList().Distinct().ToList().ToArray();
        //    var lstMontagemFichaTecnicaDoMaterialCorView = new List<MontagemFichaTecnicaDoMaterialCorView>();

        //    foreach (int corProduto in cores)
        //    {
        //        var montagem = new MontagemFichaTecnicaDoMaterialCorView();
        //        montagem.CorDoProduto = corProduto;

        //        int corMateriaPrima = gradeMateriaPrima.Where(x => x.Idcor == corProduto).Select(x => x.Idcor).FirstOrDefault();

        //        if (corMateriaPrima == 0)
        //            corMateriaPrima = gradeMateriaPrima.Select(x => x.Idcor).FirstOrDefault();

        //        montagem.CorUnicaDaMateriaPrima = gradeMateriaPrima.ToList().Exists(gmp => gmp.CorUnica == true);

        //        montagem.CorDaMateriaPrima = corMateriaPrima;
        //        lstMontagemFichaTecnicaDoMaterialCorView.Add(montagem);
        //    }


        //    //IEnumerable<ProdutoDetalhe> DetalheCoresProduto = produtoDetalhesService.GetServiceFactory().GetListByProduto(produtoId);
        //    //IEnumerable<ProdutoDetalhe> DetalheCoresMateriaPrima = produtoDetalhesService.GetServiceFactory().GetListByProduto(materiaPrimaId);
        //    //var lstMontagemFichaTecnicaDoMaterialCorView = new List<MontagemFichaTecnicaDoMaterialCorView>();
            
        //    //var lstDetalhesCoresProdutoDistintas = (from obj in DetalheCoresProduto select obj.Idcor).Distinct();
           
        //    //foreach (var item in lstDetalhesCoresProdutoDistintas)
        //    //{
        //    //    var Montagem = new MontagemFichaTecnicaDoMaterialCorView();
        //    //    Montagem.CorDoProduto = item;
        //    //    lstMontagemFichaTecnicaDoMaterialCorView.Add(Montagem);
        //    //}


        //    //int i = 0;
        //    //foreach (var item in lstMontagemFichaTecnicaDoMaterialCorView)
        //    //{
        //    //    bool achou = false;

        //    //    int idCor = 0;
        //    //    if (DetalheCoresMateriaPrima != null && DetalheCoresMateriaPrima.Any())
        //    //    {

        //    //        while (!achou)
        //    //        {
        //    //            idCor = DetalheCoresMateriaPrima.ToList()[i].Idcor;
        //    //            if (lstCor.ToList().Any(p => p.Id == idCor))
        //    //            {
        //    //                achou = true;
        //    //            }

        //    //            i += 1;
        //    //            if (i == DetalheCoresMateriaPrima.ToList().Count)
        //    //                i = 0;
        //    //        }
        //    //        if (idCor == 0)
        //    //            idCor = DetalheCoresMateriaPrima.ToList()[0].Idcor;
        //    //        item.CorDaMateriaPrima = idCor;
        //    //    }
        //    //    else
        //    //    {
        //    //       item.CorDaMateriaPrima = item.CorDoProduto;
                   
                   
        //    //    }
        //    //}
        //    return lstMontagemFichaTecnicaDoMaterialCorView;
        //}


        //public List<MontagemFichaTecnicaDoMaterialTamanhoView> getMontagemFichaTecnicaDoMaterialTamanho(int produtoId, int materiaPrimaId)
        //{

        //     var produtoDetalheService = produtoDetalhesService.GetServiceFactory();


        //    IEnumerable<ProdutoDetalhe> gradeProduto = produtoDetalheService.GetListByProduto(produtoId);
        //    IEnumerable<ProdutoDetalhe> gradeMateriaPrima = produtoDetalheService.GetListByProduto(materiaPrimaId);

        //    int[] tamanhos = gradeProduto.Select(x => x.IdTamanho).ToList().Distinct().ToList().ToArray();
        //    var lstMontagemFichaTecnicaDoMaterialTamanhoView = new List<MontagemFichaTecnicaDoMaterialTamanhoView>();

        //    foreach (int tamanhoProduto in tamanhos)
        //    {
        //        var montagem = new MontagemFichaTecnicaDoMaterialTamanhoView();
        //        montagem.TamanhoDoProduto = tamanhoProduto;

        //        int tamanhoMateriaPrima = gradeMateriaPrima.Where(x => x.IdTamanho == tamanhoProduto).Select(x => x.IdTamanho).FirstOrDefault();

        //        if (tamanhoMateriaPrima == 0)
        //            tamanhoMateriaPrima = gradeMateriaPrima.Select(x => x.IdTamanho).FirstOrDefault();

        //        montagem.TamanhoUnicoDaMateriaPrima = gradeMateriaPrima.ToList().Exists(gmp => gmp.TamanhoUnico == true);

        //        montagem.TamanhoDaMateriaPrima = tamanhoMateriaPrima;
        //        lstMontagemFichaTecnicaDoMaterialTamanhoView.Add(montagem);
        //    }

        //    //IEnumerable<ProdutoDetalhe> DetalheTamanhoProduto = produtoDetalhesService.GetServiceFactory().GetListByProduto(produtoId);
        //    //IEnumerable<ProdutoDetalhe> DetalheTamanhoMateriaPrima = produtoDetalhesService.GetServiceFactory().GetListByProduto(materiaPrimaId);
        //    //var lstMontagemFichaTecnicaDoMaterialTamanhoView = new List<MontagemFichaTecnicaDoMaterialTamanhoView>();
        //    //var lstTamanhoDoProdutoDistinto = (from obj in DetalheTamanhoMateriaPrima select obj.IdTamanho).Distinct();


        //    //foreach (var item in lstTamanhoDoProdutoDistinto)
        //    //{
        //    //    var Montagem = new MontagemFichaTecnicaDoMaterialTamanhoView();
        //    //    Montagem.TamanhoDoProduto = item;
        //    //    lstMontagemFichaTecnicaDoMaterialTamanhoView.Add(Montagem);
        //    //}

        //    //int i = 0;
        //    //foreach (var item in lstMontagemFichaTecnicaDoMaterialTamanhoView)
        //    //{
        //    //    bool achou = false;

        //    //    int idTamanho = 0;
        //    //    if (DetalheTamanhoMateriaPrima != null && DetalheTamanhoMateriaPrima.Any())
        //    //    {

        //    //        while (!achou)
        //    //        {
        //    //            idTamanho = DetalheTamanhoMateriaPrima.ToList()[i].IdTamanho;
        //    //            if (lstTamanho.ToList().Any(p => p.Id == idTamanho))
        //    //            {
        //    //                achou = true;
        //    //            }

        //    //            i += 1;
        //    //            if (i == DetalheTamanhoMateriaPrima.ToList().Count)
        //    //                i = 0;
        //    //        }
        //    //        if (idTamanho == 0)
        //    //            idTamanho = DetalheTamanhoMateriaPrima.ToList()[0].IdTamanho;
        //    //        item.TamanhoDaMateriaPrima = idTamanho;
        //    //    }
        //    //    else
        //    //    {
        //    //        item.TamanhoDaMateriaPrima = item.TamanhoDoProduto;
        //    //    }
        //    //}
        //    return lstMontagemFichaTecnicaDoMaterialTamanhoView;
           
        //}


        public List<MontagemFichaTecnicaDoMaterialCorView> getMontagemFichaTecnicaDoMaterialCor(int produtoId, int materiaPrimaId)
        {
            var produtoDetalheService = produtoDetalhesService.GetServiceFactory();


            IEnumerable<ProdutoDetalhe> gradeProduto = produtoDetalheService.GetListByProduto(produtoId, 1);
            IEnumerable<ProdutoDetalhe> gradeMateriaPrima = produtoDetalheService.GetListByProduto(materiaPrimaId, 1);

            int[] cores = gradeProduto.Select(x => x.Idcor).ToList().Distinct().ToList().ToArray();
            var lstMontagemFichaTecnicaDoMaterialCorView = new List<MontagemFichaTecnicaDoMaterialCorView>();

            foreach (int corProduto in cores)
            {
                var montagem = new MontagemFichaTecnicaDoMaterialCorView();
                montagem.CorDoProduto = corProduto;

                int corMateriaPrima = gradeMateriaPrima.Where(x => x.Idcor == corProduto && x.Idcor != 999 && x.IdTamanho != 999).Select(x => x.Idcor).FirstOrDefault();

                if (corMateriaPrima == 0)
                    corMateriaPrima = gradeMateriaPrima.Where(x => x.Idcor != 999 && x.IdTamanho != 999).Select(x => x.Idcor ).FirstOrDefault();

                montagem.CorUnicaDaMateriaPrima = gradeMateriaPrima.ToList().Exists(gmp => gmp.CorUnica == true);

                montagem.CorDaMateriaPrima = corMateriaPrima;
                lstMontagemFichaTecnicaDoMaterialCorView.Add(montagem);
            }

            return lstMontagemFichaTecnicaDoMaterialCorView;
        }

        public List<MontagemFichaTecnicaDoMaterialTamanhoView> getMontagemFichaTecnicaDoMaterialTamanho(int produtoId, int materiaPrimaId)
        {

            var produtoDetalheService = produtoDetalhesService.GetServiceFactory();


            IEnumerable<ProdutoDetalhe> gradeProduto = produtoDetalheService.GetListByProduto(produtoId, 1);
            IEnumerable<ProdutoDetalhe> gradeMateriaPrima = produtoDetalheService.GetListByProduto(materiaPrimaId, 1);

            int[] tamanhos = gradeProduto.Select(x => x.IdTamanho).ToList().Distinct().ToList().ToArray();
            var lstMontagemFichaTecnicaDoMaterialTamanhoView = new List<MontagemFichaTecnicaDoMaterialTamanhoView>();

            foreach (int tamanhoProduto in tamanhos)
            {
                var montagem = new MontagemFichaTecnicaDoMaterialTamanhoView();
                montagem.TamanhoDoProduto = tamanhoProduto;

                int tamanhoMateriaPrima = gradeMateriaPrima.Where(x => x.IdTamanho == tamanhoProduto && x.Idcor != 999 && x.IdTamanho != 999).Select(x => x.IdTamanho).FirstOrDefault();

                if (tamanhoMateriaPrima == 0)
                    tamanhoMateriaPrima = gradeMateriaPrima.Where(x => x.Idcor != 999 && x.IdTamanho != 999).Select(x => x.IdTamanho).FirstOrDefault();

                montagem.TamanhoUnicoDaMateriaPrima = gradeMateriaPrima.ToList().Exists(gmp => gmp.TamanhoUnico == true);

                montagem.TamanhoDaMateriaPrima = tamanhoMateriaPrima;
                lstMontagemFichaTecnicaDoMaterialTamanhoView.Add(montagem);
            }

            //IEnumerable<ProdutoDetalhe> DetalheTamanhoProduto = produtoDetalhesService.GetServiceFactory().GetListByProduto(produtoId);
            //IEnumerable<ProdutoDetalhe> DetalheTamanhoMateriaPrima = produtoDetalhesService.GetServiceFactory().GetListByProduto(materiaPrimaId);
            //var lstMontagemFichaTecnicaDoMaterialTamanhoView = new List<MontagemFichaTecnicaDoMaterialTamanhoView>();
            //var lstTamanhoDoProdutoDistinto = (from obj in DetalheTamanhoMateriaPrima select obj.IdTamanho).Distinct();


            //foreach (var item in lstTamanhoDoProdutoDistinto)
            //{
            //    var Montagem = new MontagemFichaTecnicaDoMaterialTamanhoView();
            //    Montagem.TamanhoDoProduto = item;
            //    lstMontagemFichaTecnicaDoMaterialTamanhoView.Add(Montagem);
            //}

            //int i = 0;
            //foreach (var item in lstMontagemFichaTecnicaDoMaterialTamanhoView)
            //{
            //    bool achou = false;

            //    int idTamanho = 0;
            //    if (DetalheTamanhoMateriaPrima != null && DetalheTamanhoMateriaPrima.Any())
            //    {

            //        while (!achou)
            //        {
            //            idTamanho = DetalheTamanhoMateriaPrima.ToList()[i].IdTamanho;
            //            if (lstTamanho.ToList().Any(p => p.Id == idTamanho))
            //            {
            //                achou = true;
            //            }

            //            i += 1;
            //            if (i == DetalheTamanhoMateriaPrima.ToList().Count)
            //                i = 0;
            //        }
            //        if (idTamanho == 0)
            //            idTamanho = DetalheTamanhoMateriaPrima.ToList()[0].IdTamanho;
            //        item.TamanhoDaMateriaPrima = idTamanho;
            //    }
            //    else
            //    {
            //        item.TamanhoDaMateriaPrima = item.TamanhoDoProduto;
            //    }
            //}
            return lstMontagemFichaTecnicaDoMaterialTamanhoView;

        }


        public void ExcluirRelacao(int FichaTecnicaId)
        {
            using (FichaTecnicaDoMaterialRelacaoRepository repository = new FichaTecnicaDoMaterialRelacaoRepository())
            {
                repository.ExcluirRelacao(FichaTecnicaId);
            }
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByFichaTecnica(int FichaTecnicaId)
        {

            using (FichaTecnicaDoMaterialRelacaoRepository repository = new FichaTecnicaDoMaterialRelacaoRepository())
            {
                return repository.GetAllViewByFichaTecnica(FichaTecnicaId);
            }
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByGrade(ProdutoDetalheView grade)
        {
            using (FichaTecnicaDoMaterialRelacaoRepository repository = new FichaTecnicaDoMaterialRelacaoRepository())
            {
                return repository.GetAllViewByGrade(grade);
            }
        }

        public void UpdateFichaRelacao(FichaTecnicaDoMaterialRelacao ficha, int TipoItem)
        {
            using (FichaTecnicaDoMaterialRelacaoRepository repository = new FichaTecnicaDoMaterialRelacaoRepository())
            {
                repository.UpdateFichaRelacao(ficha, TipoItem);
            }
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByGradeProduto(ProdutoDetalhe grade)
        {

            using (FichaTecnicaDoMaterialRelacaoRepository repository = new FichaTecnicaDoMaterialRelacaoRepository())
            {
                return repository.GetAllViewByGradeProduto(grade);
            }
        }

    }
}
