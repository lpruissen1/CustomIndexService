using Database.Model.User.CustomIndices;
namespace UserCustomIndices.Validators
{
    public class CustomIndexValidator : ICustomIndexValidator
    {
        public bool Validate(CustomIndex index)
        {
            return true;
        }
    }
}
