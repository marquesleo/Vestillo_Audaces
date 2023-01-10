using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoletoNet;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;
using Vestillo.Lib;
using static BoletoNet.AbstractInstrucao;


//using Pechkin.Synchronized;

namespace Vestillo.Business
{

  
    public class DadosBoletoBancario
    {
        public List<string> InFiles = new List<string>();

        public class DadosRemessa
        {         
            public int idBoleto { get; set; }
            public int idInstrucao { get; set; }
        }

        public BoletoBancario CriarObjetoBoletoBancario(DateTime dataEmissao, List<int> IdsDosBoletos)
        {
            BoletoBancario objBoletoBancario = new BoletoBancario();
           

            try
            {

                foreach (var IdBoleto in IdsDosBoletos)
                {
                    var empresa = new EmpresaService().GetServiceFactory().GetById(VestilloSession.EmpresaLogada.Id);
                    var DadosBoletoBancario = new BoletosGeradosService().GetServiceFactory().GetById(IdBoleto);
                    var Bancos = new BancoService().GetServiceFactory().GetById(DadosBoletoBancario.idBanco);
                    var EndEmpresa = new EmpresaService().GetServiceFactory().GetEndereco(VestilloSession.EmpresaLogada.Id);
                    var DadosCtsReceber = new ContasReceberService().GetServiceFactory().GetById(DadosBoletoBancario.idTitulo);


                    int CodigoBanco = Bancos.NomeBanco;

                    //COMEÇA ITAU

                    if (CodigoBanco == 341)
                    {

                        Cedente objCedente = new Cedente(empresa.CNPJ,
                        empresa.RazaoSocial,
                        Bancos.Agenciabanco,
                        Bancos.contabanco,
                        Bancos.DigitoContabanco);
                        objCedente.Codigo = Bancos.Convenio;
                        objCedente.CodigoTransmissao = Bancos.CodigoTransmissao;


                        string xMun = String.Empty;
                        var municipio = new MunicipioIbgeService().GetServiceFactory().GetById(Convert.ToInt32(EndEmpresa.MunicipioId));
                        xMun = VestilloSession.PreparaTexto(municipio.Municipio);

                        var UF = new EstadoService().GetServiceFactory().GetById(EndEmpresa.EstadoId);

                        objCedente.Endereco = new Endereco()
                        {
                            End = EndEmpresa.Logradouro,
                            Bairro = EndEmpresa.Bairro,
                            CEP = EndEmpresa.CEP,
                            Cidade = xMun,
                            Numero = EndEmpresa.Numero,
                            UF = UF.Abreviatura
                        };

                        var objListaBoletos = new Boletos();

                        var objBanco = new Banco(CodigoBanco);

                        Boleto objBoleto = new Boleto(DadosCtsReceber.DataVencimento, DadosCtsReceber.ValorParcela, "109", DadosBoletoBancario.NossoNumero, objCedente);

                        var DadosSacado = new ColaboradorService().GetServiceFactory().GetById(DadosBoletoBancario.idCliente);



                        Sacado objSacado = new Sacado(DadosSacado.CnpjCpf, DadosSacado.RazaoSocial);

                        string xMunSacado = String.Empty;
                        var municipioSacado = new MunicipioIbgeService().GetServiceFactory().GetById(Convert.ToInt32(DadosSacado.IdMunicipio));
                        xMunSacado = VestilloSession.PreparaTexto(municipioSacado.Municipio);

                        var UFSacado = new EstadoService().GetServiceFactory().GetById(Convert.ToInt32(DadosSacado.IdEstado));


                        objBoletoBancario = new BoletoBancario();
                        objBoleto.Sacado = objSacado;
                        objBoleto.Sacado.Endereco.End = DadosSacado.Endereco + ", " + DadosSacado.Numero;
                        objBoleto.Sacado.Endereco.Bairro = DadosSacado.Bairro;
                        objBoleto.Sacado.Endereco.Cidade = xMunSacado;
                        objBoleto.Sacado.Endereco.CEP = DadosSacado.Cep;
                        objBoleto.Sacado.Endereco.UF = UFSacado.Abreviatura;

                        objBoleto.NumeroDocumento = DadosBoletoBancario.NumDocumento;


                        //objBoleto.PercMulta = Bancos.Multa;
                        //objBoleto.JurosMora = Bancos.Juros;
                        objBoleto.Avalista = new Cedente();
                        objBoleto.Avalista.Nome = empresa.RazaoSocial;
                        objBoleto.Avalista.CPFCNPJ = empresa.CNPJ;

                        objBoleto.Avalista.Endereco = new Endereco();
                        objBoleto.Avalista.Endereco.End = EndEmpresa.Logradouro;
                        objBoleto.Avalista.Endereco.Bairro = EndEmpresa.Bairro;
                        objBoleto.Avalista.Endereco.Cidade = xMun;
                        objBoleto.Avalista.Endereco.CEP = EndEmpresa.CEP;
                        objBoleto.Avalista.Endereco.UF = UF.Abreviatura;



                        //-----------------------

                        var InstrucaoBoleto = new InstrucoesDosBoletosService().GetServiceFactory().GetViewByDataEBoleto(dataEmissao, IdBoleto);


                        if (!String.IsNullOrWhiteSpace(Bancos.MensagemCaixa))
                        {
                            Instrucao_Itau instrucaoCaixa = new Instrucao_Itau();
                            instrucaoCaixa.Descricao = Bancos.MensagemCaixa;
                            objBoleto.Instrucoes.Add(instrucaoCaixa);

                        }

                        var dadosInstrucao = new InstrucoesPorBancosService().GetServiceFactory().GetAll();

                        if (CodigoBanco == 341)
                        {
                            foreach (var item in InstrucaoBoleto)
                            {
                                var DescricaoInstrucao = dadosInstrucao.Where(x => x.IdInstrucao == item.IdInstrucao).ToList();

                                Instrucao_Itau instrucaoGeral = new Instrucao_Itau
                                {
                                    Codigo = item.IdInstrucao,
                                    Descricao = DescricaoInstrucao[0].IdInstrucao != 1 ? DescricaoInstrucao[0].Descricao : ""
                                };

                                objBoleto.Instrucoes.Add(instrucaoGeral);
                            }

                            if (Bancos.Multa > 0)
                            {
                                double PercMulta = (double)Bancos.Multa;

                                objBoleto.PercMulta = Convert.ToDecimal(PercMulta);

                                string diaSemana = objBoleto.DataVencimento.ToString("ddd");

                                if (diaSemana == "sáb")
                                {
                                    objBoleto.DataMulta = objBoleto.DataVencimento.AddDays(2);
                                }
                                else
                                {
                                    objBoleto.DataMulta = objBoleto.DataVencimento.AddDays(1);
                                }


                                objBoleto.PercMulta = Convert.ToDecimal(PercMulta);
                                objBoleto.ValorMulta = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(PercMulta)) / 100;
                                objBoleto.ValorMulta = Decimal.Round(objBoleto.ValorMulta, 2);


                                Instrucao_Itau instrucaoMulta = new Instrucao_Itau(997, PercMulta, EnumTipoValor.Percentual);
                                instrucaoMulta.Descricao = "Após o Vencimento multa de R$ " + objBoleto.ValorMulta + ".";
                                objBoleto.Instrucoes.Add(instrucaoMulta);

                                //objBoleto.ValorMulta = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(PercMulta)) / 100;
                                //objBoleto.Instrucoes.Add(new Instrucao_Itau(997, PercMulta, EnumTipoValor.Percentual));
                            }
                            
                            if (Bancos.Juros > 0)
                            {
                                double JurosMora = (double)Bancos.Juros;

                                string diaSemana = objBoleto.DataVencimento.ToString("ddd");
                                if (diaSemana == "sáb")
                                {
                                    objBoleto.DataJurosMora = objBoleto.DataVencimento.AddDays(2);
                                }
                                else
                                {
                                    objBoleto.DataJurosMora = objBoleto.DataVencimento.AddDays(1);
                                }


                                objBoleto.PercJurosMora = Convert.ToDecimal(JurosMora);
                                objBoleto.JurosMora = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(JurosMora)) / 100;
                                objBoleto.JurosMora = Decimal.Round(objBoleto.JurosMora, 2);

                                Instrucao_Itau instrucaoJuros = new Instrucao_Itau(998, JurosMora, EnumTipoValor.Percentual);
                                instrucaoJuros.Descricao = "Mais Juros de  R$ " + objBoleto.JurosMora + " ao dia";
                                objBoleto.Instrucoes.Add(instrucaoJuros);

                                //objBoleto.JurosMora = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(JurosMora)) / 100;
                                //objBoleto.JurosMora = Decimal.Round(objBoleto.JurosMora, 2);

                                //objBoleto.Instrucoes.Add(new Instrucao_Itau(998, JurosMora, EnumTipoValor.Percentual));
                            }

                        }



                       
                        objBoletoBancario.CodigoBanco = (short)CodigoBanco;
                        objBoletoBancario.Boleto = objBoleto;
                        objBoletoBancario.MostrarCodigoCarteira = true;
                        objBoletoBancario.Boleto.Valida();
                        objBoletoBancario.AjustaTamanhoFonte(12, 12, 12, 14);
                        //objBoletoBancario.MostrarComprovanteEntrega = true;

                        objBoletoBancario.MostrarEnderecoCedente = true;
                        objBoletoBancario.MostrarEnderecoCedentenoRecibo = true;
                        objBoletoBancario.OcultarEnderecoSacado = false;


                        var htmlBoleto = objBoletoBancario.MontaHtmlEmbedded();
                        string NomeArquivo = "boletos_" + DadosBoletoBancario.NumDocumento + ".html";

                        string Caminho = String.Empty;

                        Caminho = Arquivos.CriarArquivoHTML(htmlBoleto, NomeArquivo);  //FSUtils.LargeObjects.Arquivos.CriarArquivoHTML(htmlBoleto, NomeArquivo);


                        //System.Diagnostics.Process.Start(Caminho); chama o navegador e mostra o boleto

                        string NomeArquivoPdf = "boletos_" + DadosBoletoBancario.NumDocumento;

                        Caminho = Arquivos.CriarArquivoPdfDeHtml(htmlBoleto, NomeArquivoPdf);
                        InFiles.Add(Caminho);

                        //Update Nosso Numero de acordo com a rotina do boleto net
                        ;
                        using (BoletosGeradosRepository updateDigitoNossoNumero = new BoletosGeradosRepository())
                        {
                            try
                            {
                                updateDigitoNossoNumero.UpdateDvBoleto(IdBoleto, objBoleto.DigitoNossoNumero);

                            }
                            catch (VestilloException ex)
                            {
                                Funcoes.ExibirErro(ex);
                            }

                        }
                    }
                    else if (CodigoBanco == 1) //AQUI COMEÇA BANCO DO BRASIL
                    {

                        Cedente objCedente = new Cedente(empresa.CNPJ,
                        empresa.RazaoSocial,
                        Bancos.Agenciabanco,
                        Bancos.contabanco,
                        Bancos.DigitoContabanco);
                        objCedente.Codigo = Bancos.Convenio;
                        objCedente.CodigoTransmissao = Bancos.CodigoTransmissao;
                        objCedente.Convenio = Convert.ToInt64(Bancos.Convenio);

                        string xMun = String.Empty;
                        var municipio = new MunicipioIbgeService().GetServiceFactory().GetById(Convert.ToInt32(EndEmpresa.MunicipioId));
                        xMun = VestilloSession.PreparaTexto(municipio.Municipio);

                        var UF = new EstadoService().GetServiceFactory().GetById(EndEmpresa.EstadoId);

                        objCedente.Endereco = new Endereco()
                        {
                            End = EndEmpresa.Logradouro,
                            Bairro = EndEmpresa.Bairro,
                            CEP = EndEmpresa.CEP,
                            Cidade = xMun,
                            Numero = EndEmpresa.Numero,
                            UF = UF.Abreviatura
                        };

                        var objListaBoletos = new Boletos();

                        var objBanco = new Banco(CodigoBanco);

                        Boleto objBoleto = new Boleto(DadosCtsReceber.DataVencimento, DadosCtsReceber.ValorParcela, "17", DadosBoletoBancario.NossoNumero, objCedente);

                        var DadosSacado = new ColaboradorService().GetServiceFactory().GetById(DadosBoletoBancario.idCliente);



                        Sacado objSacado = new Sacado(DadosSacado.CnpjCpf, DadosSacado.RazaoSocial);

                        string xMunSacado = String.Empty;
                        var municipioSacado = new MunicipioIbgeService().GetServiceFactory().GetById(Convert.ToInt32(DadosSacado.IdMunicipio));
                        xMunSacado = VestilloSession.PreparaTexto(municipioSacado.Municipio);

                        var UFSacado = new EstadoService().GetServiceFactory().GetById(Convert.ToInt32(DadosSacado.IdEstado));


                        objBoletoBancario = new BoletoBancario();
                        objBoleto.Sacado = objSacado;
                        objBoleto.Sacado.Endereco.End = DadosSacado.Endereco + ", " + DadosSacado.Numero;
                        objBoleto.Sacado.Endereco.Bairro = DadosSacado.Bairro;
                        objBoleto.Sacado.Endereco.Cidade = xMunSacado;
                        objBoleto.Sacado.Endereco.CEP = DadosSacado.Cep;
                        objBoleto.Sacado.Endereco.UF = UFSacado.Abreviatura;

                        objBoleto.NumeroDocumento = DadosBoletoBancario.NumDocumento;
                        objBoleto.VariacaoCarteira = "019";

                        //objBoleto.PercMulta = Bancos.Multa;
                        //objBoleto.JurosMora = Bancos.Juros;

                        var InstrucaoBoleto = new InstrucoesDosBoletosService().GetServiceFactory().GetViewByDataEBoleto(dataEmissao, IdBoleto);


                        if (!String.IsNullOrWhiteSpace(Bancos.MensagemCaixa))
                        {
                            Instrucao_BancoBrasil instrucaoCaixa = new Instrucao_BancoBrasil();
                            instrucaoCaixa.Descricao = Bancos.MensagemCaixa;
                            objBoleto.Instrucoes.Add(instrucaoCaixa);

                        }

                        var dadosInstrucao = new InstrucoesPorBancosService().GetServiceFactory().GetAll();

                        if (CodigoBanco == 1)
                        {
                            foreach (var item in InstrucaoBoleto)
                            {
                                var DescricaoInstrucao = dadosInstrucao.Where(x => x.IdInstrucao == item.IdInstrucao).ToList();

                                Instrucao_BancoBrasil instrucaoGeral = new Instrucao_BancoBrasil
                                {
                                    Codigo = item.IdInstrucao,
                                    Descricao = DescricaoInstrucao[0].IdInstrucao != 1 ? DescricaoInstrucao[0].Descricao : ""
                                };

                                objBoleto.Instrucoes.Add(instrucaoGeral);
                            }

                            if (Bancos.Multa > 0)
                            {
                                double PercMulta = (double)Bancos.Multa;

                                objBoleto.PercMulta = Convert.ToDecimal(PercMulta);

                                string diaSemana = objBoleto.DataVencimento.ToString("ddd");

                                if (diaSemana == "sáb")
                                {
                                    objBoleto.DataMulta = objBoleto.DataVencimento.AddDays(2);
                                }
                                else
                                {
                                    objBoleto.DataMulta = objBoleto.DataVencimento.AddDays(1);
                                }


                                objBoleto.PercMulta = Convert.ToDecimal(PercMulta);
                                objBoleto.ValorMulta = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(PercMulta)) / 100;
                                objBoleto.ValorMulta = Decimal.Round(objBoleto.ValorMulta, 2);


                                Instrucao_BancoBrasil instrucaoMulta = new Instrucao_BancoBrasil(997, PercMulta, EnumTipoValor.Percentual);
                                instrucaoMulta.Descricao = "Após o Vencimento multa de R$ " + objBoleto.ValorMulta + ".";
                                objBoleto.Instrucoes.Add(instrucaoMulta);

                                //objBoleto.ValorMulta = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(PercMulta)) / 100;
                                //objBoleto.Instrucoes.Add(new Instrucao_Itau(997, PercMulta, EnumTipoValor.Percentual));
                            }

                            if (Bancos.Juros > 0)
                            {
                                double JurosMora = (double)Bancos.Juros;

                                string diaSemana = objBoleto.DataVencimento.ToString("ddd");
                                if (diaSemana == "sáb")
                                {
                                    objBoleto.DataJurosMora = objBoleto.DataVencimento.AddDays(2);
                                }
                                else
                                {
                                    objBoleto.DataJurosMora = objBoleto.DataVencimento.AddDays(1);
                                }


                                objBoleto.PercJurosMora = Convert.ToDecimal(JurosMora);
                                objBoleto.JurosMora = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(JurosMora)) / 100;
                                objBoleto.JurosMora = Decimal.Round(objBoleto.JurosMora, 2);

                                Instrucao_BancoBrasil instrucaoJuros = new Instrucao_BancoBrasil(998, JurosMora, EnumTipoValor.Percentual);
                                instrucaoJuros.Descricao = "Mais Juros de  R$ " + objBoleto.JurosMora + " ao dia";
                                objBoleto.Instrucoes.Add(instrucaoJuros);

                                //objBoleto.JurosMora = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(JurosMora)) / 100;
                                //objBoleto.JurosMora = Decimal.Round(objBoleto.JurosMora, 2);

                                //objBoleto.Instrucoes.Add(new Instrucao_Itau(998, JurosMora, EnumTipoValor.Percentual));
                            }

                        }




                        objBoletoBancario.CodigoBanco = (short)CodigoBanco;
                        objBoletoBancario.Boleto = objBoleto;
                        objBoletoBancario.MostrarCodigoCarteira = true;
                        objBoletoBancario.Boleto.Valida();
                        objBoletoBancario.AjustaTamanhoFonte(12, 12, 12, 14);
                        //objBoletoBancario.MostrarComprovanteEntrega = true;

                        objBoletoBancario.MostrarEnderecoCedente = true;
                        objBoletoBancario.MostrarEnderecoCedentenoRecibo = true;
                        objBoletoBancario.OcultarEnderecoSacado = false;


                        var htmlBoleto = objBoletoBancario.MontaHtmlEmbedded();
                        string NomeArquivo = "boletos_" + DadosBoletoBancario.NumDocumento + ".html";

                        string Caminho = String.Empty;

                        Caminho = Arquivos.CriarArquivoHTML(htmlBoleto, NomeArquivo);  //FSUtils.LargeObjects.Arquivos.CriarArquivoHTML(htmlBoleto, NomeArquivo);


                        //System.Diagnostics.Process.Start(Caminho); chama o navegador e mostra o boleto

                        string NomeArquivoPdf = "boletos_" + DadosBoletoBancario.NumDocumento;

                        Caminho = Arquivos.CriarArquivoPdfDeHtml(htmlBoleto, NomeArquivoPdf);
                        InFiles.Add(Caminho);

                        //Update Nosso Numero de acordo com a rotina do boleto net
                        
                        if(!String.IsNullOrEmpty(objBoleto.DigitoNossoNumero))
                        {
                            using (BoletosGeradosRepository updateDigitoNossoNumero = new BoletosGeradosRepository())
                            {
                                try
                                {
                                    updateDigitoNossoNumero.UpdateDvBoleto(IdBoleto, objBoleto.DigitoNossoNumero);

                                }
                                catch (VestilloException ex)
                                {
                                    Funcoes.ExibirErro(ex);
                                }

                            }
                        }
                        
                    }
                    



                }

                return objBoletoBancario;


                //COMEÇA BANCO DO BRASIL

            }
            catch (VestilloException ex )
            {
                
                Funcoes.ExibirErro(ex);
                return objBoletoBancario = new BoletoBancario();

            }

            
        }

        public bool GeraRemessa(int idBanco,int NumeroDaRemessa, List<int> IdBoleto, List<int> IdInstrucoes)
        {
            bool remessaGerada = false;
            var stream = new MemoryStream();
           

            var Bancos = new BancoService().GetServiceFactory().GetById(idBanco);
            var empresa = new EmpresaService().GetServiceFactory().GetById(VestilloSession.EmpresaLogada.Id);

            Boletos boletos = new Boletos();

            string dia = DateTime.Now.Day.ToString("d2");//duas casas, preenche com zero esquerda
            string mes = DateTime.Now.Month.ToString("d2");
            string ano = DateTime.Now.ToString("yy");
          
           


            string PastaRemessa = Bancos.DiretorioRemessa;
            string NomeArquivo = dia + mes + ano;
            string UltimoArquivoGerado = String.Empty;


            using (ContadorRemessaRepository ddRquivoRemessa = new ContadorRemessaRepository())
            {
                try
                {
                    UltimoArquivoGerado = ddRquivoRemessa.GetUltimoArquivoGerado(idBanco);

                }
                catch (VestilloException ex)
                {
                    Funcoes.ExibirErro(ex);
                }

            }

            string DiaDoUltimoGerado = UltimoArquivoGerado.ToString().Substring(0, UltimoArquivoGerado.Length - 1);

            string Letra = String.Empty;
            string NovaLetra = String.Empty;
            if (DiaDoUltimoGerado != NomeArquivo)
            {
                NovaLetra = "A";
            }
            else
            {
                Letra = UltimoArquivoGerado.ToString().Substring(UltimoArquivoGerado.Length - 1, 1);
                string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int pos = alfabeto.IndexOf(Letra);
                pos++;
                NovaLetra = Convert.ToString(alfabeto[pos]);
                
                
            }

            
           

            

            int CodigoBanco = Bancos.NomeBanco;

            if (CodigoBanco == 341)
            {
                var remessa = new ArquivoRemessa(TipoArquivo.CNAB400);
            }
            else
            {
                var remessa = new ArquivoRemessa(TipoArquivo.CNAB240);
            }

            var objBanco = new Banco(CodigoBanco);

            Cedente objCedente = new Cedente(empresa.CNPJ,
                    empresa.RazaoSocial,
                    Bancos.Agenciabanco,
                    Bancos.contabanco,
                    Bancos.DigitoContabanco);
            objCedente.Codigo = Bancos.Convenio;
            objCedente.CodigoTransmissao = Bancos.CodigoTransmissao;
            if (CodigoBanco == 1)
            {
                objCedente.Carteira = Bancos.carteira.ToString();
                objCedente.ContaBancaria.DigitoAgencia = Bancos.DigitoAgenciabanco;
            }
            

            Boleto UmBoleto = new Boleto();

            foreach (var idBoleto in IdBoleto)
            {
                UmBoleto = CriarObjetoBoletoBancarioRemessa(idBoleto, IdInstrucoes);
                var DadosBoletoBancario = new BoletosGeradosService().GetServiceFactory().GetById(idBoleto);
                UmBoleto.NossoNumero = DadosBoletoBancario.NossoNumero;
                boletos.Add(UmBoleto);

            }


            ArquivoRemessa arquivo;
            if (CodigoBanco == 341)
            {
                arquivo  = new ArquivoRemessa(TipoArquivo.CNAB400);
                
            }
            else
            {
                arquivo  = new ArquivoRemessa(TipoArquivo.CNAB240);
            }



            string vMsgRetorno = string.Empty;
            bool vValouOK = arquivo.ValidarArquivoRemessa(objCedente.Codigo, objBanco,objCedente, boletos, NumeroDaRemessa, out vMsgRetorno);
            if (!vValouOK)
            {
                MessageBox.Show(String.Concat("Foram localizados inconsistências na validação da remessa!", Environment.NewLine, vMsgRetorno),
                                "Vestillo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                remessaGerada = false;
            }
            else
            {
                arquivo.GerarArquivoRemessa(objCedente.Codigo, objBanco, objCedente, boletos, stream, NumeroDaRemessa);
                if (!Directory.Exists(PastaRemessa))
                {
                    MessageBox.Show(String.Concat("Pasta de gravação da remessa não localizada !", Environment.NewLine, vMsgRetorno),
                                "Vestillo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    
                    return false;

                }

                var CaminhoArquivo = PastaRemessa + "\\" + NomeArquivo + NovaLetra + ".txt";// DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString() + NumeroDaRemessa + ".txt";
                File.WriteAllBytes(CaminhoArquivo, stream.ToArray());

                MessageBox.Show("Arquivo gerado com sucesso!", "Vestillo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                remessaGerada = true;

                using (ContadorRemessaRepository ddRquivoRemessa = new ContadorRemessaRepository())
                {
                    try
                    {
                         ddRquivoRemessa.UpdateUltimoArquivoGerado(idBanco,NomeArquivo + NovaLetra);

                    }
                    catch (VestilloException ex)
                    {
                        Funcoes.ExibirErro(ex);
                    }

                }

            }
            return remessaGerada;
        }

        public Boleto CriarObjetoBoletoBancarioRemessa(int IdBoleto, List<int> IdInstrucoes)
        {
            Boleto objBoleto = new Boleto();
            try
            {

                
                var empresa = new EmpresaService().GetServiceFactory().GetById(VestilloSession.EmpresaLogada.Id);
                var DadosBoletoBancario = new BoletosGeradosService().GetServiceFactory().GetById(IdBoleto);
                var Bancos = new BancoService().GetServiceFactory().GetById(DadosBoletoBancario.idBanco);
                var EndEmpresa = new EmpresaService().GetServiceFactory().GetEndereco(VestilloSession.EmpresaLogada.Id);
                var DadosCtsReceber = new ContasReceberService().GetServiceFactory().GetById(DadosBoletoBancario.idTitulo);


                int CodigoBanco = Bancos.NomeBanco;

                BoletoBancario objBoletoBancario = new BoletoBancario();

                if (CodigoBanco == 341)
                {
                    Cedente objCedente = new Cedente(empresa.CNPJ,
                    empresa.RazaoSocial,
                    Bancos.Agenciabanco,
                    Bancos.contabanco,
                    Bancos.DigitoContabanco);
                    objCedente.Codigo = Bancos.Convenio;
                    objCedente.CodigoTransmissao = Bancos.CodigoTransmissao;


                    string xMun = String.Empty;
                    var municipio = new MunicipioIbgeService().GetServiceFactory().GetById(Convert.ToInt32(EndEmpresa.MunicipioId));
                    xMun = VestilloSession.PreparaTexto(municipio.Municipio);

                    var UF = new EstadoService().GetServiceFactory().GetById(EndEmpresa.EstadoId);

                    objCedente.Endereco = new Endereco()
                    {
                        End = EndEmpresa.Logradouro,
                        Bairro = EndEmpresa.Bairro,
                        CEP = EndEmpresa.CEP,
                        Cidade = xMun,
                        Numero = EndEmpresa.Numero,
                        UF = UF.Abreviatura
                    };

                    var objListaBoletos = new Boletos();

                    var objBanco = new Banco(CodigoBanco);

                    objBoleto = new Boleto(DadosCtsReceber.DataVencimento, DadosCtsReceber.ValorParcela, "109", DadosBoletoBancario.NossoNumero, objCedente);

                    var DadosSacado = new ColaboradorService().GetServiceFactory().GetById(DadosBoletoBancario.idCliente);



                    Sacado objSacado = new Sacado(DadosSacado.CnpjCpf, DadosSacado.RazaoSocial);

                    string xMunSacado = String.Empty;
                    var municipioSacado = new MunicipioIbgeService().GetServiceFactory().GetById(Convert.ToInt32(DadosSacado.IdMunicipio));
                    xMunSacado = VestilloSession.PreparaTexto(municipioSacado.Municipio);

                    var UFSacado = new EstadoService().GetServiceFactory().GetById(Convert.ToInt32(DadosSacado.IdEstado));


                    
                    objBoleto.Sacado = objSacado;
                    objBoleto.Sacado.Endereco.End = DadosSacado.Endereco + ", " + DadosSacado.Numero;
                    objBoleto.Sacado.Endereco.Bairro = DadosSacado.Bairro;
                    objBoleto.Sacado.Endereco.Cidade = xMunSacado;
                    objBoleto.Sacado.Endereco.CEP = DadosSacado.Cep;
                    objBoleto.Sacado.Endereco.UF = UFSacado.Abreviatura;

                    objBoleto.NumeroDocumento = DadosBoletoBancario.NumDocumento;


                    //objBoleto.PercMulta = Bancos.Multa;
                    //objBoleto.JurosMora = Bancos.Juros;
                    objBoleto.Avalista = new Cedente();
                    objBoleto.Avalista.Nome = empresa.RazaoSocial;
                    objBoleto.Avalista.CPFCNPJ = empresa.CNPJ;

                    objBoleto.Avalista.Endereco = new Endereco();
                    objBoleto.Avalista.Endereco.End = EndEmpresa.Logradouro;
                    objBoleto.Avalista.Endereco.Bairro = EndEmpresa.Bairro;
                    objBoleto.Avalista.Endereco.Cidade = xMun;
                    objBoleto.Avalista.Endereco.CEP = EndEmpresa.CEP;
                    objBoleto.Avalista.Endereco.UF = UF.Abreviatura;



                    //-----------------------


                    //--------------------------------------

                    if (!String.IsNullOrWhiteSpace(Bancos.MensagemCaixa))
                    {
                        Instrucao_Itau instrucaoCaixa = new Instrucao_Itau();
                        instrucaoCaixa.Descricao = Bancos.MensagemCaixa;
                        objBoleto.Instrucoes.Add(instrucaoCaixa);
                    }


                    var dadosInstrucao = new InstrucoesPorBancosService().GetServiceFactory().GetAll();

                    if (CodigoBanco == 341)
                    {
                        foreach (var itemInstrucao in IdInstrucoes) //Rodo cada instruçao selecionada pra ver se é desse boleto
                        {

                            var InstrucaoBoleto = new InstrucoesDosBoletosService().GetServiceFactory().GetViewByBoletoEInstrucao(IdBoleto, itemInstrucao);

                            if (InstrucaoBoleto != null && InstrucaoBoleto.Count() > 0)
                            {
                                foreach (var item in InstrucaoBoleto)
                                {
                                    var DescricaoInstrucao = dadosInstrucao.Where(x => x.IdInstrucao == item.IdInstrucao).ToList();

                                    Instrucao_Itau instrucaoGeral = new Instrucao_Itau
                                    {
                                        Codigo = item.IdInstrucao,
                                        Descricao = DescricaoInstrucao[0].Descricao
                                    };

                                    objBoleto.Instrucoes.Add(instrucaoGeral);


                                }
                            }
                        }

                        if (Bancos.Multa > 0)
                        {
                            double PercMulta = (double)Bancos.Multa;

                            objBoleto.PercMulta = Convert.ToDecimal(PercMulta);

                            string diaSemana = objBoleto.DataVencimento.ToString("ddd");

                            if (diaSemana == "sáb")
                            {
                                objBoleto.DataMulta = objBoleto.DataVencimento.AddDays(2);
                            }
                            else
                            {
                                objBoleto.DataMulta = objBoleto.DataVencimento.AddDays(1);
                            }


                            objBoleto.PercMulta = Convert.ToDecimal(PercMulta);
                            objBoleto.ValorMulta = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(PercMulta)) / 100;
                            objBoleto.ValorMulta = Decimal.Round(objBoleto.ValorMulta, 2);


                            Instrucao_Itau instrucaoMulta = new Instrucao_Itau(997, PercMulta, EnumTipoValor.Percentual);
                            instrucaoMulta.Descricao = "Após o Vencimento multa de R$ " + objBoleto.ValorMulta + ".";
                            objBoleto.Instrucoes.Add(instrucaoMulta);

                            //objBoleto.ValorMulta = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(PercMulta)) / 100;
                            //objBoleto.Instrucoes.Add(new Instrucao_Itau(997, PercMulta, EnumTipoValor.Percentual));
                        }

                        if (Bancos.Juros > 0)
                        {
                            double JurosMora = (double)Bancos.Juros;

                            string diaSemana = objBoleto.DataVencimento.ToString("ddd");

                            if (diaSemana == "sáb")
                            {
                                objBoleto.DataJurosMora = objBoleto.DataVencimento.AddDays(2);
                            }
                            else
                            {
                                objBoleto.DataJurosMora = objBoleto.DataVencimento.AddDays(1);
                            }


                            objBoleto.PercJurosMora = Convert.ToDecimal(JurosMora);
                            objBoleto.JurosMora = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(JurosMora)) / 100;
                            objBoleto.JurosMora = Decimal.Round(objBoleto.JurosMora, 2);

                            Instrucao_Itau instrucaoJuros = new Instrucao_Itau(998, JurosMora, EnumTipoValor.Percentual);
                            instrucaoJuros.Descricao = "Mais Juros de  R$ " + objBoleto.JurosMora + " ao dia";
                            objBoleto.Instrucoes.Add(instrucaoJuros);

                            //objBoleto.JurosMora = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(JurosMora)) / 100;
                            //objBoleto.JurosMora = Decimal.Round(objBoleto.JurosMora, 2);

                            //objBoleto.Instrucoes.Add(new Instrucao_Itau(998, JurosMora, EnumTipoValor.Percentual));
                        }

                    }

                    objBoletoBancario.CodigoBanco = (short)CodigoBanco;
                    objBoletoBancario.Boleto = objBoleto;
                    objBoletoBancario.MostrarCodigoCarteira = true;
                    objBoletoBancario.Boleto.Valida();
                    objBoletoBancario.AjustaTamanhoFonte(12, 12, 12, 14);
                    //objBoletoBancario.MostrarComprovanteEntrega = true;

                    objBoletoBancario.MostrarEnderecoCedente = true;
                    objBoletoBancario.MostrarEnderecoCedentenoRecibo = true;
                    objBoletoBancario.OcultarEnderecoSacado = false;


                }
                else if(CodigoBanco ==1)
                {
                    Cedente objCedente = new Cedente(empresa.CNPJ,
                    empresa.RazaoSocial,
                    Bancos.Agenciabanco,
                    Bancos.contabanco,
                    Bancos.DigitoContabanco);
                    objCedente.Codigo = Bancos.Convenio;
                    objCedente.CodigoTransmissao = Bancos.CodigoTransmissao;
                    objCedente.ContaBancaria.DigitoAgencia = Bancos.DigitoAgenciabanco;
                    objCedente.Convenio = Convert.ToInt64(Bancos.Convenio);



                    string xMun = String.Empty;
                    var municipio = new MunicipioIbgeService().GetServiceFactory().GetById(Convert.ToInt32(EndEmpresa.MunicipioId));
                    xMun = VestilloSession.PreparaTexto(municipio.Municipio);

                    var UF = new EstadoService().GetServiceFactory().GetById(EndEmpresa.EstadoId);

                    objCedente.Endereco = new Endereco()
                    {
                        End = EndEmpresa.Logradouro,
                        Bairro = EndEmpresa.Bairro,
                        CEP = EndEmpresa.CEP,
                        Cidade = xMun,
                        Numero = EndEmpresa.Numero,
                        UF = UF.Abreviatura
                    };

                    var objListaBoletos = new Boletos();

                    var objBanco = new Banco(CodigoBanco);

                    objBoleto = new Boleto(DadosCtsReceber.DataVencimento, DadosCtsReceber.ValorParcela, "17", DadosBoletoBancario.NossoNumero, objCedente);

                    var DadosSacado = new ColaboradorService().GetServiceFactory().GetById(DadosBoletoBancario.idCliente);

                    /*
                    var Remessa = new BoletoNet.Remessa();
                    Remessa.TipoDocumento = "1";
                    objBoleto.Remessa = Remessa;
                    */

                    Sacado objSacado = new Sacado(DadosSacado.CnpjCpf, DadosSacado.RazaoSocial);

                    string xMunSacado = String.Empty;
                    var municipioSacado = new MunicipioIbgeService().GetServiceFactory().GetById(Convert.ToInt32(DadosSacado.IdMunicipio));
                    xMunSacado = VestilloSession.PreparaTexto(municipioSacado.Municipio);

                    var UFSacado = new EstadoService().GetServiceFactory().GetById(Convert.ToInt32(DadosSacado.IdEstado));



                    objBoleto.Sacado = objSacado;
                    objBoleto.Sacado.Endereco.End = DadosSacado.Endereco + ", " + DadosSacado.Numero;
                    objBoleto.Sacado.Endereco.Bairro = DadosSacado.Bairro;
                    objBoleto.Sacado.Endereco.Cidade = xMunSacado;
                    objBoleto.Sacado.Endereco.CEP = DadosSacado.Cep;
                    objBoleto.Sacado.Endereco.UF = UFSacado.Abreviatura;

                    objBoleto.NumeroDocumento = DadosBoletoBancario.NumDocumento;
                    objBoleto.VariacaoCarteira = "019";


                    //objBoleto.PercMulta = Bancos.Multa;
                    //objBoleto.JurosMora = Bancos.Juros;




                    if (!String.IsNullOrWhiteSpace(Bancos.MensagemCaixa))
                    {
                        Instrucao_BancoBrasil instrucaoCaixa = new Instrucao_BancoBrasil();
                        instrucaoCaixa.Descricao = Bancos.MensagemCaixa;
                        objBoleto.Instrucoes.Add(instrucaoCaixa);
                    }


                    var dadosInstrucao = new InstrucoesPorBancosService().GetServiceFactory().GetAll();

                    if (CodigoBanco == 1)
                    {
                        foreach (var itemInstrucao in IdInstrucoes) //Rodo cada instruçao selecionada pra ver se é desse boleto
                        {

                            var InstrucaoBoleto = new InstrucoesDosBoletosService().GetServiceFactory().GetViewByBoletoEInstrucao(IdBoleto, itemInstrucao);

                            if (InstrucaoBoleto != null && InstrucaoBoleto.Count() > 0)
                            {
                                foreach (var item in InstrucaoBoleto)
                                {
                                    var DescricaoInstrucao = dadosInstrucao.Where(x => x.IdInstrucao == item.IdInstrucao).ToList();

                                    Instrucao_BancoBrasil instrucaoGeral = new Instrucao_BancoBrasil
                                    {
                                        Codigo = item.IdInstrucao,
                                        Descricao = DescricaoInstrucao[0].Descricao
                                    };

                                    objBoleto.Instrucoes.Add(instrucaoGeral);


                                }
                            }
                        }

                        if (Bancos.Multa > 0)
                        {
                            double PercMulta = (double)Bancos.Multa;

                            objBoleto.PercMulta = Convert.ToDecimal(PercMulta);

                            string diaSemana = objBoleto.DataVencimento.ToString("ddd");

                            if (diaSemana == "sáb")
                            {
                                objBoleto.DataMulta = objBoleto.DataVencimento.AddDays(2);
                            }
                            else
                            {
                                objBoleto.DataMulta = objBoleto.DataVencimento.AddDays(1);
                            }


                            objBoleto.PercMulta = Convert.ToDecimal(PercMulta);
                            objBoleto.ValorMulta = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(PercMulta)) / 100;
                            objBoleto.ValorMulta = Decimal.Round(objBoleto.ValorMulta, 2);


                            Instrucao_BancoBrasil instrucaoMulta = new Instrucao_BancoBrasil(997, PercMulta, EnumTipoValor.Percentual);
                            instrucaoMulta.Descricao = "Após o Vencimento multa de R$ " + objBoleto.ValorMulta + ".";
                            objBoleto.Instrucoes.Add(instrucaoMulta);

                            //objBoleto.ValorMulta = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(PercMulta)) / 100;
                            //objBoleto.Instrucoes.Add(new Instrucao_Itau(997, PercMulta, EnumTipoValor.Percentual));
                        }

                        if (Bancos.Juros > 0)
                        {
                            double JurosMora = (double)Bancos.Juros;

                            string diaSemana = objBoleto.DataVencimento.ToString("ddd");

                            if (diaSemana == "sáb")
                            {
                                objBoleto.DataJurosMora = objBoleto.DataVencimento.AddDays(2);
                            }
                            else
                            {
                                objBoleto.DataJurosMora = objBoleto.DataVencimento.AddDays(1);
                            }


                            objBoleto.PercJurosMora = Convert.ToDecimal(JurosMora);
                            objBoleto.JurosMora = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(JurosMora)) / 100;
                            objBoleto.JurosMora = Decimal.Round(objBoleto.JurosMora, 2);

                            Instrucao_BancoBrasil instrucaoJuros = new Instrucao_BancoBrasil(998, JurosMora, EnumTipoValor.Percentual);
                            instrucaoJuros.Descricao = "Mais Juros de  R$ " + objBoleto.JurosMora + " ao dia";
                            objBoleto.Instrucoes.Add(instrucaoJuros);

                            //objBoleto.JurosMora = (DadosCtsReceber.ValorParcela * Convert.ToDecimal(JurosMora)) / 100;
                            //objBoleto.JurosMora = Decimal.Round(objBoleto.JurosMora, 2);

                            //objBoleto.Instrucoes.Add(new Instrucao_Itau(998, JurosMora, EnumTipoValor.Percentual));
                        }

                    }

                    objBoletoBancario.CodigoBanco = (short)CodigoBanco;
                    objBoletoBancario.Boleto = objBoleto;
                    objBoletoBancario.MostrarCodigoCarteira = true;
                    objBoletoBancario.Boleto.Valida();
                    objBoletoBancario.AjustaTamanhoFonte(12, 12, 12, 14);
                    //objBoletoBancario.MostrarComprovanteEntrega = true;

                    objBoletoBancario.MostrarEnderecoCedente = true;
                    objBoletoBancario.MostrarEnderecoCedentenoRecibo = true;
                    objBoletoBancario.OcultarEnderecoSacado = false;
                }


                return objBoletoBancario.Boleto;

            }
            catch (VestilloException ex)
            {

                Funcoes.ExibirErro(ex);
                return objBoleto = new Boleto();

            }

        }

        public  void GerarArquivoRemessa(int idBanco, Boletos boletos, int numeroArquivoRemessa)
        {
            /*
            try
            {
                saveFileDialog.Filter = "Arquivos de Retorno (*.rem)|*.rem|Todos Arquivos (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ArquivoRemessa arquivo = new ArquivoRemessa(TipoArquivo.CNAB400);

                    //Valida a Remessa Correspondentes antes de Gerar a mesma...
                    string vMsgRetorno = string.Empty;
                    bool vValouOK = arquivo.ValidarArquivoRemessa(cedente.Convenio.ToString(), banco, cedente, boletos, 1, out vMsgRetorno);
                    if (!vValouOK)
                    {
                        MessageBox.Show(String.Concat("Foram localizados inconsistências na validação da remessa!", Environment.NewLine, vMsgRetorno),
                                        "Teste",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        arquivo.GerarArquivoRemessa("0", banco, cedente, boletos, saveFileDialog.OpenFile(), 1);

                        MessageBox.Show("Arquivo gerado com sucesso!", "Teste",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        public List<clsTitulosRetornados> retornorTitulosDoArquivo(string CaminhoDoArquivo,int Banco)
        {
            clsArquivoCNAB oArquivoCNAB = new clsArquivoCNAB();

            if(Banco == 341)
            {
                oArquivoCNAB.Banco = enuBancos.Itau;
                oArquivoCNAB.conteudoDoArquivo = retornarConteudoDoArquivo(CaminhoDoArquivo);
                oArquivoCNAB.tipoDoArquivo = enuTipoDeArquivo.CNAB400;
            }
            else if(Banco == 1)
            {
                oArquivoCNAB.Banco = enuBancos.Brasil;
                oArquivoCNAB.conteudoDoArquivo = retornarConteudoDoArquivo(CaminhoDoArquivo);
                oArquivoCNAB.tipoDoArquivo = enuTipoDeArquivo.CNAB240;
            }

            

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(oArquivoCNAB);

            string retorno = InformacoesApi.ChamarAPI("RetornoCNAB", json);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<clsTitulosRetornados>>(retorno);
        }


        private string retornarConteudoDoArquivo(string CaminhoDoArquivo)
        {
            var arquivoret = new MemoryStream(File.ReadAllBytes(CaminhoDoArquivo));

            //File.WriteAllBytes("C:\\TESTE\\CN04080A.RET", arquivoret.ToArray());

            System.Text.StringBuilder Conteudo = new System.Text.StringBuilder();
            System.IO.StreamReader ArquivoTxt = new System.IO.StreamReader(arquivoret);

            do
                Conteudo.AppendLine(ArquivoTxt.ReadLine());
            while (!ArquivoTxt.EndOfStream);

            return Conteudo.ToString();
        }

        public enum enuTipoDeArquivo
        {
            CNAB240 = 0,
            CNAB400 = 1,
            CBR643 = 2
        }


        public enum enuBancos
        {
            Itau = 341,
            Brasil = 1
        }

        public class clsArquivoCNAB
        {
            public enuBancos Banco { get; set; }

            public string conteudoDoArquivo { get; set; }

            public enuTipoDeArquivo tipoDoArquivo { get; set; }
        }

        public class clsTitulosRetornados
        {
            public string NossoNumero { get; set; }
            public string NumeroDocumento { get; set; }
            public string NumeroControle { get; set; }
            public decimal ValorPago { get; set; }
            public DateTime DataOcorrencia { get; set; }
            public DateTime DataVencimento { get; set; }
            public short CodigoOcorrencia { get; set; }
            public List<string> MotivosOcorrencia { get; set; }
            public DateTime DataCredito { get; set; }
            public decimal ValorTaxaCobranca { get; set; }
            public decimal ValorCredito { get; set; }
            public decimal ValorMultaPaga { get; set; }
            public decimal ValorOutrosAcrescimos { get; set; }
            public decimal ValorDesconto { get; set; }
            public decimal ValorJurosPago { get; set; }
            public bool Pagamento { get; set; }
            public string NomeSacado { get; set; }
        }





    }
}
