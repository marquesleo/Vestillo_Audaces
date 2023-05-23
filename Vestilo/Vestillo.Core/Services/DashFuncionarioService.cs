using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;
using System.Data;

namespace Vestillo.Core.Services
{
    public class DashFuncionarioService : GenericService<MetaXProduzidoFuncionario, DashFuncionarioRepository>
    {
        public MetaXProduzidoFuncionario GetMetaXProduzidoEmpresa(List<int> idsOperadoras, string mes, string ano, int empresaLogada,
                                                                        PercentuaisGeraisFuncionario percentuais)
        {
            return _repository.GetMetaXProduzidoEmpresa(idsOperadoras, mes, ano, empresaLogada, percentuais);
        }

        public MetaXProduzidoFuncionario GetMetaXProduzidoFuncionario(List<int> idsOperadoras, string mes, string ano, int empresaLogada,
                                                                        PercentuaisGeraisFuncionario percentuais)
        {
            return _repository.GetMetaXProduzidoFuncionario(idsOperadoras, mes, ano, empresaLogada, percentuais);
        }

        public PercentuaisGeraisFuncionario GetPercentuaisGeraisFuncionario(List<int> idsOperadoras, string mes, string ano)
        {
            return _repository.GetPercentuaisGeraisFuncionario(idsOperadoras, mes, ano);
        }

        public Decimal GetProdutividadeEmpresa(int qtdFuncionario, string mes, string ano, decimal minProduzidos)
        {
            return _repository.GetProdutividadeEmpresa(qtdFuncionario, mes, ano, minProduzidos);
        }

        public IndicadoresFuncionario GetIndicadoresQuantitativos(List<int> idsFuncionario, string mes, string ano)
        {
            return _repository.GetIndicadoresQuantitativos(idsFuncionario, mes, ano);
        }

        public IEnumerable<TempoOcorrenciaFuncionario> GetTempoOcorrencia(List<int> idsFuncionario, string mes, string ano, bool agrupaDescricao)
        {
            return _repository.GetTempoOcorrencia(idsFuncionario, mes, ano, agrupaDescricao);
        }

    }
}
