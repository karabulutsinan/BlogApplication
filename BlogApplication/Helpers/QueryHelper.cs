using System.Linq.Dynamic.Core;
using BlogApplication.Domain.Entities;

namespace BlogApplication.Helpers
{
	public static class QueryHelper
	{
		public static IQueryable<T> IsAvailable<T>(this IQueryable<T> query)
		{
			var type = typeof(T);
			var isActiveProp = type.GetProperty("IsActive");
			var isDeletedProp = type.GetProperty("IsDeleted");

			if (isActiveProp != null)
				query = query.Where($"IsActive == true");

			if (isDeletedProp != null)
				query = query.Where($"IsDeleted == false");
			
			return query;
		}
	}
}
