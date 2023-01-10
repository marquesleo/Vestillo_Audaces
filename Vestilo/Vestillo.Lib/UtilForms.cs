using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    /// <summary>
    /// Delegate para informar ao programa principal que um formulário deve ser carregado na Tab principal
    /// </summary>
    /// <param name="codigoFormulario">Código interno do formulário</param>
    /// <param name="codigoRegistro">Código do registro a ser carregado</param>
    public delegate void AbrirFormularioTabProgramaPrincipal(int codigoFormulario, string codigoRegistro);
    /// <summary>
    /// Delegate para informar ao programa principal que uma consulta custimizada deve ser carregada na Tag Principal
    /// </summary>
    /// <param name="codigoConsulta"></param>
    public delegate void AbrirConsultaCustomizadaTabProgramaPrincipal(int codigoConsulta);
        
    /// <summary>
    /// Dekegate para o evento de quando os botoes do form browser forem pressionados
    /// </summary>
    /// <param name="id">ID do botão</param>
    /// <param name="chave">Chave, vinda do grid</param>
    public delegate void CliqueBotao(string id, int chave);

    /// <summary>
    /// Delegate para evento ser disparado quando o formulário é fechado
    /// </summary>
    /// <param name="codigoInterno">Código interno do formulário, chave</param>
    public delegate void FormularioFechado(string codigoInterno);

    /// <summary>
    /// Delegate para o evento ser disparado quando iniciar o carregamento do grid 
    /// </summary>
    /// <param name="id">Código interno do formulário, chave</param>
    public delegate void IniciouCarregamentoGrid(string id);

    /// <summary>
    /// Delegate para o evento ser disparado quando iniciar o carregamento do grid de filtro
    /// </summary>    
    public delegate void IniciouCarregamentoGridFiltro(string campo, string valorfiltro);

    /// <summary>
    /// Delegate para o evento ser disparado quando um registro for selecionado no form_filtro
    /// </summary>
    /// <param name="campo">Campo que foi pesquisado</param>
    /// <param name="valorfiltro">Valor utilizado para a busca</param>
    /// <param name="id">Id do registro selecionado</param>    
    public delegate void SelecionouRegistroGridFiltro(string campo, string valorfiltro, int id);

    /// <summary>
    /// Delegate para eventos simples
    /// </summary>
    public delegate void EventoSimples();

    /// <summary>
    /// Delegate para o evento ser disparado quando o carregamento do grid ser finalizado
    /// </summary>
    /// <param name="id">Código interno do formulário, chave</param>
    public delegate void TerminouCarregamentoGrid(string id);
    
    /// <summary>
    /// Delegate para o evento ser disparado quando a gravação de um registro ser finalizada com sucesso
    /// </summary>
    /// <param name="codigoRegistro">Código do registro incluído, alterado ou excluído</param>
    /// <param name="acao">Ação que estava no formulário</param>
    public delegate void GravouRegistro(string codigoRegistro, AcaoForm acao);

    /// <summary>
    /// Enumeração para no tratamento de exceção o sistema saber se está Carregando ou Gravando
    /// </summary>
    public enum EventoAsync
    {
        /// <summary>
        /// Operação de Carregar
        /// </summary>
        Carregar,
        /// <summary>
        /// Operação de Gravar
        /// </summary>
        Gravar
    }

    /// <summary>
    /// Enumeração para ações de um formulário de registro
    /// </summary>
    public enum AcaoForm
    {
     
        Visualizar,

        /// <summary>
        /// Formulário em modo de Inclusão
        /// </summary>
        Incluir,

        /// <summary>
        /// Formulário em modo de Alteração
        /// </summary>
        Alterar,

        /// <summary>
        /// Formulário em modo de Exclusão
        /// </summary>
        Excluir,

        /// <summary>
        /// Formulário em modo de Cancelamento
        /// </summary>
        Cancelar,

        /// <summary>
        /// Formulário em modo de Baixa
        /// </summary>
        Baixar,

        Prorrogar,
        /// <summary>
        /// Formulário em modo de Cópia
        /// </summary>
        Copiar,

        /// <summary>
        /// Formulário em modo Impressão
        /// </summary>
        Filtrar,

        Devolver,

        Compensar,

        Resgatar,

        /// <summary>
        /// Estornar
        /// </summary>
        Estornar,

        Transferir,
            
        Manutencao,

        /// <summary>
        /// Indefinido
        /// </summary>
        Indefinido

    }
}
