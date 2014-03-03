using System;

namespace ResxToJs
{
	class Program
	{
		static void Main(string[] args)
		{
			var options = new Options();
			var parseSuccess = CommandLine.Parser.Default.ParseArguments(args, options);
			if (parseSuccess)
			{
				IDeepCopier copier = new DeepCopier();
				IResxReader resxReader = new ResxReader();
				IJsonHelper jsonHelper = new NewtonsoftJsonHelper();
				IResxToJsConverter converter = new ResxToJsConverter(copier, resxReader, jsonHelper);
				converter.Convert(options);
			}
			else
			{
				Console.WriteLine("An error occurred while parsing the resx files.");
			}
		}
	}
}