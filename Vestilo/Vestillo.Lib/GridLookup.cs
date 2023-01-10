using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Repository;
namespace Vestillo.Lib
{

    public class RepositoryItemLookUpEdit
    {

       public static void FormatarGrid(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit grd,
            string coluna, string nomeAmigavel, int indice, bool visivel)
        {
            grd.PopulateColumns();
            if (grd.DataSource != null)
            {

                grd.Columns[coluna].Caption = nomeAmigavel;
                grd.Columns[coluna].Visible = visivel;
             
            }
        }

        public static void OcultarColunas(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit grd)
        {
           
            for (int i = 0; i < grd.Columns.Count-1; i++)
			{
			   grd.Columns[i].Visible = false;
			}
        }
    }

    public class GridLookup
    {

        public static void OcultarColunas(RepositoryItemGridLookUpEdit grd)
        {
            grd.PopulateViewColumns();
            for (int i = 0; i < grd.View.Columns.Count-1; i++)
			{
			   grd.View.Columns[i].Visible = false;
			}
        }

           public static void FormatarGrid(RepositoryItemGridLookUpEdit grd,
               string coluna, string nomeAmigavel, int indice, bool visivel)
           
           {
               grd.PopulateViewColumns();
               if (grd.DataSource != null) {
                       
                     grd.View.Columns[coluna].Caption = nomeAmigavel;
                     grd.View.Columns[coluna].Visible = visivel;
                     grd.View.Columns[coluna].VisibleIndex = indice;
                     grd.View.Columns[coluna].OptionsColumn.AllowSize = true;
                  }
           }
       
    }
}
