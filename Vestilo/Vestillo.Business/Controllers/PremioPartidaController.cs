using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class PremioPartidaController : GenericController<PremioPartida, PremioPartidaRepository>
    {
        public PremioPartida GetByIdView(int id)
        {
            using (var repository = new PremioPartidaRepository())
            {
                return repository.GetByIdView(id);
            }
        }

        public IEnumerable<PremioPartida> GetByDescricao(string descricao)
        {
            using (var repository = new PremioPartidaRepository())
            {
                return repository.GetByDescricao(descricao);
            }
        }

        public IEnumerable<PremioPartida> GetByReferencia(string referencia)
        {
            using (var repository = new PremioPartidaRepository())
            {
                return repository.GetByReferencia(referencia);
            }
        }

        public override void Save(ref PremioPartida premio)
        {
            var premioFuncionariosRepository = new PremioPartidaFuncionariosRepository();

            try
            {
                premioFuncionariosRepository.BeginTransaction();
                base.Save(ref premio);

                if (premio.Funcionarios != null && premio.Funcionarios.Count() > 0)
                {
                    IEnumerable<PremioPartidaFuncionarios> funcionarios = premioFuncionariosRepository.GetByPremioView(premio.Id);
                    if (funcionarios != null && funcionarios.Count() > 0)
                    {
                        foreach (var f in funcionarios)
                        {
                            premioFuncionariosRepository.Delete(f.Id);
                        }
                    }
                    
                    foreach (PremioPartidaFuncionariosView funcionario in premio.Funcionarios)
                    {
                        PremioPartidaFuncionarios pessoaSave;
                        pessoaSave = funcionario;
                        pessoaSave.Id = 0;
                        pessoaSave.IdPremio = premio.Id;
                        premioFuncionariosRepository.Save(ref pessoaSave);
                    }
                }

                premioFuncionariosRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                premioFuncionariosRepository.RollbackTransaction();
                throw ex;
            }
        }

        public override void Delete(int id)
        {
            var premioFuncionariosRepository = new PremioPartidaFuncionariosRepository();

            try
            {
                premioFuncionariosRepository.BeginTransaction();

                IEnumerable<PremioPartidaFuncionarios> funcionarios = premioFuncionariosRepository.GetByPremioView(id);
                if (funcionarios != null && funcionarios.Count() > 0)
                {
                    foreach (var p in funcionarios)
                    {
                        premioFuncionariosRepository.Delete(p.Id);
                    }
                }                

                base.Delete(id);

                premioFuncionariosRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                premioFuncionariosRepository.RollbackTransaction();
                throw ex;
            }
        }
    }
}