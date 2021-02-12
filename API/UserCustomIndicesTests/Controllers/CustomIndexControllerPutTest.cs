using Database.Model.User;
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
    class CustomIndexControllerPutTest
    {
        private ICustomIndexService customIndexService;
        private CustomIndexController sut;

        private Mock<ICustomIndexValidator> CustomIndexValidatorMock;
        private Guid clientId = Guid.NewGuid();
        private string indexId = "345674679867564532426375";

        [SetUp]
        public void SetUp()
        {
            CustomIndexValidatorMock = new Mock<ICustomIndexValidator>();
        }

        [Test]
        public void TestPut_ValidCustomIndex_ReturnsOkResult()
        {
            var index = new CustomIndex()
            {
                Id = indexId,
                SectorAndIndsutry = new Sectors()
                {
                    SectorGroups = new Sector[]
                        {
                            new Sector()
                            {
                                Name = "Example Industry"
                            }
                        }
                }
            };

            var userIndices = new Dictionary<Guid, List<string>>();
            userIndices.Add(clientId, new List<string> { indexId});

            customIndexService = new CustomIndexServiceFake(new List<CustomIndex> { index }, userIndices);
            CustomIndexValidatorMock.Setup(x => x.Validate(index)).Returns(true);

            SetupController(customIndexService, CustomIndexValidatorMock.Object);
            
            index.SectorAndIndsutry.SectorGroups[0].Name = "New example industry";
            var response = sut.Update(clientId, index);
           
            Assert.IsInstanceOf<OkObjectResult>(response);

            var customIndex = ((OkObjectResult)response).Value as CustomIndex;
            Assert.AreEqual("New example industry", customIndex.SectorAndIndsutry.SectorGroups[0].Name);
        }

        [Test]
        public void TestPut_InvalidCustomIndex_ReturnsBadRequestResult()
        {
            var index = new CustomIndex();
            CustomIndexValidatorMock.Setup(x => x.Validate(index)).Returns(false);

            SetupController(customIndexService, CustomIndexValidatorMock.Object);

            var response = sut.Update(clientId, index);

            Assert.IsInstanceOf<BadRequestResult>(response);
        }

        [Test]
        public void TestPut_CustomIndexDoesNotExist_ReturnsNotFoundResult()
        {
            var index = new CustomIndex();
            CustomIndexValidatorMock.Setup(x => x.Validate(index)).Returns(true);
            customIndexService = new CustomIndexServiceFake(new List<CustomIndex>(), new Dictionary<Guid, List<string>>());
            SetupController(customIndexService, CustomIndexValidatorMock.Object);

            var response = sut.Update(clientId, index);

            Assert.IsInstanceOf<NotFoundResult>(response);
        }

        private void SetupController(ICustomIndexService service, ICustomIndexValidator validator)
        {
            sut = new CustomIndexController(service, validator);
        }
    }
}
