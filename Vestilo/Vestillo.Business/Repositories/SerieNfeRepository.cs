

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class SerieNfeRepository : GenericRepository<SerieNfe>
    {
        public SerieNfeRepository()
            : base(new DapperConnection<SerieNfe>())
        {
        }

        public SerieNfe  GetByNumeracao(int SerieNota)
        {
                        
            var Serie = new SerieNfe();
            _cn.ExecuteToModel(" Serie = " + SerieNota , ref Serie);
            return Serie;

        }

        public SerieNfe NumNotaPorEmpresa(string serie,int empresa)
        {
            SerieNfe NumNota = new SerieNfe();
            string SQL = String.Empty;

            SQL = "SELECT * FROM serienfe WHERE serienfe.Serie = " + "'" + serie + "'" + " AND serienfe.IdEmpresa = " + empresa;

            var cn = new DapperConnection<SerieNfe>();
            var ser = new SerieNfe();
            var dados = cn.ExecuteStringSqlToList(ser, SQL);

            
            if(dados !=null && dados.Count() > 0)
            {
                foreach (var item in dados)
                {
                    NumNota.Id = item.Id;
                    NumNota.IdEmpresa = item.IdEmpresa;
                    NumNota.NumeracaoAtual = item.NumeracaoAtual;
                    NumNota.Serie = item.Serie;
                }
            }

            return NumNota;
        }

        public List<SerieNfe> SeriesPorEmpresa(int empresa)
        {
            List<SerieNfe> NumNota = new List<SerieNfe>();
            string SQL = String.Empty;

            SQL = "SELECT * FROM serienfe WHERE  serienfe.IdEmpresa = " + empresa;

            var cn = new DapperConnection<SerieNfe>();
            var ser = new SerieNfe();
            var dados = cn.ExecuteStringSqlToList(ser, SQL);


            if (dados != null)
            {
                
                foreach (var item in dados)
                {
                    var ItemNota = new SerieNfe();
                    ItemNota.Id = item.Id;
                    ItemNota.IdEmpresa = item.IdEmpresa;
                    ItemNota.NumeracaoAtual = item.NumeracaoAtual;
                    ItemNota.Serie = item.Serie;
                    NumNota.Add(ItemNota);
                }
            }

            return NumNota;
        }

    }
}
