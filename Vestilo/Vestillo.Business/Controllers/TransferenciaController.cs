
using Vestillo.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;

namespace Vestillo.Business.Controllers
{
    public class TransferenciaController : GenericController<Transferencia, TransferenciaRepository>
    {


        public IEnumerable<TransferenciaView> GetAllView()
        {
            using (TransferenciaRepository repository = new TransferenciaRepository())
            {
                return repository.GetAllView();
            }
        }


        public override void Save(ref Transferencia Trans)
        {
            bool openTransaction = false;

            using (TransferenciaRepository repository = new TransferenciaRepository())
            {
                try
                {
                    openTransaction = repository.BeginTransaction();

                    var estoqueController = new EstoqueController();
                    var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();

                    //PEGA A REFERENCIA
                    base.Save(ref Trans);

                    //grava os itens da nota
                    using (TransferenciaItensRepository itensRepository = new TransferenciaItensRepository())
                    {
                        
                        foreach (var gr in Trans.ItensTransferencia)
                        {
                            TransferenciaItens g = gr;
                            g.Id = 0;
                            g.Idtransferencia = Trans.Id;
                            itensRepository.Save(ref g);
                        }

                        //Movimenta itens no estoque
                        if (Trans.ItensMovimentacaoEstoque != null)
                        {

                            foreach (var est in Trans.ItensMovimentacaoEstoque)
                            {
                                var mov = new MovimentacaoEstoque() { Saida = 0, Entrada = 0 };

                                mov.ProdutoId = est.ProdutoId;
                                mov.AlmoxarifadoId = est.AlmoxarifadoId;
                                mov.DataMovimento = est.DataMovimento;
                                mov.UsuarioId = est.UsuarioId;
                                mov.TamanhoId = est.TamanhoId;
                                mov.CorId = est.CorId;
                                if (est.Entrada > 0)
                                {
                                    mov.Entrada = est.Entrada;
                                }
                                else
                                {
                                    mov.Saida = est.Saida;
                                }
                                mov.Observacao = "Movimento realizado pela Transferência: " + Trans.Referencia;
                                listMovimentacaoEstoque.Add(mov);
                            }
                            estoqueController.MovimentarEstoque(listMovimentacaoEstoque, false,false);
                        }
                    }


                    //dar update nas notas
                    using (FatNfeRepository NfeTRansf = new FatNfeRepository())
                    {
                        if (!String.IsNullOrEmpty(Trans.IdsDasNotas))
                        {
                            int IdNfe = 0;
                            Char delimiter = ',';
                            String[] substrings = Trans.IdsDasNotas.Split(delimiter);
                            foreach (var substring in substrings)
                            {
                                IdNfe = Convert.ToInt32(substring);
                                NfeTRansf.UpdateTransferida(IdNfe);
                            }
                        }
                    }

                    if (openTransaction)
                        repository.CommitTransaction();
                }
                catch (Exception ex)
                {
                    if (openTransaction)
                        repository.RollbackTransaction();

                    throw ex;
                }
            }
        }

    }
}
