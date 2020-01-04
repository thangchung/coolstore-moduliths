using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using StrawberryShake;
using StrawberryShake.Configuration;
using StrawberryShake.Http;
using StrawberryShake.Http.Subscriptions;
using StrawberryShake.Transport;

namespace CoolStore.UI.Blazor
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class GetProductsResultParser
        : JsonResultParserBase<IGetProducts>
    {
        private readonly IValueSerializer _stringSerializer;
        private readonly IValueSerializer _floatSerializer;

        public GetProductsResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _stringSerializer = serializerResolver.Get("String");
            _floatSerializer = serializerResolver.Get("Float");
        }

        protected override IGetProducts ParserData(JsonElement data)
        {
            return new GetProducts
            (
                ParseGetProductsProducts(data, "products")
            );

        }

        private IReadOnlyList<ICatalogProductDto> ParseGetProductsProducts(
            JsonElement parent,
            string field)
        {
            if (!parent.TryGetProperty(field, out JsonElement obj))
            {
                return null;
            }

            if (obj.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            int objLength = obj.GetArrayLength();
            var list = new ICatalogProductDto[objLength];
            for (int objIndex = 0; objIndex < objLength; objIndex++)
            {
                JsonElement element = obj[objIndex];
                list[objIndex] = new CatalogProductDto
                (
                    DeserializeNullableString(element, "id"),
                    DeserializeNullableString(element, "name"),
                    DeserializeNullableString(element, "categoryName"),
                    DeserializeNullableString(element, "inventoryLocation"),
                    DeserializeFloat(element, "price"),
                    DeserializeNullableString(element, "imageUrl"),
                    DeserializeNullableString(element, "description")
                );

            }

            return list;
        }

        private string DeserializeNullableString(JsonElement obj, string fieldName)
        {
            if (!obj.TryGetProperty(fieldName, out JsonElement value))
            {
                return null;
            }

            if (value.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return (string)_stringSerializer.Deserialize(value.GetString());
        }

        private double DeserializeFloat(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (double)_floatSerializer.Deserialize(value.GetDouble());
        }
    }
}
