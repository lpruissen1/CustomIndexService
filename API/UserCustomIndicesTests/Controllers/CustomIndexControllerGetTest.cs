using Database.Model.User.CustomIndices;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UserCustomIndices.Controllers;
using UserCustomIndices.Services;
using UserCustomIndicesTests.Controllers.Fakes;

namespace UserCustomIndicesTests.Controllers
{
    public class CustomIndexControllerGetTest
    {
        private ICustomIndexService customIndexService;
        private CustomIndexController sut;

        [Test]
        public void TestGet_WithUserId_ReturnsAllIndicesForTheUser()
        {
            var userId = Guid.NewGuid();
            var indexId1 = "123456789009876543211234";
            var indexId2 = "123456789009876543211235";

            var customIndices = new List<CustomIndex>() 
            { 
                new CustomIndex(){ Id = indexId1},
                new CustomIndex(){ Id = indexId2}
            };

            var userIndices = new Dictionary<Guid, List<string>>();
            userIndices.Add(userId, new List<string> { indexId1, indexId2 });

            SetupCustomIndexCollection(customIndices, userIndices);

            var response = sut.Get(userId);

            Assert.IsInstanceOf<OkObjectResult>(response);

            var customIndex = ((OkObjectResult)response).Value as List<CustomIndex>;
            Assert.AreEqual(indexId1, customIndex[0].Id);
            Assert.AreEqual(indexId2, customIndex[1].Id);
        }

        [Test]
        public void TestGet_WithNonExistantUserId_ReturnsNotFoundResult()
        {
            var userId = Guid.NewGuid();
            var id1 = "123456789009876543211234";

            var customIndices = new List<CustomIndex>() { new CustomIndex(){ Id = id1} };
            var userIndices = new Dictionary<Guid, List<string>>();
            userIndices.Add(userId, new List<string> { id1 });
            SetupCustomIndexCollection(customIndices, userIndices);

            var response = sut.Get(Guid.NewGuid());

            Assert.IsInstanceOf<NotFoundResult>(response);
        }

        [TestCase("")]
        [TestCase("12345678900987654321123")]
        public void TestGet_WithIncorrectLengthIndexId_ReturnsBadRequestActionResponse(string id)
        {
            SetupCustomIndexCollection(new List<CustomIndex>(), null);

            var response = sut.Get(Guid.NewGuid(), id);

            Assert.IsInstanceOf<BadRequestResult>(response);
        }

        [Test]
        public void TestGet_WithIdThatDoesntExistInCollection_ReturnsObjectNotFoundActionResponse()
        {
            var userId = Guid.NewGuid();
            var id = "123456789009876543211234";
            var customIndices = new List<CustomIndex>
            {
                  new CustomIndex{ Id = "NotTheSameId"}
            };
            var userIndices = new Dictionary<Guid, List<string>>();
            userIndices.Add(userId, new List<string> { "NotTheSameId" });
            SetupCustomIndexCollection(customIndices, userIndices);

            var response = sut.Get(userId, id);

            Assert.IsInstanceOf<NotFoundResult>(response);
        }

        [Test]
        public void TestGet_WithIndexId_ReturnsOkObjectResult()
        {
            var userId = Guid.NewGuid();
            var indexId = "123456789009876543211234";

            var customIndex = new CustomIndex { Id = indexId };
            var customIndices = new List<CustomIndex> { customIndex };
            var userIndices = new Dictionary<Guid, List<string>>();
            userIndices.Add(userId, new List<string> { indexId });

            SetupCustomIndexCollection(customIndices, userIndices);

            var response = sut.Get(userId, indexId);
            Assert.IsInstanceOf<OkObjectResult>(response);

            var responseIndex = ((OkObjectResult)response).Value as CustomIndex;
            Assert.AreEqual(indexId, responseIndex.Id);
        }

        private void SetupCustomIndexCollection(List<CustomIndex> customIndices, Dictionary<Guid, List<string>> userIndicesCollection)
        {
            customIndexService = new CustomIndexServiceFake(customIndices, userIndicesCollection);
            sut = new CustomIndexController(customIndexService, new CustomIndexValidatorFake());
        }
    }
}