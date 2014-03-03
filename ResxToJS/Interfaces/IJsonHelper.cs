using System.Collections.Generic;

namespace ResxToJs
{
	public interface IJsonHelper
	{
		string GenerateJson(Dictionary<string, string> input, string objectName = "Resources", bool prettyPrint = false);
	}
}
