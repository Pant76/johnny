using System.Collections.Generic;
using System.Threading.Tasks;

namespace JonnyGallo.Abstractions
{
	/// <summary>
	/// Defines a conract for data source that exposes CRUD operations for a specific type.
	/// </summary>
	public interface IWordPressDataSource<T>:IDataSource<T> where T : IObservableEntityData
	{
	    Task<IEnumerable<string>> GetTags(string contains, string onlyElementsWithTag = null);

	}
}

