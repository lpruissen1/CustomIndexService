using Database.Model.User.CustomIndices;
using Microsoft.AspNetCore.Mvc;
using Moq;
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

        private Mock<ICustomIndexValidator> CustomIndexValidatorMock;

        [SetUp]
        public void SetUp()
        {
            CustomIndexValidatorMock = new Mock<ICustomIndexValidator>();
        }

        [Test]
        public void TestPost_InvalidCustomIndex()
        {
            var index = new CustomIndex();
            CustomIndexValidatorMock.Setup(x => x.Validate(index)).Returns(false);

            SetupController(CustomIndexValidatorMock.Object);

            var response = sut.Create(index).Result;

            Assert.IsInstanceOf<BadRequestResult>(response);
        }

        [Test]
        public void TestPost_ValidCustomIndex()
        {
            var index = new CustomIndex() { Id = "IAmAnId"};
            CustomIndexValidatorMock.Setup(x => x.Validate(index)).Returns(true);

            SetupController(CustomIndexValidatorMock.Object);

            var response = sut.Create(index);

            Assert.IsInstanceOf<ObjectResult>(response.Result);
            Assert.AreEqual(index, response.Value);
        }

        private void SetupController(ICustomIndexValidator validator)
        {
            customIndexService = new CustomIndexServiceFake(new List<CustomIndex>(), new Dictionary<Guid, List<string>>());
            sut = new CustomIndexController(customIndexService, validator);
        }
    }
}
