using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.UI.Blazor
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class GetProductsOperation
        : IOperation<IGetProducts>
    {
        public string Name => "getProducts";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Query;

        public Type ResultType => typeof(IGetProducts);

        public Optional<int> CurrentPage { get; set; }

        public Optional<double> HighPrice { get; set; }

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            var variables = new List<VariableValue>();

            if (CurrentPage.HasValue)
            {
                variables.Add(new VariableValue("currentPage", "Int", CurrentPage.Value));
            }

            if (HighPrice.HasValue)
            {
                variables.Add(new VariableValue("highPrice", "Float", HighPrice.Value));
            }

            return variables;
        }
    }
}
