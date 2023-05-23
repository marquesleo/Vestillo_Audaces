using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using System.Globalization;

using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;

namespace Vestillo.Business
{
    public static class VestilloSession
    {
        private static string _urlWebAPI;
        public static bool UtilizaApi = false;
        private static TipoAcessoDados _tipoAcesso = 0;
        private static ControleEstoque _ControleDeEstoqueAtivo = 0;
        private static Sistemas _SistemasContratados = 0;
        private static EnviaEmailParaEmpresaAutomatico _EnviaEmailAutomatico = 0;
        public static Usuario UsuarioLogado { get; set; }
        public static IEnumerable<Empresa> EmpresasAcesso { get; set; }
        public static IEnumerable<ModuloSistema> ModulosSistema { get; set; }
        public static ModuloSistema ModuloLogado { get; set; }
        private static IEnumerable<Parametro> _parametros = null;
        private static int _qtdMaximaUsuariosLogados = 0;
        private static IEnumerable<Permissao> _permissoes = null;
        private static Empresa _empresaLogada;
        public static IEnumerable<EmpresaAcesso> EmpresaAcessoDados { get; set; }
        public static bool UsandoBanco = false;

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public static string _startPath = Application.StartupPath;//Caminho do exe.


        public static Empresa EmpresaLogada
        {
            get
            {
                return _empresaLogada;
            }
            set
            {
                _empresaLogada = value;
                Lib.Funcoes.SetIdEmpresaLogada = _empresaLogada.Id;
                List<EmpresaAcesso> empresas = VestilloSession.EmpresaAcessoDados.Where(x => x.EmpresaId == _empresaLogada.Id).ToList();
                //empresas.Add(_empresaLogada.Id);

                List<Vestillo.Lib.Funcoes.ListItem> acessoTabelas = new List<Lib.Funcoes.ListItem>();

                foreach (EmpresaAcesso e in empresas)
                {
                    acessoTabelas.Add(new Vestillo.Lib.Funcoes.ListItem() { Key = e.EmpresaAcessoId, Value = e.Tabela ?? "" });
                }

                acessoTabelas.Add(new Vestillo.Lib.Funcoes.ListItem() { Key = _empresaLogada.Id, Value = "" });

                Lib.Funcoes.EmpresasAcesso = acessoTabelas;
            }
        }

        public enum TipoAcessoDados
        {
            BD = 1,
            WebAPI = 2
        }

        public enum ControleEstoque
        {
            SIM = 1,
            NAO = 2
        }

        public enum EnviaEmailParaEmpresaAutomatico
        {
            SIM = 1,
            NAO = 2
        }

        public enum Sistemas
        {
            GESTAO = 1,
            PRODUCAO = 2,
            AMBOS = 3,
            PRODUCAO_JUNIOR = 4
        }

        public static string UrlWebAPI
        {
            get
            {
                if (string.IsNullOrEmpty(_urlWebAPI))
                {
                    _urlWebAPI = Vestillo.Lib.Funcoes.LerConfiguracao("service");
                }
                return _urlWebAPI;
            }
        }

        public static TipoAcessoDados TipoAcesso
        {
            get
            {
                if (!Vestillo.Connection.ProviderFactory.IsAPI) //Alterado para Audaces, testar
                {
                    if (_tipoAcesso == 0)
                    {
                        _tipoAcesso = (TipoAcessoDados)Int32.Parse(Vestillo.Lib.Funcoes.LerConfiguracao("type"));
                    }
                }
                else
                    //_tipoAcesso = TipoAcessoDados.BD; ALterado para Léo 17/05 - Audaces
                    return TipoAcessoDados.BD;

                return _tipoAcesso;
            }
        }

        public static bool UsaFiltroPacote
        {
            get
            {
                string param = ValorParametro("USA_FILTRO_PACOTE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public static int DiasFichaTecnica
        {
            get
            {
                string param = ValorParametro("DIAS_ATUALIZACAO_FICHA");

                if (!string.IsNullOrEmpty(param))
                {
                    return Int32.Parse(param);
                }
                else
                {
                    return 0;
                }
            }
        }

        public static int QtdMaximaUsuariosLogados
        {
            get
            {
                if (_qtdMaximaUsuariosLogados == 0)
                {
                    _qtdMaximaUsuariosLogados = Int32.Parse(ValorParametro("USUARIOS"));
                }

                return _qtdMaximaUsuariosLogados;
            }
        }

        public static int QtdCasasPreco
        {
            get
            {
                string param = ValorParametro("CASAS_PRECO");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int QtdCasasTempo
        {
            get
            {
                string param = ValorParametro("CASAS_TEMPO");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 5;
            }
        }

        public static bool TabelaPrecoVenda
        {
            get
            {
                string param = ValorParametro("TABELA_DE_PRECO_VENDA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool UsaTabelaPrecoBase
        {
            get
            {
                string param = ValorParametro("USA_TABELA_PRECO_BASE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool SomenteOperacoes
        {
            get
            {
                string param = ValorParametro("SOMENTE_OPERACOES");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool OperacaoPacoteFinalizado
        {
            get
            {
                string param = ValorParametro("OPERACAO_PACOTE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool UsaConferencia
        {
            get
            {
                string param = ValorParametro("USA_CONFERENCIA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool LiberaPedidoSemEmpenho
        {
            get
            {
                string param = ValorParametro("LIBERACAO_SEM_EMPENHO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool AtuaizaTempoPacote
        {
            get
            {
                string param = ValorParametro("ATUALIZA_TEMPO_PACOTE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool UsaTabelaPCP
        {
            get
            {
                string param = ValorParametro("USA_TABELA_PCP");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static string FormatarCustoEmpresa(string valor)
        {
            decimal valorSaida;

            if (Decimal.TryParse(valor.Trim(), out valorSaida))
                return string.Format(Vestillo.Business.VestilloSession.FormatoCusto, valorSaida);
            else
                return "";
        }

        public static string FormatoCusto
        {
            get
            {
                string formato = new string('0', QtdCasasCustoEmpresa);
                formato = "{0:#,0." + formato + "}";
                return formato;
            }
        }

        public static int QtdCasasCustoEmpresa
        {
            get
            {
                string param = ValorParametro("CUSTO_EMPRESA");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 4;
            }
        }

        public static bool AtualizaPreco
        {
            get
            {
                string param = ValorParametro("ATUALIZA_PRECO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool CopiaDescricaoCupom
        {
            get
            {
                string param = ValorParametro("COPIA_DESCRICAO_CUPOM");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool ManterMaiorPreco
        {
            get
            {
                string param = ValorParametro("MANTER_MAIOR_PRECO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool UsaImpressaoPedidoPersonalizada
        {
            get
            {
                string param = ValorParametro("IMPRESSAO_PERSONALIZADA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static string FormatoPreco
        {
            get
            {
                string formato = new string('0', QtdCasasPreco);
                formato = "{0:#,0." + formato + "}";
                return formato;
            }
        }

        public static string FormatoTempo
        {
            get
            {
                string formato = new string('0', QtdCasasTempo);
                formato = "{0:#,0." + formato + "}";
                return formato;
            }
        }



        public static string FormatoMovimento
        {
            get
            {
                string formato = new string('0', QtdCasasMovimento);
                formato = "{0:#,0." + formato + "}";
                return formato;
            }
        }

        public static string FormatoPrecoTotalizador
        {
            get
            {
                string formato = new string('0', 2);
                formato = "{0:#,0." + formato + "}";
                return formato;
            }
        }

        public static string FormatoQuantidade
        {
            get
            {
                string formato = new string('0', QtdCasasQuantidade);
                formato = "{0:#,0." + formato + "}";
                return formato;
            }
        }

        public static int QtdCasasQuantidade
        {
            get
            {
                string param = ValorParametro("CASAS_QUANTIDADE");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int TipoCalculoCustoFornecedor
        {
            get
            {
                string param = ValorParametro("CUSTO FORNECEDOR");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 1;
            }
        }

        public static string FormatarMoeda(string valor)
        {
            decimal valorSaida;

            if (Decimal.TryParse(valor.Trim(), out valorSaida))
                return string.Format(Vestillo.Business.VestilloSession.FormatoPreco, valorSaida);
            else
                return "";
        }

        public static string FormatarTempo(string valor)
        {
            decimal valorSaida;

            if (Decimal.TryParse(valor.Trim(), out valorSaida))
                return string.Format(Vestillo.Business.VestilloSession.FormatoTempo, valorSaida);
            else
                return "";
        }

        public static string FormatarMovimento(string valor)
        {
            decimal valorSaida;

            if (Decimal.TryParse(valor.Trim(), out valorSaida))
                return string.Format(Vestillo.Business.VestilloSession.FormatoMovimento, valorSaida);
            else
                return "";
        }

        public static int DiasPercentuaisEmpresa
        {
            get
            {
                string param = ValorParametro("DIAS_PERCENTUAIS_EMPRESA");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static bool TriggerPercentuaisEmpresa
        {
            get
            {
                string param = ValorParametro("PERCENTUAIS_EMPRESAS");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static string FormatarMoedaTotalizador(string valor)
        {
            decimal valorSaida;

            if (Decimal.TryParse(valor.Trim(), out valorSaida))
                return string.Format(Vestillo.Business.VestilloSession.FormatoPrecoTotalizador, valorSaida);
            else
                return "";
        }

        public static string FormatarQuantidade(string qtd)
        {
            decimal valorSaida;

            if (Decimal.TryParse(qtd.Trim(), out valorSaida))
                return string.Format(Vestillo.Business.VestilloSession.FormatoQuantidade, valorSaida);
            else
                return "";
        }

        private static string ValorParametro(string chave)
        {
            CarregarParametros();
            string param = _parametros.Where(x => x.Chave == chave && (x.EmpresaId == 0 || x.EmpresaId == _empresaLogada.Id)).Select(x => x.Valor).FirstOrDefault();

            if (string.IsNullOrEmpty(param))
                return "";

            return param;
        }

        public static void RecarregarParametros()
        {
            _parametros = null;
            CarregarParametros();
        }

        private static void CarregarParametros()
        {

            var service = new Vestillo.Business.Service.ParametroService();
            _parametros = service.GetServiceFactory().GetAll();

        }

        public static string limparCpfCnpj(string CpfCnpj)
        {
            if (CpfCnpj == null)
                return null;

            return CpfCnpj.Replace("-", "").Replace("(", "").Replace(",", "")
                      .Replace(")", "").Replace("-", "")
                      .Replace("-", "").Replace("/", "")
                      .Replace(".", "").Trim();
        }

        public static string UsuarioLogadoWindows()
        {
            return Environment.UserName;
        }

        public static string NomeComputador()
        {
            return Environment.MachineName;
        }

        public static string Ip()
        {
            string Retorno = string.Empty;
            string NomeMaquina = Dns.GetHostName();
            IPHostEntry ipE = Dns.GetHostEntry(NomeMaquina);
            IPAddress[] IpA = ipE.AddressList;
            if (IpA.Length > 0)
            {
                foreach (IPAddress IP in IpA)
                    if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        Retorno = IP.ToString();
                        break;
                    }
            }
            else
            {
                Retorno = string.Empty;
            }
            return Retorno;
        }


        public static bool PermissaoAcesso(string chave)
        {
            if (string.IsNullOrEmpty(chave))
                return false;

            if (UsuarioLogado.Grupos == null)
                return false;

            if (UsuarioLogado.Grupos.Count() == 0)
                return false;

            if (_permissoes == null)
            {
                var listaGrupos = UsuarioLogado.Grupos.ToList();

                string grupos = "";

                for (int i = 0; i < listaGrupos.Count(); i++)
                {
                    grupos += listaGrupos[i].GrupoId.ToString() + ",";
                }

                if (grupos != "")
                    grupos = grupos.Remove(grupos.ToString().Length - 1, 1);

                var service = new Service.PermissaoService().GetServiceFactory();
                _permissoes = service.GetByGrupos(grupos);
            }

            var result = (from p in _permissoes where p.Chave == chave select p).FirstOrDefault();

            return result != null;
        }

        public static int DefineEstoqueVenda
        {
            get
            {
                string param = ValorParametro("DEFINE_ESTOQUE_VENDA");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int EstoquePadraNfce
        {
            get
            {
                string param = ValorParametro("ESTOQUE_PADRAO_NFCE");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int DefineVendedorPadraoNFC
        {
            get
            {
                string param = ValorParametro("DEFINE_VENDEDORPADRAO_NFC");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int DefineClientePadraoNFC
        {
            get
            {
                string param = ValorParametro("DEFINE_CLIENTEPADRAO_NFC");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }


        public static int DefineTabPrecoPadraoNFC
        {
            get
            {
                string param = ValorParametro("DEFINE_TABPRECOPADRAO_NFC");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }


        public static int VerificaScanNFe
        {
            get
            {
                string param = ValorParametro("HABILITASCANNFE");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int GravarXmlNaRede
        {
            get
            {
                string param = ValorParametro("HABILLITA_XML_NA_REDE");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }


        public static string EstacaoParaGravarXml
        {
            get
            {
                return ValorParametro("UNIDADE_MAPEADA_XML_NA_RE");
            }
        }




        public static int VerificaContingenciaNfce
        {
            get
            {
                string param = ValorParametro("HABILITA_CONTINGENCIA_NFC");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int DefineCfopNfc
        {
            get
            {
                string param = ValorParametro("DEFINE_CFOP_NFC");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int DefineSerieNfc
        {
            get
            {
                string param = ValorParametro("DEFINE_SERIE_NFC");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int PossuiRegistroEan
        {
            get
            {
                string param = ValorParametro("POSSUIREGISTROEAN");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }


        public static string ObservacaoPadraoNotaVenda
        {
            get
            {
                return ValorParametro("OBS_NOTA_VENDA");
            }
        }




        public static string ValorSeNull(this string str, string valor)
        {
            if (string.IsNullOrWhiteSpace(str))
                return valor;
            else
                return str;
        }

        public static DateTime DataXMLNFeToDateTime(this string str)
        {
            // Exemplo: 2016-03-31T00:00:00-03:00

            // 2016-03-31

            int Ano = int.Parse(str.Substring(6, 4));
            int Mes = int.Parse(str.Substring(3, 2));
            int Dia = int.Parse(str.Substring(0, 2));



            DateTime Retorno = new DateTime(Ano, Mes, Dia);

            return Retorno;
        }

        public static string RemoverAcentos(this string str)
        {
            if (str == null)
            {
                str = String.Empty;
            }
            string s = str.Normalize(NormalizationForm.FormD);

            var sb = new StringBuilder();

            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }

        public static decimal ToRound(this decimal valor, int precisao)
        {
            return decimal.Round(valor, precisao);
        }

        public static string ToValorNFe(this decimal valor)
        {
            return valor.ToString().Replace(",", ".");
        }

        public static string ToValorNFe(this decimal valor, int casasdecimais)
        {
            if (casasdecimais > 0)
                return valor.ToString("F" + casasdecimais).Replace(",", ".");
            else
                return valor.ToString().Replace(",", ".");
        }


        public static string PreparaTexto(string strTexto)
        {
            return strTexto.Replace("<", "").Replace(">", "").Replace("  ", "").Replace(Environment.NewLine, " ")
                .Replace("&", "").Replace("ª", "").Replace("º", "").Replace("²", " ")
                .Replace("³", "").Replace("¹", "").Replace("/", "").Replace("//", "").Replace("///", "/")
                .Replace("°", "").Replace("–", "-").Replace("º", " ")
                .Replace("§", "").Replace("'", " ").Replace("½", " ")
                .Replace("¼", " ").Replace("¼", " ").Replace("¾", " ").Trim();

        }


        public static string RemoverCaracterEspecial(string text, bool allowSpace = false)
        {
            string ret;

            if (allowSpace)
                ret = System.Text.RegularExpressions.Regex.Replace(text, @"[^0-9a-zA-Z/éúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ\s]+?", string.Empty);
            else
                ret = System.Text.RegularExpressions.Regex.Replace(text, @"[^0-9a-zA-Z/éúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ]+?", string.Empty);

            return ret;
        }


        public static string CodPaisCodBarras
        {
            get
            {
                return ValorParametro("COD_PAIS_CODBARRAS");
            }
        }

        public static string CodEmpresaCodBarras
        {
            get
            {
                return ValorParametro("COD_EMPRESA_CODBARRAS");
            }
        }

        public static int ContadorCodBarras
        {
            get
            {
                string param = ValorParametro("SEQ_CODBARRAS");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static bool PontoDeVenda
        {
            get
            {
                string param = ValorParametro("PONTO_DE_VENDA");
                return param.Equals("1");
            }
        }

        public static int DataSaidaNfePadrao
        {
            get
            {
                string param = ValorParametro("DATA_SAIDA_PADRAO");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }


        public static int HabilitaTimerPedido
        {
            get
            {
                string param = ValorParametro("LIGAR_TIMER_PEDIDO");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int AgruparColunaRelatorio
        {
            get
            {
                string param = ValorParametro("AGRUPA_RELATORIO_TITULOS");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static string ColunaParaAgruparRelatorio
        {
            get
            {
                return ValorParametro("AGRUPAR_COLUNA_RELATORIO");
            }
        }


        public static decimal CalculaIpi(decimal BaseCalculo, decimal AlqIpi)
        {
            decimal ValorIpi = 0;
            BaseCalculo = decimal.Parse(string.Format(FormatoPrecoTotalizador, BaseCalculo));
            ValorIpi = (BaseCalculo * AlqIpi) / 100;
            ValorIpi = decimal.Parse(string.Format(FormatoPrecoTotalizador, ValorIpi));
            return ValorIpi;

        }

        public static ControleEstoque ControleDeEstoqueAtivo
        {
            get
            {


                _ControleDeEstoqueAtivo = (ControleEstoque)Int32.Parse(ValorParametro("CONTROLE_ESTOQUE_ATIVO"));
                return _ControleDeEstoqueAtivo;
            }
        }

        public static EnviaEmailParaEmpresaAutomatico EnviaEmailAutomatico
        {
            get
            {


                _EnviaEmailAutomatico = (EnviaEmailParaEmpresaAutomatico)Int32.Parse(ValorParametro("ENVIO_XML_AUTOMATICO"));


                return _EnviaEmailAutomatico;
            }
        }

        public static Sistemas SistemasContratados
        {
            get
            {

                if (_SistemasContratados == 0)
                {
                    _SistemasContratados = (Sistemas)Int32.Parse(ValorParametro("SISTEMAS_CONTRATADOS"));
                }

                return _SistemasContratados;
            }
        }

        public static string VersaoBanco
        {
            get
            {
                return ValorParametro("VersaoBanco");
            }
        }

        public static bool LeiDaModa
        {
            get
            {
                string param = ValorParametro("LEI_DA_MODA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool EstoquePedido
        {
            get
            {
                string param = ValorParametro("ESTOQUE_PEDIDO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool DescontarIcmsPisCofins
        {
            get
            {
                string param = ValorParametro("DECONTA_ICMS_PIS_COFINS");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool ExibirTipoNegocio
        {
            get
            {
                string param = ValorParametro("EXIBIR_TIPO_NEGOCIO");

                if (!string.IsNullOrEmpty(param))
                {
                    return param.Equals("1");
                }

                return false;
            }
        }

        public static bool EmitirNfceDiretoNaImpressora
        {
            get
            {
                string param = ValorParametro("EMITIR_NFCE_DIRETO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }


        public static string ModeloCaminhoNfce
        {
            get
            {
                string param = ValorParametro("NOME_IMPRESSORA_NFCE");

                if (!string.IsNullOrEmpty(param))
                {
                    return ValorParametro("NOME_IMPRESSORA_NFCE");
                }

                return String.Empty;
            }
        }


        public static bool ExibirEstimativaProducao
        {
            get
            {
                string param = ValorParametro("EXIBIR_ESTIMATIVA_PROD");

                if (!string.IsNullOrEmpty(param))
                {
                    return param.Equals("1");
                }

                return false;
            }
        }

        public static bool UtilizaSintegra
        {
            get
            {
                string param = ValorParametro("UTILIZA_SINTEGRA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }


        public static bool ExibirRefClienteRelPedido
        {
            get
            {
                string param = ValorParametro("REFCLIENTE_RELPEDIDO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool ControlaInadimplenciaCliente
        {
            get
            {
                string param = ValorParametro("CONTROLA_INADIMPLENCIA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }


        public static int DiasParaBloqueio
        {
            get
            {
                string param = ValorParametro("QTD_DIAS_INADIMPLENTE");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static bool ObrigaAlertaNaAtiviade
        {
            get
            {
                string param = ValorParametro("OBRIGA_ALERTA_ATIVIDADE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool HabilitaControleDeCaixa
        {
            get
            {
                string param = ValorParametro("HABILITA_CONTROLE_CAIXA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static int DefineCaixaPadrao
        {
            get
            {
                string param = ValorParametro("DEFINE_CAIXA_PADRAO");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }


        public static int AtualizaTempoDasFichas
        {
            get
            {
                string param = ValorParametro("ATUALIZA_TEMPO_FICHAS");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int QtdMaximaLogVestillo
        {
            get
            {
                string param = ValorParametro("QTD_MAXIMA_DIAS_LOG");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static bool HabilitaDataSaida
        {
            get
            {
                string param = ValorParametro("HABILITA_DATA_SAIDA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                    return false;
                }
            }
        }


        public static bool ExibeTelefoneNFe
        {
            get
            {
                string param = ValorParametro("EXIBIR_TELEFONE_NFE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static int QtdPacotePadrao
        {
            get
            {
                string param = ValorParametro("QTD_PACOTE_PADRAO");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static int QtdCasasMovimento
        {
            get
            {
                string param = ValorParametro("CASAS_MOVIMENTO");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 2;
            }
        }

        public static bool ValidaDiferimento
        {
            get
            {
                string param = ValorParametro("VALIDA_DIFERIMENTO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool PesquisaItensPeloEanXml
        {
            get
            {
                string param = ValorParametro("PESQUISA_ITEM_EAN_XML");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool TrataOperacaoManual
        {
            get
            {
                string param = ValorParametro("TRATA_OPERACAO_MANUAL");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                    return false;
                }
            }
        }

        public static bool ExibeImagemRelPedidoCompra
        {
            get
            {
                string param = ValorParametro("IMAGEM_REL_PEDIDO_COMPRA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool ControlaLicenca
        {

            get
            {
                bool VerificaALicenca = false;
                using (var empresa = new Vestillo.Business.Repositories.EmpresaRepository())
                {
                    try
                    {
                        VerificaALicenca = empresa.VerificaLicenca(VestilloSession.EmpresaLogada.CNPJ);
                    }
                    catch (Exception ex)
                    {
                    }
                }


                return VerificaALicenca;
            }
        }

        public static bool ImprimirEtiquetaCompacta
        {
            get
            {
                string param = ValorParametro("IMPRIMIR_ETIQUETA_COMPACT");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool UsaValorPartida
        {
            get
            {
                string param = ValorParametro("USA_VALOR_PARTIDA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool LimitaTotalPremio
        {
            get
            {
                string param = ValorParametro("LIMITA_TOTAL_PREMIO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool VerificaLicenca(string CNPJ)
        {
            String _conexaoMySQL = "";
            MySqlConnection con = null;
            string bloqueado = "NAO";


            if (String.IsNullOrEmpty(CNPJ))
            {
                bloqueado = "SIM";
                return true;
            }
            string cnpj = VestilloSession.limparCpfCnpj(CNPJ);
            decimal cnpjL = Convert.ToDecimal(cnpj);

            try
            {
                if (IsConnectedToInternet()) //Se emissão normal
                {

                    /*
                    var cripto = new Vestillo.Lib.Cripto();
                    var cs = cripto.Decrypt(Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "cs"));
                    var provider = Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "provider");
                    var servidor = Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "database");
                    var pw = cripto.Decrypt(Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "pw"));
                    var database = cripto.Decrypt(Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "databasename"));
                    var user = cripto.Decrypt(Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "username"));
                    */

                    var cs = "Server={0};Database=rvclientes;Uid=rvclientes;Pwd={1};Pooling=true";
                    var provider = "Mysql.Data.MysqlClient";
                    var servidor = "rvclientes.mysql.dbaas.com.br";
                    var pw = "Rv13023722";
                    var database = "rvclientes";
                    var user = "rvclientes";

                    _conexaoMySQL = string.Format(cs, servidor, pw);

                    String sql = "SELECT * FROM clientes where cnpj = " + cnpjL;
                    con = new MySqlConnection(_conexaoMySQL);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        bloqueado = dt.Rows[0]["BLOQUEADO"].ToString();
                    }
                    else
                    {
                        bloqueado = "SIM";
                    }

                    if (bloqueado == "SIM")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static int VerificaLicencaCupom(string CNPJ)
        {
            String _conexaoMySQL = "";
            MySqlConnection con = null;
            int licencas = 0 ;


            if (String.IsNullOrEmpty(CNPJ))
            {
                
                return licencas;
            }
            string cnpj = VestilloSession.limparCpfCnpj(CNPJ);
            decimal cnpjL = Convert.ToDecimal(cnpj);

            try
            {
                if (IsConnectedToInternet()) //Se emissão normal
                {

                    /*
                    var cripto = new Vestillo.Lib.Cripto();
                    var cs = cripto.Decrypt(Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "cs"));
                    var provider = Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "provider");
                    var servidor = Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "database");
                    var pw = cripto.Decrypt(Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "pw"));
                    var database = cripto.Decrypt(Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "databasename"));
                    var user = cripto.Decrypt(Vestillo.Lib.Funcoes.LerConfiguracao("Plus", "username"));
                    */

                    var cs = "Server={0};Database=rvclientes;Uid=rvclientes;Pwd={1};Pooling=true";
                    var provider = "Mysql.Data.MysqlClient";
                    var servidor = "rvclientes.mysql.dbaas.com.br";
                    var pw = "Rv13023722";
                    var database = "rvclientes";
                    var user = "rvclientes";

                    _conexaoMySQL = string.Format(cs, servidor, pw);

                    String sql = "SELECT * FROM clientes where cnpj = " + cnpjL;
                    con = new MySqlConnection(_conexaoMySQL);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        licencas = Convert.ToInt32(dt.Rows[0]["LICENCACUPOM"]);
                    }
                    else
                    {
                        licencas = 0;
                    }
                    return licencas;
                }
                else
                {
                    licencas = 0;
                    return licencas;
                }
            }
            catch (Exception ex)
            {
                licencas = 0;
                return licencas;
            }
        }
        /// <summary>
        /// verificar se está conectado à internet e se consegue acessar um site 
        /// </summary>
        /// <returns>true se existir uma conexão</returns>
        public static bool IsConnectedToInternet()
        {
            int Desc;
            bool ret = InternetGetConnectedState(out Desc, 0);

            if (ret)
                ret = IsReachable("http://google.com.br");

            return ret;
        }

        /// <summary>
        /// verifica se o destino informado está alcançável. (Acessível)
        /// </summary>
        /// <param name="_url">endereço URL</param>
        /// <returns>true se estiver alcançável</returns>
        public static bool IsReachable(string _url)
        {

            System.Uri Url = new System.Uri(_url);
            System.Net.WebRequest webReq;
            System.Net.WebResponse resp;
            webReq = System.Net.WebRequest.Create(Url);

            try
            {
                resp = webReq.GetResponse();
                resp.Close();
                webReq = null;

                return true;
            }
            catch
            {
                webReq = null;
                return false;
            }
        }

        public static void ModificaIni(string CaminhoAplicacao)
        {
            bool JaExiste = false;
            if (File.Exists(CaminhoAplicacao))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(CaminhoAplicacao))
                    {
                        String linha;
                        // Lê linha por linha até o final do arquivo
                        while ((linha = sr.ReadLine()) != null)
                        {
                            if (linha == "[ImpressoraEcf]")
                            {
                                JaExiste = true;
                                break;
                            }

                        }
                    }

                    if (JaExiste == false)
                    {
                        using (StreamWriter writer = new StreamWriter(CaminhoAplicacao, true))
                        {
                            writer.WriteLine("");
                            writer.WriteLine("");
                            writer.WriteLine("[ImpressoraEcf]");
                            writer.WriteLine("nome=Daruma");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static bool EmpresaNaoUsaNFCE
        {
            get
            {
                string param = ValorParametro("EMPRESA_NAOUSA_NFCE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static int AlteraTituloOperacaoFicha
        {
            get
            {
                string param = ValorParametro("ALTERA_TITULO_OPERACAO");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static bool UtilizaEnvioDeEmailAutomatico
        {
            get
            {
                string param = ValorParametro("UTILIZA_EMAIL_COBRANCA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool ImprimirBoletoAutomatico
        {
            get
            {
                string param = ValorParametro("IMPRIMIR_BOLETO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static int QtdViasImprimirNfce
        {
            get
            {
                string param = ValorParametro("QTD_VIAS_NFCE");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 1;
            }
        }

        public static string TempoDoPacote
        {
            get
            {
                string param = ValorParametro("DEFINIR_TEMPO_PACOTE");

                if (!string.IsNullOrEmpty(param))
                {
                    return param;
                }

                return "0,70";
            }
        }

        public static bool TemSistemaContratado(int TipoSistema)
        {
            bool TemPermissaoDeUso = false;
            Sistemas SistemasContratados = 0;


            try
            {
                var Param = new Service.ParametroService().GetServiceFactory().GetByChave("SISTEMAS_CONTRATADOS");



                if (Param != null)
                {
                    SistemasContratados = (Sistemas)Int32.Parse(Param.Valor);
                    if (TipoSistema == 1)
                    {
                        if (SistemasContratados == Sistemas.AMBOS || SistemasContratados == Sistemas.GESTAO)
                        {
                            TemPermissaoDeUso = true;
                        }

                    }
                    else if (TipoSistema == 2)
                    {
                        if (SistemasContratados == Sistemas.AMBOS || SistemasContratados == Sistemas.PRODUCAO)
                        {
                            TemPermissaoDeUso = true;
                        }
                    }
                    else if (TipoSistema == 4)
                    {
                        if (SistemasContratados == Sistemas.PRODUCAO_JUNIOR)
                        {
                            TemPermissaoDeUso = true;
                        }
                    }


                }
                else
                {
                    TemPermissaoDeUso = false;
                }

            }
            catch (Lib.VestilloException ex)
            {
                Lib.Funcoes.ExibirErro(ex);
                return false;
            }



            return TemPermissaoDeUso;
        }


        public static bool UtilizaPrecoOperacao
        {
            get
            {
                string param = ValorParametro("UTILIZA_PRECO_OPERACAO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool GravarBoletoNaRede
        {
            get
            {
                string param = ValorParametro("BOLETO_NA_REDE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }


        public static string EstacaoParaGravarBoleto
        {
            get
            {
                return ValorParametro("UNIDADE_BOLETO_NA_REDE");
            }
        }


        public static bool ControlaEanDuplicado
        {
            get
            {
                string param = ValorParametro("CONTROLA_EAN_DUPLICADO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                    return false;
                }
            }
        }

        public static int UsuarioEanDuplicado
        {
            get
            {
                string param = ValorParametro("USUARIO_EAN_DUPLICADO");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static bool AlteraTempoRelFicha
        {
            get
            {
                string param = ValorParametro("ALTERA_TEMPO_REL_FICHA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                    return false;
                }
            }
        }

        public static bool RealizaBaixaParcial
        {
            get
            {
                string param = ValorParametro("REALIZA_BAIXA_PARCIAL");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                    return false;
                }
            }
        }

        public static bool ManipulaEstoqueDevolucao
        {
            get
            {
                string param = ValorParametro("USA_ESTOQUE_DEVOLUCAO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {

                    return false;
                }
            }
        }

        public static int EstoquePadraDevolucao
        {
            get
            {
                string param = ValorParametro("ESTOQUE_PADRAO_DEVOLUCAO");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static bool UsaRelPedidoLoja
        {
            get
            {
                string param = ValorParametro("USA_REL_PEDIDO_LOJA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool UsaOrdenacaoFixa
        {
            get
            {
                if (!Vestillo.Connection.ProviderFactory.IsAPI)
                {
                    string param = ValorParametro("USA_ORDENACAO_FIXA"); //Alterado para Audaces, testar

                    if (!string.IsNullOrEmpty(param))
                    {
                        if (int.Parse(param) == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    return false;
                }
                else
                    return false;
            }
        }

        public static bool UserEscolheTabela
        {
            get
            {
                string param = ValorParametro("USER_ESCOLHE_TABELA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool UsaPrioridadeOrdem
        {
            get
            {
                string param = ValorParametro("USA_PRIORIDADE_ORDEM");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool UsaTipoMovPadrao
        {
            get
            {
                string param = ValorParametro("USA_TIPOMOV_PADRAO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static string TipoMovPadrao
        {
            get
            {
                string param = ValorParametro("TIPO_MOV_PADRAO");

                if (!string.IsNullOrEmpty(param))
                {
                    return param;
                }

                return "501";
            }
        }

        public static bool UsaPortalRepresentante
        {
            get
            {
                string param = ValorParametro("USA_PORTAL_REPRESENTANTE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool CalculaComissaoSobreParcela
        {
            get
            {
                string param = ValorParametro("CALCULA_COMISSAO_PARCELA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool UtilizaObservacaoDoClienteNaNota
        {
            get
            {
                string param = ValorParametro("OBS_CLIENTE_NOTA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool FinalizaPacoteFaccao
        {
            get
            {
                string param = ValorParametro("FINALIZA_PACOTE_FACCAO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool DesconsideraBancoPadrao
        {
            get
            {
                string param = ValorParametro("DESCONSIDERA_BANCO_PADRAO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool OcultaSegUMFicha
        {
            get
            {
                string param = ValorParametro("OCULTA_SEG_UM_FICHA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool UsaStatusCorte
        {
            get
            {
                string param = ValorParametro("STATUS_CORTE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }


        public static bool DescontarDesonerado
        {
            get
            {
                string param = ValorParametro("DESCONTAR_DESONERADO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }


        public static string PadraoPlanilhaProduto
        {
            get
            {
                string param = ValorParametro("PADRAO_PLANILHA_PRODUTO");

                if (!string.IsNullOrEmpty(param))
                {
                    return param;
                }

                return "0";
            }
        }


        public static bool TabelaPrecoManual
        {
            get
            {
                string param = ValorParametro("TABELA_PRECO_MANUAL");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool ConsideraProdutividade
        {
            get
            {
                string param = ValorParametro("CONSIDERA_PRODUTIVIDADE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool HabilitaDescontoAval
        {
            get
            {
                string param = ValorParametro("HABILITA_DESCONTO_AVAL");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool CopiaObsFaturamento
        {
            get
            {
                string param = ValorParametro("COPIA_OBS_FATURAMENTO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool NaoExibeGradeItem
        {
            get
            {
                string param = ValorParametro("NAO_EXIBE_GRADE_ITEM");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }


        public static string DefineSerieNFe
        {
            get
            {
                string param = ValorParametro("DEFINE_SERIE_NFE");

                if (!string.IsNullOrEmpty(param))
                {
                    return param;
                }

                return "1";
            }
        }

        public static bool FiltraItemPorDescricao
        {
            get
            {
                string param = ValorParametro("FILTRA_ITEM_DESCRICAO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool CustoFichaPorExcecao
        {
            get
            {
                string param = ValorParametro("CUSTO_FICHA_EXCECAO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool IncluiFichaAutomatica
        {
            get
            {
                if (!Connection.ProviderFactory.IsAPI) // alterado Audaces
                {
                    string param = ValorParametro("INCLUI_FICHA_AUTOMATICA");

                    if (!string.IsNullOrEmpty(param))
                    {
                        if (int.Parse(param) == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return false;
            }
        }

        public static string OperacaoFichaAutomatica
        {
            get
            {
                if (!Vestillo.Connection.ProviderFactory.IsAPI) // Alterado Audaces
                {
                    string param = ValorParametro("OPERAÇÃO_FICHA_AUTOMATICA");

                    if (!string.IsNullOrEmpty(param))
                    {
                        return param;
                    }
                }

                return "0";
            }
        }

        public static bool OcultaObsRomaneio
        {
            get
            {
                string param = ValorParametro("OCULTA_OBS_ROMANEIO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static string LimiteCreditoPadrao
        {
            get
            {
                string param = ValorParametro("LIMITE_CREDITO_PADRAO");

                if (!string.IsNullOrEmpty(param))
                {
                    return param;
                }

                return "0";
            }
        }

        public static bool ExibirCorRota
        {
            get
            {
                string param = ValorParametro("COR_ROTA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool UsaFichaProduto
        {
            get
            {

                if (!Vestillo.Connection.ProviderFactory.IsAPI) //alterado para audaces
                {
                    string param = ValorParametro("USA_FICHA_PRODUTO");

                    if (!string.IsNullOrEmpty(param))
                    {
                        if (int.Parse(param) == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
        }

        public static bool UsaDescricaoAlternativa
        {
            get
            {
                string param = ValorParametro("DESC_ALTERNATIVA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool RelOpPontoCerto
        {
            get
            {
                string param = ValorParametro("REL_OP_PONTOCERTO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public static int DefineTipoNFCe
        {
            get
            {
                string param = ValorParametro("DEFINE_TIPO_NFCE");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static bool ObrigaGuiaNFCe
        {
            get
            {
                string param = ValorParametro("OBRIGA_GUIA_NFCE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static bool UsaPrecoCompraFaccao
        {
            get
            {
                string param = ValorParametro("USA_PRECO_COMPRA_FACCAO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static bool ObrigaChequeNFCe
        {
            get
            {
                string param = ValorParametro("OBRIGA_CHEQUE_NFCE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static bool SalvaObsChequeCliente
        {
            get
            {
                string param = ValorParametro("SALVA_OBS_CHEQUE_CLIENTE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static bool AlteraNFCeVenda
        {
            get
            {
                string param = ValorParametro("ALTERA_NFCE_VENDA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static bool AgrupaRelPreVenda
        {
            get
            {
                string param = ValorParametro("AGRUPA_REL_PREVENDA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static bool ObrigaGradeItem
        {
            get
            {
                string param = ValorParametro("OBRIGAGRADEITEM");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static bool AtualizaProtheus
        {

            get
            {
                string param = ValorParametro("ATUALIZA_PROTHEUS");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }


        public static bool LancaOperacaoAuto
        {

            get
            {
                string param = ValorParametro("LANCA_OPERACAO_AUTO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }


        public static int DefineFuncionarioLancamento
        {
            get
            {
                string param = ValorParametro("FUNC_OPERACAO_AUTO");

                if (!string.IsNullOrEmpty(param))
                {
                    return int.Parse(param);
                }

                return 0;
            }
        }

        public static bool IntegracaoComMarketPlace
        {

            get
            {
                string param = ValorParametro("INTEGRA_MARKET");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static bool UtilizaLiberacaoParcialOrdemProducao
        {

            get
            {
                string param = ValorParametro("LIBERA_ORDEM_PARCIAL");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static bool ObrigaTempoPacote
        {
            get
            {
                string param = ValorParametro("OBRIGA_TEMPO_PACOTE");
                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    return false;

                }
                return false;
            }
        }

        public static bool SalvaAlteracaoGrid
        {
            get
            {
                string param = ValorParametro("SALVAR_GRID");
                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    return false;

                }
                return false;
            }
        }

        public static bool GravarRomaneioNaRede
        {
            get
            {
                string param = ValorParametro("ROMANEIO_NA_REDE");
                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }

        public static string EstacaoParaGravarRomaneio
        {
            get
            {
                return ValorParametro("UNIDADE_ROMANEIO_NA_REDE");
            }
        }

        public static bool UtilizaCombinacaoNfe
        {

            get
            {
                string param = ValorParametro("UTILIZA_COMBINACAO_NFE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static bool UtilizaConversaoCaixa
        {

            get
            {
                string param = ValorParametro("CONVERTE_CAIXA");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                }

                return false;

            }
        }

        public static bool AtualizaPrecoFichaPorFornecedor
        {
            get
            {
                string param = ValorParametro("ATUALIZA_FC_FORNECEDOR");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool AcessoPorTsWindows
        {
            get
            {
                string param = ValorParametro("ACESSO_TS_WINDOWS");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool IncluirItensNotaEntrada
        {
            get
            {
                string param = ValorParametro("INCLUIR_ITENS_XML");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool AgrupaLiberacaoOpSequencia
        {
            get
            {
                string param = ValorParametro("AGRUPAR_LIBERACAO_OP");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }


        public static bool ExibirEstoqueNaGrade
        {
            get
            {
                string param = ValorParametro("EXIBIR_ESTOQUE_GRADE");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool EmitirNFCeSemSelecionarCertificado
        {
            get
            {
                string param = ValorParametro("NFCE_SEM_CERTIFICADO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }


        public static bool VencimentoPorEmissao
        {
            get
            {
                string param = ValorParametro("VENCIMENTO_EMISSAO");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool ExecutaAcertoEstoquePorOp
        {
            get
            {
                string param = ValorParametro("ACERTO_ESTOQUE_OP");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool PintaLinhaMovMaterial
        {
            get
            {
                string param = ValorParametro("PINTA_LINHA_MATERIAL");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static bool BuscaEcfpDoArquivoIni
        {
            get
            {
                string param = ValorParametro("NFC_ECF_INI");

                if (!string.IsNullOrEmpty(param))
                {
                    if (int.Parse(param) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public static string ImpressoraEcf()
        {
            string NomeEcf = String.Empty;            
            var ECF = Vestillo.Lib.Funcoes.LerConfiguracao("ImpressoraEcf", "nome");
            NomeEcf = ECF;
            return NomeEcf;
        }


    }

}