using Database.Model.User.CustomIndices;
using UserCustomIndices.Validators;

namespace UserCustomIndicesTests.Controllers.Fakes
{
    class CustomIndexValidatorFake : ICustomIndexValidator
    {
        public bool Validate(CustomIndex index)
        {
            throw new System.NotImplementedException();
        }
    }
}
