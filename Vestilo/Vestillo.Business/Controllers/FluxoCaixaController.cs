using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class FluxoCaixaController
    {
        public List<FluxoCaixa> GetByDatas(DateTime daData, DateTime ateData)
        {
            List<FluxoCaixa> fluxos = new List<FluxoCaixa>();
            FluxoCaixa fluxo = new FluxoCaixa();
            ContasPagarRepository contasPagarRepository = new ContasPagarRepository();
            ContasReceberRepository contasReceberRepository = new ContasReceberRepository();
            CreditoFornecedorRepository creditoFornecedorRepository = new CreditoFornecedorRepository();
            CreditosClientesRepository creditosClientesRepository = new CreditosClientesRepository();
            ChequeRepository chequeRepository = new ChequeRepository();


            var date = daData.Date;
            decimal saidaAcumulada = 0;
            decimal entradaAcumulada = 0;
            decimal creditoAcumilado = 0;
            decimal debitoAcumulado = 0;
            decimal saldoAcumulado = 0;

            var cheques = chequeRepository.GetByAteData(date);
            decimal chequeCliente = 0;
            decimal chequeEmpresa = 0;

            if (cheques != null && cheques.Count() > 0)
            {
                cheques.ForEach(c =>
                {
                    if (c.TipoEmitenteCheque == 1)
                    {
                        chequeCliente += c.Valor;
                    }
                    else
                    {
                        chequeEmpresa += c.Valor;
                    }
                });
            }

            creditoAcumilado = creditoFornecedorRepository.GetByAteData(date);
            debitoAcumulado = creditosClientesRepository.GetByAteData(date);
            saidaAcumulada = contasPagarRepository.GetByAteData(date) + debitoAcumulado + chequeEmpresa;
            entradaAcumulada = contasReceberRepository.GetByAteData(date) + creditoAcumilado + chequeCliente;
            saldoAcumulado = entradaAcumulada - saidaAcumulada;

            fluxo.Dia = date;
            fluxo.SaidaDia = saidaAcumulada;
            fluxo.EntradaDia = entradaAcumulada;
            fluxo.CreditoEmpresaDia = creditoAcumilado;
            fluxo.DebitoEmpresaDia = debitoAcumulado;
            fluxo.SaldoDia = saldoAcumulado;
            fluxo.SaidaAcumulada = saidaAcumulada;
            fluxo.EntradaAcumulada = entradaAcumulada;
            fluxo.SaldoAcumulado = saldoAcumulado;

            fluxos.Add(fluxo);

            for (date = date.AddDays(1); date.Date <= ateData.Date; date = date.AddDays(1))
            {
                fluxo = new FluxoCaixa();

                cheques = chequeRepository.GetByData(date);
                chequeCliente = 0;
                chequeEmpresa = 0;

                if (cheques != null && cheques.Count() > 0)
                {
                    cheques.ForEach(c =>
                    {
                        if (c.TipoEmitenteCheque == 1)
                        {
                            chequeCliente += c.Valor;
                        }
                        else
                        {
                            chequeEmpresa += c.Valor;
                        }
                    });
                }

                fluxo.Dia = date;
                fluxo.CreditoEmpresaDia = creditoFornecedorRepository.GetByData(date);
                fluxo.DebitoEmpresaDia = creditosClientesRepository.GetByData(date);
                fluxo.SaidaDia = contasPagarRepository.GetByData(date) + fluxo.DebitoEmpresaDia + chequeEmpresa;
                fluxo.EntradaDia = contasReceberRepository.GetByData(date) + fluxo.CreditoEmpresaDia + chequeCliente;
                fluxo.SaldoDia = fluxo.EntradaDia - fluxo.SaidaDia;

                saidaAcumulada = saidaAcumulada + fluxo.SaidaDia;
                entradaAcumulada = entradaAcumulada + fluxo.EntradaDia;
                saldoAcumulado = saldoAcumulado + fluxo.SaldoDia;

                fluxo.SaldoAcumulado = saldoAcumulado;
                fluxo.SaidaAcumulada = saidaAcumulada;
                fluxo.EntradaAcumulada = entradaAcumulada;

                fluxos.Add(fluxo);
            }

            return fluxos;
        }

        public List<FluxoCaixa> GetByDatasEBanco(DateTime daData, DateTime ateData, int bancoId)
        {
            List<FluxoCaixa> fluxos = new List<FluxoCaixa>();
            FluxoCaixa fluxo = new FluxoCaixa();
            ContasPagarRepository contasPagarRepository = new ContasPagarRepository();
            ContasReceberRepository contasReceberRepository = new ContasReceberRepository();
            CreditoFornecedorRepository creditoFornecedorRepository = new CreditoFornecedorRepository();
            CreditosClientesRepository creditosClientesRepository = new CreditosClientesRepository();
            ChequeRepository chequeRepository = new ChequeRepository();

            var date = daData.Date;
            decimal saidaAcumulada = 0;
            decimal entradaAcumulada = 0;
            decimal creditoAcumilado = 0;
            decimal debitoAcumulado = 0;
            decimal saldoAcumulado = 0;

            var cheques = chequeRepository.GetByAteDataEBanco(date, bancoId);
            decimal chequeCliente = 0;
            decimal chequeEmpresa = 0;

            if (cheques != null && cheques.Count() > 0)
            {
                cheques.ForEach(c =>
                {
                    if (c.TipoEmitenteCheque == 1)
                    {
                        chequeCliente += c.Valor;
                    }
                    else
                    {
                        chequeEmpresa += c.Valor;
                    }
                });
            }

            creditoAcumilado = creditoFornecedorRepository.GetByAteData(date);
            debitoAcumulado = creditosClientesRepository.GetByAteData(date);
            saidaAcumulada = contasPagarRepository.GetByAteDataEBanco(date, bancoId) + debitoAcumulado + chequeEmpresa;
            entradaAcumulada = contasReceberRepository.GetByAteDataEBanco(date, bancoId) + creditoAcumilado + chequeCliente;
            saldoAcumulado = entradaAcumulada - saidaAcumulada;


            fluxo.Dia = date;
            fluxo.SaidaDia = saidaAcumulada;
            fluxo.EntradaDia = entradaAcumulada;
            fluxo.CreditoEmpresaDia = creditoAcumilado;
            fluxo.DebitoEmpresaDia = debitoAcumulado;
            fluxo.SaldoDia = saldoAcumulado;
            fluxo.SaidaAcumulada = saidaAcumulada;
            fluxo.EntradaAcumulada = entradaAcumulada;
            fluxo.SaldoAcumulado = saldoAcumulado;

            fluxos.Add(fluxo);

            for (date = date.AddDays(1); date.Date <= ateData.Date; date = date.AddDays(1))
            {
                fluxo = new FluxoCaixa();

                cheques = chequeRepository.GetByDataEBanco(date, bancoId);
                chequeCliente = 0;
                chequeEmpresa = 0;

                if (cheques != null && cheques.Count() > 0)
                {
                    cheques.ForEach(c =>
                    {
                        if (c.TipoEmitenteCheque == 1)
                        {
                            chequeCliente += c.Valor;
                        }
                        else
                        {
                            chequeEmpresa += c.Valor;
                        }
                    });
                }

                fluxo.Dia = date;
                fluxo.CreditoEmpresaDia = creditoFornecedorRepository.GetByData(date);
                fluxo.DebitoEmpresaDia = creditosClientesRepository.GetByData(date);
                fluxo.SaidaDia = contasPagarRepository.GetByDataEBanco(date, bancoId) + fluxo.DebitoEmpresaDia + chequeEmpresa;
                fluxo.EntradaDia = contasReceberRepository.GetByDataEBanco(date, bancoId) + fluxo.CreditoEmpresaDia + chequeCliente;
                fluxo.SaldoDia = fluxo.EntradaDia - fluxo.SaidaDia;

                saidaAcumulada = saidaAcumulada + fluxo.SaidaDia;
                entradaAcumulada = entradaAcumulada + fluxo.EntradaDia;
                saldoAcumulado = saldoAcumulado + fluxo.SaldoDia;

                fluxo.SaldoAcumulado = saldoAcumulado;
                fluxo.SaidaAcumulada = saidaAcumulada;
                fluxo.EntradaAcumulada = entradaAcumulada;

                fluxos.Add(fluxo);
            }

            return fluxos;
        }

        public FluxoCaixaConsulta GetByDataEBancoConsulta(DateTime data, int bancoId)
        {
            FluxoCaixaConsulta fluxo = new FluxoCaixaConsulta();
            ContasPagarRepository contasPagarRepository = new ContasPagarRepository();
            ContasReceberRepository contasReceberRepository = new ContasReceberRepository();
            CreditoFornecedorRepository creditoFornecedorRepository = new CreditoFornecedorRepository();
            CreditosClientesRepository creditosClientesRepository = new CreditosClientesRepository();
            ChequeRepository chequeRepository = new ChequeRepository();

            fluxo.Cheques = chequeRepository.GetByDataEBancoConsulta(data, bancoId);
            fluxo.Entradas = contasReceberRepository.GetByDataEBancoConsulta(data, bancoId);
            fluxo.Saidas = contasPagarRepository.GetByDataEBancoConsulta(data, bancoId);
            fluxo.CreditoEmpresa = creditoFornecedorRepository.GetByDataConsulta(data);
            fluxo.DebitosEmpresa = creditosClientesRepository.GetByDataConsulta(data);

            return fluxo;
        }

        public FluxoCaixaConsulta GetByAteDataEBancoConsulta(DateTime data, int bancoId)
        {
            FluxoCaixaConsulta fluxo = new FluxoCaixaConsulta();
            ContasPagarRepository contasPagarRepository = new ContasPagarRepository();
            ContasReceberRepository contasReceberRepository = new ContasReceberRepository();
            CreditoFornecedorRepository creditoFornecedorRepository = new CreditoFornecedorRepository();
            CreditosClientesRepository creditosClientesRepository = new CreditosClientesRepository();
            ChequeRepository chequeRepository = new ChequeRepository();

            fluxo.Cheques = chequeRepository.GetByAteDataEBancoConsulta(data, bancoId);
            fluxo.Entradas = contasReceberRepository.GetByAteDataEBancoConsulta(data, bancoId);
            fluxo.Saidas = contasPagarRepository.GetByAteDataEBancoConsulta(data, bancoId);
            fluxo.CreditoEmpresa = creditoFornecedorRepository.GetByAteDataConsulta(data);
            fluxo.DebitosEmpresa = creditosClientesRepository.GetByAteDataConsulta(data);

            return fluxo;
        }
    }
}
