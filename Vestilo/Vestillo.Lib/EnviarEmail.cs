using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Vestillo.Lib
{
    public class EnviarEmail
    {
        public static bool OcorreuEnvio;


        public object email(string Smtp, int porta, bool habillitassl, bool habilitacredenciais, string enderecoRemetente, string SenhaEmail, List<string> destinatario, string assunto, string mensagem, List<string> Anexos)
        {
            OcorreuEnvio = false;
            SmtpClient smtp = new SmtpClient();

            if (Anexos.Count > 0)
            {
                foreach (var file in Anexos)
                {
                    if (!File.Exists(file))
                    {
                        OcorreuEnvio = false;
                        return OcorreuEnvio;
                    }
                }
            }

            foreach (var endereco in destinatario)
            {
                if (!ValidaEmail(endereco))
                {
                    OcorreuEnvio = false;
                    return OcorreuEnvio;
                }              
            }
           
            smtp.Host = Smtp; //smtp.live.com;
            smtp.Port = porta; //25            
            smtp.EnableSsl = habillitassl; // define se habilita ssl
            smtp.UseDefaultCredentials = habilitacredenciais; //As credenciais são necessárias se o servidor exigir que o cliente autentique antes de enviar o e-mail.    
            smtp.Credentials = new System.Net.NetworkCredential(enderecoRemetente, SenhaEmail);
            

             /*        

             MailAddress from = new MailAddress(enderecoRemetente,enderecoRemetente);
             MailMessage mail;

            foreach (var address in destinatario)
            {
                MailAddress to = new MailAddress(address, address);
                

                mail = new MailMessage(from, to)
                { 
                    
                    Subject = assunto,
                    Body = mensagem,
                    IsBodyHtml = true
                };

                foreach (var file in Anexos)
                {
                    Attachment attachment = new Attachment(file);
                    mail.Attachments.Add(attachment);
                }
                //cyber_souza@hotmail.com - smtp-mail.outlook.com - motmzlhpxkdaqpcl
                //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true; 
                smtp.Send(mail);
            }

            */
          

            MailMessage mailMessage = new MailMessage();          

            mailMessage.From = new System.Net.Mail.MailAddress(enderecoRemetente);

            for (int i = 0; i < destinatario.Count; i++)
            {
                if (i == 0)
                {                   
                    mailMessage.To.Add(new System.Net.Mail.MailAddress(destinatario[i]));
                }
                else
                {
                    mailMessage.CC.Add(new System.Net.Mail.MailAddress(destinatario[i]));                   
                }
                
            }


            //mailMessage.To.Add(new System.Net.Mail.MailAddress("shirleyherdy@hotmail.com"));
            //mailMessage.CC.Add(new System.Net.Mail.MailAddress("vendas.aline@dechelles.com.br"));
            //mailMessage.CC.Add(new System.Net.Mail.MailAddress("alexsandro.souza@vestillo.com.br"));

            mailMessage.Subject = assunto;
            mailMessage.Body = mensagem;
            mailMessage.IsBodyHtml = false;

            foreach (var file in Anexos)
            {
                Attachment attachment = new Attachment(file);
                mailMessage.Attachments.Add(attachment);
            }

            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
            smtp.Send(mailMessage);

           
           
            OcorreuEnvio = true;
            return OcorreuEnvio;
            //certificado é tls

        }


        public bool ValidaEmail(string email)
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

        
      
    }
}
