using Database.Model.User.CustomIndices;
using System;

namespace UserCustomIndices.Validators
{
    public interface ICustomIndexValidator
    {
        bool Validate(CustomIndex index);
    }
}
