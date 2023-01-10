using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace Vestillo.Lib
{
    public static class CustomExtensions
    {        
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static string ValorMoeda(this decimal valor)
        {
            return valor.ToString("###,###,##0.00");
        }

        public static string ToMoney(this decimal value)
        {
            return value.ToString("###,###,##0.00");
        }

        public static int DiffDaysToday(this DateTime value)
        {
            return (int)((TimeSpan)(DateTime.Now.Date - value.Date)).TotalDays;
        }

        public static string ValorSeNull(this string str, string valor)
        {
            if (string.IsNullOrWhiteSpace(str))
                return valor;
            else
                return str;
        }

        public static string RemoverAcentos(this string str)
        {
            string s = str.Normalize(NormalizationForm.FormD);

            var sb = new StringBuilder();

            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }

        public static string Ultimos(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }

        public static string NFeTelefone(this string str)
        {
            if (str == null)
                return null;

            return str.Replace("-", "").Replace("(", "")
                      .Replace(")", "").Replace("-", "")
                      .Replace("-", "").Replace("/", "")
                      .Replace(".", "").Replace(" ", "")
                      .Replace("  ", "").Replace("    ", "").Trim();
        }

        public static string NFe(this string str)
        {
            if (str == null)
                return null;

            return str.Replace("-", "").Replace("(", "")
                      .Replace(")", "").Replace("-", "")
                      .Replace("-", "").Replace("/", "")
                      .Replace(".", "").Trim();
        }

        public static string FormatarCPFCNPJ(this string str)        
        {
            long CNPJ = long.Parse(str);
            string CNPJFormatado = string.Empty;
            
            if (str.Length == 11)  // CPF
                CNPJFormatado = string.Format(@"{0:000\.000\.000\-00}", CNPJ); //Formatar de Long para CPF
            
            if (str.Length == 14)
                CNPJFormatado = string.Format(@"{0:00\.000\.000\/0000\-00}", CNPJ); //Formatar de Long para CNPJ

            return CNPJFormatado;
        }

        public static DateTime DataXMLNFeToDateTime(this string str)        
        {
            // Exemplo: 2016-03-31T00:00:00-03:00

            // 2016-03-31

            int Ano = int.Parse(str.Substring(0, 4));
            int Mes = int.Parse(str.Substring(5, 2));
            int Dia = int.Parse(str.Substring(8, 2));

            int Hora = 0;
            int Minutos = 0;
            int Segundos = 0;

            if (str.Length == 25)
            {
                Hora = int.Parse(str.Substring(11, 2));
                Minutos = int.Parse(str.Substring(14, 2));
                Segundos = int.Parse(str.Substring(17, 2));
            }

            DateTime Retorno = new DateTime(Ano, Mes, Dia, Hora, Minutos, Segundos);

            return Retorno;
        }

        public static decimal ToDecimal(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;

            string value = str.Trim().Replace(".", "");

            decimal result = 0;

            decimal.TryParse(value, out result);

            return result;
        }

        public static decimal ToDecimal(this object obj)
        {

            if (obj == null)
                return 0;

            string str = obj.ToString();

            if (string.IsNullOrWhiteSpace(str))
                return 0;

            string value = str.Trim().Replace(".", "");

            decimal result = 0;

            decimal.TryParse(value, out result);

            return result;
        }


        public static int ToInt(this object obj)
        {
            if (obj == null)
                return 0;

            string str = obj.ToString();

            if (string.IsNullOrWhiteSpace(str))
                return 0;

            int result = 0;

            int.TryParse(str, out result);

            return result;
        }

        public static int ToInt(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;
            
            int result = 0;
            
            int.TryParse(str, out result);

            return result;
        }


        public static string ToLikeSQL(this String str)
        {
            if (str == null)
                return str;

            return "%" + str + "%";
        }
   
        public static string ToBooleanString(this bool val)
        {
            if (val)
                return "Sim";
            else
                return "Não";
        }

        public static string ToValorNFe(this decimal valor)
        {
            return valor.ToString().Replace(",", ".");
        }

        public static string ToValorNFe(this decimal valor, int casasdecimais)
        {
            if(casasdecimais > 0)
                return valor.ToString("F" + casasdecimais).Replace(",", ".");
            else
                return valor.ToString().Replace(",", ".");
        }

        public static string toExtenso(this decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += escreva_parte(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " TRILHÃO" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " TRILHÕES" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " BILHÃO" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " BILHÕES" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " MILHÃO" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " MILHÕES" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " MIL" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " E " : string.Empty);

                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                                valor_por_extenso += " DE";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                                    valor_por_extenso += " DE";
                                else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                                        valor_por_extenso += " DE";

                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " REAL";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " REAIS";

                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " E ";
                    }

                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " CENTAVO";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " CENTAVOS";
                }
                return valor_por_extenso.Replace("UM MIL", "HUM MIL");
            }
        }

        public static string ToMoeda(this string valor)
        {
            decimal valorSaida;

            string formato = new string('0', 2);
            formato = "{0:0." + formato + "}";

            if (Decimal.TryParse(valor.Trim(), out valorSaida))
                return string.Format(formato, valorSaida);
            else
                return ""; 
        }

        static string escreva_parte(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2) montagem += "DUZENTOS";
                else if (a == 3) montagem += "TREZENTOS";
                else if (a == 4) montagem += "QUATROCENTOS";
                else if (a == 5) montagem += "QUINHENTOS";
                else if (a == 6) montagem += "SEISCENTOS";
                else if (a == 7) montagem += "SETECENTOS";
                else if (a == 8) montagem += "OITOCENTOS";
                else if (a == 9) montagem += "NOVECENTOS";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "UM";
                    else if (c == 2) montagem += "DOIS";
                    else if (c == 3) montagem += "TRÊS";
                    else if (c == 4) montagem += "QUATRO";
                    else if (c == 5) montagem += "CINCO";
                    else if (c == 6) montagem += "SEIS";
                    else if (c == 7) montagem += "SETE";
                    else if (c == 8) montagem += "OITO";
                    else if (c == 9) montagem += "NOVE";

                return montagem;
            }
        }
    
    }
}
