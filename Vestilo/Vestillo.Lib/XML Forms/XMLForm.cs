using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using System.Windows.Forms;

namespace Vestillo.Lib
{
    public class XMLForm
    {
        public XMLForm()
        {
            Botoes = new List<XMLFormBotao>();
            Validacoes = new List<XMLFormValidacao>();
            VinculosCampos = new List<XMLFormVinculoCampo>();
            Tooltips = new List<XMLFormTooltip>();
        }

        public bool XMLValido
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Titulo);
            }
        }

        public int Id { get; set; }
        
        public string Titulo { get; set; }

        public bool BarraBotoes { get; set; }
        public bool ExibirFiltros { get; set; }
        public bool FecharBotaoSair { get; set; }
        public bool AtualizarBotaoAutomatico { get; set; }
                
        public bool CarregarGridAoIniciar { get; set; }
        
        public List<XMLFormBotao> Botoes { get; set; }
        public XMLFormGrid Grid { get; set; }

        public List<XMLFormTooltip> Tooltips { get; set; }
        public List<XMLFormValidacao> Validacoes { get; set; }
        public List<XMLFormVinculoCampo> VinculosCampos { get; set; }

        public XMLFormBotao RetornarBotaoPorId(string pId)
        {
            return Botoes.Where(d => d.Id == pId).SingleOrDefault();
        }
                
        public void IniciarLeitura(string conteudoXML)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            XmlDocument Xml = new XmlDocument();

            try
            {
                if (conteudoXML.Trim() == string.Empty)
                    throw new Exception("Conteúdo XML em Branco");

                Xml.LoadXml(conteudoXML);
            }
            catch (Exception ex)
            {
                throw new VestilloException(Enum_Tipo_VestilloNet_Exception.Formulario, "Falha na leitura XML.\n\n" + ex.Message);
            }

            foreach (XmlNode Principal in Xml.SelectNodes("formulario"))
            {
                if (Principal.HasChildNodes)
                {
                    foreach (XmlNode Node in Principal.ChildNodes)
                    {
                        if (Node.Name.Equals("titulo"))
                            Titulo = Node.InnerText;
                        if (Node.Name.Equals("barra_botoes"))
                            BarraBotoes = true;
                        if (Node.Name.Equals("exibir_filtros"))
                            ExibirFiltros = true;
                        if (Node.Name.Equals("carregar_grid_ao_iniciar"))
                            CarregarGridAoIniciar = true;
                        if (Node.Name.Equals("fechar_botao_sair"))
                            FecharBotaoSair = true;
                        if (Node.Name.Equals("atualizar_botao_automatico"))
                            AtualizarBotaoAutomatico = true;                                                
                        if (Node.Name.Equals("botoes"))
                            LerTagBotoes(Node);

                        if (Node.Name.Equals("tooltips"))
                            LerTagTooltips(Node);

                        if (Node.Name.Equals("vinculo_campo"))
                            LerTagVinculoCampo(Node);

                        if (Node.Name.Equals("validacao"))
                            LerTagValidacao(Node);

                        if (Node.Name.Equals("grid"))
                            LerTagGrid(Node);
                    }
                }
            }

            Xml = null;
        }

        private void LerTagTooltips(XmlNode xml)
        {
            foreach (XmlNode Node in xml.ChildNodes)
            {
                string Id = Node.Attributes["id"].Value;
                string Titulo = Node.Attributes["titulo"].Value;
                string Texto = Node.Attributes["texto"].Value;

                this.Tooltips.Add(new XMLFormTooltip()
                {
                    Id = Id,
                    Titulo = Titulo,
                    Texto = Texto
                });
            }
        }

        private void LerTagValidacao(XmlNode xml)
        {
            foreach (XmlNode Node in xml.ChildNodes)
            {
                var Novo = new XMLFormValidacao();

                Novo.Id = Node.Attributes["id"].Value;
                
                foreach(XmlNode Item in Node.ChildNodes)
                {
                    if (Item.Name.Equals("requerido"))
                        Novo.Requerido = Item.InnerText;

                    if (Item.Name.Equals("minimo_caracteres"))
                    {
                        Novo.MsgMinimoCaracteres = Item.Attributes["msg"].Value;
                        Novo.MinimoCaracteres = int.Parse(Item.InnerText);
                    }

                    if (Item.Name.Equals("email_Valido"))
                        Novo.EMailValido = Item.InnerText;

                    if (Item.Name.Equals("maior_que_zero"))
                        Novo.MsgMaiorQueZero = Item.InnerText;

                    if (Item.Name.Equals("maior_que_valor"))
                    {
                        Novo.MsgMaiorQueValor = Item.InnerText;
                        Novo.MaiorQueValor = int.Parse(Item.Attributes["valor"].Value);
                    }
                }

                this.Validacoes.Add(Novo);
            }
        }

        private void LerTagVinculoCampo(XmlNode xml)
        {
            foreach (XmlNode Node in xml.ChildNodes)
            {
                string Id = Node.Attributes["id"].Value;
                string Componente = Node.Attributes["componente"].Value;
                
                this.VinculosCampos.Add(new XMLFormVinculoCampo()
                {
                    Id = Id,
                    Componente = Componente
                });
            }
        }

        private void LerTagGrid(XmlNode xml)
        {
            XMLFormGrid Grid = new XMLFormGrid();

            foreach (XmlNode Node in xml.ChildNodes)
            {
                if (Node.Name.Equals("informacao_sem_dados"))
                    Grid.InformacaoSemDados = Node.InnerText;

                if (Node.Name.Equals("linhas_titulo_coluna"))
                    Grid.LinhasTituloColuna = int.Parse(Node.InnerText);

                if (Node.Name.Equals("linhas_titulo_agrupamento"))
                    Grid.LinhasTituloAgrupamento = int.Parse(Node.InnerText);

                if (Node.Name.Equals("coluna_codigo_registro"))
                    Grid.ColunaCodigoRegistro = Node.InnerText;
                
                if (Node.Name.Equals("permitir_congelar_linhas"))
                    Grid.PermitirCongelarLinhas = true;

                if (Node.Name.Equals("tamanho_colunas_automatico"))
                    Grid.TamanhoColunasAutomatico = true;

                if (Node.Name.Equals("permitir_cancelamento_exportacao_excel"))
                    Grid.PermitirCancelamentoExportaroExcel = true;
            }

            this.Grid = Grid;
        }

        private void LerTagBotoes(XmlNode xml)
        {
            foreach (XmlNode Node in xml.ChildNodes)
            {
                var Botao = new XMLFormBotao();
                Botao.Id = Node.Attributes["id"].Value;
                Botao.Label = Node.Attributes["label"].Value;
                
                if (Node.Attributes["imagem"] != null)
                    Botao.Imagem = int.Parse(Node.Attributes["imagem"].Value);

                if (Node.Attributes["registro_valido"] != null)
                    Botao.RegistroValido = Node.Attributes["registro_valido"].Value.Equals("SIM");

                if (Node.Attributes["form_registro_automatico"] != null)
                    Botao.FormRegistroAutomatico = Node.Attributes["form_registro_automatico"].Value.Equals("SIM");                

                if (Node.Attributes["confirmar"] != null)
                    Botao.Confirmar = Node.Attributes["confirmar"].Value.Equals("SIM");

                if (Node.Attributes["msg_confirmacao"] != null)
                    Botao.MsgConfirmacao = Node.Attributes["msg_confirmacao"].Value;

                this.Botoes.Add(Botao);
            }
        }
    }
}
