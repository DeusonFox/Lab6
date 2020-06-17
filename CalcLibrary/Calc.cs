using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CalcLibrary
{
    public static class Calc
    {
        public static string DoOperation(string s)
        {
            string[] operands = GetOperands(s);//получить операнды
            string op = GetOperation(s);//получить операцию
            s = DoubleOperation[op](double.Parse(operands[0]), double.Parse(operands[1])).ToString();
            return s; //вычислить и получить строку;
        }
        public static string[] GetOperands(string s)
        {
            //string pattern = @"[^\d,\.]";
            //Regex rgx = new Regex(pattern);
            Regex rgx = new Regex(@"[^\d,\.]");
            MatchCollection mc = rgx.Matches(s);
            List<string> lm = new List<string>();
            foreach (Match m in mc)
            {
                lm.Add(m.Value);
            }
            return s.Split(lm.ToArray(), StringSplitOptions.None);
        }
        public static string GetOperation(string s)
        {
            Regex rgx = new Regex(@"[+*/\-]");
            MatchCollection mc = rgx.Matches(s);
            List<string> lm = new List<string>();
            foreach (Match m in mc)
            {
                lm.Add(m.Value);
                s = m.Value;
            }
            return s;
        }
        public delegate T OperationDelegate<T>(T x, T y);

        public static Dictionary<string, OperationDelegate<double>> DoubleOperation = new Dictionary<string, OperationDelegate<double>>
        {
            //{ "+", delegate(double x, double y){ return x + y; } },
            //{ "-", delegate(double x, double y){ return x - y; } },
            //{ "*", delegate(double x, double y){ return x * y; } },
            //{ "/", delegate(double x, double y){ return x / y; } },
            { "+", (x, y) => x + y },
            { "-", (x, y) => x - y },
            { "*", (x, y) => x * y },
            { "/", (x, y) => x / y },
        };
    }
}
