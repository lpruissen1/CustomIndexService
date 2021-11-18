using Users.Core.Request;
using Users.Database.Model.CustomIndex;

namespace Users.CustomIndices
{
	public interface IRequestMapper
    {
        CustomIndex Map(CustomIndexRequest response);
        CustomIndex Map(CreateCustomIndexRequest response);
	}
}
