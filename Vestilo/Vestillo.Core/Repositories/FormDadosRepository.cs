using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class FormDadosRepository : GenericRepository<FormDados>
    {
        public FormDados ListByFormEUsuario(string form, int usuarioId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  *");
            sql.AppendLine("FROM    FormDados");
            sql.AppendLine("WHERE   LOWER(Form) = '" + form.ToLower().Trim() + "' AND UsuarioId = " + usuarioId.ToString());
            sql.AppendLine("        AND " + FiltroEmpresa());

            return VestilloConnection.ExecSQLToModel<FormDados>(sql.ToString());
        }

        public void DeletaRegistro(int Usuario, string NomeTela)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("DELETE FROM  formdados WHERE  ");
            SQL.AppendLine("UsuarioId = ");
            SQL.Append(Usuario);
            SQL.AppendLine(" AND Form =  ");           
            SQL.Append("'" + NomeTela + "'");

            VestilloConnection.ExecNonQuery(SQL.ToString());
        }
    }
}
