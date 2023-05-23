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
    public class FormDadosService : GenericService<FormDados, FormDadosRepository>
    {
        public FormDados ListByFormEUsuario(string form, int usuarioId)
        {
            return _repository.ListByFormEUsuario(form, usuarioId);
        }

        public void DeletaRegistro(int Usuario, string NomeTela)
        {
            _repository.DeletaRegistro(Usuario, NomeTela);            
        }
    }
}


