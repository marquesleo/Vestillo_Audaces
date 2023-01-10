using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class CalendarioController: GenericController<Calendario, CalendarioRepository>
    {

        public override Calendario GetById(int id)
        {
            Calendario calendario = base.GetById(id);

            if (calendario != null)
            {
                using (CalendarioFaixasRepository calendarioFaixasRepository = new CalendarioFaixasRepository())
                {
                    calendario.Faixas = calendarioFaixasRepository.GetByCalendario(id);
                }
            }

            return calendario;
        }

        public override void Delete(int id)
        {
            using (CalendarioFaixasRepository calendarioFaixasRepository = new CalendarioFaixasRepository())
            {
                calendarioFaixasRepository.DeleteByCalendario(id);
            }

            base.Delete(id);
        }

        public override void Save(ref Calendario calendario)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                base.Save(ref calendario);

                using (CalendarioFaixasRepository calendarioFaixasRepository = new CalendarioFaixasRepository())
                {

                    calendarioFaixasRepository.DeleteByCalendario(calendario.Id);

                    foreach (CalendarioFaixas f in calendario.Faixas)
                    {
                        CalendarioFaixas faixa = f;
                        faixa.CalendarioId = calendario.Id;
                        calendarioFaixasRepository.Save(ref faixa);
                    }
                }

                using (FuncionarioController funcionarioController = new FuncionarioController())
                {
                    funcionarioController.AtualizarMinutoDia(calendario.Id);
                }

                if (openTransaction)
                    _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();

                throw ex;
            }
        }
    }
}
