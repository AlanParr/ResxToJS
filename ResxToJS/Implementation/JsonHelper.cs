using System.Collections.Generic;
using System.Text;

namespace ResxToJs
{
	public class JsonHelper : IJsonHelper
	{
		private const string IndentString = "    ";

		public string GenerateJson(Dictionary<string, string> input, string objectName = "Resources", bool prettyPrint = false)
		{
			var sb = new StringBuilder(objectName + " = {");
			foreach (var entry in input)
			{
				sb.AppendFormat("\"{0}\":\"{1}\",", entry.Key, entry.Value);
			}

			if (sb.Length > 0)
			{
				sb = sb.Remove(sb.Length - 1, 1);
			}
			sb.Append("};");

            var result = prettyPrint ? PrettyPrintJson(sb.ToString()) : sb.ToString();

		    return result;
		}

		private string PrettyPrintJson(string jsonString)
		{
			var quoted = false;
			var sb = new StringBuilder();
			for (var i = 0; i < jsonString.Length; i++)
			{
				var ch = jsonString[i];
				sb.Append(ch);
				switch (ch)
				{
					case '{':
						sb.Append("\n");
						break;

					case '"':
						var escaped = false;
						var index = i;
						while (index > 0 && jsonString[--index] == '\\')
							escaped = !escaped;
						if (!escaped)
							quoted = !quoted;
						break;

					case ':':
						sb.Append(" ");
						break;

					case ',':
						if (!quoted)
						{
							sb.Append("\n");
						}
						break;
				}
			}

			return sb.ToString();
		}
	}
}
