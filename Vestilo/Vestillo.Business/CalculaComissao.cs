using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Service;
using Vestillo.Lib;

namespace Vestillo.Business
{
    public class CalculaComissao
    {
        //tipo documento = 1 NFe - 2 NFCe
        Comissoesvendedor GeraComissao = new Comissoesvendedor();
        decimal ValorComissao = 0;
        decimal BaseDeCalculo = 0;

        public Comissoesvendedor ComissaoNaBaixa(bool PagamentoAVista,DateTime dataEmissao,int idVendedor, int? idGuia, int idParcela, int idDocumento, int tipoDocumento, string referenciadocumento, string parcela, int qtdParcelas, decimal percentual, decimal valor, bool naoGravar = false)
        {
            try
            {
                GeraComissao.Ativo = true;
                GeraComissao.Id = 0;
                GeraComissao.Status = (int)enumStatusComissao.Aberto;
                GeraComissao.dataemissao = dataEmissao;
                GeraComissao.dataliberacao = null;
                if(PagamentoAVista)
                {
                    GeraComissao.ExibirTitulo = 1;
                }
                else
                {
                    GeraComissao.ExibirTitulo = 0;
                }
                
                GeraComissao.idcontasreceber = idParcela;
                GeraComissao.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                if (tipoDocumento == 1)
                {
                    GeraComissao.idnotaconsumidor = null;
                    GeraComissao.idNotaFat = idDocumento;
                    GeraComissao.Obs = "Comissão gerada pelo faturamento: " + referenciadocumento;
                }
                else
                {
                    GeraComissao.idnotaconsumidor = idDocumento;
                    GeraComissao.idNotaFat = null;
                    GeraComissao.Obs = "Comissão gerada pela NFCe" + referenciadocumento;
                }
                GeraComissao.idvendedor = idVendedor;
                GeraComissao.idGuia = idGuia;
                GeraComissao.parcela = parcela;
                GeraComissao.percentual = percentual;

                if(VestilloSession.CalculaComissaoSobreParcela)
                {
                    var dadosParcela = new ContasReceberService().GetServiceFactory().GetById(idParcela);
                    BaseDeCalculo = dadosParcela.ValorParcela;
                }
                else
                {
                    BaseDeCalculo = (valor / qtdParcelas);
                }
                

                GeraComissao.basecalculo = decimal.Parse(VestilloSession.FormatarMoedaTotalizador(BaseDeCalculo.ToString()));
                ValorComissao = GeraComissao.basecalculo * percentual;
                ValorComissao = ValorComissao / 100;
                ValorComissao = decimal.Parse(VestilloSession.FormatarMoedaTotalizador(ValorComissao.ToString()));
                GeraComissao.valor = ValorComissao;
                
                if (!naoGravar)
                    new ComissoesvendedorService().GetServiceFactory().Save(ref GeraComissao);
              
            }
            catch (VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }
            catch (Exception ex)
            {
                Funcoes.ExibirErro(ex);
            }

            return GeraComissao;

        }

        public Comissoesvendedor ComissaoNaEmissao(DateTime dataEmissao,int idVendedor, int? idGuia, int? idParcela, int idDocumento, int tipoDocumento, string referenciadocumento, string parcela, int qtdParcelas, decimal percentual, decimal valor, bool naoGravar = false)
        {
            try
            {
                GeraComissao.Ativo = true;
                GeraComissao.Id = 0;
                GeraComissao.Status = (int)enumStatusComissao.Aberto;
                GeraComissao.dataemissao = dataEmissao;
                GeraComissao.dataliberacao = null;
                GeraComissao.ExibirTitulo = 1;
                GeraComissao.idcontasreceber = idParcela;
                GeraComissao.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                if (tipoDocumento == 1)
                {
                    GeraComissao.idnotaconsumidor = null;
                    GeraComissao.idNotaFat = idDocumento;
                    GeraComissao.Obs = "Comissão gerada pelo faturamento: " + referenciadocumento;
                }
                else
                {
                    GeraComissao.idnotaconsumidor = idDocumento;
                    GeraComissao.idNotaFat = null;
                    GeraComissao.Obs = "Comissão gerada pela NFCe " + referenciadocumento;
                }
                GeraComissao.idvendedor = idVendedor;
                GeraComissao.idGuia = idGuia;
                GeraComissao.parcela = parcela;
                GeraComissao.percentual = percentual;

                if (VestilloSession.CalculaComissaoSobreParcela && Convert.ToInt32(idParcela) > 0)
                {
                    var dadosParcela = new ContasReceberService().GetServiceFactory().GetById(Convert.ToInt32(idParcela));
                    BaseDeCalculo = dadosParcela.ValorParcela;
                }
                else
                {
                    BaseDeCalculo = (valor / qtdParcelas);
                }


                GeraComissao.basecalculo = decimal.Parse(VestilloSession.FormatarMoedaTotalizador(BaseDeCalculo.ToString()));
                ValorComissao = GeraComissao.basecalculo * percentual;
                ValorComissao = ValorComissao / 100;
                ValorComissao = decimal.Parse(VestilloSession.FormatarMoedaTotalizador(ValorComissao.ToString()));
                GeraComissao.valor = ValorComissao;
                
                if (!naoGravar)
                    new ComissoesvendedorService().GetServiceFactory().Save(ref GeraComissao);

            }
            catch (VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }
            catch (Exception ex)
            {
                Funcoes.ExibirErro(ex);
            }

            return GeraComissao;
        }
    }
}
