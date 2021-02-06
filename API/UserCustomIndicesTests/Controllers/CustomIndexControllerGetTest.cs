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
            var id1 = "123456789009876543211234";
            var id2 = "123456789009876543211235";

            var customIndices = new List<CustomIndex>() 
            { 
                new CustomIndex(){ Id = id1},
                new CustomIndex(){ Id = id2}
            };

            var userIndices = new Dictionary<Guid, List<string>>();
            userIndices.Add(userId, new List<string> { id1, id2 });

            SetupCustomIndexCollection(customIndices, userIndices);

            var response = sut.Get(userId).Value;

            Assert.AreEqual(response.Count, 2);
            Assert.IsTrue(response.Exists(x => x.Id == id1));
            Assert.IsTrue(response.Exists(x => x.Id == id2));
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

            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }

        [TestCase("")]
        [TestCase("12345678900987654321123")]
        public void TestGet_WithIncorrectLengthIndexId_ReturnsBadRequestActionResponse(string id)
        {
            SetupCustomIndexCollection(new List<CustomIndex>());

            var response = sut.Get(id);

            Assert.IsInstanceOf<BadRequestResult>(response.Result);
        }

        [Test]
        public void TestGet_WithIdThatDoesntExistInCollection_ReturnsObjectNotFoundActionResponse()
        {
            var id = "123456789009876543211234";
            var customIndices = new List<CustomIndex>
            {
                  new CustomIndex{ Id = "NotTheSameId"}
            };
            SetupCustomIndexCollection(customIndices);

            var response = sut.Get(id);

            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }

        [Test]
        public void TestGet_WithIndexId_ReturnsCustomIndex()
        {
            var id = "123456789009876543211234";
            var customIndex = new CustomIndex { Id = id };
            var customIndices = new List<CustomIndex> { customIndex };

            SetupCustomIndexCollection(customIndices);

            var response = sut.Get(id).Value;

            Assert.AreSame(customIndex, response);
        }

        private void SetupCustomIndexCollection(List<CustomIndex> customIndices, Dictionary<Guid, List<string>> userIndicesCollection = null)
        {
            customIndexService = new CustomIndexServiceFake(customIndices, userIndicesCollection);
            sut = new CustomIndexController(customIndexService);
        }
    }
}