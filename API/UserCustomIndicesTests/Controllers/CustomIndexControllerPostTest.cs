using Database.Model.User.CustomIndices;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UserCustomIndices.Controllers;
using UserCustomIndices.Services;
using UserCustomIndices.Validators;
using UserCustomIndicesTests.Controllers.Fakes;

namespace UserCustomIndicesTests.Controllers
{
    class CustomIndexControllerPostTest
    {
        private ICustomIndexService customIndexService;
        private CustomIndexController sut;

        [Test]
        public void TestPost_InvalidCustomIndex()
        {
        }

        [Test]
        public void TestPost_ValidCustomIndex()
        {
        }

        private void SetupCustomIndexCollection(ICustomIndexValidator validator)
        {
            customIndexService = new CustomIndexServiceFake(new List<CustomIndex>(), new Dictionary<Guid, List<string>>());
            sut = new CustomIndexController(customIndexService, validator);
        }
    }
}
