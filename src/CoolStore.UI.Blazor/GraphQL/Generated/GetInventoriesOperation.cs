using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.UI.Blazor
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class GetInventoriesOperation
        : IOperation<IGetInventories>
    {
        public string Name => "getInventories";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Query;

        public Type ResultType => typeof(IGetInventories);

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            return Array.Empty<VariableValue>();
        }
    }
}
