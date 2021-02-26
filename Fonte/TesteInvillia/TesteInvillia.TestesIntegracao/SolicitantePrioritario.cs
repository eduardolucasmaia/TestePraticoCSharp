using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TesteInvillia.TestesIntegracao
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PrioridadeTesteAttribute : Attribute
    {
        public PrioridadeTesteAttribute(int prioridade)
        {
            Prioridade = prioridade;
        }

        public int Prioridade { get; }
    }

    public class SolicitantePrioritario : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> CasosTeste) where TTestCase : ITestCase
        {
            var metodosClassificados = new SortedDictionary<int, List<TTestCase>>();

            foreach (var casoTeste in CasosTeste)
            {
                var prioridade = 0;

                foreach (var attr in casoTeste.TestMethod.Method.GetCustomAttributes((typeof(PrioridadeTesteAttribute).AssemblyQualifiedName)))
                    prioridade = attr.GetNamedArgument<int>("Priority");

                PegarOuCriar(metodosClassificados, prioridade).Add(casoTeste);
            }

            foreach (var list in metodosClassificados.Keys.Select(priority => metodosClassificados[priority]))
            {
                list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
                foreach (var testCase in list)
                    yield return testCase;
            }
        }

        private static TValue PegarOuCriar<TKey, TValue>(IDictionary<TKey, TValue> dicionario, TKey chave) where TValue : new()
        {
            if (dicionario.TryGetValue(chave, out var resultado)) return resultado;

            resultado = new TValue();
            dicionario[chave] = resultado;
            return resultado;
        }
    }
}
