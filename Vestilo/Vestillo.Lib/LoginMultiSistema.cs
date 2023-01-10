using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Vestillo.Lib
{
    public class LoginMultiSistema
    {

        private string _pathSecurityFile = Path.Combine(Path.GetTempPath(), "Vestillo");

        public enum ModuloSistema
        {
            Gestao,
            Producao,
            ProducaoJunior
        }

        private ModuloSistema _currentSystem;

        public LoginMultiSistema(ModuloSistema currentSystem)
        {
            _currentSystem = currentSystem;
        }

        public bool VerificarUsuarioLogado(out int userId, out int empresaId)
        {
            
            try
            {
                userId = 0;
                empresaId = 0;
                Cripto cripto = new Cripto();

                string nomeArquivo = "";
                if (_currentSystem == ModuloSistema.Gestao)
                {
                    nomeArquivo = cripto.Encrypt("vestillo-producao-login");
                }
                else
                {
                    nomeArquivo = cripto.Encrypt("vestillo-gestao-login");
                }

                string filePath = _pathSecurityFile + @"\" + nomeArquivo + ".vest";

                if (!File.Exists(filePath))
                {
                    return false;
                }

                string securityText = cripto.Decrypt(System.IO.File.ReadAllText(filePath).Trim());

                string[] securityTextSpplited = securityText.Split('|');

                if (securityTextSpplited.Length != 4)
                    return false;

                Process localByName = null;
                
                try
                {
                    localByName = Process.GetProcessById(int.Parse(securityTextSpplited[2]));
                }
                catch (Exception)
                {
                    File.Delete(filePath);
                    return false;
                }
                

                if (localByName == null)
                    return false;

                empresaId = int.Parse(securityTextSpplited[0]);
                userId = int.Parse(securityTextSpplited[1]);

                return true;
            }
            finally
            {

            }

            return false;
         
        }

        public void GerarArquivoLogin(int usuarioId, int empresaId)
        {
            try
            {
                string securityText = "";

                Cripto cripto = new Cripto();
                Process localByName = Process.GetCurrentProcess();
                securityText = cripto.Encrypt(string.Concat(empresaId.ToString(), "|", usuarioId.ToString(), "|", localByName.Id, "|", localByName.SessionId));
                string nomeArquivo = "";

                if (_currentSystem == ModuloSistema.Gestao)
                {
                    nomeArquivo = cripto.Encrypt("vestillo-gestao-login");
                }
                else
                {
                    nomeArquivo = cripto.Encrypt("vestillo-producao-login");
                }

                string filePath = _pathSecurityFile + @"\" + nomeArquivo + ".vest";

                if (!Directory.Exists(_pathSecurityFile))
                {
                    Directory.CreateDirectory(_pathSecurityFile);
                }

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }


                System.IO.File.WriteAllText(filePath, securityText);
            }
            finally
            {

            }
        }
    }
}