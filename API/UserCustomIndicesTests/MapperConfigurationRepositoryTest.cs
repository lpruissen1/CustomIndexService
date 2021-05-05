using NUnit.Framework;
using UserCustomIndices.Mappers;

namespace UserCustomIndicesTests
{
    [TestFixture]
    public class MapperConfigurationRepositoryTest
    {
        [Test]
        public void ValidateConfiguration() 
        {
            var config = MapperConfigurationRepository.Create();

            config.AssertConfigurationIsValid();
        }

    }
}
