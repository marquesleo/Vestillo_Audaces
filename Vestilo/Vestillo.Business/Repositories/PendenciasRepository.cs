using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Business.Service;
using System.Reflection;
using System.Data;

namespace Vestillo.Business.Repositories
{
    public class PendenciasRepository: GenericRepository<Pendencias>
    {

        
        public PendenciasRepository() : base(new DapperConnection<Pendencias>())
        {

        }


        public static void VerificarPendencia(object entity, TipoOperacaoLog op)
        {
            string[] tabelasCriacaoPendencia = { "FatNfe", "NfeEmitida" };
            
            string entityName = entity.GetType().Name;

            if (tabelasCriacaoPendencia.Contains(entityName))
            {
                CriarPendencia(entity, op, entityName);
            }
        }

        private static void CriarPendencia(object entity, TipoOperacaoLog operacao, string rotina)
        {
            int codigo = 0;
            int status = 0;
            bool bcodigo = false;
            bool bstatus = false;

            try
            {

                PropertyInfo[] properties = entity.GetType().GetProperties();
                foreach (var propertyInfo in properties)
                {

                    if (propertyInfo.Name.Equals("StatusNota"))
                    {
                        status = Convert.ToInt32(propertyInfo.GetValue(entity));
                        bstatus = true;
                    }

                    //if (rotina == "NfeEmitida") // os clientes agora vão usar somente o novo pra emitir notas
                   // {
                        if (propertyInfo.Name.Equals("Id"))
                        {
                            codigo = Convert.ToInt32(propertyInfo.GetValue(entity));
                            bcodigo = true;
                        }
                    //}
                    //else
                   // {
                    //    if (propertyInfo.Name.Equals("IdAntigo"))
                     //   {
                     //       codigo = Convert.ToInt32(propertyInfo.GetValue(entity));
                     //       bcodigo = true;
                      //  }

                    //}


                    if (bstatus == true && bcodigo == true)
                    {
                        break;
                    }

                }

                //if (rotina == "NfeEmitida")
                //{
                    var dados = new FatNfeService().GetServiceFactory().GetById(codigo);
                    if (dados != null)
                    {
                        if (status == 2)
                        {//grava a pendencia da nota cancelada e não excluida
                            int AttEstoque = 0;
                            var pnd = new PendenciasRepository();
                            var JaFoiIncluida = pnd.VerificaSePendenciaNfeExiste(codigo);

                            if (JaFoiIncluida == false)
                            {
                                using (FatNfeItensRepository gradeRepository = new FatNfeItensRepository())
                                {
                                    var litens = gradeRepository.GetListByNfeItens(dados.Id);
                                    
                                    List<FatNfeItens> pd = new List<FatNfeItens>();
                                    pd = litens.ToList();
                                    var tpmov = new TipoMovimentacaoService().GetServiceFactory().GetById(pd[0].IdTipoMov);
                                    if (tpmov != null)
                                    {
                                        AttEstoque = tpmov.AtualizaEstoque;
                                    }

                                }

                                if (AttEstoque == 1)
                                {
                                    if (dados.Tipo == 0 || dados.Tipo == 2 || dados.Tipo == 5 || dados.Tipo == 7) // cancelou uma nota de saida, itens devem voltar ao estoque
                                    {
                                        var pendenciaENTRADA = new Pendencias();
                                        pendenciaENTRADA.Id = 0;
                                        pendenciaENTRADA.Evento = "ENTRADA_ESTOQUE";
                                        pendenciaENTRADA.idItem = dados.Id;
                                        pendenciaENTRADA.Tabela = "nfe";
                                        pendenciaENTRADA.Status = 0;
                                        var pendenciaRepoENTRADA = new PendenciasRepository();
                                        pendenciaRepoENTRADA.Save(ref pendenciaENTRADA);
                                    }
                                    else // cancelou uma nota de entrada, itens devem sair do estoque
                                    {
                                        var pendenciaSAIDA = new Pendencias();
                                        pendenciaSAIDA.Id = 0;
                                        pendenciaSAIDA.Evento = "SAIDA_ESTOQUE";
                                        pendenciaSAIDA.idItem = dados.Id;
                                        pendenciaSAIDA.Tabela = "nfe";
                                        pendenciaSAIDA.Status = 0;
                                        var pendenciaRepoSAIDA = new PendenciasRepository();
                                        pendenciaRepoSAIDA.Save(ref pendenciaSAIDA);

                                    }
                                }

                            }

                            /*
                            var pendencia = new Pendencias();
                            pendencia.Id = 0;
                            pendencia.Evento = "DELETE";
                            pendencia.idItem = codigo;
                            pendencia.Tabela = "nfe";
                            pendencia.Status = 0;
                            var pendenciaRepo = new PendenciasRepository();
                            pendenciaRepo.Save(ref pendencia);
                             * */
                        }
                            
                        

                        //}

                    }
                //

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public  bool VerificaSePendenciaNfeExiste(int codigo)
        {
            bool JaIncluiu = false;
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * from  pendencias WHERE  (Evento = 'ENTRADA_ESTOQUE' OR Evento = 'SAIDA_ESTOQUE') AND idItem = " + codigo);
            DataTable dt = _cn.ExecuteToDataTable(SQL.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                JaIncluiu = true;
            }
            else
            {
                JaIncluiu = false;

            }
            return JaIncluiu;
            
        }


        public IEnumerable<Pendencias> VerificaPendeciasAtivas()
        {
            string SQL = String.Empty;
            SQL = "SELECT * FROM pendencias WHERE Status = 0";
            return _cn.ExecuteStringSqlToList(new Pendencias(), SQL);
        }

        public void AtualizaTarefa(int IdTarefa)
        {
            string SQL = String.Empty;
            SQL = "UPDATE pendencias set status = 1 WHERE id = " + IdTarefa;
            _cn.ExecuteNonQuery(SQL);
        }
            



    }
}
