using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotaFiscalEletronica;
using NotaFiscalEletronica.NFe;
using NotaFiscalEletronica.NFe.Classes.Distribuicao;
using NotaFiscalEletronica.NFe.Classes.Envio.evento;

using System.IO;

namespace Vestillo.Lib
{
    public class EventosNFe
    {
        public string _startPath;
        public byte _tpAmb;
        public string _startPathNota;        
        public byte _IdUfEmpresa;
        public string _CNPJ;
        public string _ufAbreviaturaEmpresa;
        public string _Message;

        public  bool EnviarEventoDeCancelamentoOuCartaCorrecaoNFe(string chaveNFe, string textoCorrecaoOUJustificativa, long numProtocolo, DateTime dataEHoraEvto, byte seqEvto, string certificadoDigital, NotaFiscalEletronica.NFe.Classes.Distribuicao.DistribuicaoDeEventoNFe.enuTipoEvento tipoDeEvento, int TipoDocumento)
        {
            bool dadoscarregados = false;
            

            DateTime dia = DateTime.Now;
            string PastaXml = String.Empty; ;
            string  PathXmls = String.Empty;
            string diaEmissao = dia.ToShortDateString();


            PastaXml = diaEmissao.ToString().Substring(3, 2) + diaEmissao.ToString().Substring(6, 4);


            if (TipoDocumento == 1)
            {
                 PathXmls = _startPath + @"\NFe\" + @"XmlDestinatario\" + PastaXml + "\\";
            }
            else
            {
                 PathXmls = _startPath + @"\NFCe\" + @"XmlDestinatario\" + PastaXml + "\\";
            }


            string MsgErroAoCarregar = String.Empty;
            var lstEvento = new List<NotaFiscalEletronica.NFe.Classes.Envio.evento.evento>();

            var lstEventoComErroAoGerar = new List<NotaFiscalEletronica.NFe.clsAuxEventoComErroAoGerar>();


            NotaFiscalEletronica.NFe.clsAuxEventoComErroAoGerar oEventoComErroAoGerar = new NotaFiscalEletronica.NFe.clsAuxEventoComErroAoGerar();
            NotaFiscalEletronica.NFe.Classes.Envio.evento.evento oEvento = null;
            if (tipoDeEvento == NotaFiscalEletronica.NFe.Classes.Distribuicao.DistribuicaoDeEventoNFe.enuTipoEvento.Cancelamento)
            {
                oEvento = CriarObjEventoDeCancelamento(chaveNFe, textoCorrecaoOUJustificativa, numProtocolo, seqEvto, dataEHoraEvto);
            }
            else if (tipoDeEvento == NotaFiscalEletronica.NFe.Classes.Distribuicao.DistribuicaoDeEventoNFe.enuTipoEvento.Carta_de_Correcao)
            {
                oEvento = CriarObjEventoDeCartaCorrecao(chaveNFe, textoCorrecaoOUJustificativa, seqEvto, dataEHoraEvto);

            }


            lstEvento.Add(oEvento);



            string MsgErroAoGerarXml = String.Empty;
            List<NotaFiscalEletronica.NFe.clsAuxEventoComErroAoGerar> lstEventoComErroAoGerarXml = new List<NotaFiscalEletronica.NFe.clsAuxEventoComErroAoGerar>();

            //string CaminhoXMLESchemas = RetornarPastaDoXML(DateTime.Today, false);

            var objTipoServico = new NotaFiscalEletronica.NFe.clsTipoDeServico(_tpAmb == 2 ? NotaFiscalEletronica.NFe.clsTipoDeServico.enuTipoDeAmbiente.Homologacao : clsTipoDeServico.enuTipoDeAmbiente.Producao, _ufAbreviaturaEmpresa, false, TipoDocumento == 1 ? NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFe : NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFCe, Uteis.enuVersao.Versao400);

            NotaFiscalEletronica.NFe.GeracaoXML.GerarXmlEventoDeNFe oGeracaoXmlEventoDeNFe = new NotaFiscalEletronica.NFe.GeracaoXML.GerarXmlEventoDeNFe();
            oGeracaoXmlEventoDeNFe.GerarEventoDeNFe(lstEvento, ref MsgErroAoGerarXml, ref lstEventoComErroAoGerarXml, certificadoDigital, _startPathNota, objTipoServico);



            if ((!string.IsNullOrEmpty(MsgErroAoCarregar) && !string.IsNullOrEmpty(MsgErroAoGerarXml)))
            {
                throw new Exception((MsgErroAoCarregar + ("\r\n" + MsgErroAoGerarXml)));
            }
            else if (!string.IsNullOrEmpty(MsgErroAoCarregar))
            {
                throw new Exception(MsgErroAoCarregar);
            }
            else if (!string.IsNullOrEmpty(MsgErroAoGerarXml))
            {
                throw new Exception(MsgErroAoGerarXml);
            }

            List<NotaFiscalEletronica.NFe.Classes.Distribuicao.DistribuicaoDeEventoNFe> lstDistribuicaoDeEventoNFe = CriarListaDistribuicaoDeEventoNFe(chaveNFe, seqEvto, tipoDeEvento, oEvento, _startPathNota, TipoDocumento);

            var oEnvio = new NotaFiscalEletronica.NFe.Operacoes.EnviarEventoDeNFe();
            // esse método também gera o xml de distribuição:lstDistribuicaoDeEventoNFe 
            var retEnvEvento = oEnvio.EnviarEventoDeNFe(lstDistribuicaoDeEventoNFe.Select(i => i.ConteudoXmlEnvio).ToList(), certificadoDigital, _startPathNota, objTipoServico, ref lstDistribuicaoDeEventoNFe);


            var objChaveAcesso = new ChaveDeAcesso(chaveNFe);// helper da fullscreen para extrair informações da chave de acesso
            var itemDistribuicaoEventoNfe = lstDistribuicaoDeEventoNFe[0];

            string nomeXML = String.Empty;
            if (tipoDeEvento == NotaFiscalEletronica.NFe.Classes.Distribuicao.DistribuicaoDeEventoNFe.enuTipoEvento.Cancelamento)
            {                
                nomeXML = "Num_Nota_" + objChaveAcesso.Numero_documento.ToString() + "-" + objChaveAcesso.Serie.ToString() + "Canc_" + ".xml";
            }
            else if (tipoDeEvento == NotaFiscalEletronica.NFe.Classes.Distribuicao.DistribuicaoDeEventoNFe.enuTipoEvento.Carta_de_Correcao)
            {
                string PastaXmlCCe = "";
                

                PastaXmlCCe = diaEmissao.ToString().Substring(3, 2) + diaEmissao.ToString().Substring(6, 4);

                PathXmls = _startPath + @"\NFe\" + @"XmlDestinatario\CCe\" + PastaXmlCCe + "\\";
                if (Directory.Exists(PathXmls) == false)
                {
                    Directory.CreateDirectory(PathXmls);
                }

                nomeXML = chaveNFe.ToString()  +"-cce.xml";
            }

            if (File.Exists(PathXmls + "\\" + nomeXML) == true)
            {
                File.Delete(PathXmls + "\\" + nomeXML);
            }

            //Gera o xml destinatário pasta do xml 

            //_spdNFeX.DiretorioXmlDestinatario = _startPath + @"\NFe\XmlDestinatario\" + PastaXml;
            System.IO.File.AppendAllText(PathXmls + nomeXML, itemDistribuicaoEventoNfe.ConteudoXmlDistribuicao);

            //PAREI 27/04/2020 RETORNA EVENTOS E MUDAR MÉTODO PRA BOOLEAN IF(retEnvEvento.CSTAT)
            if (retEnvEvento != null)
            {
                string strStatus = String.Empty;
                strStatus = retEnvEvento.cStat.ToString();
                string strSegundoStatus = retEnvEvento.retEvento[0].infEvento.cStat.ToString();
                if (strStatus.Trim() != "")
                {
                    var trataRetorno = new TratamentoDocEletronico(strStatus);
                    var retornoSefaz = trataRetorno.ValorRetornoEnviado;
                    var mensagemValidacaoNota = trataRetorno.MensagemRetornoEnviada;

                    if (retornoSefaz == "0")
                    {
                        _Message =  retEnvEvento.xMotivo;
                        return false;
                    }
                    else
                    {
                        if (strSegundoStatus.Trim() != "")
                        {
                            var trataRetorno2 = new TratamentoDocEletronico(strSegundoStatus);
                            var retornoSefaz2 = trataRetorno2.ValorRetornoEnviado;
                            var mensagemValidacaoNota2 = trataRetorno2.MensagemRetornoEnviada;

                            if (retornoSefaz2 == "0")
                            {
                                _Message = retEnvEvento.retEvento[0].infEvento.xMotivo;
                                return false;
                            }

                        }

                    }
                }
                else
                {
                    return false;
                }

            }

            return true;
            //return retEnvEvento.ToString();
        }


        private NotaFiscalEletronica.NFe.Classes.Envio.evento.evento CriarObjEventoDeCancelamento(string chaveNFe, string justificativa, long numProtocolo, byte seqEvto, DateTime dataEHoraEvto)
        {
            NotaFiscalEletronica.NFe.Classes.Envio.evento.infEvento.enuTipoEvento tipoEvento = NotaFiscalEletronica.NFe.Classes.Envio.evento.infEvento.enuTipoEvento.Cancelamento;
            NotaFiscalEletronica.NFe.Classes.Envio.evento.evento oEvento = new NotaFiscalEletronica.NFe.Classes.Envio.evento.evento();

            oEvento.infEvento = new NotaFiscalEletronica.NFe.Classes.Envio.evento.infEvento()
            {
                cOrgao = _IdUfEmpresa,
                tpEvento = tipoEvento,
                chNFe = chaveNFe,
                CNPJ = long.Parse(limparCpfCnpj(_CNPJ)),
                dhEvento = dataEHoraEvto,
                nSeqEvento = seqEvto,
                verEvento = 1,
                tpAmb = _tpAmb,
                Id = CriarIDParaEventoNFe(chaveNFe, seqEvto, tipoEvento),
                detEvento = evento_infEvento_detEvento_Cancelamento(numProtocolo, justificativa)
            };
            return oEvento;
        }



        private NotaFiscalEletronica.NFe.Classes.Envio.evento.evento CriarObjEventoDeCartaCorrecao(string chaveNFe, string correcao, byte seqEvto, DateTime dataEHoraEvto)
        {
            NotaFiscalEletronica.NFe.Classes.Envio.evento.infEvento.enuTipoEvento tipoEvento = NotaFiscalEletronica.NFe.Classes.Envio.evento.infEvento.enuTipoEvento.Carta_de_Correcao;
            NotaFiscalEletronica.NFe.Classes.Envio.evento.evento oEvento = new NotaFiscalEletronica.NFe.Classes.Envio.evento.evento();
            oEvento.infEvento = new NotaFiscalEletronica.NFe.Classes.Envio.evento.infEvento()
            {
                cOrgao = _IdUfEmpresa,
                tpEvento = tipoEvento,
                chNFe = chaveNFe,
                CNPJ = long.Parse(limparCpfCnpj(_CNPJ)),
                dhEvento = dataEHoraEvto,
                nSeqEvento = seqEvto,
                verEvento = 1, 
                tpAmb = _tpAmb,
                Id = CriarIDParaEventoNFe(chaveNFe, seqEvto, tipoEvento),
                detEvento = evento_infEvento_detEvento_CartaDeCorrecao(correcao)
            };
            return oEvento;
        }

        private List<DistribuicaoDeEventoNFe> CriarListaDistribuicaoDeEventoNFe(string chaveNFe, byte seqEvto, NotaFiscalEletronica.NFe.Classes.Distribuicao.DistribuicaoDeEventoNFe.enuTipoEvento tipoEvento, NotaFiscalEletronica.NFe.Classes.Envio.evento.evento oEvento, string CaminhoXMLEventos, int TipoDocumento)
        {
            int idEvento = 0;
            var objTipoServico = new NotaFiscalEletronica.NFe.clsTipoDeServico(_tpAmb == 2 ? NotaFiscalEletronica.NFe.clsTipoDeServico.enuTipoDeAmbiente.Homologacao : clsTipoDeServico.enuTipoDeAmbiente.Producao, _ufAbreviaturaEmpresa, false, TipoDocumento == 1 ? NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFe : NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFCe, Uteis.enuVersao.Versao400);

            if (oEvento.infEvento.tpEvento == infEvento.enuTipoEvento.Cancelamento)
            {
                idEvento = 110111;
            }
            else
            {
                idEvento = 110110;
            }
            string ArquivoXML = String.Empty;

            if (_tpAmb == 2)
            {
                 ArquivoXML = CaminhoXMLEventos +
                                    "\\evento\\HOMOLOGACAO_ID" +
                                    idEvento +
                                    chaveNFe +
                                    seqEvto.ToString("D2") + "-evento.xml";
            }
            else
            {
                 ArquivoXML = CaminhoXMLEventos +
                                    "\\evento\\ID" +
                                    idEvento +
                                    chaveNFe +
                                    seqEvto.ToString("D2") + "-evento.xml";
            }
            //NotaFiscalEletronica.Uteis.AcrescentarPalavraHomologacaoAoNomeDoArquivo(ref ArquivoXML, objTipoServico);

            var xDoc = new System.Xml.XmlDocument();
            xDoc.Load(ArquivoXML);

            string xmlEvento = xDoc.InnerXml;

            var lstDistribuicaoDeEventoNFe = new List<NotaFiscalEletronica.NFe.Classes.Distribuicao.DistribuicaoDeEventoNFe>()
            {
                new NotaFiscalEletronica.NFe.Classes.Distribuicao.DistribuicaoDeEventoNFe()
                {
                     ChaveDeAcesso = oEvento.infEvento.chNFe,
                     TipoEvento=tipoEvento,
                     SequenciaEvento = seqEvto,
                     ConteudoXmlEnvio = xmlEvento
                }

            };


            return lstDistribuicaoDeEventoNFe;
        }

        private string CriarIDParaEventoNFe(string chaveNFe, byte seqEvto, NotaFiscalEletronica.NFe.Classes.Envio.evento.infEvento.enuTipoEvento tipoEvento)
        {
            int idEvento = 0;
            if (tipoEvento == infEvento.enuTipoEvento.Cancelamento)
            {
                idEvento = 110111;
            }
            else
            {
                idEvento = 110110;
            }

            return "ID" + idEvento + chaveNFe + seqEvto.ToString("D2");
        }

        private detEvento_Cancelamento evento_infEvento_detEvento_Cancelamento(long numProtocolo, string justificativa)
        {

            NotaFiscalEletronica.NFe.Classes.Envio.evento.detEvento_Cancelamento detEvento = new NotaFiscalEletronica.NFe.Classes.Envio.evento.detEvento_Cancelamento();
            detEvento.nProt = numProtocolo;
            detEvento.xJust = justificativa;

            return detEvento;
        }

        private NotaFiscalEletronica.NFe.Classes.Envio.evento.detEvento_CartaDeCorrecao evento_infEvento_detEvento_CartaDeCorrecao(string correcao)
        {
            NotaFiscalEletronica.NFe.Classes.Envio.evento.detEvento_CartaDeCorrecao detEvento_CartaDeCorrecao = new NotaFiscalEletronica.NFe.Classes.Envio.evento.detEvento_CartaDeCorrecao();
            detEvento_CartaDeCorrecao.xCorrecao = correcao;
            return detEvento_CartaDeCorrecao;
        }

        public bool EnviarInutilizacao(byte uf,byte Ambt, string caminho, string aNotaID, string Ano, string aCNPJ, string aModelo, string aSerie, string aNFini, string aNFfim, clsTipoDeServico oTipoServico, string SerieCertificado, ref string Status, ref string Mensagem )
        {

            try
            {
                NotaFiscalEletronica.NFe.FNotaFiscalEletronica.Versao = Uteis.enuVersao.Versao400;
                NotaFiscalEletronica.NFe.Classes.Retorno.retInutNFe.retInutNFe retInutNFe = new NotaFiscalEletronica.NFe.Classes.Retorno.retInutNFe.retInutNFe();
                string inutilizado = string.Empty;                
                var oInutilizar = new NotaFiscalEletronica.NFe.Operacoes.InutilizarNFe();
                //var OinfInut = new NotaFiscalEletronica.NFe.Classes.Envio.inutNFe.infInut_310().in;
                var OinfInut = FNotaFiscalEletronica.CriarInstancia(oTipoServico).InstanciarInutilizacao();
                OinfInut.ano = Convert.ToByte(Ano);
                OinfInut.CNPJ = Convert.ToInt64(aCNPJ);
                OinfInut.cUF = uf;
                OinfInut.Id = aNotaID;
                OinfInut.mod = aModelo == "55" ? NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFe : NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFCe;
                OinfInut.nNFIni = int.Parse(aNFini);
                OinfInut.nNFFin = int.Parse(aNFfim);
                OinfInut.serie = short.Parse(aSerie);
                OinfInut.xJust = "Houve um erro na contagem da numeração da série";
                OinfInut.tpAmb = Ambt;
                //passagem
                var inutNFe = new NotaFiscalEletronica.NFe.Classes.Envio.inutNFe.inutNFe();
                inutNFe.infInut = OinfInut;
                /*
                inutNFe.infInut.ano = Convert.ToByte(Ano);
                inutNFe.infInut.CNPJ = Convert.ToInt64(aCNPJ);
                inutNFe.infInut.Id = aNotaID;
                inutNFe.infInut.mod = aModelo == "55" ? NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFe : NotaFiscalEletronica.NFe.Classes.Envio.NFe.ide.enuMod.NFCe;
                inutNFe.infInut.nNFIni = int.Parse(aNFini);
                inutNFe.infInut.nNFFin = int.Parse(aNFfim);                
                inutNFe.infInut.serie = short.Parse(aSerie);
                inutNFe.infInut.xJust = "";
                inutNFe.infInut.tpAmb = Ambt;
                */

                retInutNFe = oInutilizar.InutilizarNFe(inutNFe, SerieCertificado, caminho, oTipoServico);

                //var retorno = oInutilizar.InutilizarNFe(inutNFe, SerieCertificado,  caminho, oTipoServico);
                if (retInutNFe != null)
                {
                    Status = retInutNFe.infInut.cStat.ToString();
                    Mensagem = retInutNFe.infInut.xMotivo.ToString();
                }
                else
                {
                    return false;

                }

            }
            catch(VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
                return false;
            }


            return true;
        }

        public static string limparCpfCnpj(string CpfCnpj)
        {
            if (CpfCnpj == null)
                return null;

            return CpfCnpj.Replace("-", "").Replace("(", "")
                      .Replace(")", "").Replace("-", "")
                      .Replace("-", "").Replace("/", "")
                      .Replace(".", "").Trim();
        }

        public void LerXmlNfe(string nomeArquivoXml, ref NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe oNFE)
        {
            var xDoc = new System.Xml.XmlDocument();
            xDoc.Load(nomeArquivoXml);
            var NFe = new NotaFiscalEletronica.NFe.Classes.Envio.NFe.NFe();
            NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe oLeituraNFe = new NotaFiscalEletronica.NFe.LeituraXML.LerXmlNFe();
            oLeituraNFe.LerXmlNFe(xDoc.InnerXml, ref NFe);
            oNFE = NFe;
            
        }





    }
}
