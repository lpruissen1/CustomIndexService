using Users.Core.Response;
using Users.Database.Model.CustomIndex;

namespace Users.CustomIndices
{
	public interface IResponseMapper
    {
		CustomIndexResponse Map(CustomIndex index);
	}
}
