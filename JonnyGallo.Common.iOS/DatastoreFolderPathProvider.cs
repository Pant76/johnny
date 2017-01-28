using System;
using JonnyGallo.Abstractions;

namespace JonnyGallo.Common.iOS
{
	public class DatastoreFolderPathProvider : IDatastoreFolderPathProvider
	{
		public string GetPath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		}
	}
}

