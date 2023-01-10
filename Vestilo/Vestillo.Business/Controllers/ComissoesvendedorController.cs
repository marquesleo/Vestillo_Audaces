
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;


namespace Vestillo.Business.Controllers
{
    public class ComissoesvendedorController : GenericController<Comissoesvendedor, ComissoesvendedorRepository>
    {

        public override void Save(ref Comissoesvendedor comissao)
        {
            bool openTransaction = false;

            try
            {
                               
                openTransaction = _repository.BeginTransaction();

                Comissoesvendedor comissaoOld = null;

                if (comissao.Id > 0)
                    comissaoOld = GetById(comissao.Id);
                else
                    comissaoOld = comissao;

                comissao.idcontasreceber = comissao.idcontasreceber == 0 ? null : comissao.idcontasreceber;
                comissao.idcontaspagar = comissao.idcontaspagar == 0 ? null : comissao.idcontaspagar;
                comissao.idnotaconsumidor = comissao.idnotaconsumidor == 0 ? null : comissao.idnotaconsumidor;
                comissao.idNotaFat = comissao.idNotaFat == 0 ? null : comissao.idNotaFat;
                comissao.idGuia = comissao.idGuia == 0 ? null : comissao.idGuia;

                if (comissao.idNotaFat.GetValueOrDefault() > 0)
                {
                    FatNfeRepository fatNfeRepository = new FatNfeRepository();
                    FatNfe fat = fatNfeRepository.GetById(comissao.idNotaFat.GetValueOrDefault());

                    if (fat != null && comissao.idGuia == null)
                    {
                        if (fat.idvendedor.GetValueOrDefault() == comissaoOld.idvendedor)
                        {
                            fat.idvendedor = comissao.idvendedor;
                            fatNfeRepository.Save(ref fat);
                        }
                        else if (fat.idvendedor2.GetValueOrDefault() == comissaoOld.idvendedor)
                        {
                            fat.idvendedor2 = comissao.idvendedor;
                            fatNfeRepository.Save(ref fat);
                        }
                        else
                        {
                            fat.idvendedor = comissao.idvendedor;
                            fatNfeRepository.Save(ref fat);
                        }
                    }

                }
                else if (comissao.idnotaconsumidor.GetValueOrDefault() > 0)
                {
                    NfceRepository nfceRepository = new NfceRepository();
                    Nfce nfce = nfceRepository.GetById(comissao.idnotaconsumidor.GetValueOrDefault());

                    if (nfce != null)
                    {
                        if (comissao.idvendedor > 0)
                            nfce.IdVendedor = comissao.idvendedor;
                        else
                            nfce.IdGuia = comissao.idGuia;

                        nfceRepository.Save(ref nfce);
                    }
                }


                base.Save(ref comissao);

                
                if(openTransaction)
                    _repository.CommitTransaction();

                
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();
                
                throw ex;
            }
            
        }

        public IEnumerable<ComissoesvendedorView> GetCamposBrowse()
        {
            using (ComissoesvendedorRepository repository = new ComissoesvendedorRepository())
            {
                return repository.GetCamposBrowse();
            }
        }

        public IEnumerable<Comissoesvendedor> GetByParcelaCtr(int idParcela)
        {
            using (ComissoesvendedorRepository repository = new ComissoesvendedorRepository())
            {
                return repository.GetByParcelaCtr(idParcela);
            }
        }

        public IEnumerable<ComissoesvendedorView> GetLiberarComissoes(string Vendedores, string Guias, DateTime DataInicio, DateTime DataFim)
        {
            using (ComissoesvendedorRepository repository = new ComissoesvendedorRepository())
            {
                return repository.GetLiberarComissoes(Vendedores, Guias, DataInicio, DataFim);
            }
        }

         public IEnumerable<Comissoesvendedor> GetCancelarLiberacao(int idContasPagar)
        {
            using (ComissoesvendedorRepository repository = new ComissoesvendedorRepository())
            {
                return repository.GetCancelarLiberacao(idContasPagar);
            }
        }

        public IEnumerable<ComissoesvendedorView> GetListagemPorPeriodo(string Vendedores, string Guias, DateTime DataInicio,DateTime DataFim)
         {
             using (ComissoesvendedorRepository repository = new ComissoesvendedorRepository())
             {
                 return repository.GetListagemPorPeriodo(Vendedores, Guias, DataInicio, DataFim);
             }
         }


        public IEnumerable<Comissoesvendedor> GetByParcelaCtrDeletar(int idParcela)
        {
            using (ComissoesvendedorRepository repository = new ComissoesvendedorRepository())
            {
                return repository.GetByParcelaCtrDeletar(idParcela);
            }
        }

        public void DeletePorNotaConsumidor(int idnotaconsumidor)
        {
            using (ComissoesvendedorRepository repository = new ComissoesvendedorRepository())
            {
                repository.DeletePorNotaConsumidor(idnotaconsumidor);
            }
        }
    }
}
