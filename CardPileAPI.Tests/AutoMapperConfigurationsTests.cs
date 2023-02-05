using AutoMapper;
using Xunit;

namespace CardPileAPI.Tests
{

    public class AutoMapperConfigurationsTests
    {

        #region - - - - - - Configuration Tests - - - - - -

        [Fact]
        public void AutoMapperConfigurations_AllConfigurationsAreValid_Success()
        {
            // Arrange
            var _MapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(AssemblyUtility.GetAssembly()));
            var _Mapper = _MapperConfig.CreateMapper();

            // Act

            // Assert
            _Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        #endregion Configuration Tests

    }

}
