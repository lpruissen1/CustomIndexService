using Database.Model.User.CustomIndices;

namespace UserCustomIndices.Validators
{
    public interface ICustomIndexValidator
    {
        bool Validate(CustomIndex index);
    }
}
