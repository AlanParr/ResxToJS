using System.Collections.Generic;
using System;
using System.Collections;
using System.IO;
using System.Resources;

namespace ResxToJs
{
	public class ResxReader : IResxReader
	{
		public List<ResourceFile> GetResourceFiles(string directory)
		{
			var outputResourceFiles = new List<ResourceFile>();
			var resourceFiles = Directory.GetFiles(directory, "*.resx");
			foreach (var filePathName in resourceFiles)
			{
				var resourceFile = new ResourceFile { IsBaseResourceType = false, ResourceFilePathName = filePathName };

				var nameWithoutResx = Path.GetFileNameWithoutExtension(filePathName);

				// The file which does not have the ISO culture code in it is the base resource file.
				if (nameWithoutResx != null && nameWithoutResx.IndexOf(".", StringComparison.Ordinal) == -1)
				{
					resourceFile.IsBaseResourceType = true;
				}
				outputResourceFiles.Add(resourceFile);
			}

			return outputResourceFiles;
		}

		public Dictionary<string, string> GetKeyValuePairsFromResxFile(ResourceFile resourceFile)
		{
			var resourceFileDict = new Dictionary<string, string>();
			try
			{
				var resourceReader = new ResXResourceReader(resourceFile.ResourceFilePathName);
				foreach (DictionaryEntry d in resourceReader)
				{
					var key = d.Key as string;
					resourceFileDict.Add(key, d.Value.ToString());
				}
				resourceReader.Close();
			}
			catch (Exception ex)
			{
				
			}
			
			return resourceFileDict;
		}
	}
}
