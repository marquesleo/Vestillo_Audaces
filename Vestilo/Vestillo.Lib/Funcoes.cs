using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Threading;
using System.Text.RegularExpressions;

namespace Vestillo.Lib
{
    public static class Funcoes
    {

        public class ListItem
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }

        private static frmProcessamento _frmProcessamento;
        private static bool _processar;
        public static bool UtilizaAPI = false; // Leo para API
        private static string _filtroEmpresa;
        private static string _cs = "";
        private static int _idEmpresaLogada;
        public delegate void DelegateProcessamentoBackground();
        public delegate object DelegateProcessentoBackgroundData();

        private static List<ListItem> _EmpresasAcesso;
        public static List<ListItem> EmpresasAcesso
        {
            get
            {
                if (_EmpresasAcesso == null)
                {
                    _EmpresasAcesso = new List<Lib.Funcoes.ListItem>();
                    if (UtilizaAPI)
                        _EmpresasAcesso.Add(new Lib.Funcoes.ListItem() { Key = Funcoes.GetIdEmpresaLogada, Value = "" });
                }

                return _EmpresasAcesso;
            }
            set
            {
                _EmpresasAcesso = value;
            }
        }

        public static int GetIdEmpresaLogada { get { return _idEmpresaLogada; } }
        public static int SetIdEmpresaLogada { set { _idEmpresaLogada = value; } }
      
        public static string FiltroEmpresa(string campo)
        {
                return _filtroEmpresa;
        }

        public static Exception TratarException(Exception ex)
        {
            // GRAVAR LOG
            return new VestilloException(Enum_Tipo_VestilloNet_Exception.Outros, "Erro");
            
        }

        public static Exception TratarException(Exception ex, string msg)
        {
            // GRAVAR LOG

            return new VestilloException(Enum_Tipo_VestilloNet_Exception.Outros, msg);
        }

        public static void RetornaEndereco(string cep, ref TextBox txtEndereco, ref TextBox txtBairro, ref string municipio, ref string estado)
        {
    
            string tipo;
            string end;           

            // Objeto DataSet que receberá a tabela em XML que contém os dados da pesquisa
            DataSet ds = new DataSet();
            // Armazena o arquivo XML retirado da página onde o CEP foi pesquisado
            ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" + cep + "&formato=xml");
            // Caso tenha encontrado o CEP o valor da primeira célula da primeira linha da tabela será 1 
            if (ds.Tables[0].Rows[0]["resultado"].ToString() == "1")//CEP COMPLETO
            {
                estado = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                municipio = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                txtBairro.Text =  ds.Tables[0].Rows[0]["bairro"].ToString().Trim();
                txtBairro.Text = txtBairro.Text.Replace("'", "");
                tipo = ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim();
                end = ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
                txtEndereco.Text = tipo + " " + end;
            }
            else if (ds.Tables[0].Rows[0]["resultado"].ToString() == "2") //CEP UNICO
            {
                estado = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                municipio = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                txtBairro.Text = "";
                tipo = "";
                end = "";               
                txtEndereco.Text = "";
            }
            else //NÃO ENCONTRADO
            {
                estado = "";
                municipio = "";
                txtBairro.Text = "";
                tipo = "";
                txtEndereco.Text = "";
            }

        }


        public static void RetornaEnderecoMarketPlace(string cep, ref string txtEndereco, ref string txtBairro, ref string municipio, ref string estado)
        {

            string tipo;
            string end;

            // Objeto DataSet que receberá a tabela em XML que contém os dados da pesquisa
            DataSet ds = new DataSet();
            // Armazena o arquivo XML retirado da página onde o CEP foi pesquisado
            ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" + cep + "&formato=xml");
            // Caso tenha encontrado o CEP o valor da primeira célula da primeira linha da tabela será 1 
            if (ds.Tables[0].Rows[0]["resultado"].ToString() == "1")//CEP COMPLETO
            {
                estado = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                municipio = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                txtBairro = ds.Tables[0].Rows[0]["bairro"].ToString().Trim();
                txtBairro = txtBairro.ToString().Replace("'", "");
                tipo = ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim();
                end = ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
                txtEndereco = tipo + " " + end;
            }
            else if (ds.Tables[0].Rows[0]["resultado"].ToString() == "2") //CEP UNICO
            {
                estado = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                municipio = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                txtBairro = "";
                tipo = "";
                end = "";
                txtEndereco = "";
            }
            else //NÃO ENCONTRADO
            {
                estado = "";
                municipio = "";
                txtBairro = "";
                tipo = "";
                txtEndereco = "";
            }

        }

        ///<summary>
        /// Método converte Image para binario
        /// </summary>
        public static byte[] ConverteImageParaBinario(Image image)
        {
            if (image == null)
                return null;

            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Bmp);
            return ms.ToArray();
        }

        /// <summary>
        /// Método converte binario para Image
        /// </summary>
        /// <returns>Retorna um array de binario</returns>
        public static Image ConverteBinarioParaImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return (null);
            }

            return (Image.FromStream(new MemoryStream(byteArray)));
        }

        /// <summary>
        /// Se não for número ou backspace trava a tecla
        /// </summary>
        public static void ValidaCampoNumerico(KeyPressEventArgs e)
        {
            //Se não for número ou backspace trava a tecla
            if (!(Char.IsNumber(e.KeyChar)) && !(e.KeyChar == (char)8))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Tratamentos para impedir entradas inválidas em textbox decimal
        /// </summary>
        public static void ValidaCampoDecimal(TextBox txt, KeyPressEventArgs e)
        {
            //Se digitar um ponto ou virgurla na primeira posição do textbox impede a operação
           if (txt.Text.Length == 0 && (e.KeyChar == (char)44 || e.KeyChar == (char)46))
            {
                e.Handled = true;
            }
            //Verifica se já tem uma vígula e se a tecla precionada for uma virgula impede a operação
            if (txt.Text.Contains(",") && e.KeyChar == (char)44)
            {
                e.Handled = true;
            }

            //Verifica se já tem um ponto e se a tecla precionada for um ponto impede a operação
            if (txt.Text.Contains(".") && e.KeyChar == (char)46)
            {
                e.Handled = true;
            }

            //Se não for número, backspace, virgula ou ponto trava a tecla
            if (!(Char.IsNumber(e.KeyChar)) && !(e.KeyChar == (char)8) && !(e.KeyChar == (char)44) && !(e.KeyChar == (char)46))
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// Só permite digitar numero, virgula e backspace
        /// </summary>
        public static void SoNumeroVirgula(KeyPressEventArgs e)
        {
            //Se não for número, backspace, virgula trava a tecla
            if (!(Char.IsNumber(e.KeyChar)) && !(e.KeyChar == (char)8) && !(e.KeyChar == (char)44))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Só permite digitar numero, virgula e backspace
        /// </summary>
        public static void SoNumero(KeyPressEventArgs e)
        {
            //Se não for número, backspace, virgula trava a tecla
            if (!(Char.IsNumber(e.KeyChar)) && !(e.KeyChar == (char)8))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Não permite digitar letras ou espacos 
        /// </summary>
        public static void SemLetrasEspacos(KeyPressEventArgs e)
        {
            if ((Char.IsLetter(e.KeyChar)) || (Char.IsWhiteSpace(e.KeyChar)))//letras e espaço
                e.Handled = true;
        }

        /// <summary>
        /// Não permite digitar determinado caracter (@"'!@#$%¨&*()-_=+£¢¬§|\;/<>:?´~[]`^{}")
        /// </summary>
        public static void VerificaCaracter(TextBox txt)
        {
            string CharacIlegal = @"'!@#$%¨&*()-_=+£¢¬§|\;/<>:?´~[]`^{}"; 
            txt.Text = String.Join("", txt.Text.Split(CharacIlegal.ToCharArray()));
            txt.SelectionStart = txt.Text.Length;

        }

        public static byte[] LerAquivoParaBinario(string path)
        {
            byte[] file;
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) 
            {
                using (var reader = new BinaryReader(stream)) 
                {
                    file = reader.ReadBytes((int) stream.Length);       
                }          
            }
            return file;
        }

        public static void LerBinarioParaArquivo(string pathOut, byte[] file)
        {
            File.WriteAllBytes(pathOut, file);
        }
     
        public static string LerConfiguracao(string secao, string chave)
        {
            IniParser ini = new IniParser("Vestillo.ini");
            return ini.GetSetting(secao, chave);
        }

        public static string LerConfiguracao(string chave)
        {
            IniParser ini = new IniParser("Vestillo.ini");
            return ini.GetSetting("config", chave);
        }

        public static void ExibirErro(VestilloException ex)
        {
            FinalizarProcessamento();

            if (ex.TipoException == Enum_Tipo_VestilloNet_Exception.Registro_Duplicado)
            {
                MessageBox.Show(ex.Mensagem, "Vestillo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (ex.Mensagem.StartsWith("Cannot delete or update a parent row: a foreign key constraint fails"))
            {
                MessageBox.Show("Esse item não pode ser excluído porque está sendo utilizado.\n" + "Verifique se essa rotina possui a opção Inativar", "Vestillo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ocorreu um erro, entre em contato com o suporte. \n\n" + ex.Mensagem, "Vestillo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                        
        }

        public static void ExibirErro(Exception ex, IWin32Window owner = null)
        {
            if (ex is VestilloException)
            {
                ExibirErro(ex as VestilloException);
            }
            else
            {
                FinalizarProcessamento();

                if (ex.Message.Equals("Referência de objeto não definida para uma instância de um objeto."))
                {
                    if (owner != null)
                        MessageBox.Show(owner, "Ocorreu um erro, entre em contato com o suporte. \n\nObjeto não preenchido corretamente. \n\nFunção: " + ex.TargetSite.ToString(), "Vestillo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Ocorreu um erro, entre em contato com o suporte. \n\nObjeto não preenchido corretamente. \n\nFunção: " + ex.TargetSite.ToString(), "Vestillo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message.StartsWith("Cannot delete or update a parent row: a foreign key constraint fails"))
                {
                    if (owner != null)
                        MessageBox.Show(owner, "Esse item não pode ser excluído porque está sendo utilizado.\n" + "Verifique se essa rotina possui a opção Inativar", "Vestillo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Esse item não pode ser excluído porque está sendo utilizado.\n" + "Verifique se essa rotina possui a opção Inativar", "Vestillo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (owner != null)
                        MessageBox.Show(owner, "Ocorreu um erro, entre em contato com o suporte. \n\n" + ex.Message, "Vestillo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Ocorreu um erro, entre em contato com o suporte. \n\n" + ex.Message, "Vestillo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static bool ExibirPergunta(string mensagem)
        {
            Application.UseWaitCursor = false;
            FinalizarProcessamento();
            var result = MessageBox.Show(mensagem, "Vestillo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (result == DialogResult.Yes);
        }

        public static DialogResult ExibirMensagem(string mensagem, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            Application.UseWaitCursor = false;
            FinalizarProcessamento();
            return MessageBox.Show(mensagem, "Vestillo", buttons, icon);
        }

        public static void Processar(string mensagem)
        {
            _frmProcessamento.MensagemProcessamento = mensagem;
            _frmProcessamento.BringToFront();
            //Application.DoEvents();  
        }

        public static void Processar(Form formPai, string mensagem = "Carregando...")
        {
           //_backgroundWorker = new BackgroundWorker();
           _frmProcessamento = new frmProcessamento();
            _frmProcessamento.Show(formPai);
           _frmProcessamento.BringToFront();
           _frmProcessamento.MensagemProcessamento = mensagem;
           //Application.DoEvents();

           var d = new DelegateProcessamentoBackground(Processamento);
           d.BeginInvoke(null, null);
           //_backgroundWorker.DoWork += (sender, e) => Processamento();
           // _backgroundWorker.RunWorkerAsync();
        }

        private static void Processamento()
        {
            if (_frmProcessamento == null)
                return;

            _processar = true;
            _frmProcessamento.prgProcessando.Refresh();
            Application.UseWaitCursor = true;
            //Application.DoEvents();  

            while (_processar)
            {
                _frmProcessamento.BringToFront();
                _frmProcessamento.Refresh();
                _frmProcessamento.prgProcessando.Refresh();
                //Application.DoEvents();  
                Thread.Sleep(500);
            }

            _frmProcessamento.Close();
            _frmProcessamento.Dispose();
            _frmProcessamento = null;
            //_backgroundWorker.Dispose();
            //_backgroundWorker = null;
            Application.UseWaitCursor = false;
        }

        public static void FinalizarProcessamento()
        {
            _processar = false;
            Application.UseWaitCursor = false;
            Application.DoEvents();
        }

        public static void FinalizarProcessamento(IAsyncResult result)
        {
            _processar = false;
            Application.UseWaitCursor = false;
            Application.DoEvents();
        }

        public static bool Processando
        {
            get {return _processar;}
        }

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "").Replace(",", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static Image RedimensionarImagem(Image imgFoto, int TWidth, int THeight)
        {


            int fonteLargura = imgFoto.Width;     //armazena a largura original da imagem origem
            int fonteAltura = imgFoto.Height;   //armazena a altura original da imagem origem
            int origemX = 0;        //eixo x da imagem origem
            int origemY = 0;        //eixo y da imagem origem

            int destX = 0;          //eixo x da imagem destino
            int destY = 0;          //eixo y da imagem destino
            //Calcula a altura e largura da imagem redimensionada
            int destWidth = TWidth;
            int destHeight = THeight;

            //Cria um novo objeto bitmap
            Bitmap bmImagem = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            //Define a resolu~ção do bitmap.
            bmImagem.SetResolution(imgFoto.HorizontalResolution, imgFoto.VerticalResolution);
            //Crima um objeto graphics e defina a qualidade
            Graphics grImagem = Graphics.FromImage(bmImagem);
            grImagem.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //Desenha a imge usando o método DrawImage() da classe grafica
            grImagem.DrawImage(imgFoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(origemX, origemY, fonteLargura, fonteAltura),
                GraphicsUnit.Pixel);
            grImagem.Save();
            grImagem.Dispose();  //libera o objeto grafico
            MemoryStream objMemoryStreamModificado = new MemoryStream();
            bmImagem.Save(objMemoryStreamModificado, System.Drawing.Imaging.ImageFormat.Bmp);
            return bmImagem;
        }

        public static string FormatarCpfCnpj(string strCpfCnpj)
        {

            if (strCpfCnpj.Length <= 11)
            {

                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");

                mtpCpf.Set(ZerosEsquerda(strCpfCnpj, 11));

                return mtpCpf.ToString();

            }

            else
            {

                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");

                mtpCnpj.Set(ZerosEsquerda(strCpfCnpj, 11));

                return mtpCnpj.ToString();

            }

        }



        public static string ZerosEsquerda(string strString, int intTamanho)
        {

            string strResult = "";

            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {

                strResult += "0";

            }

            return strResult + strString;

        }

        public static string GetConnectionString()
        {

            if (string.IsNullOrWhiteSpace(_cs))
            {
                var cripto = new Cripto();
                var cs = cripto.Decrypt(Vestillo.Lib.Funcoes.LerConfiguracao("cs"));
                var provider = Vestillo.Lib.Funcoes.LerConfiguracao("provider");
                var servidor = Vestillo.Lib.Funcoes.LerConfiguracao("database");
                var pw = cripto.Decrypt(Vestillo.Lib.Funcoes.LerConfiguracao("pw"));

                _cs = string.Format(cs, servidor, pw);
            }

            return _cs;
        }

        public static int DigitoM10(string numero)
        {
            int i = 2;
            int sum = 0;
            int res = 0;
            foreach (char c in numero.Reverse().ToArray())
            {
                res = Convert.ToInt32(c.ToString()) * i;
                sum += res > 9 ? (res - 9) : res;
                i = i == 2 ? 1 : 2;
            }
            if (sum % 10 > 0)
            {
                return 10 - (sum % 10);
            }
            else
            {
                return sum % 10;
            }
            

        }

        public static int DigitoM11(long intNumero)
        {
            int[] intPesos = { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6, 7, 8, 9 };
            string strText = intNumero.ToString();

            if (strText.Length > 16)
                throw new Exception("Número não suportado pela função!");

            int intSoma = 0;
            int intIdx = 0;
            for (int intPos = strText.Length - 1; intPos >= 0; intPos--)
            {
                intSoma += Convert.ToInt32(strText[intPos].ToString()) * intPesos[intIdx];
                intIdx++;
            }
            int intResto = (intSoma * 10) % 11;
            int intDigito = intResto;
            if (intDigito >= 10)
                intDigito = 0;

            return intDigito;
        }

        public  static string FormatCode(string text, int length)
        {
            return text.PadLeft(length, '0');
        }

        public static bool ValidaEmail(string email)
        {
            //Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            //return expression.IsMatch(email);


            bool emailValido = false;

            //Expressão regular retirada de
            //https://msdn.microsoft.com/pt-br/library/01escwtf(v=vs.110).aspx
            string emailRegex = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            try
            {
                emailValido = Regex.IsMatch(
                    email,
                    emailRegex);
            }
            catch (RegexMatchTimeoutException)
            {
                emailValido = false;
            }

            return emailValido;

        }

        public static string Cor_Para_Hexa(Color c)
        {
            return ColorTranslator.ToOle(c).ToString("X");
        }

        public static Color Hexa_Para_Cor(string hx)
        {
            int x = int.Parse(hx, System.Globalization.NumberStyles.HexNumber);
            Color cor = ColorTranslator.FromOle(x);
            return cor;
        }

        public static bool ValidaTelefone(string Celular)
        {

            bool celularlValido = false;

            string padraoRegex = @"^[(]{1}\d{2}[)]{1}\d{4}[-]{1}\d{4}$";      
            try
            {
                celularlValido = Regex.IsMatch(
                    Celular,
                    padraoRegex);
            }
            catch (RegexMatchTimeoutException)
            {
                celularlValido = false;
            }

            return celularlValido;
        }
    }
      
}
