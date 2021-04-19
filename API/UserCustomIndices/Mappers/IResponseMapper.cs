using Database.Model.User.CustomIndices;
using UserCustomIndices.Model.Response;

namespace UserCustomIndices.Mappers
{
    public interface IResponseMapper
    {
        CustomIndex Map(CustomIndexResponse response);
    }
}