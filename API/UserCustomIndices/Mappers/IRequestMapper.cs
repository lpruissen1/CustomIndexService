using Database.Model.User.CustomIndices;
using UserCustomIndices.Core.Model.Requests;

namespace UserCustomIndices.Mappers
{
	public interface IRequestMapper
    {
        CustomIndex Map(CustomIndexRequest response);
        CustomIndex Map(CreateCustomIndexRequest response);
	}
}
