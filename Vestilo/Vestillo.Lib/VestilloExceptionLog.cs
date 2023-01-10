using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.IO;

namespace Vestillo.Lib
{  
    public class VestilloExceptionLog
    {
        private string _CaminhoLog = AppDomain.CurrentDomain.BaseDirectory;     
        private VestilloException _Exception;
        public VestilloExceptionLog(VestilloException ex)
        {
            _Exception = ex;
        }
        public void GravarLog()
        {            
            string DataHora = DateTime.Now.ToString("dd-MM-yyyy-HH-mmm-ss");
            string Arquivo = _CaminhoLog + "Log_" + DataHora + "_" + new Random().Next(10000) + ".txt";

            StreamWriter SW = new StreamWriter(Arquivo);
            SW.WriteLine(_Exception.ToString());            
            SW.Close();
        }
    }
}
