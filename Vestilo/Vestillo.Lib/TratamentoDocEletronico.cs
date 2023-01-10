using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    public class TratamentoDocEletronico
    {
        
        public string  ValorRetornoEnviado { get; set; }
        public string MensagemRetornoEnviada { get; set; }



        public TratamentoDocEletronico(string Status)
        {

            switch (Status)
            {


                case "100":
                    MensagemRetornoEnviada = "Autorizado o uso da NF-e";
                    ValorRetornoEnviado = "1";
                    break;
                case "101":
                    MensagemRetornoEnviada = "Cancelamento de NF-e homologado";
                    ValorRetornoEnviado = "1";
                    break;
                case "102":
                    MensagemRetornoEnviada = "Inutilização de número homologado";
                    ValorRetornoEnviado = "1";
                     break;
                case "103":
                    MensagemRetornoEnviada = "Lote recebido com sucesso";
                    ValorRetornoEnviado = "1";
                     break;
                case "104":
                    MensagemRetornoEnviada = "Lote processado";
                    ValorRetornoEnviado = "1";
                     break;
                case "105":
                    MensagemRetornoEnviada = "Lote em processamento";
                    ValorRetornoEnviado = "0";
                    break;
                case "106":
                    MensagemRetornoEnviada = "Lote não localizado";
                    ValorRetornoEnviado = "0";
                    break;
                case "107":
                    MensagemRetornoEnviada = "Serviço em Operação";
                    ValorRetornoEnviado = "1";
                     break;
                case "108":
                    MensagemRetornoEnviada = "Serviço Paralisado Momentaneamente (curto prazo)";
                    ValorRetornoEnviado = "0";
                     break;
                case "109":
                    MensagemRetornoEnviada = "Serviço Paralisado sem Previsão";
                    ValorRetornoEnviado = "0";
                     break;
                case "110":
                    MensagemRetornoEnviada = "Uso Denegado";
                    ValorRetornoEnviado = "0";
                     break;
                case "111":
                    MensagemRetornoEnviada = "Consulta cadastro com uma ocorrência";
                    ValorRetornoEnviado = "0";
                     break;
                case "112":
                    MensagemRetornoEnviada = "Consulta cadastro com mais de uma ocorrência";
                    ValorRetornoEnviado = "0";
                     break;
                case "128":
                    MensagemRetornoEnviada = "Lote de Evento Processado";
                    ValorRetornoEnviado = "1";
                     break;
                case "135":
                    MensagemRetornoEnviada = "Evento registrado e vinculado a NF-e";
                    ValorRetornoEnviado = "1";
                     break;
                case "136":
                    MensagemRetornoEnviada = "Evento registrado, mas não vinculado a NF-e";
                    ValorRetornoEnviado = "1";
                     break;
                case "150":
                    MensagemRetornoEnviada = "Autorizado o uso da NF-e, autorização concedida fora de prazo";
                    ValorRetornoEnviado = "1";
                     break;
                case "151":
                    MensagemRetornoEnviada = "Cancelamento de NF-e homologado fora de prazo";
                    ValorRetornoEnviado = "1";
                     break;
                case "201":
                    MensagemRetornoEnviada = "Rejeição: O numero máximo de numeração de NF-e a inutilizar ultrapassou o limite";
                    ValorRetornoEnviado = "0";
                     break;
                case "202":
                    MensagemRetornoEnviada = "Rejeição: Falha no reconhecimento da autoria ou integridade do arquivo digital";
                    ValorRetornoEnviado = "0";
                     break;
                case "203":
                    MensagemRetornoEnviada = "Rejeição: Emissor não habilitado para emissão da NF-e";
                    ValorRetornoEnviado = "0";
                     break;
                case "204":
                    MensagemRetornoEnviada = "Rejeição: Duplicidade de NF-e";
                    ValorRetornoEnviado = "0";
                     break;
                case "205":
                    MensagemRetornoEnviada = "Rejeição: NF-e está denegada na base de dados da SEFAZ";
                    ValorRetornoEnviado = "0";
                     break;
                case "206":
                    MensagemRetornoEnviada = "Rejeição: NF-e já está inutilizada na Base de dados da SEFAZ";
                    ValorRetornoEnviado = "0";
                     break;
                case "207":
                    MensagemRetornoEnviada = "Rejeição: CNPJ do emitente inválido";
                    ValorRetornoEnviado = "0";
                     break;
                case "208":
                    MensagemRetornoEnviada = "Rejeição: CNPJ do destinatário inválido";
                    ValorRetornoEnviado = "0";
                     break;
                case "209":
                    MensagemRetornoEnviada = "Rejeição: IE do emitente inválida";
                    ValorRetornoEnviado = "0";
                     break;
                case "210":
                    MensagemRetornoEnviada = "Rejeição: IE do destinatário inválida";
                    ValorRetornoEnviado = "0";
                     break;
                case "211":
                    MensagemRetornoEnviada = "Rejeição: IE do substituto inválida";
                    ValorRetornoEnviado = "0";
                     break;
                case "212":
                    MensagemRetornoEnviada = "Rejeição: Data de emissão NF-e posterior a data de recebimento";
                    ValorRetornoEnviado = "0";
                     break;
                case "213":
                    MensagemRetornoEnviada = "Rejeição: CNPJ-Base do Emitente difere do CNPJ-Base do Certificado Digital";
                    ValorRetornoEnviado = "0";
                     break;
                case "214":
                    MensagemRetornoEnviada = "Rejeição: Tamanho da mensagem excedeu o limite estabelecido";
                    ValorRetornoEnviado = "0";
                     break;
                case "215":
                    MensagemRetornoEnviada = "Rejeição: Falha no schema XML";
                    ValorRetornoEnviado = "0";
                     break;
                case "216":
                    MensagemRetornoEnviada = "Rejeição: Chave de Acesso difere da cadastrada";
                    ValorRetornoEnviado = "0";
                     break;
                case "217":
                    MensagemRetornoEnviada = "Rejeição: NF-e não consta na base de dados da SEFAZ'";
                    ValorRetornoEnviado = "0";
                     break;
                case "218":
                    MensagemRetornoEnviada = "Rejeição: NF-e já esta cancelada na base de dados da SEFAZ";
                    ValorRetornoEnviado = "0";
                     break;
                case "219":
                    MensagemRetornoEnviada = "Rejeição: Circulação da NF-e verificada";
                    ValorRetornoEnviado = "0";
                     break;
                case "220":
                    MensagemRetornoEnviada = "Rejeição: Prazo de Cancelamento Superior ao Previsto na Legislacao";
                    ValorRetornoEnviado = "0";
                     break;
                case "221":
                    MensagemRetornoEnviada = "Rejeição: Confirmado o recebimento da NF-e pelo destinatário";
                    ValorRetornoEnviado = "0";
                     break;
                case "222":
                    MensagemRetornoEnviada = "Rejeição: Protocolo de Autorização de Uso difere do cadastrado";
                    ValorRetornoEnviado = "0";
                     break;
                case "223":
                    MensagemRetornoEnviada = "Rejeição: CNPJ do transmissor do lote difere do CNPJ do transmissor da consulta";
                    ValorRetornoEnviado = "0";
                     break; 
                case "224":
                    MensagemRetornoEnviada = "Rejeição: A faixa inicial é maior que a faixa final";
                    ValorRetornoEnviado = "0";
                     break;
                case "225":
                    MensagemRetornoEnviada = "Rejeição: Falha no Schema XML da NFe";
                    ValorRetornoEnviado = "0";
                     break;
                case "226":
                    MensagemRetornoEnviada = "Rejeição: Código da UF do Emitente diverge da UF autorizadora";
                    ValorRetornoEnviado = "0";
                     break;
                case "227":
                    MensagemRetornoEnviada = "Rejeição: Erro na Chave de Acesso - Campo ID";
                    ValorRetornoEnviado = "0";
                      break;           
                case "228":
                    MensagemRetornoEnviada = "Rejeição: Data de Emissão muito atrasada";
                    ValorRetornoEnviado = "0";
                     break;            
                case "229":
                    MensagemRetornoEnviada = "Rejeição: IE do emitente não informada";
                    ValorRetornoEnviado = "0";
                     break;
                case "230":
                    MensagemRetornoEnviada = "Rejeição: IE do emitente não cadastrada";
                    ValorRetornoEnviado = "0";
                    break;
                case "231":
                    MensagemRetornoEnviada = "Rejeição: IE do emitente não vinculada ao CNPJ";
                    ValorRetornoEnviado = "0";
                   break;
                case "232":
                    MensagemRetornoEnviada = "Rejeição: IE do destinatário não informada";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "233":
                    MensagemRetornoEnviada = "Rejeição: IE do destinatário não cadastrada";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "234":
                    MensagemRetornoEnviada = "Rejeição: IE do destinatário não vinculada ao CNPJ";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "235":
                    MensagemRetornoEnviada = "Rejeição: Inscrição SUFRAMA inválida";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "236":
                    MensagemRetornoEnviada = "Rejeição: Chave de Acesso com dígito verificador inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "237":
                    MensagemRetornoEnviada = "Rejeição: CPF do destinatário inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "238":
                    MensagemRetornoEnviada = "Rejeição: Cabeçalho - Versão do arquivo XML superior a Versão vigente";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "239":
                    MensagemRetornoEnviada = "Rejeição: Cabeçalho - Versão do arquivo XML não suportada";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "240":
                    MensagemRetornoEnviada = "Rejeição: Cancelamento/Inutilização - Irregularidade Fiscal do Emitente";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "241":
                    MensagemRetornoEnviada = "Rejeição: Um número da faixa já foi utilizado";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "242":
                    MensagemRetornoEnviada = "Rejeição: Cabeçalho - Falha no Schema XML";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "243":
                    MensagemRetornoEnviada = "Rejeição: XML Mal Formado";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "244":
                    MensagemRetornoEnviada = "Rejeição: CNPJ do Certificado Digital difere do CNPJ da Matriz e do CNPJ do Emitente";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "245":
                    MensagemRetornoEnviada = "Rejeição: CNPJ Emitente não cadastrado";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "246":
                    MensagemRetornoEnviada = "Rejeição: CNPJ Destinatário não cadastrado";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "247":
                    MensagemRetornoEnviada = "Rejeição: Sigla da UF do Emitente diverge da UF autorizadora";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "248":
                    MensagemRetornoEnviada = "Rejeição: UF do Recibo diverge da UF autorizadora";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "249":
                    MensagemRetornoEnviada = "Rejeição: UF da Chave de Acesso diverge da UF autorizadora";
                    ValorRetornoEnviado = "0";
                   break;
                case "250":
                    MensagemRetornoEnviada = "Rejeição: UF diverge da UF autorizadora";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "251":
                    MensagemRetornoEnviada = "Rejeição: UF/Município destinatário não pertence a SUFRAMA";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "252":
                    MensagemRetornoEnviada = "Rejeição: Ambiente informado diverge do Ambiente de recebimento";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "253":
                    MensagemRetornoEnviada = "Rejeição: Digito Verificador da chave de acesso composta inválida'";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "254":
                    MensagemRetornoEnviada = "Rejeição: NF-e complementar não possui NF referenciada";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "255":
                    MensagemRetornoEnviada = "Rejeição: NF-e complementar possui mais de uma NF referenciada";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "256":
                    MensagemRetornoEnviada = "Rejeição: Uma NF-e da faixa já está inutilizada na Base de dados da SEFAZ";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "257":
                    MensagemRetornoEnviada = "Rejeição: Solicitante não habilitado para emissão da NF-e";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "258":
                    MensagemRetornoEnviada = "Rejeição: CNPJ da consulta inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "259":
                    MensagemRetornoEnviada = "Rejeição: CNPJ da consulta não cadastrado como contribuinte na UF";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "260":
                    MensagemRetornoEnviada = "Rejeição: IE da consulta inválida";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "261":
                    MensagemRetornoEnviada = "Rejeição: IE da consulta não cadastrada como contribuinte na UF";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "262":
                    MensagemRetornoEnviada = "Rejeição: UF não fornece consulta por CPF";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "263":
                    MensagemRetornoEnviada = "Rejeição: CPF da consulta inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "264":
                    MensagemRetornoEnviada = "Rejeição: CPF da consulta não cadastrado como contribuinte na UF'";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "265":
                    MensagemRetornoEnviada = "Rejeição: Sigla da UF da consulta difere da UF do Web Service";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "266":
                    MensagemRetornoEnviada = "Rejeição: Série utilizada não permitida no Web Service";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "267":
                    MensagemRetornoEnviada = "Rejeição: NF Complementar referencia uma NF-e inexistente";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "268":
                    MensagemRetornoEnviada = "Rejeição: NF Complementar referencia uma outra NF-e Complementar";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "269":
                    MensagemRetornoEnviada = "Rejeição: CNPJ Emitente da NF Complementar difere do CNPJ da NF Referenciada";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "270":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Fato Gerador: dígito inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "271":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Fato Gerador: difere da UF do emitente";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "272":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Emitente: dígito inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "273":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Emitente: difere da UF do emitente";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "274":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Destinatário: dígito inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "275":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Destinatário: difere da UF do Destinatário";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "276":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Local de Retirada: dígito inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "277":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Local de Retirada: difere da UF do Local de Retirada";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "278":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Local de Entrega: dígito inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "279":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Local de Entrega: difere da UF do Local de Entrega";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "280":
                    MensagemRetornoEnviada = "Rejeição: Certificado Transmissor inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "281":
                    MensagemRetornoEnviada = "Rejeição: Certificado Transmissor Data Validade";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "282":
                    MensagemRetornoEnviada = "Rejeição: Certificado Transmissor sem CNPJ";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "283":
                    MensagemRetornoEnviada = "Rejeição: Certificado Transmissor - erro Cadeia de Certificação";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "284":
                    MensagemRetornoEnviada = "Rejeição: Certificado Transmissor revogado";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "285":
                    MensagemRetornoEnviada = "Rejeição: Certificado Transmissor difere ICP-Brasil";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "286":
                    MensagemRetornoEnviada = "Rejeição: Certificado Transmissor erro no acesso a LCR";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "287":
                    MensagemRetornoEnviada = "Rejeição: Código Município do FG - ISSQN: dígito inválido";
                    ValorRetornoEnviado = "0";
                    break;
                case "288":
                    MensagemRetornoEnviada = "Rejeição: Código Município do FG - Transporte: dígito inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "289":
                    MensagemRetornoEnviada = "Rejeição: Código da UF informada diverge da UF solicitada";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "290":
                    MensagemRetornoEnviada = "Rejeição: Certificado Assinatura inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "291":
                    MensagemRetornoEnviada = "Rejeição: Certificado Assinatura Data Validade";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "292":
                    MensagemRetornoEnviada = "Rejeição: Certificado Assinatura sem CNPJ";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "293":
                    MensagemRetornoEnviada = "Rejeição: Certificado Assinatura - erro Cadeia de Certificação";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "294":
                    MensagemRetornoEnviada = "Rejeição: Certificado Assinatura revogado";
                    ValorRetornoEnviado = "0"; 
                    break;
            
                case "295":
                    MensagemRetornoEnviada = "Rejeição: Certificado Assinatura difere ICP-Brasil";
                    ValorRetornoEnviado = "0";
                     break;
                case "296":
                    MensagemRetornoEnviada = "Rejeição: Certificado Assinatura erro no acesso a LCR";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "297":
                    MensagemRetornoEnviada = "Rejeição: Assinatura difere do calculado";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "298":
                    MensagemRetornoEnviada = "Rejeição: Assinatura difere do padrão do Projeto";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "299":
                    MensagemRetornoEnviada = "Rejeição: XML da área de cabeçalho com codificação diferente de UTF-8";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "301":
                    MensagemRetornoEnviada = "Uso Denegado : Irregularidade fiscal do emitente";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "302":
                    MensagemRetornoEnviada = "Uso Denegado : Irregularidade fiscal do destinatário";
                    ValorRetornoEnviado = "0";
                    break;

                case "306":
                    MensagemRetornoEnviada = "Rejeição : IE do Destinatario nao esta ativa na UF";
                    ValorRetornoEnviado = "0";
                    break;               

        
                case "321":
                    MensagemRetornoEnviada = "Rejeicao: NF-e de devolucao de mercadoria nao possui documento fiscal referenciado";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "328":
                    MensagemRetornoEnviada = "Rejeicao: CFOP de devolucao de mercadoria para NF-e que nao tem finalidade de devolucao de mercadoria";
                    ValorRetornoEnviado = "0";
                    break;

                case "383":
                    MensagemRetornoEnviada = "Rejeicao: Item com CSOSN indevido";
                    ValorRetornoEnviado = "0";
                    break;                
       
        
                case "401":
                    MensagemRetornoEnviada = "Rejeição: CPF do remetente inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "402":
                    MensagemRetornoEnviada = "Rejeição: XML da área de dados com codificação diferente de UTF-8";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "403":
                    MensagemRetornoEnviada = "Rejeição: O grupo de informações da NF-e avulsa é de uso exclusivo do Fisco";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "404":
                    MensagemRetornoEnviada = "Rejeição: Uso de prefixo de namespace não permitido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "405":
                    MensagemRetornoEnviada = "Rejeição: Código do país do emitente: dígito inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "406":
                    MensagemRetornoEnviada = "Rejeição: Código do país do destinatário: dígito inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "407":
                    MensagemRetornoEnviada = "Rejeição: O CPF só pode ser informado no campo emitente para a NF-e avulsa";
                    ValorRetornoEnviado = "0";
                    break;
       
               case "409":
                    MensagemRetornoEnviada = "Rejeição: Campo cUF inexistente no elemento nfeCabecMsg do SOAP Header";
                    ValorRetornoEnviado = "0";
                    break;
     
               case "410":
                    MensagemRetornoEnviada = "Rejeição: UF informada no campo cUF não é atendida pelo Web Service ";
                    ValorRetornoEnviado = "0";
                    break;
     
               case "411":
                    MensagemRetornoEnviada = "Rejeição: Campo versaoDados inexistente no elemento nfeCabecMsg do SOAP Header";
                    ValorRetornoEnviado = "0";
                    break;
     
               case "420":
                    MensagemRetornoEnviada = "Rejeição: Cancelamento para NF-e já cancelada";
                    ValorRetornoEnviado = "0";
                    break;
 
 
               case "450":
                    MensagemRetornoEnviada = "Rejeição: Modelo da NF-e diferente de 55";
                    ValorRetornoEnviado = "0";
                    break;
 
 
               case "451":
                    MensagemRetornoEnviada = "Rejeição: Processo de emissão informado inválido";
                    ValorRetornoEnviado = "0";
                    break;
 

               case "452":
                    MensagemRetornoEnviada = "Rejeição: Tipo Autorizador do Recibo diverge do Órgão Autorizador ";
                    ValorRetornoEnviado = "0";
                    break;
 
               case "453":
                    MensagemRetornoEnviada = "Rejeição: Ano de inutilização não pode ser superior ao Ano atual ";
                    ValorRetornoEnviado = "0";
                    break;
 
               case "454":
                    MensagemRetornoEnviada = "Rejeição: Ano de inutilização não pode ser inferior a 2006";
                    ValorRetornoEnviado = "0";
                    break;
            
        
               case "478":
                    MensagemRetornoEnviada = "Rejeição: Local da entrega não informado para faturamento direto de veículos novos";
                    ValorRetornoEnviado = "0";
                    break;

               case "462":
                    MensagemRetornoEnviada = "Rejeição: Codigo identificador do CSC no QR-Code nao cadastrado na SEFAZ";
                    ValorRetornoEnviado = "0";
                    break;

            
               case "489":
                    MensagemRetornoEnviada = "Rejeição: CNPJ informado inválido (DV ou zeros)";
                    ValorRetornoEnviado = "0";
                    break;
           
               case "490":
                    MensagemRetornoEnviada = "Rejeição: CPF informado inválido (DV ou zeros)";
                    ValorRetornoEnviado = "0";
                    break;
            
            
                case "491":
                    MensagemRetornoEnviada = "Rejeicao: O tipo de evento informado é inválido";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "492":
                    MensagemRetornoEnviada = "Rejeição: O verEvento informado inválido";
                    ValorRetornoEnviado = "0";
                    break;
       
                case "493":
                    MensagemRetornoEnviada = "Rejeição: Evento não atende o Schema XML específico";
                    ValorRetornoEnviado = "0";
                    break;

                case "494":
                    MensagemRetornoEnviada = "Rejeição: Chave de Acesso inexistente";
                    ValorRetornoEnviado = "0";
                    break;
            
                case "501":
                    MensagemRetornoEnviada = "Rejeição: Prazo de cancelamento superior ao previsto na Legislação";
                    ValorRetornoEnviado = "0";
                    break;
            
            
                case "502":
                    MensagemRetornoEnviada = "Rejeição: Erro na Chave de Acesso - Campo Id não corresponde à concatenação dos campos ";
                    ValorRetornoEnviado = "0";
                    break;

                case "503":
                    MensagemRetornoEnviada = "Rejeição: Série utilizada fora da faixa permitida no SCAN (900-999)";
                    ValorRetornoEnviado = "0";
                    break;
                case "504":
                    MensagemRetornoEnviada = "Rejeição: Data de Entrada/Saída posterior ao permitido";
                    ValorRetornoEnviado = "0";
                    break;

                             
				case "505":
                    MensagemRetornoEnviada = "Rejeição: Data de Entrada/Saída anterior ao permitido";
                    ValorRetornoEnviado = "0";
					break;
                case "506":
                    MensagemRetornoEnviada = "Rejeição: Data de Saída menor que a Data de Emissão";
                    ValorRetornoEnviado = "0";
					break;
                case "507":
                    MensagemRetornoEnviada = "Rejeição: O CNPJ do destinatário/remetente não deve ser informado em operação com o exterior ";
                    ValorRetornoEnviado = "0";
					break;
                case "508":
                    MensagemRetornoEnviada = "Rejeição: CST incompatível na operação com Não Contribuinte [nItem:999]";
                    ValorRetornoEnviado = "0";
					break;
                case "509":
                    MensagemRetornoEnviada = "Rejeição: Informado código de município diferente de “9999999” para operação com o exterior ";
                    ValorRetornoEnviado = "0";
					break;
                case "510":
                    MensagemRetornoEnviada = "Rejeição: Operação com Exterior e Código País destinatário é 1058 (Brasil) ou não informado ";
                    ValorRetornoEnviado = "0";
					break;

                case "511":
                    MensagemRetornoEnviada = "Rejeição: Não é de Operação com Exterior e Código País destinatário difere de 1058 (Brasil)";
                    ValorRetornoEnviado = "0";
					break;
                case "512":
                    MensagemRetornoEnviada = "Rejeição: CNPJ do Local de Retirada inválido";
                    ValorRetornoEnviado = "0";
					break;
     			case "513":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Local de Retirada deve ser 9999999 para UF retirada = EX";
                    ValorRetornoEnviado = "0";
					break;
                case "514":
                    MensagemRetornoEnviada = "Rejeição: CNPJ do Local de Entrega inválido";
                    ValorRetornoEnviado = "0";
					break;
                case "515":
                    MensagemRetornoEnviada = "Rejeição: Código Município do Local de Entrega deve ser 9999999 para UF entrega = EX";
                    ValorRetornoEnviado = "0";
					break;
                case "516":
                    MensagemRetornoEnviada = "Rejeição: Falha no schema XML – inexiste a tag raiz esperada para a mensagem";
                    ValorRetornoEnviado = "0";
					break;

                case "517":
                    MensagemRetornoEnviada = "Rejeição: Falha no schema XML – inexiste atributo versao na tag raiz da mensagem";
                    ValorRetornoEnviado = "0";
                    break;
                case "518":
                    MensagemRetornoEnviada = "Rejeição: CFOP de entrada para NF-e de saída";
                    ValorRetornoEnviado = "0";	
 					break;

                case "519":
                    MensagemRetornoEnviada = "Rejeição: CFOP de saída para NF-e de entrada";
                    ValorRetornoEnviado = "0";		
					break;

                case "520":
                    MensagemRetornoEnviada = "Rejeição: CFOP de Operação com Exterior e UF destinatário difere de EX";
                    ValorRetornoEnviado = "0";				
					break;

                case "521":
                    MensagemRetornoEnviada = "Rejeição: CFOP de Operacao Estadual e UF do emitente difere da UF do destinatario "; 
                    ValorRetornoEnviado = "0";				
					break;

                case "522":
                    MensagemRetornoEnviada = "Rejeição: CFOP de Operação Estadual e UF emitente difere UF destinatário";
                    ValorRetornoEnviado = "0";			
					break;

                case "523":
                    MensagemRetornoEnviada = "Rejeição: CFOP não é de Operação Estadual e UF emitente igual a UF destinatário";
                    ValorRetornoEnviado = "0";				
					break;

                case "524":
                    MensagemRetornoEnviada = "Rejeição: CFOP de Operação com Exterior e não informado NCM";
                    ValorRetornoEnviado = "0";				
					break;

                case "525":
                    MensagemRetornoEnviada = "Rejeição: CFOP de Importação e não informado dados da DI";
                    ValorRetornoEnviado = "0";					
					break;

                case "526":
                    MensagemRetornoEnviada = "Rejeição: CFOP de Exportação e não informado Local de Embarque";
                    ValorRetornoEnviado = "0";				
					break;

                case "527":
                    MensagemRetornoEnviada = "Rejeição: Operação de Exportação com informação de ICMS incompatível";
                    ValorRetornoEnviado = "0";				
					break;
        
                case "528":
                    MensagemRetornoEnviada = "Rejeição: Valor do ICMS difere do produto BC e Alíquota";
                    ValorRetornoEnviado = "0";				
					break;

                case "529":
                    MensagemRetornoEnviada = "Rejeição: CST incompativel na operacao com Contribuinte Isento de Inscricao Estadual ";
                    ValorRetornoEnviado = "0";				
					break;

                case "530":
                    MensagemRetornoEnviada = "Rejeição: Operação com tributação de ISSQN sem informar a Inscrição Municipal";
                    ValorRetornoEnviado = "0";					
					break;

                case "531":
                    MensagemRetornoEnviada = "Rejeição: Total da BC ICMS difere do somatório dos itens";
                    ValorRetornoEnviado = "0";
					break;
                case "532":
                    MensagemRetornoEnviada = "Rejeição: Total do ICMS difere do somatório dos itens";
                    ValorRetornoEnviado = "0";
					break;
                case "533":
                    MensagemRetornoEnviada = "Rejeição: Total da BC ICMS-ST difere do somatório dos itens";
                    ValorRetornoEnviado = "0";
					break;
                case "534":
                    MensagemRetornoEnviada = "Rejeição: Total do ICMS-ST difere do somatório dos itens";
                    ValorRetornoEnviado = "0";
					break;
                case "535":
                    MensagemRetornoEnviada = "Rejeição: Total do Frete difere do somatório dos itens";
                    ValorRetornoEnviado = "0";
					break;
                case "536":
                    MensagemRetornoEnviada = "Rejeição: Total do Seguro difere do somatório dos itens";
                    ValorRetornoEnviado = "0";
					break;
                case "537":
                    MensagemRetornoEnviada = "Rejeição: Total do Desconto difere do somatório dos itens";
                    ValorRetornoEnviado = "0";
					break;
                case "538":
                    MensagemRetornoEnviada = "Rejeição: Total do IPI difere do somatório dos itens";
                    ValorRetornoEnviado = "0";
					break;
                case "539":
                    MensagemRetornoEnviada = "Rejeição: Duplicidade de NF-e, com diferença na Chave de Acesso [99999999999999999999999999999999999999999]";
                    ValorRetornoEnviado = "0";
					break;
                case "540":
                    MensagemRetornoEnviada = "Rejeição: CPF do Local de Retirada inválido";
                    ValorRetornoEnviado = "0";
					break;
                case "541":
                    MensagemRetornoEnviada = "Rejeição: CPF do Local de Entrega inválido";
                    ValorRetornoEnviado = "0";
					break;
                case "542":
                    MensagemRetornoEnviada = "Rejeição: CPF do Transportador inválido";
                    ValorRetornoEnviado = "0";
					break;
                case "543":
                    MensagemRetornoEnviada = "Rejeição: CPF do Transportador inválido";
                    ValorRetornoEnviado = "0";
					break;
                case "544":
                    MensagemRetornoEnviada = "Rejeição: IE do Transportador inválida";
                    ValorRetornoEnviado = "0";
					break;
                case "545":
                    MensagemRetornoEnviada = "Rejeição: Falha no schema XML – versão informada na versaoDados do SOAPHeader diverge da versão da mensagem";
                    ValorRetornoEnviado = "0";
					break;
                case "546":
                    MensagemRetornoEnviada = "Rejeição: Erro na Chave de Acesso – Campo Id – falta a literal NFe";
                    ValorRetornoEnviado = "0";
					break;
                case "547":
                    MensagemRetornoEnviada = "Rejeição: Dígito Verificador da Chave de Acesso da NF-e Referenciada inválido";
                    ValorRetornoEnviado = "0";
					break;
                case "548":
                    MensagemRetornoEnviada = "Rejeição: CNPJ da NF referenciada inválido";
                    ValorRetornoEnviado = "0";
					break;
                case "549":
                    MensagemRetornoEnviada = "Rejeição: CNPJ da NF referenciada de produtor inválido";
                    ValorRetornoEnviado = "0";
					break;
                case "550":
                    MensagemRetornoEnviada = "Rejeição: CPF da NF referenciada de produtor inválido";
                    ValorRetornoEnviado = "0";
					break;
                case "551":
                    MensagemRetornoEnviada = "Rejeição: IE da NF referenciada de produtor inválido.";
                    ValorRetornoEnviado = "0";
					break;
                case "552":
                    MensagemRetornoEnviada = "Rejeição: Dígito Verificador da Chave de Acesso do CT-e Referenciado inválido";
                    ValorRetornoEnviado = "0";
					break;
                case "553":
                    MensagemRetornoEnviada = "Rejeição: Tipo autorizador do recibo diverge do Órgão Autorizador.";
                    ValorRetornoEnviado = "0";
					break;
                case "554":
                    MensagemRetornoEnviada = "Rejeição: Série difere da faixa 0-899";
                    ValorRetornoEnviado = "0";
					break;
                case "555":
                    MensagemRetornoEnviada = ";Tipo autorizador do protocolo diverge do Órgão Autorizador";
                    ValorRetornoEnviado = "0";
					break;
                case "556":
                    MensagemRetornoEnviada = "Rejeição: Justificativa de entrada em contingência não deve ser informada para tipo de emissão normal.";
                    ValorRetornoEnviado = "0";
					break;
                case "557":
                    MensagemRetornoEnviada = "Rejeição: A Justificativa de entrada em contingência deve ser informada.";
                    ValorRetornoEnviado = "0";
					break;
                case "558":
                    MensagemRetornoEnviada = "Rejeição: Data de entrada em contingência posterior a data de emissão.";
                    ValorRetornoEnviado = "0";
					break;
                case "559":
                    MensagemRetornoEnviada = "Rejeição: UF do Transportador não informada";
                    ValorRetornoEnviado = "0";
					break;
                case "560":
                    MensagemRetornoEnviada = "Rejeição: CNPJ base do emitente difere do CNPJ base da primeira NF-e do lote recebido";
                    ValorRetornoEnviado = "0";
					break;
                case "561":
                    MensagemRetornoEnviada = "Rejeição: Mês de Emissão informado na Chave de Acesso difere do Mês de Emissão da NFe ";
                    ValorRetornoEnviado = "0";
					break;
                case "562":
                    MensagemRetornoEnviada = "Rejeição: Código Numérico informado na Chave de Acesso difere do Código Numérico da NF-e";
                    ValorRetornoEnviado = "0";
					break;
                case "563":
                    MensagemRetornoEnviada = "Rejeição: Já existe pedido de Inutilização com a mesma faixa de inutilização";
                    ValorRetornoEnviado = "0";
					break;
                case "564":
                    MensagemRetornoEnviada = "Rejeição: Total do Produto / Serviço difere do somatório dos itens";
                    ValorRetornoEnviado = "0";
					break;
                case "565":
                    MensagemRetornoEnviada = "Rejeição: Falha no schema XML – inexiste a tag raiz esperada para o lote de NF-e";
                    ValorRetornoEnviado = "0";
					break;
                case "567":
                    MensagemRetornoEnviada = "Rejeição: Falha no schema XML – versão informada na versaoDados do SOAPHeader diverge da versão do lote de NF-e";
                    ValorRetornoEnviado = "0";
					break;
                case "568":
                    MensagemRetornoEnviada = "Rejeição: Falha no schema XML – inexiste atributo versao na tag raiz do lote de NF-e";
                    ValorRetornoEnviado = "0";
					break;
                case "569":
                    MensagemRetornoEnviada = "Rejeicao: Data de entrada em contingencia muito atrasada";
                    ValorRetornoEnviado = "0";
					break;
            
                case "572":
                    MensagemRetornoEnviada = "Rejeição: Erro Atributo ID do evento não corresponde a concatenação dos campos (“ID” + tpEvento + chNFe + nSeqEvento)";
                    ValorRetornoEnviado = "0";
                    break;
                case "573":
                    MensagemRetornoEnviada = "Rejeição: Duplicidade de Evento";
                    ValorRetornoEnviado = "0";		
					break;
                        
                case "574":
                    MensagemRetornoEnviada = "Rejeição: O autor do evento diverge do emissor da NF-e";
                    ValorRetornoEnviado = "0";
					break;
                case "575":
                    MensagemRetornoEnviada = "Rejeição: O autor do evento diverge do destinatário da NF-e";
                    ValorRetornoEnviado = "0";
					break;
                case "576":
                    MensagemRetornoEnviada = "Rejeição: O autor do evento não é um órgão autorizado a gerar o evento";
                    ValorRetornoEnviado = "0";
					break;
                case "577":
                    MensagemRetornoEnviada = "Rejeição: A data do evento não pode ser menor que a data de emissão da NF-e";
                    ValorRetornoEnviado = "0";
					break;
                case "578":
                    MensagemRetornoEnviada = "Rejeição: A data do evento não pode ser maior que a data do processamento";
                    ValorRetornoEnviado = "0";
					break;
                case "579":
                    MensagemRetornoEnviada = "Rejeição: A data do evento não pode ser menor que a data de autorização para NF-e não emitida em contingência";
                    ValorRetornoEnviado = "0";
					break;
                case "580":
                    MensagemRetornoEnviada = "Rejeição: O evento exige uma NF-e autorizada";
                    ValorRetornoEnviado = "0";
					break;
                case "594":
                    MensagemRetornoEnviada = "Rejeição: O número de sequencia do evento informado é maior que o permitido";
                    ValorRetornoEnviado = "0";
					break;
               case "600":
                    MensagemRetornoEnviada = "Rejeicao: CSOSN incompativel na operacao com Nao Contribuinte";
                    ValorRetornoEnviado = "0";
					break;
                case "602":
                    MensagemRetornoEnviada = "Rejeicao: Total do PIS difere do somatorio dos itens sujeitos ao ICMS.";
                    ValorRetornoEnviado = "0";
					break;            
                case "604":
                    MensagemRetornoEnviada = "Rejeição: Total do vOutro difere do somatorio dos itens";
                    ValorRetornoEnviado = "0";
					break;
                case "610":
                    MensagemRetornoEnviada = "Rejeição: Total da NF difere do somatorio dos Valores compoe o valor Total da NF.";
                    ValorRetornoEnviado = "0";
					break;
       
               case "611":
                    MensagemRetornoEnviada = "Rejeição: cEAN invalido";
                    ValorRetornoEnviado = "0";
					break;
            
                case "614":
                    MensagemRetornoEnviada = "Rejeição: Chave de Acesso inválida (Código UF inválido)";
                    ValorRetornoEnviado = "0";
					break;
                case "615":
                    MensagemRetornoEnviada = "Rejeição: Chave de Acesso inválida (Ano menor que 05 ou Ano maior que Ano corrente)";
                    ValorRetornoEnviado = "0";
					break;
                case "616":
                    MensagemRetornoEnviada = "Rejeição: Chave de Acesso inválida (Mês menor que 1 ou Mês maior que 12)";
                    ValorRetornoEnviado = "0";
					break;
                case "617":
                    MensagemRetornoEnviada = "Rejeição: Chave de Acesso inválida (CNPJ zerado ou dígito inválido)";
                    ValorRetornoEnviado = "0";
					break;

                case "618":
                    MensagemRetornoEnviada = "Rejeição: Chave de Acesso inválida (modelo diferente de 55)";
                    ValorRetornoEnviado = "0";
					break;
        
                case "619":
                    MensagemRetornoEnviada = "Rejeição: Chave de Acesso inválida (número NF = 0)";
                    ValorRetornoEnviado = "0";
					break;

               case "625":
                    MensagemRetornoEnviada = "Rejeicao: Inscricao SUFRAMA deve ser informada na venda com isencao para ZFM";
                    ValorRetornoEnviado = "0";
                    break;
               case "626":
                     MensagemRetornoEnviada = "Rejeicao: O CFOP da Operação deve ser isenta para ZFM- Zona Franca";
                     ValorRetornoEnviado = "0";
                    break;
               case "629":
                     MensagemRetornoEnviada = ";Valor do Produto difere do produto Valor Unitario de Comercializacao e Quantidade Comercial";
                     ValorRetornoEnviado = "0";
                     break;
               case "630":
                     MensagemRetornoEnviada = ";Valor do Produto difere do produto Valor Unitario de Tributacao e Quantidade Tributavel";
                     ValorRetornoEnviado = "0";
                      break;
       
       
               case "642":
                     MensagemRetornoEnviada = "Rejeição: Falha na Consulta do Registro de Passagem, tente novamente após 5 minutos";
                     ValorRetornoEnviado = "0";
                      break;
       
               case "656":
                     MensagemRetornoEnviada = "Rejeição: Consumo Indevido do WebService do SEFAZ";
                     ValorRetornoEnviado = "0";
                     break;
               case "678":
                     MensagemRetornoEnviada = "Rejeição: NF referenciada com UF diferente da UF da NF-e complementar";
                     ValorRetornoEnviado = "0";
                     break; 
               case "679":
                     MensagemRetornoEnviada = "Rejeição: Modelo da NF-e referenciada diferente de 55";
                     ValorRetornoEnviado = "0";
                     break;
                case "680":
                     MensagemRetornoEnviada = "Rejeição: Duplicidade de NF-e referenciada (Chave de Acesso referenciada mais de uma vez)";
                     ValorRetornoEnviado = "0";
                     break;
               case "681":
                     MensagemRetornoEnviada = "Rejeição: Duplicidade de NF Modelo 1 referenciada (CNPJ, Modelo, Série e Número)";
                     ValorRetornoEnviado = "0";
                      break;
               case "682":
                     MensagemRetornoEnviada = "Rejeição: Duplicidade de NF de Produtor referenciada (IE, Modelo, Série e Número)";
                     ValorRetornoEnviado = "0";
                     break;
               case "683":
                     MensagemRetornoEnviada = "Rejeição: Modelo do CT-e referenciado diferente de 57";
                     ValorRetornoEnviado = "0";
                      break;
               case "684":
                     MensagemRetornoEnviada = "Rejeição: Duplicidade de Cupom Fiscal referenciado (Modelo, Número e Ordem e COO)";
                     ValorRetornoEnviado = "0";
                     break;
               case "685":
                     MensagemRetornoEnviada = "Rejeição: Total do Valor Aproximado dos Tributos difere do somatório dos itens";
                     ValorRetornoEnviado = "0";
                     break;
               case "686":
                     MensagemRetornoEnviada = "Rejeição: NF Complementar referencia uma NF-e cancelada";
                     ValorRetornoEnviado = "0";
                      break;
              case "687":
                     MensagemRetornoEnviada = "Rejeição: NF Complementar referencia uma NF-e denegada";
                     ValorRetornoEnviado = "0";
                     break;
              case "688":
                     MensagemRetornoEnviada = "Rejeição: NF referenciada de Produtor com IE inexistente [nRef: xxx]";
                     ValorRetornoEnviado = "0";
                     break;
              case "689":
                     MensagemRetornoEnviada = "Rejeição: NF referenciada de Produtor com IE não vinculada ao CNPJ/CPF informado [nRef: xxx]";
                     ValorRetornoEnviado = "0";
                      break;
              case "690":
                     MensagemRetornoEnviada = "Rejeição: Pedido de Cancelamento para NF-e com CT-e";
                     ValorRetornoEnviado = "0";
                     break;
              case "694":
                     MensagemRetornoEnviada = "Rejeicao: Grupo de ICMS Interestadual para a UF de destino deve ser informado.";
                     ValorRetornoEnviado = "0";
                      break;
              case "696":
                      MensagemRetornoEnviada = "Rejeicao: Operacao com nao contribuinte deve indicar operacao com consumidor Final.";
                      ValorRetornoEnviado = "0";
                      break;
              case "699":
                      MensagemRetornoEnviada = "Rejeicao: Percentual do ICMS Interestadual para a UF de destino difere do previsto para o ano da Data de Emissao.";
                      ValorRetornoEnviado = "0";
                      break;                

              case "703":
                     MensagemRetornoEnviada = "Rejeição: Data-Hora de Emissao posterior ao horario de recebimento";
                     ValorRetornoEnviado = "0";
                     break;
             case "704":
                     MensagemRetornoEnviada = "Rejeicao: NFC-e com Data-Hora de emissao atrasada";
                     ValorRetornoEnviado = "0";
                     break;
             case "716":
                     MensagemRetornoEnviada = "Rejeicao: NFC-e em operacao nao destinada a consumidor final";
                     ValorRetornoEnviado = "0";
                     break;  
             case "725":
                     MensagemRetornoEnviada = "Rejeicao: NFC-e com CFOP invalido";
                     ValorRetornoEnviado = "0";
                      break;
            case "721":
                    MensagemRetornoEnviada = "Rejeição: Informado idEstrangeiro e Operacao nao é com consumidor final";
                    ValorRetornoEnviado = "0";
                    break;

                case "728":
                     MensagemRetornoEnviada = "Rejeicao: NF-e sem tag IE do destinatario";
                     ValorRetornoEnviado = "0";
                     break;
             case "732":
                     MensagemRetornoEnviada = "Rejeicao: CFOP de operacao interestadual e Destinatário interno";
                     ValorRetornoEnviado = "0";
                      break;
             case "767":
                     MensagemRetornoEnviada = "NFC-e com somatorio dos pagamentos diferente do total da Nota Fiscal";
                     ValorRetornoEnviado = "0";
                     break;

             case "772":
                     MensagemRetornoEnviada = "Rejeicao: Operacao interestadual e UF destinatario igual a UF de origem";
                     ValorRetornoEnviado = "0";
                     break;

             case "773":
                     MensagemRetornoEnviada = "Rejeicao: Operacao Interna e UF de destino diferente da UF do emitente";
                     ValorRetornoEnviado = "0";
                     break;

             case "750":
                     MensagemRetornoEnviada = "Rejeicao: NFC-e com valor total superior ao permitido para destinatario nao identificado";
                     ValorRetornoEnviado = "0";
                     break;

            

             case "778":
                     MensagemRetornoEnviada = "Rejeicao: Rejeicao: Informado NCM inexistente";
                     ValorRetornoEnviado = "0";
                     break;

             case "791":
                     MensagemRetornoEnviada = "Rejeicao: NF-e com indicacao de destinatario isento de IE, com a informacao da IE do destinatario";
                     ValorRetornoEnviado = "0";
                     break;      


             case "793":
                     MensagemRetornoEnviada = "Rejeicao: Valor do ICMS relativo ao Fundo de Combate a Pobreza na UF de destino difere do calculado";
                     ValorRetornoEnviado = "0";
                      break;

             case "794":
                      MensagemRetornoEnviada = "Rejeição : NF-e com indicativo de NFC-e com entrega a domicilio";
                      ValorRetornoEnviado = "0";
                      break;

             case "799":
                      MensagemRetornoEnviada = "Rejeição : Valor total do ICMS interestadual da UF de destino difere do somatorio dos itens";
                      ValorRetornoEnviado = "0";
                      break;
            

             case "805":
                      MensagemRetornoEnviada = "Rejeicao: A SEFAZ do destinatario nao permite Contribuinte Isento de Inscricao Estadual";
                      ValorRetornoEnviado = "0";
                      break;

             case "813":
                      MensagemRetornoEnviada = "Rejeicao: QR-Code com sequencia de escape para o e-comercial. Usar CDATA";
                      ValorRetornoEnviado = "0";
                      break;

             case "851":
                      MensagemRetornoEnviada = "Rejeicao:Soma do valor das parcelas difere do Valor Liquido da Fatura";
                      ValorRetornoEnviado = "0";
                      break; 

             case "852":
                      MensagemRetornoEnviada = "Rejeicao:Numero da parcela invalido ou nao informado [Ocorr:1]";
                      ValorRetornoEnviado = "0";
                      break; 

                    
            case "857":
                    MensagemRetornoEnviada = "Rejeicao: Número da parcela inválido ou não informado";
                    ValorRetornoEnviado = "0";
                    break;

            case "876":
                    MensagemRetornoEnviada = "Rejeicao:Operacao interestadual para Consumidor Final e valor do FCP informado em campo diferente de vFCPUFDest";
                    ValorRetornoEnviado = "0";
                    break;

            case "817":
                    MensagemRetornoEnviada = "Rejeição: Unidade Tributável incompatível com o NCM informado na operação com Comércio Exterior";
                    ValorRetornoEnviado = "0";
                    break;

            case "866":
                    MensagemRetornoEnviada = "Rejeição : Ausencia de troco quando o valor dos pagamentos informados for maior que o total da nota";
                    ValorRetornoEnviado = "0";
                    break;

            case "900":
                    MensagemRetornoEnviada = "Rejeição : Data de vencimento da parcela nao informada ou menor que Data de Emissao";
                    ValorRetornoEnviado = "0";
                    break;


            case "927":
                    MensagemRetornoEnviada = "Rejeição : Numero do item fora da ordem sequencial";
                    ValorRetornoEnviado = "0";
                    break;

            case "999":
                    MensagemRetornoEnviada = "Rejeição: Erro não catalogado (Erro 999)";
                    ValorRetornoEnviado = "0";
				    break;
            default:
                   MensagemRetornoEnviada = "Rejeição: Erro não catalogado";
                   ValorRetornoEnviado = "0";
				   break;
                }
                        
        }
    

    }
}
