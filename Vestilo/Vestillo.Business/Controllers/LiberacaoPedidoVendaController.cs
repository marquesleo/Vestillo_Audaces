using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Controllers
{
    public class LiberacaoPedidoVendaController : GenericController<LiberacaoPedidoVenda, LiberacaoPedidoVendaRepository>
    {
        public List<LiberacaoPedidoVendaView> GetByPedidoIdView(int pedidoId)
        {
            var itemLiberacaoPedidoVendaRepository = new ItemLiberacaoPedidoVendaRepository();
            var liberacaoPedidoVendaRepository = new LiberacaoPedidoVendaRepository();
            var liberacaoPedidoVenda = liberacaoPedidoVendaRepository.GetLiberacaoPedidoVenda(pedidoId).ToList();
            foreach (LiberacaoPedidoVendaView lpv in liberacaoPedidoVenda)
            {
                lpv.ItensLiberacao = itemLiberacaoPedidoVendaRepository.GetItensLiberacaoViewByLiberacaoIdEPedido(lpv.LiberacaoId, pedidoId).ToList();
                if (lpv.ItensLiberacao.Exists(p => p.Status == (int)enumStatusLiberacaoPedidoVenda.Sem_Estoque)
                    && (lpv.ItensLiberacao.Exists(p => p.Status == (int)enumStatusLiberacaoPedidoVenda.Atendido) || lpv.ItensLiberacao.Exists(p => p.Status == (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial)))
                {
                    lpv.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial;
                }

                if (lpv.ItensLiberacao.Exists(p => p.Status == (int)enumStatusLiberacaoPedidoVenda.Atendido)
                   &&  lpv.ItensLiberacao.Exists(p => p.Status == (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial))
                {
                    lpv.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial;
                }

                if (lpv.ItensLiberacao.Exists(p => p.Status == (int)enumStatusLiberacaoPedidoVenda.Atendido)
                    && lpv.ItensLiberacao.Exists(p => p.Status == (int)enumStatusLiberacaoPedidoVenda.Producao))
                {
                    lpv.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido_Producao;
                }
            }
            
            return liberacaoPedidoVenda;
        }
    }
}
