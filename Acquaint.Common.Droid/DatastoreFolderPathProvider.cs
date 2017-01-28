using System;
using JonnyGallo.Abstractions;

namespace JonnyGallo.Common.Droid
{
	public class DatastoreFolderPathProvider : IDatastoreFolderPathProvider
	{
		public string GetPath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		}
	}
}

