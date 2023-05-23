using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class VendedorMetaService : GenericService<VendedorMeta, VendedorMetaRepository>
    {

        public void Save(IEnumerable<VendedorMetaView> metas, int vendedorId)
        {
            bool openTransaction = false;
            
            try
            {
                openTransaction = _repository.BeginTransaction();

                _repository.DeleteByVendedor(vendedorId);

                foreach (VendedorMetaView m in metas)
                {
                    if (m.Meta > 0 || m.MetaCliente > 0)
                    {
                        m.VendedorId = vendedorId;
                        
                        VendedorMeta meta = m as VendedorMeta;
                        meta.Id = 0;

                        _repository.Save(meta);
                    }
                }


                if(openTransaction)
                    _repository.CommitTransaction();

            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();
                
                throw ex;
            }
        }

        public IEnumerable<VendedorMetaView> ListByVendedor(int vendedorId)
        {

            IEnumerable<VendedorMetaView> mesesBD = _repository.ListByVendedor(vendedorId);

            List<VendedorMetaView> meses = MontaListaMeses().ToList();

            foreach (VendedorMetaView mesBD in mesesBD)
            {
                meses[((int)(mesBD.Mes)) - 1] = mesBD;
            }

            return meses;
        }

        public IEnumerable<VendedorMetaView> MontaListaMeses()
        {
            List<VendedorMetaView> meses = new List<VendedorMetaView>();
            meses.Add(new VendedorMetaView { Mes = Meses.Janeiro});
            meses.Add(new VendedorMetaView { Mes = Meses.Fevereiro });
            meses.Add(new VendedorMetaView { Mes = Meses.Março });
            meses.Add(new VendedorMetaView { Mes = Meses.Abril });
            meses.Add(new VendedorMetaView { Mes = Meses.Maio });
            meses.Add(new VendedorMetaView { Mes = Meses.Junho });
            meses.Add(new VendedorMetaView { Mes = Meses.Julho });
            meses.Add(new VendedorMetaView { Mes = Meses.Agosto });
            meses.Add(new VendedorMetaView { Mes = Meses.Setembro });
            meses.Add(new VendedorMetaView { Mes = Meses.Outubro });
            meses.Add(new VendedorMetaView { Mes = Meses.Novembro });
            meses.Add(new VendedorMetaView { Mes = Meses.Dezembro });
            return meses;
        }
    }
}

