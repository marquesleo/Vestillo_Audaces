using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ItemCombo
    {
        public ItemCombo()
        {
        }

        public ItemCombo(string id) : this(id, "")
        {

        }

        public ItemCombo(string id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }


        public string Id { get; set; }
        public string Descricao { get; set; }
    }
}
