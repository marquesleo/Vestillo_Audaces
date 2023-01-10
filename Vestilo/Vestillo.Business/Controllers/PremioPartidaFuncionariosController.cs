using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Vestillo.Business.Controllers
{
    public class PremioPartidaFuncionariosController : GenericController<PremioPartidaFuncionarios, PremioPartidaFuncionariosRepository>
    {
        public PremioPartidaFuncionarios GetByIdView(int id)
        {
            using (var repository = new PremioPartidaFuncionariosRepository())
            {
                return repository.GetByIdView(id);
            }
        }

        public IEnumerable<PremioPartidaFuncionariosView> GetByPremioView(int premioId)
        {
            using (var repository = new PremioPartidaFuncionariosRepository())
            {
                return repository.GetByPremioView(premioId);
            }
        }

        public IEnumerable<PremioPartidaFuncionariosView> GetFuncionariosGrid(int? premioId)
        {
            using (var repository = new PremioPartidaFuncionariosRepository())
            {
                return repository.GetFuncionariosGrid(premioId);
            }
        }

        public decimal CalculaPremioPartidaAssiduidade(DateTime DaData, DateTime AteData, int idFuncionario, PremioPartida premioPartida)
        {
            decimal premio = 0;

            List<OcorrenciaFuncionario> ocorrencia = new List<OcorrenciaFuncionario>();

            ocorrencia = new OcorrenciaFuncionarioRepository().GetFaltaByFuncionario(idFuncionario, DaData.Month, AteData.Month).ToList();                       
            
            var cal = CultureInfo.InvariantCulture.Calendar;
            var DaDataSemana = cal.GetWeekOfYear(DaData, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);
            var AteDataSemana = cal.GetWeekOfYear(AteData, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);

            decimal diferenca = AteDataSemana - DaDataSemana;
            if (diferenca < 0) Math.Abs(diferenca);
            decimal semanas = diferenca + 1;
                
            DateTime proxData = DaData;
            List<SemanaAssiduidade> semanaAssiduidade = new List<SemanaAssiduidade>();

            for (int i = 1; i <= semanas; i++)
            {           
                SemanaAssiduidade semana = new SemanaAssiduidade();
                semana.Semana = proxData;
                semana.pagar = true;

                DateTime comecoSemana = proxData;
                DateTime fimSemana;

                switch (proxData.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        comecoSemana = proxData;
                        break;
                    case DayOfWeek.Monday:
                        comecoSemana = proxData.AddDays(-1);
                        break;
                    case DayOfWeek.Tuesday:
                        comecoSemana = proxData.AddDays(-2);
                        break;
                    case DayOfWeek.Wednesday:
                        comecoSemana = proxData.AddDays(-3);
                        break;
                    case DayOfWeek.Thursday:
                        comecoSemana = proxData.AddDays(-4);
                        break;
                    case DayOfWeek.Friday:
                        comecoSemana = proxData.AddDays(-5);
                        break;
                    case DayOfWeek.Saturday:
                        comecoSemana = proxData.AddDays(-6);
                        break;

                }
                fimSemana = comecoSemana.AddDays(6);

                var ocorrenciaPeriodo = ocorrencia.Where(o => o.Data >= comecoSemana && o.Data <= fimSemana).FirstOrDefault();

                if (ocorrenciaPeriodo != null)
                {
                    semana.pagar = false;
                }

                semanaAssiduidade.Add(semana);
                proxData = DaData.AddDays(i * 7);
            }

            if (premioPartida.AssTipo == 1)
            {
                var descontado = semanaAssiduidade.Where(s => s.pagar == false).Count();//qtd de semanas não pagas

                premio = (premioPartida.AssValor * semanas) - (premioPartida.AssValor * descontado) ;
            }
            else
            {
                int qtdMeses = ((AteData.Year - DaData.Year) * 12) + AteData.Month - DaData.Month;
                qtdMeses++;

                if (qtdMeses > 0)
                    premio = premioPartida.AssValor * qtdMeses;
                else
                    premio = premioPartida.AssValor;

                decimal descontado = 0;

                var mes = DaData.Month;
                for (int i = 0; i <= qtdMeses; i++)
                {                        
                    if (semanaAssiduidade.Exists(s => s.Semana.Month == mes && s.pagar == false))
                    {
                        descontado += premioPartida.AssValor;
                    }
                    mes++;
                    if (mes > 12) mes = 1;
                }

                premio -= descontado;                    
                    
            }
                
            if (premio < 0) premio = 0;

            

            return premio;
        }

        public IEnumerable<GrupoPremioPartidaDiario> CalculaPremioGrupoDiario(DateTime DaData, DateTime AteData, List<int> premioId)
        {
            List<GrupoPremioPartidaDiario> grupoPremios = new List<GrupoPremioPartidaDiario>();
            FuncionarioController funcionarioController = new FuncionarioController();
            PremioPartidaController premioController = new PremioPartidaController();
            var funcionariosGrupo = funcionarioController.GetByPremioDataProducao(premioId, DaData, AteData, "f.id", true);

            foreach(int id in premioId) {

                var funcionariosPremio = funcionariosGrupo.Where(f => f.PremioPartida.Id == id);            

                var premio = premioController.GetById(id);

                DateTime date = new DateTime();
                decimal premioDia = 0;
                decimal tempoUtilFinal = 0;
                decimal ocorrenciaFinal = 0;
                decimal descontoFinal = 0;
                decimal punicaoFinal = 0;
                decimal acrescimoFinal = 0;
                decimal produtividadeFinal = 0;
                decimal jornadaFinal = 0;
                decimal eficienciaFinal = 0;
                

                for (date = DaData.Date; date.Date <= AteData.Date; date = date.AddDays(1))
                {
                    GrupoPremioPartidaDiario grupo = new GrupoPremioPartidaDiario();
                    grupo.premioId = id;
                    grupo.dia = date;

                    premioDia = 0;
                    foreach (var funcionario in funcionariosPremio)
                    {                        
                        tempoUtilFinal = 0;
                        ocorrenciaFinal = 0;
                        descontoFinal = 0;
                        punicaoFinal = 0;
                        acrescimoFinal = 0;
                        produtividadeFinal = 0;
                        jornadaFinal = 0;
                        eficienciaFinal = 0;

                        if (funcionario.Produtividades.Count > 0 && funcionario.Produtividades.Exists(p => p.Data.Date == date.Date))
                        {
                            if (funcionario.Produtividades.Count > 0)
                            {
                                var prod = funcionario.Produtividades.FindAll(p => p.Data.Date == date.Date);
                                if (prod != null && prod.Count > 0)
                                {
                                    tempoUtilFinal = prod.Sum(p => p.Jornada);
                                    jornadaFinal = prod.Sum(p => p.Jornada);
                                }
                            }
                            if (funcionario.Tempos.Count > 0)
                            {
                                var tempo = funcionario.Tempos.FindAll(p => p.Data.Date == date.Date);
                                if (tempo != null && tempo.Count > 0)
                                {
                                    produtividadeFinal += tempo.Sum(p => p.Tempo);
                                }
                            }

                            if (funcionario.Operacoes.Count > 0)
                            {
                                var operacao = funcionario.Operacoes.FindAll(p => p.Data.Date == date.Date);
                                if (operacao != null && operacao.Count > 0)
                                {
                                    produtividadeFinal += operacao.Sum(p => p.Tempo);
                                }
                            }

                            foreach (var ocorencia in funcionario.Ocorrencias)
                            {
                                switch (ocorencia.OcorrenciaTipo)
                                {
                                    case 0:
                                        if (ocorencia.Data.Date == date.Date)
                                        {
                                            tempoUtilFinal -= ocorencia.Tempo;
                                            ocorrenciaFinal += ocorencia.Tempo;
                                        }
                                        break;
                                    case 1:
                                        if (ocorencia.Data.Date == date.Date)
                                        {
                                            tempoUtilFinal -= ocorencia.Tempo;
                                            descontoFinal += ocorencia.Tempo;
                                        }
                                        break;
                                    case 2:
                                        if (ocorencia.Data.Date == date.Date)
                                        {
                                            produtividadeFinal -= ocorencia.Tempo;
                                            punicaoFinal += ocorencia.Tempo;
                                        }
                                        break;
                                    case 3:
                                        if (ocorencia.Data.Date == date.Date)
                                        {
                                            tempoUtilFinal += ocorencia.Tempo;
                                            acrescimoFinal += ocorencia.Tempo;
                                        }
                                        break;
                                }
                            }

                            if (tempoUtilFinal > 0)
                            {
                                eficienciaFinal = (produtividadeFinal * 100 / tempoUtilFinal);
                            }

                            decimal dblAux = 0;
                            if (premio.GruTipo == 1)
                            { // 'Calcula por Tempo
                                decimal dblAux2 = (tempoUtilFinal * premio.GruMaximo) / 100;
                                dblAux = ( tempoUtilFinal * premio.GruMinimo) / 100;
                                if( (produtividadeFinal - dblAux) > 0 )
                                {
                                    dblAux2 = (premio.GruValPartida/ funcionariosPremio.Count()) + ((dblAux2 - dblAux) * premio.GruValor);
                                    dblAux = (premio.GruValPartida/ funcionariosPremio.Count()) + ((produtividadeFinal - dblAux) * premio.GruValor);

                                    if( dblAux > dblAux2 )
                                        dblAux = dblAux2;
                                }
                                else
                                    dblAux = 0;

                                if (dblAux > 0)
                                    dblAux = dblAux / funcionariosPremio.Count() ;
                            }
                            else
                            {// 'Calcula por Eficiência
                                if (eficienciaFinal >= premio.GruMinimo) {
                                    if( (eficienciaFinal - premio.GruMinimo) > (premio.GruMaximo - premio.GruMinimo) )
                                        dblAux = premio.GruValPartida + ((premio.GruMaximo - premio.GruMinimo) * premio.GruValor);
                                    else
                                        dblAux = premio.GruValPartida + ((eficienciaFinal - premio.GruMinimo) * premio.GruValor);
                                } else
                                    dblAux = 0;
                            }
                            if (dblAux == 0)
                            {
                                premioDia = 0;
                                break;
                            }
                            else                                
                                premioDia += dblAux;   

                        }
                    }
                    grupo.valorGrupoDiario = premioDia;
                    grupoPremios.Add(grupo);
                }

            }

            return grupoPremios;
        }

        public IEnumerable<GrupoPremioPartidaMedia> CalculaPremioGrupoMedia(DateTime DaData, DateTime AteData, List<int> premioId)
        {
            int qtdDias = 0; //qtdDias do Premio            

            List<GrupoPremioPartidaMedia> grupoPremios = new List<GrupoPremioPartidaMedia>();
            FuncionarioController funcionarioController = new FuncionarioController();
            PremioPartidaController premioController = new PremioPartidaController();
            var funcionariosGrupo = funcionarioController.GetByPremioDataProducao(premioId, DaData, AteData, "f.id", true);

            foreach (int id in premioId)
            {
                var funcionariosPremio = funcionariosGrupo.Where(f => f.PremioPartida.Id == id);

                var premio = premioController.GetById(id);

                DateTime date = new DateTime();
                decimal premioMedia = 0;
                decimal tempoUtilFinal = 0;
                decimal ocorrenciaFinal = 0;
                decimal descontoFinal = 0;
                decimal punicaoFinal = 0;
                decimal acrescimoFinal = 0;
                decimal produtividadeFinal = 0;
                decimal jornadaFinal = 0;
                decimal eficienciaFinal = 0;
                decimal valorPartidaTotal = 0;

                decimal tempoUtil = 0;
                decimal ocorrencia = 0;
                decimal desconto = 0;
                decimal punicao = 0;
                decimal acrescimo = 0;
                decimal produtividade = 0;
                decimal jornada = 0;
                decimal eficiencia = 0;

                qtdDias = 0;

                GrupoPremioPartidaMedia grupo = new GrupoPremioPartidaMedia();
                grupo.premioId = id;

                List<DateTime> diasFuncionarios = new List<DateTime>();
                foreach (var funcionario in funcionariosPremio) 
                {                    
                    List<decimal> mediaEficiencia = new List<decimal>();

                    
                    for (date = DaData.Date; date.Date <= AteData.Date; date = date.AddDays(1))
                    {
                        tempoUtilFinal = 0;
                        ocorrenciaFinal = 0;
                        descontoFinal = 0;
                        punicaoFinal = 0;
                        acrescimoFinal = 0;
                        produtividadeFinal = 0;
                        jornadaFinal = 0;
                        eficienciaFinal = 0;

                        if (funcionario.Produtividades.Count > 0 && funcionario.Produtividades.Exists(p => p.Data.Date == date.Date))
                        {
                            if(!diasFuncionarios.Exists(d => d == date.Date))
                                diasFuncionarios.Add(date.Date);

                            if (funcionario.Produtividades.Count > 0)
                            {
                                var prod = funcionario.Produtividades.FindAll(p => p.Data.Date == date.Date);
                                if (prod != null && prod.Count > 0)
                                {
                                    tempoUtilFinal = prod.Sum(p => p.Jornada);
                                    jornadaFinal = prod.Sum(p => p.Jornada);
                                }
                            }
                            if (funcionario.Tempos.Count > 0)
                            {
                                var tempo = funcionario.Tempos.FindAll(p => p.Data.Date == date.Date);
                                if (tempo != null && tempo.Count > 0)
                                {
                                    produtividadeFinal += tempo.Sum(p => p.Tempo);
                                }
                            }

                            if (funcionario.Operacoes.Count > 0)
                            {
                                var operacao = funcionario.Operacoes.FindAll(p => p.Data.Date == date.Date);
                                if (operacao != null && operacao.Count > 0)
                                {
                                    produtividadeFinal += operacao.Sum(p => p.Tempo);
                                }
                            }

                            foreach (var ocorencia in funcionario.Ocorrencias)
                            {
                                switch (ocorencia.OcorrenciaTipo)
                                {
                                    case 0:
                                        if (ocorencia.Data.Date == date.Date)
                                        {
                                            tempoUtilFinal -= ocorencia.Tempo;
                                            ocorrenciaFinal += ocorencia.Tempo;
                                        }
                                        break;
                                    case 1:
                                        if (ocorencia.Data.Date == date.Date)
                                        {
                                            tempoUtilFinal -= ocorencia.Tempo;
                                            descontoFinal += ocorencia.Tempo;
                                        }
                                        break;
                                    case 2:
                                        if (ocorencia.Data.Date == date.Date)
                                        {
                                            produtividadeFinal -= ocorencia.Tempo;
                                            punicaoFinal += ocorencia.Tempo;
                                        }
                                        break;
                                    case 3:
                                        if (ocorencia.Data.Date == date.Date)
                                        {
                                            tempoUtilFinal += ocorencia.Tempo;
                                            acrescimoFinal += ocorencia.Tempo;
                                        }
                                        break;
                                }
                            }

                            tempoUtil += tempoUtilFinal;
                            jornada += jornadaFinal;
                            produtividade += produtividadeFinal;
                            acrescimo += acrescimoFinal;
                            punicao += punicaoFinal;
                            desconto += descontoFinal;
                            ocorrencia += ocorrenciaFinal;                           
                            
                        }
                    }
                    
                }

                qtdDias = diasFuncionarios.Count();
                valorPartidaTotal = premio.GruValPartida * qtdDias;

                if (tempoUtil > 0)
                {
                    eficiencia = (produtividade * 100 / tempoUtil);                    
                }

                decimal dblAux = 0;
                if (premio.GruTipo == 1) { // 'Calcula por Tempo
                    decimal dblAux2 = (tempoUtil * premio.GruMaximo) / 100;
                    dblAux = (tempoUtil * premio.GruMinimo) / 100;
                    if ((produtividade - dblAux) > 0) {
                        dblAux2 = valorPartidaTotal + ((dblAux2 - dblAux) * premio.GruValor);
                        dblAux = valorPartidaTotal + ((produtividade - dblAux) * premio.GruValor);

                        if (dblAux > dblAux2) dblAux = dblAux2;
                    }
                    else
                        dblAux = 0;

                    if (dblAux > 0)
                        dblAux = dblAux / funcionariosPremio.Count();

                } else { // 'Calcula por Eficiência
                    if (eficiencia >= premio.GruMinimo) {
                        if((eficiencia - premio.GruMinimo) > (premio.GruMaximo - premio.GruMinimo))
                           dblAux = valorPartidaTotal + ((premio.GruMaximo - premio.GruMinimo) * (premio.GruValor * qtdDias));
                        else
                            dblAux = valorPartidaTotal + ((eficiencia - premio.GruMinimo) * (premio.GruValor * qtdDias));
                    } else
                        dblAux = 0;
                }
                premioMedia = dblAux;

                grupo.valorGrupoMedia = premioMedia;
                grupoPremios.Add(grupo);

            }

            return grupoPremios;
        }



        public IEnumerable<GrupoPremioPartidaDiario> CalculaPremioGrupoDiarioVisualBasic(DateTime DaData, DateTime AteData, List<int> premioId)
        {
            List<GrupoPremioPartidaDiario> grupoPremios = new List<GrupoPremioPartidaDiario>();
            FuncionarioController funcionarioController = new FuncionarioController();
            PremioPartidaController premioController = new PremioPartidaController();
            var funcionariosGrupo = funcionarioController.GetByPremioDataProducao(premioId, DaData, AteData, "f.id", true);

            int id = premioId[0];

            var funcionariosPremio = funcionariosGrupo.Where(f => f.PremioPartida.Id == id);

            var premio = premioController.GetById(id);

            DateTime date = new DateTime();
            decimal premioDia = 0;
            decimal tempoUtilFinal = 0;
            decimal ocorrenciaFinal = 0;
            decimal descontoFinal = 0;
            decimal punicaoFinal = 0;
            decimal acrescimoFinal = 0;
            decimal produtividadeFinal = 0;
            decimal jornadaFinal = 0;
            decimal eficienciaFinal = 0;


            for (date = DaData.Date; date.Date <= AteData.Date; date = date.AddDays(1))
            {
                GrupoPremioPartidaDiario grupo = new GrupoPremioPartidaDiario();
                grupo.premioId = id;
                grupo.dia = date;

                premioDia = 0;
                foreach (var funcionario in funcionariosPremio)
                {
                    tempoUtilFinal = 0;
                    ocorrenciaFinal = 0;
                    descontoFinal = 0;
                    punicaoFinal = 0;
                    acrescimoFinal = 0;
                    produtividadeFinal = 0;
                    jornadaFinal = 0;
                    eficienciaFinal = 0;

                    if (funcionario.Produtividades.Count > 0 && funcionario.Produtividades.Exists(p => p.Data.Date == date.Date))
                    {
                        if (funcionario.Produtividades.Count > 0)
                        {
                            var prod = funcionario.Produtividades.FindAll(p => p.Data.Date == date.Date);
                            if (prod != null && prod.Count > 0)
                            {
                                tempoUtilFinal = prod.Sum(p => p.Jornada);
                                jornadaFinal = prod.Sum(p => p.Jornada);
                            }
                        }
                        if (funcionario.Tempos.Count > 0)
                        {
                            var tempo = funcionario.Tempos.FindAll(p => p.Data.Date == date.Date);
                            if (tempo != null && tempo.Count > 0)
                            {
                                produtividadeFinal += tempo.Sum(p => p.Tempo);
                            }
                        }

                        if (funcionario.Operacoes.Count > 0)
                        {
                            var operacao = funcionario.Operacoes.FindAll(p => p.Data.Date == date.Date);
                            if (operacao != null && operacao.Count > 0)
                            {
                                produtividadeFinal += operacao.Sum(p => p.Tempo);
                            }
                        }

                        foreach (var ocorencia in funcionario.Ocorrencias)
                        {
                            switch (ocorencia.OcorrenciaTipo)
                            {
                                case 0:
                                    if (ocorencia.Data.Date == date.Date)
                                    {
                                        tempoUtilFinal -= ocorencia.Tempo;
                                        ocorrenciaFinal += ocorencia.Tempo;
                                    }
                                    break;
                                case 1:
                                    if (ocorencia.Data.Date == date.Date)
                                    {
                                        tempoUtilFinal -= ocorencia.Tempo;
                                        descontoFinal += ocorencia.Tempo;
                                    }
                                    break;
                                case 2:
                                    if (ocorencia.Data.Date == date.Date)
                                    {
                                        produtividadeFinal -= ocorencia.Tempo;
                                        punicaoFinal += ocorencia.Tempo;
                                    }
                                    break;
                                case 3:
                                    if (ocorencia.Data.Date == date.Date)
                                    {
                                        tempoUtilFinal += ocorencia.Tempo;
                                        acrescimoFinal += ocorencia.Tempo;
                                    }
                                    break;
                            }
                        }

                        if (tempoUtilFinal > 0)
                        {
                            eficienciaFinal = (produtividadeFinal * 100 / tempoUtilFinal);
                        }

                        decimal dblAux = 0;
                        if (premio.GruTipo == 1)
                        { // 'Calcula por Tempo
                            decimal dblAux2 = (tempoUtilFinal * premio.GruMaximo) / 100;
                            dblAux = (tempoUtilFinal * premio.GruMinimo) / 100;
                            if ((produtividadeFinal - dblAux) > 0)
                            {
                                dblAux2 = (premio.GruValPartida / funcionariosPremio.Count()) + ((dblAux2 - dblAux) * premio.GruValor);
                                dblAux = (premio.GruValPartida / funcionariosPremio.Count()) + ((produtividadeFinal - dblAux) * premio.GruValor);

                                if (dblAux > dblAux2)
                                    dblAux = dblAux2;
                            }
                            else
                                dblAux = 0;

                            if (dblAux > 0)
                                dblAux = dblAux / funcionariosPremio.Count();
                        }
                        else
                        {// 'Calcula por Eficiência
                            if (eficienciaFinal >= premio.GruMinimo)
                            {
                                if ((eficienciaFinal - premio.GruMinimo) > (premio.GruMaximo - premio.GruMinimo))
                                    dblAux = premio.GruValPartida + ((premio.GruMaximo - premio.GruMinimo) * premio.GruValor);
                                else
                                    dblAux = premio.GruValPartida + ((eficienciaFinal - premio.GruMinimo) * premio.GruValor);
                            }
                            else
                                dblAux = 0;
                        }
                       
                         premioDia += dblAux;

                    }
                }
                grupo.valorGrupoDiario = premioDia;
                grupoPremios.Add(grupo);
            }

            return grupoPremios;
        }
    }
}