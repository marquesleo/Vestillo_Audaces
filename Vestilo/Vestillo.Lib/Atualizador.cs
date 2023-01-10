using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtualizadorLIB;
using System.IO;

namespace Vestillo.Lib
{
    public static class Atualizador
    {

        public static bool VerificarAtualizacao(string versao, LoginMultiSistema.ModuloSistema modulo)
        {
            try
            {
                Atualizacao atualizador = new Atualizacao();
                IEnumerable<VersaoSistema> versoesDisponiveis = atualizador.VerificarAtualizacao(versao, modulo.ToString());

                return (versoesDisponiveis != null && versoesDisponiveis.Count() > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool BaixarAtualizacao(string versao, LoginMultiSistema.ModuloSistema modulo, out string fileName)
        {
            try
            {
                fileName = "";
                Atualizacao atualizador = new Atualizacao();
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Atualizacao");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                return  atualizador.DownloadAtualizacao(versao,modulo.ToString(), folder,  out fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
