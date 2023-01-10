
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;
using Vestillo.Lib;


namespace Vestillo.Business.Controllers
{
    public class BoletosGeradosController : GenericController<BoletosGerados, BoletosGeradosRepository>
    {
        bool PossuiProtesto = false;
        int IdBanco = 0;
        public void Save(List<BoletosGerados> boletos)
        {
            var repository = new BoletosGeradosRepository();
            string NossoNumero = String.Empty;
            string GeraodorNossoNumero = String.Empty;
            int DvNossoNumero = 0;            
            List<int> _IdsDosBoletos = new List<int>();
            //ticket 56724
            try
            {
                repository.BeginTransaction();

                foreach (var b in boletos)
                {                    

                    var boleto = b;

                    string preFixo = boleto.IdEmpresa == 1 ? "00" : "400";

                    var DadosTitulo = new ContasReceberService().GetServiceFactory().GetById(boleto.idTitulo);
                    var DadosBanco = new BancoService().GetServiceFactory().GetById(boleto.idBanco);
                    IdBanco = boleto.idBanco;

                    if (DadosBanco.DiasParaProtesto > 0)
                    {
                        PossuiProtesto = true;
                    }
                    else
                    {
                        PossuiProtesto = false;
                    }
                    DeleteBoletoPeloTitulo(boleto.idTitulo);


                    if (String.IsNullOrWhiteSpace(boleto.NumDocumento))
                    {                        
                        boleto.NumDocumento = DadosTitulo.NumTitulo + DadosTitulo.Parcela;
                        NossoNumero =  new ContadorNossoNumeroService().GetServiceFactory().GetProximo(boleto.idBanco);
                                                
                        if(DadosBanco.NomeBanco == 341)
                        {
                            GeraodorNossoNumero = DadosBanco.Agenciabanco + DadosBanco.contabanco + "109" + preFixo + NossoNumero;
                        }
                        else if(DadosBanco.NomeBanco == 1)
                        {
                            GeraodorNossoNumero = DadosBanco.Agenciabanco + DadosBanco.contabanco + "11" + preFixo + NossoNumero;
                        }
                        

                        DvNossoNumero = Funcoes.DigitoM10(GeraodorNossoNumero);

                        NossoNumero = preFixo + NossoNumero;
                        if (NossoNumero.Length < 8)
                            NossoNumero = Funcoes.FormatCode(NossoNumero, 8);
                    }
                    else
                    {
                        if (DadosBanco.NomeBanco == 341)
                        {
                            GeraodorNossoNumero = DadosBanco.Agenciabanco + DadosBanco.contabanco + "109" + preFixo + boleto.NumDocumento;
                        }
                        else if (DadosBanco.NomeBanco == 1)
                        {
                            GeraodorNossoNumero = DadosBanco.Agenciabanco + DadosBanco.contabanco + "11" + preFixo + boleto.NumDocumento;
                        }
                        DvNossoNumero = Funcoes.DigitoM10(GeraodorNossoNumero);
                        NossoNumero = preFixo + boleto.NumDocumento;
                        if (NossoNumero.Length < 8)
                        {
                            NossoNumero = Funcoes.FormatCode(NossoNumero, 8);
                        }
                        else if (NossoNumero.Length == 9)
                        {
                            if(preFixo == "00")
                            {
                                NossoNumero = "0" + boleto.NumDocumento;
                            }
                            else if (preFixo == "400")
                            {
                                NossoNumero = "40" + boleto.NumDocumento;
                            }

                        }
                        else if(NossoNumero.Length == 10)
                        {
                            if (preFixo == "00")
                            {
                                NossoNumero = boleto.NumDocumento;
                            }
                            else if (preFixo == "400")
                            {
                                NossoNumero = "4" + boleto.NumDocumento;
                            }
                        }




                         boleto.NumDocumento = boleto.NumDocumento;

                    }
                    boleto.NossoNumero = NossoNumero;
                    boleto.DvNossoNumero = DvNossoNumero.ToString();



                    base.Save(ref boleto);

                    if (_IdsDosBoletos == null)
                    {
                        _IdsDosBoletos = new List<int>();
                    }
                    _IdsDosBoletos.Add(boleto.Id);


                }

                MarcarTituloComBoleto(boletos);
                CriaInstrucaoInicialBoletos(_IdsDosBoletos, DateTime.Now, IdBanco);
                MontaBoletos(_IdsDosBoletos, DateTime.Now);
                if (PossuiProtesto)
                {
                    CriaInstrucaoDeProtestoBoletos(_IdsDosBoletos, DateTime.Now, IdBanco);
                }

                repository.CommitTransaction();
                

                
            }
            catch (Exception ex)
            {
                Funcoes.ExibirErro(ex);
                repository.RollbackTransaction();
                throw new Vestillo.Lib.VestilloException(ex);
                
            }
            finally
            {
                repository.Dispose();
                repository = null;
            }
        }

        public void MontaBoletos(List<int> IdsDosBoletos, DateTime DataEmissaoBoleto)
        {
            try
            {
                var bol = new DadosBoletoBancario();
                var objBoleto = bol.CriarObjetoBoletoBancario(DataEmissaoBoleto, IdsDosBoletos);

                if (VestilloSession.ImprimirBoletoAutomatico)
                {
                    if (bol.InFiles.Count > 0)
                    {
                        //IMPRIMIR BOLETOS
                        for (int i = 0; i < bol.InFiles.Count; i++)
                        {
                            System.Diagnostics.Process.Start(bol.InFiles[i]);
                        }
                        Thread.Sleep(5000);
                    }
                }
            }
            catch(VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }
        }

        public void MarcarTituloComBoleto(List<BoletosGerados> boletos)
        {
            using (ContasReceberRepository updateContasReceber = new ContasReceberRepository())
            {
                try
                {
                    foreach (var b in boletos)
                    {
                        updateContasReceber.UpdateDadosBoleto(b.idTitulo,b.idBanco);
                    }                  

                }
                catch(VestilloException ex)
                {
                    Funcoes.ExibirErro(ex);
                }
                
            }
        }


        public void CriaInstrucaoInicialBoletos(List<int> IdsDosBoletos, DateTime DataEmissaoBoleto, int IdBanco)
        {
            try
            {
                foreach (var IdBoleto in IdsDosBoletos)
                {
                    var Inst = new InstrucoesDosBoletos();
                    Inst.DataEmissao = DataEmissaoBoleto;
                    Inst.IdBoleto = IdBoleto;
                    Inst.IdBanco = IdBanco;
                    Inst.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                    Inst.IdInstrucao = 1;


                    var InstaSave = new InstrucoesDosBoletosService().GetServiceFactory();
                    InstaSave.Save(ref Inst);
                    
                }
                

                
            }
            catch (VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }
        }



        public void CriaInstrucaoDeProtestoBoletos(List<int> IdsDosBoletos, DateTime DataEmissaoBoleto,int IdBanco)
        {
            try
            {
                foreach (var IdBoleto in IdsDosBoletos)
                {
                    var Inst = new InstrucoesDosBoletos();
                    Inst.DataEmissao = DataEmissaoBoleto;
                    Inst.IdBoleto = IdBoleto;
                    Inst.IdBanco = IdBanco;
                    Inst.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                    Inst.IdInstrucao = 9;


                    var InstaSave = new InstrucoesDosBoletosService().GetServiceFactory();
                    InstaSave.Save(ref Inst);

                }



            }
            catch (VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }
        }


        public void DeleteBoletoPeloTitulo(int idTitulo)
        {
            using (BoletosGeradosRepository repository = new BoletosGeradosRepository())
            {
                repository.DeleteBoletoPeloTitulo(idTitulo);
            }
        }

        public BoletosGerados GetViewByIdTitulo(int idTitulo)
        {
            using (var repository = new BoletosGeradosRepository())
            {
                return repository.GetViewByIdTitulo(idTitulo);
            }
        }

        public BoletosGerados GetViewByNossoNumero(string NossoNumero)
        {

            using (var repository = new BoletosGeradosRepository())
            {
                return repository.GetViewByNossoNumero(NossoNumero);
            }
        }

        public BoletosGerados GetViewByNumDocumento(string NumDocumento)
        {

            using (var repository = new BoletosGeradosRepository())
            {
                return repository.GetViewByNumDocumento(NumDocumento);
            }
        }

    }
}
