using System.Collections.Generic;
using Newtonsoft.Json;

namespace ResxToJs
{
    public class NewtonsoftJsonHelper :IJsonHelper
    {
        public string GenerateJson(Dictionary<string, string> input, string objectName = "Resources", bool prettyPrint = false)
        {
            var outputJson = objectName + " = ";
            outputJson += prettyPrint
                ? JsonConvert.SerializeObject(input, Formatting.Indented)
                : JsonConvert.SerializeObject(input, Formatting.None);
            return outputJson;
        }
    }
}
