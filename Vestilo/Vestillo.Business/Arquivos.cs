
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;



namespace Vestillo.Business
{
    public class Arquivos
    {
        /// <summary>
        ///         ''' Exporta e Retorna o caminho do documento exportado
        ///         ''' </summary>
        public static string RetornarArquivoDeCampoBinario(byte[] Documento, string NomeDoDocumento)
        {
            return Arquivos.RetornarArquivoDeCampoBinario(Documento, RetornarNomeCompletoDoArquivo(NomeDoDocumento), true);
        }

        public static string CriarArquivoDeCampoBinario(byte[] Documento, string NomeDoDocumento)
        {
            return Arquivos.RetornarArquivoDeCampoBinario(Documento, RetornarNomeCompletoDoArquivo(NomeDoDocumento), false);
        }

        public static string CriarArquivoHTML(string Html, string NomeDoArquivo)
        {
            string NomeCompleto = RetornarNomeCompletoDoArquivo(NomeDoArquivo);

            if (File.Exists(NomeCompleto))
                File.Delete(NomeCompleto);

            System.IO.StreamWriter arquivoHTML = new System.IO.StreamWriter(NomeCompleto);
            arquivoHTML.Write(Html);
            arquivoHTML.Close();

            return NomeCompleto;
        }

        /// <summary>
        ///         ''' Html = String com o html já finalizado
        ///         ''' NomeDoPdf = Nome do PDF SEM A EXTENÇAO
        ///         ''' </summary>
        ///         ''' <param name="Html"></param>
        ///         ''' <param name="NomeDoPdf"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public static string CriarArquivoPdfDeHtml(string Html, string NomeDoPdf)
        {
            NReco.PdfGenerator.HtmlToPdfConverter oNReco = new NReco.PdfGenerator.HtmlToPdfConverter();
            oNReco.Zoom = 1.25F;
            return CriarArquivoDeCampoBinario(oNReco.GeneratePdf(Html), NomeDoPdf + ".pdf");
        }



        public static string DiretorioDeGravacao
        {
            get
            {
                string _startPath = String.Empty;
                DateTime dia = DateTime.Now;
                string diaEmissao = dia.ToShortDateString();
                string MesAno = diaEmissao.ToString().Substring(3, 2) + diaEmissao.ToString().Substring(6, 4);

                if(VestilloSession.GravarBoletoNaRede)
                {
                    _startPath = VestilloSession.EstacaoParaGravarBoleto + "\\Boleto" + "\\" + MesAno;//Caminho do exe.
                }
                else
                {
                    _startPath = Application.StartupPath + "\\Boleto" + "\\" + MesAno;//Caminho do exe.
                }
                

                if(!Directory.Exists(_startPath))
                {
                    Directory.CreateDirectory(_startPath);
                }

                return _startPath; //My.Computer.FileSystem.SpecialDirectories.Temp;
            }
        }

        public static string RetornarNomeCompletoDoArquivo(string NomeDoArquivo)
        {
            if (string.IsNullOrEmpty(NomeDoArquivo))
                return string.Empty;
            else
                return string.Format(@"{0}\{1}", DiretorioDeGravacao, NomeDoArquivo);
        }

        protected static string RetornarArquivoDeCampoBinario(byte[] Documento, string NomeCompleto, bool AproveitarExistente)
        {
            if (string.IsNullOrEmpty(NomeCompleto))
                return string.Empty;
            else
            {
                if (System.IO.File.Exists(NomeCompleto) && !AproveitarExistente)
                    System.IO.File.Delete(NomeCompleto);

                if (!System.IO.File.Exists(NomeCompleto))
                    File.WriteAllBytes(NomeCompleto, Documento);

                return NomeCompleto;
            }
        }




        public static void unificarPDFs(List<string> InFiles, string OutFile)
        {
            OutFile = RetornarNomeCompletoDoArquivo(OutFile);
            using (FileStream stream = new FileStream(OutFile, FileMode.Create))
            {
                iTextSharp.text.Document doc = new iTextSharp.text.Document();
                iTextSharp.text.pdf.PdfCopy pdf = new iTextSharp.text.pdf.PdfCopy(doc, stream);

                doc.Open();

                foreach (string nomeArquivoPDF in InFiles)
                {
                    iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(nomeArquivoPDF);

                    for (int i = 0; i <= reader.NumberOfPages - 1; i++)
                        pdf.AddPage(pdf.GetImportedPage(reader, i + 1));

                    pdf.FreeReader(reader);
                    reader.Close();
                    try
                    {
                        File.Delete(nomeArquivoPDF);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
    }
}
