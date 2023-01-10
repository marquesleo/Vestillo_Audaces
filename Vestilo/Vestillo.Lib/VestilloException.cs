using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace Vestillo.Lib
{    
    /// <summary>
    /// Enumeração para o tipo de Exception
    /// </summary>
    public enum Enum_Tipo_VestilloNet_Exception
    {
        /// <summary>
        /// Ocorrências de Banco de Dados - Query
        /// </summary>        
        BancoDeDados_Query,
        /// <summary>
        /// Ocorrências de Banco de Dados - Servidor
        /// </summary>        
        BancoDeDados_Servidor,
        /// <summary>
        /// Ocorrências de Permissão em objetos do Banco de Dados
        /// </summary>        
        BancoDeDados_Permissao,
        /// <summary>
        /// Ocorrências de Problemas ao acessar recursos no disco
        /// </summary>        
        AcessoAoDisco,
        /// <summary>
        /// Ocorrências de Formulários
        /// </summary>        
        Formulario,
        /// <summary>
        /// Ocorrências de Assembly
        /// </summary>        
        Assembly,
        /// <summary>
        /// Ocorrências de Acesso a Registro no Banco de Dados
        /// </summary>        
        Registro_Nao_Encontrado,
        /// <summary>
        /// Ocorrência de Problemas em Autenticação
        /// </summary>
        Autenticacao,
        /// <summary>
        /// Ocorrência de Permissão em Sistema (Tabela Permissao_T)
        /// </summary>
        Permissao_Acesso_Sistema,
        /// <summary>
        /// Ocorrências Não Mapeadas
        /// </summary>        
        Outros,
        /// <summary>
        /// Erro ao tentar incluir um registro com propriedade [RegistroUnico] encontrado no banco
        /// </summary>        
        Registro_Duplicado
    }
        
    public class VestilloException: Exception
    {
        public bool TentarNovamente = false;
        public bool CancelarTransacao = true;
        public Enum_Tipo_VestilloNet_Exception TipoException = Enum_Tipo_VestilloNet_Exception.Outros;
        private Exception _ExceptionOriginal;
        StringBuilder _ErrosSQL = new StringBuilder();
        public string Mensagem = string.Empty;
        public string MensagemChamadaStoredProcedure = string.Empty;
        public bool EntrarEmContatoSuporte = false;
        public string RegistroDuplicado = string.Empty;

        public VestilloException(Enum_Tipo_VestilloNet_Exception pTipo)
        {            
            this.TipoException = pTipo;
        }

        public VestilloException(Enum_Tipo_VestilloNet_Exception pTipo, string pMensagem):base(pMensagem)
        {
            this.TipoException = pTipo;
            this.Mensagem = pMensagem;
        }

        public VestilloException(SqlException pEx)
        {
            _ExceptionOriginal = pEx;
            TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Query;

            TratarErroSQLServer(pEx);
        }

        public VestilloException(SqlException pEx, SqlCommand pCMD)
        {
            _ExceptionOriginal = pEx;
            TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Query;

            StringBuilder SB = new StringBuilder();            
            SB.AppendLine("Stored Procedure: " + pCMD.CommandText);
            SB.AppendLine("Time Out: " + pCMD.CommandTimeout.ToString());
            if (pCMD.Parameters.Count > 0)
            {
                SB.AppendLine("  Parâmetros: ");
                
                foreach (SqlParameter P in pCMD.Parameters)
                {
                    string Str = "    " + P.ParameterName + "  =  ";

                    if (P.Value == null)
                        Str += "NULL";
                    else
                        Str += P.Value.ToString();

                    SB.AppendLine(Str);
                }                
            }

            MensagemChamadaStoredProcedure = SB.ToString();

            TratarErroSQLServer(pEx);
        }

        private void TratarErroSQLServer(SqlException pEx)
        {
            foreach (SqlError Erro in pEx.Errors)
            {
                _ErrosSQL.AppendLine("**************************************************************");
                string MsgErro = "Class: {0}\nLineNumber: {1}\nMessage: {2}\nNumber: {3}\nProcedure: {4}\nServer: {5}\nSource: {6}\nState: {7}";
                _ErrosSQL.AppendLine(string.Format(MsgErro, Erro.Class, Erro.LineNumber, Erro.Message, Erro.Number, Erro.Procedure, Erro.Server, Erro.Source, Erro.State));                
            }

            switch (pEx.Number)
            {
                case -2:
                    Mensagem = "Tempo Excedido, por favor tente novamente";
                    TentarNovamente = true;
                    CancelarTransacao = false;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 201:
                    Mensagem = "Parâmetro não fornecido para o Procedimento Armazenado";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 8145:
                    Mensagem = "Parâmetro inválido para o Procedimento Armazenado";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 2812:
                    Mensagem = "Procedimento Armazenado não encontrado";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 208:
                    Mensagem = "Objeto não encontrado no Banco de Dados";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 207:
                    Mensagem = "Campo não encontrado no Banco de Dados";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 229:
                    Mensagem = "Permissão negada para acesso ao objeto no banco de dados";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Permissao;
                    break;
                case 8144:
                    Mensagem = "O Procedimento Armazenado recebeu mais parâmetros do que o esperado";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 10054:
                    Mensagem = "A conexão com o banco de dados foi perdida";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 137:
                    Mensagem = "Um parâmetro solicitado não foi declarado";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 547:
                    Mensagem = "Vínculo com outra tabela não validado";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Query;
                    break;
                case 121:
                    Mensagem = "A conexão com o banco de dados foi perdida";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 53:
                    Mensagem = "A conexão com o banco de dados foi perdida";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 515:
                    Mensagem = "Um campo obrigatório não foi informado no banco de dados";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Servidor;
                    break;
                case 18456:
                    Mensagem = "Usuário ou Senha Não Encontrados";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.Autenticacao;
                    break;
                case 8152:
                    Mensagem = "O valor passado para o parâmetro é maior do que o esperado";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Query;
                    break;
                case 102:
                    Mensagem = "Erro interno de sintaxe na consulta";
                    EntrarEmContatoSuporte = true;
                    TentarNovamente = false;
                    CancelarTransacao = true;
                    TipoException = Enum_Tipo_VestilloNet_Exception.BancoDeDados_Query;
                    break;
                default:
                    Mensagem = "Ocorrência não mapeada";
                    break;
            }

            VestilloExceptionLog Log = new VestilloExceptionLog(this);
            Log.GravarLog();
        }

        public VestilloException(Exception pEx)
        {
            TipoException = Enum_Tipo_VestilloNet_Exception.Outros;
            Mensagem = pEx.Message;
            TentarNovamente = false;
            CancelarTransacao = true;
            EntrarEmContatoSuporte = true;

            //Provisorio - Melhorr depois (Apenas para testes)

            if (pEx.Source == "MySql.Data" && (this.Mensagem == "Fatal error encountered during command execution." ||
                                               this.Mensagem == "Fatal error encountered attempting to read the resultset." ||
                                               this.Mensagem == "Connection must be valid and open."))
            {
                TentarNovamente = true;
            }

            // Melhorar depois
            if (pEx.Message.Equals("Invalid operation. The connection is closed."))
                Mensagem = "A conexão com o banco de dados está fechada.";

            VestilloExceptionLog Log = new VestilloExceptionLog(this);
            Log.GravarLog();           
        }

        //public override string ToString()
        //{            
            //StringBuilder SB = new StringBuilder();
            //SB.AppendLine("**************************************************************");
            //SB.AppendLine("Nome da Estação: " + VestilloNetAmbiente.RetornarNomeComputador());
            //SB.AppendLine("Usuário Logado na Estação: " + VestilloNetAmbiente.RetornarNomeUsuarioLogadoWindows());
            //SB.AppendLine("Usuário Logado MAS ERP: " + VestilloNetAmbiente.CodigoUsuarioLogado.ToString());
            //SB.AppendLine("IP: " + VestilloNetAmbiente.RetornarIP());            
            //SB.AppendLine("Tentar Novamente: " + TentarNovamente.ToString());
            //SB.AppendLine("Cancelar Transação: " + CancelarTransacao.ToString());
            //SB.AppendLine("Tipo de Exception: " + TipoException.ToString());
            //SB.AppendLine("Entrar em Contato com o Suporte " + EntrarEmContatoSuporte.ToString());
            //SB.AppendLine("**************************************************************");
            //SB.AppendLine("Mensagem: " + Mensagem);
            //SB.AppendLine("**************************************************************");
            //SB.AppendLine("Erros SQL:").AppendLine(_ErrosSQL.ToString());
            //SB.AppendLine("**************************************************************");
            //if (MensagemChamadaStoredProcedure != string.Empty)
            //{
            //    SB.AppendLine(MensagemChamadaStoredProcedure);
            //    SB.AppendLine("**************************************************************");
            //}

            //if (_ExceptionOriginal != null && _ExceptionOriginal.InnerException != null)
            //{
            //    SB.AppendLine("**************************************************************");
            //    SB.AppendLine("Inner Exception Original").AppendLine(_ExceptionOriginal.InnerException.ToString());
            //}

            //return SB.ToString();
        //}
    }
}
