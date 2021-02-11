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
    class CustomIndexControllerDeleteTest
    {
        private ICustomIndexService customIndexService;
        private CustomIndexController sut;

        private Mock<ICustomIndexValidator> CustomIndexValidatorMock;
        private Guid clientId = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            CustomIndexValidatorMock = new Mock<ICustomIndexValidator>();
        }

        [Test]
        public void TestDelete_IndexDoesNotExist_ReturnsBadRequest()
        {
            var indexId = "123456789009876543211234";

            SetupController(new CustomIndexServiceFake(new List<CustomIndex>(), new Dictionary<Guid, List<string>>()), CustomIndexValidatorMock.Object);

            var response = sut.Delete(clientId, indexId);

            Assert.IsInstanceOf<BadRequestResult>(response);
        }

        [Test]
        public void TestDelete_IndexDoesExist_NotOwnedByClient_ReturnsBadRequest()
        {
            var invalidIndexId = "123456789009876543211234";
            var indexId = "123456789009876543211111";
            var index = new CustomIndex() { Id = indexId };

            var userIndices = new Dictionary<Guid, List<string>>();
            userIndices.Add(clientId, new List<string>() { indexId });
            SetupController(new CustomIndexServiceFake(new List<CustomIndex>() { index }, userIndices), CustomIndexValidatorMock.Object);

            var response = sut.Delete(clientId, invalidIndexId);

            Assert.IsInstanceOf<BadRequestResult>(response);
        }

        [Test]
        public void TestDelete_IndexDoesExist_OwnedByClient_ReturnsOkResult()
        {
            var indexId = "123456789009876543211234";
            var index = new CustomIndex() { Id = indexId };

            var userIndices = new Dictionary<Guid, List<string>>();
            userIndices.Add(clientId, new List<string>() { index.Id });
            SetupController(new CustomIndexServiceFake(new List<CustomIndex>() { index }, userIndices), CustomIndexValidatorMock.Object);

            var response = sut.Delete(clientId, indexId);

            Assert.IsInstanceOf<OkResult>(response);
        }

        [TestCase("")]
        [TestCase("12345678900987654321123")]
        public void TestGet_WithIncorrectLengthIndexId_ReturnsBadRequestActionResponse(string id)
        {
            SetupController(null, null);

            var response = sut.Get(Guid.NewGuid(), id);

            Assert.IsInstanceOf<BadRequestResult>(response);
        }

        private void SetupController(ICustomIndexService service, ICustomIndexValidator validator)
        {
            sut = new CustomIndexController(service, validator);
        }
    }
}
