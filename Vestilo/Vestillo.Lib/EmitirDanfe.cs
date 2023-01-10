using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotaFiscalEletronica;
using NotaFiscalEletronica.NFe;
using NotaFiscalEletronica.NFe.Classes.Distribuicao;
using NotaFiscalEletronica.NFe.Classes.Envio.evento;



namespace Vestillo.Lib
{
    public class EmitirDanfe
    {

        public void VisualizarDanfe(string xmlLote,string caminho,clsTipoDeServico oTipoServico, int ModNota,string Observacao,string CaminhoImagem,int TipoImpressao,string idDaNota, string Protocolo, DateTime dhRecbto)
        {
            string obs = string.Empty;
            try
            {


                var logo = new System.Drawing.Bitmap(CaminhoImagem);


                NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe _NFe = new NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe();
                NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe oLeituraNFe = new NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe();
                var protocolo = new NotaFiscalEletronica.NFe.Classes.Retorno.retConsReciNFe.protNFe();
                oLeituraNFe.LerXmlNFe(xmlLote, ref _NFe);

                NotaFiscalEletronica.NFe.FNotaFiscalEletronica.Versao = Uteis.enuVersao.Versao400;
                NotaFiscalEletronica.NFe.Operacoes.EmitirDANFE oEmissaoDANFE = NotaFiscalEletronica.NFe.FNotaFiscalEletronica.CriarInstancia(ModNota == 55 ? NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFe : NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFCe).InstanciarEmitirDANFE();

                long Nprotocolo = Convert.ToInt64(Protocolo);
                //var oImpressao = FNotaFiscalEletronica.CriarInstancia(ModNota == 55?NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFe: NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFCe).InstanciarEmitirDANFE();

                oEmissaoDANFE.EmitirDANFE(_NFe, ("NFe"
                                + (idDaNota)), Convert.ToInt64(Protocolo), dhRecbto,
                                TipoImpressao == 1 ? NotaFiscalEletronica.NFe.Operacoes.EmitirDANFE.enuTipoDeImpressao.VisualizarImpressao : NotaFiscalEletronica.NFe.Operacoes.EmitirDANFE.enuTipoDeImpressao.GerarEmArquivoPDF,
                                logo, caminho, oTipoServico, obs);
            }
            catch(VestilloException ex )
            {
                Funcoes.ExibirErro(ex);
            }
        }

        public void VisualizarCCe(string xmlCCE, string caminho, clsTipoDeServico oTipoServico, string idDaNota, string Protocolo, DateTime dhRecbto, DateTime dhGeradaCCe, string MensagemCCe,long ProtocoloDoEvento,short SeqEvento, string NomeArquivo, string CaminhoPdf)
        {

            try
            {
                NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe _NFe = new NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe();
                NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe oLeituraNFe = new NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe();
                var protocolo = new NotaFiscalEletronica.NFe.Classes.Retorno.retConsReciNFe.protNFe();

                oLeituraNFe.LerXmlNFe(xmlCCE, ref _NFe, ref protocolo);



                NotaFiscalEletronica.NFe.FNotaFiscalEletronica.Versao = Uteis.enuVersao.Versao400;
                NotaFiscalEletronica.NFe.Operacoes.ImprimirCartaDeCorrecao oImpressao = NotaFiscalEletronica.NFe.FNotaFiscalEletronica.CriarInstancia(NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFe).InstanciarImprimirCartaDeCorrecao();



                long Nprotocolo = Convert.ToInt64(Protocolo);
                //var oImpressao = FNotaFiscalEletronica.CriarInstancia(ModNota == 55?NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFe: NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFCe).InstanciarEmitirDANFE();

                // var caminhoo2 = "impressao-evento\\ID & 110110 & ChaveDeAcessoNfe & DvDaChaveDeAcessoNfe & SequenciadoEvento(formato "D2") & -evento.pdf"


                oImpressao.ImprimirCartaDeCorrecao(_NFe, ("NFe"
                                    + (idDaNota)), Convert.ToInt64(Protocolo), dhRecbto, true, dhGeradaCCe, dhGeradaCCe, "Correção: " + MensagemCCe, ProtocoloDoEvento, "EVENTO_AUTORIZADO", SeqEvento, MensagemCCe, caminho, CaminhoPdf + "\\" + NomeArquivo + ".pdf");



                oImpressao.ImprimirCartaDeCorrecao(_NFe, ("NFe"
                                    + (idDaNota)), Convert.ToInt64(Protocolo), dhRecbto, false, dhGeradaCCe, dhGeradaCCe, "Correção: " + MensagemCCe, ProtocoloDoEvento, "EVENTO_AUTORIZADO", SeqEvento, MensagemCCe, caminho, CaminhoPdf + "\\" + NomeArquivo + ".pdf");

            }
            catch (VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }

        }

        public void EmitirEspelhoNFe(NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe oNFe, clsTipoDeServico oTipoServico, string Observacao)
        {
            try
            {
                DateTime? dia = null;
                NotaFiscalEletronica.NFe.Operacoes.EmitirDANFE oEmissaoEspelhoDANFE = NotaFiscalEletronica.NFe.FNotaFiscalEletronica.CriarInstancia(oTipoServico).InstanciarEmitirDANFE();
                //NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe _NFe = new NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe();
                NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe oLeituraNFe = new NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe();
                var protocolo = new NotaFiscalEletronica.NFe.Classes.Retorno.retConsReciNFe.protNFe();
                //oLeituraNFe.LerXmlNFe(xmlLote, ref _NFe);

                oEmissaoEspelhoDANFE.EmitirEspelhoDANFE(oNFe, String.Empty, 0, Convert.ToDateTime(dia), false, String.Empty, String.Empty, oTipoServico, Observacao);
            }
            catch (VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }


        }

        public void VisualizarNFCe(string xmlLote, string diaEmissao, string caminho, string NomeArquivo, string Observacao,string idDaNota, string Protocolo, DateTime dhRecbto,decimal TotaisTrib, string CSC,string CIDTOKEN, bool ConsInformado,short NumVias, List<NotaFiscalEletronica.DANFE.clsDuplicatas> lstDuplicata,bool exibirReport = true)
        {

            try
            {
                NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe _NFe = new NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe();
                NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe oLeituraNFe = new NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe();

                oLeituraNFe.LerXmlNFe(xmlLote, ref _NFe);

                

                

                long Nprotocolo = Convert.ToInt64(Protocolo);

                NotaFiscalEletronica.NFe.FNotaFiscalEletronica.Versao = Uteis.enuVersao.Versao400;
                NotaFiscalEletronica.DANFE.NFCe.InformacaoParaImpressao objImpressao = new NotaFiscalEletronica.DANFE.NFCe.InformacaoParaImpressao
                {
                    CONSUMIDOR_INFORMADO = ConsInformado,
                    NUMERODEVIAS = NumVias,
                    NFe = _NFe,
                    DT_PROTOCOLO = dhRecbto,
                    NUMERO_DO_PROTOCOLO = Nprotocolo,
                    CHAVE_DE_ACESSO = idDaNota,
                    NOTA_AUTORIZADA = true,
                    XML = xmlLote,
                    ImprimirDetalhe = true,
                    OBSERVACAOINSTITUCIONAL = Observacao,
                    IMPOSTO_APROXIMADO_FEDERAL_VALOR = 0,
                    IMPOSTO_APROXIMADO_ESTADUAL_VALOR = TotaisTrib,
                    CSC = CSC,
                    CIDTOKEN = CIDTOKEN,
                    lstDuplicata = lstDuplicata
                };

                //DateTime.Parse(_dataRecebido)

                GerarPdf(xmlLote, diaEmissao, caminho, NomeArquivo, Observacao, idDaNota, Protocolo, dhRecbto, TotaisTrib, CSC, CIDTOKEN, ConsInformado, NumVias, lstDuplicata);
                
                if(exibirReport)
                {
                    NotaFiscalEletronica.DANFE.NFCe.NFCeLoader.ShowReport(objImpressao, true);
                }
                
            }
            catch (VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }

        }

        public void EmitirNFCe(string xmlLote, string diaEmissao, string NomeArquivo, string caminho, string Observacao,string idDaNota, string Protocolo, DateTime dhRecbto,decimal TotaisTrib, string CSC,string CIDTOKEN, bool ConsInformado,short NumVias, List<NotaFiscalEletronica.DANFE.clsDuplicatas> lstDuplicata, string CaminhoImagem,string NomeImpressora)
        {

            try
            {
                NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe _NFe = new NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe();
                NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe oLeituraNFe = new NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe();

                oLeituraNFe.LerXmlNFe(xmlLote, ref _NFe);

                //NotaFiscalEletronica.DANFE.NFCe.Interface.IimpressaoTermicaNFCe oImpressoraTermica =  NotaFiscalEletronica.DANFE.NFCe.Fabrica.FDanfeNFCe.InstanciarDanfeNFCe(NotaFiscalEletronica.DANFE.NFCe.Interface.IConfiguracaoDeImpressao.enuDrive.Generica);

                var oImpressoraTermica =  NotaFiscalEletronica.DANFE.NFCe.Fabrica.FDanfeNFCe.InstanciarDanfeNFCe(NotaFiscalEletronica.DANFE.NFCe.Interface.IConfiguracaoDeImpressao.enuDrive.Generica);


                if (File.Exists(CaminhoImagem))
                {
                    
                }



                oImpressoraTermica.ALINHAMENTO = 46;
                oImpressoraTermica.DiretorioArquivoLogoImprNFCe = CaminhoImagem;
                oImpressoraTermica.NUMERO_LINHAS_NO_FINAL= 0;
                oImpressoraTermica.NUMERO_COLUNAS_IMPRESSORA = 46;
                oImpressoraTermica.NOME_IMPRESSORA = NomeImpressora;
                


                long Nprotocolo = String.IsNullOrEmpty(Protocolo) ? 0 : Convert.ToInt64(Protocolo);

                NotaFiscalEletronica.NFe.FNotaFiscalEletronica.Versao = Uteis.enuVersao.Versao400;
                NotaFiscalEletronica.DANFE.NFCe.InformacaoParaImpressao objImpressao = new NotaFiscalEletronica.DANFE.NFCe.InformacaoParaImpressao
                {
                    CONSUMIDOR_INFORMADO = ConsInformado,
                    NUMERODEVIAS = NumVias,
                    NFe = _NFe,
                    DT_PROTOCOLO = dhRecbto,
                    NUMERO_DO_PROTOCOLO = Nprotocolo,
                    CHAVE_DE_ACESSO = idDaNota,
                    NOTA_AUTORIZADA = true,
                    XML = xmlLote,
                    ImprimirDetalhe = true,
                    OBSERVACAOINSTITUCIONAL = Observacao,
                    IMPOSTO_APROXIMADO_FEDERAL_VALOR = 0,
                    IMPOSTO_APROXIMADO_ESTADUAL_VALOR = TotaisTrib,
                    CSC = CSC,
                    CIDTOKEN = CIDTOKEN,
                    lstDuplicata = lstDuplicata
                };

                //DateTime.Parse(_dataRecebido)

                GerarPdf( xmlLote, diaEmissao, caminho, NomeArquivo,  Observacao,  idDaNota,  Protocolo,  dhRecbto,  TotaisTrib,  CSC,  CIDTOKEN,  ConsInformado,  NumVias,lstDuplicata);

                NotaFiscalEletronica.DANFE.NFCe.Termica.ImprimirNFCeTermica oImprimirNFCe = new NotaFiscalEletronica.DANFE.NFCe.Termica.ImprimirNFCeTermica(objImpressao, oImpressoraTermica);
                oImprimirNFCe.Imprimir();

                
            }
            catch (VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }
        }

        public void GerarPdf(string xmlLote, string diaEmissao, string caminho, string NomeArquivo, string Observacao, string idDaNota, string Protocolo, DateTime dhRecbto, decimal TotaisTrib, string CSC, string CIDTOKEN, bool ConsInformado, short NumVias, List<NotaFiscalEletronica.DANFE.clsDuplicatas> lstDuplicata)
        {
            

            var PastaXml = diaEmissao.ToString().Substring(3, 2) + diaEmissao.ToString().Substring(6, 4);

            var PathXmls = caminho + @"\NFCe\" + @"XmlDestinatario\" + PastaXml + "\\" + NomeArquivo;

            NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe _NFe = new NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe();
            NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe oLeituraNFe = new NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe();

            oLeituraNFe.LerXmlNFe(xmlLote, ref _NFe);


            long Nprotocolo = Convert.ToInt64(Protocolo);

            NotaFiscalEletronica.NFe.FNotaFiscalEletronica.Versao = Uteis.enuVersao.Versao400;
            NotaFiscalEletronica.DANFE.NFCe.InformacaoParaImpressao objImpressao = new NotaFiscalEletronica.DANFE.NFCe.InformacaoParaImpressao
            {
                CONSUMIDOR_INFORMADO = ConsInformado,
                NUMERODEVIAS = NumVias,
                NFe = _NFe,
                DT_PROTOCOLO = dhRecbto,
                NUMERO_DO_PROTOCOLO = Nprotocolo,
                CHAVE_DE_ACESSO = idDaNota,
                NOTA_AUTORIZADA = true,
                XML = xmlLote,
                ImprimirDetalhe = true,
                OBSERVACAOINSTITUCIONAL = Observacao,
                IMPOSTO_APROXIMADO_FEDERAL_VALOR = 0,
                IMPOSTO_APROXIMADO_ESTADUAL_VALOR = TotaisTrib,
                CSC = CSC,
                CIDTOKEN = CIDTOKEN,
                lstDuplicata = lstDuplicata
            };

            NotaFiscalEletronica.DANFE.NFCe.NFCeLoader.ExportarParaPDF(objImpressao, PathXmls);
        }


        
        

    }
}
