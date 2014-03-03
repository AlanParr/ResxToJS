using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Resources;

namespace ResxToJs
{
	public class ResxToJsConverter : IResxToJsConverter
	{
		private readonly IDeepCopier objectCopier;

		private readonly IResxReader resxReader;

		private readonly IJsonHelper jsonHelper;

		public ResxToJsConverter(IDeepCopier objectCopier, IResxReader resxReader, IJsonHelper jsonHelper)
		{
			this.objectCopier = objectCopier;
			this.resxReader = resxReader;
			this.jsonHelper = jsonHelper;
		}

		public void Convert(Options options)
		{
			var resourceFiles = resxReader.GetResourceFiles(options.InputFolder);

			var baseResourceFile = resourceFiles.First(x => x.IsBaseResourceType);

			var baseResourceDict = resxReader.GetKeyValuePairsFromResxFile(baseResourceFile);
			
			foreach (var resourceFile in resourceFiles)
			{
				var jsFileNameWithoutPath = resourceFile.ResourceFilePathName.Substring(resourceFile.ResourceFilePathName.LastIndexOf("\\") + 1) + ".js";
				var outputJsFilePathName = Path.Combine(options.OutputFolder, jsFileNameWithoutPath);

				if (resourceFile.IsBaseResourceType)
				{
					WriteOutput(options, baseResourceDict, outputJsFilePathName);
				}
				else
				{
					var cultureSpecificResourceDict = objectCopier.Copy(baseResourceDict);
					var rsxr = new ResXResourceReader(resourceFile.ResourceFilePathName);
					foreach (DictionaryEntry d in rsxr)
					{
						var key = d.Key as string;
						cultureSpecificResourceDict[key] = d.Value.ToString();
					}
					//Close the reader.
					rsxr.Close();

					WriteOutput(options, cultureSpecificResourceDict, outputJsFilePathName);
				}
			}
		}

		public void WriteOutput(Options options, Dictionary<string, string> dict, string outputLocation)
		{
		    var json = jsonHelper.GenerateJson(dict, "Resources", options.PrettyPrint);
			File.WriteAllText(outputLocation, json);
		}
	}
}
