using FluentAssertions;
using Hqv.Seedwork.Test.Unit.Entities;
using Hqv.Seedwork.Utilities;
using Xunit;

namespace Hqv.Seedwork.Test.Unit
{
    public class TypeHelperTest
    {
        private readonly TypeHelper _typeHelper;
        private string _fields;
        private bool _doesTypeHaveProperties;

        public TypeHelperTest()
        {
            _typeHelper = new TypeHelper();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_HaveFields()
        {
            GivenAStringOfFields();
            WhenTypeHasPropertiesIsCalled();
            _doesTypeHaveProperties.Should().BeTrue();            
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_NotHaveFields()
        {
            GivenAStringOfFieldThatDoNotExist();
            WhenTypeHasPropertiesIsCalled();
            _doesTypeHaveProperties.Should().BeFalse();
        }

        private void GivenAStringOfFields()
        {
            _fields = "FirstName, Age";
        }

        private void GivenAStringOfFieldThatDoNotExist()
        {
            _fields = "MiddelName, Age";
        }

        private void WhenTypeHasPropertiesIsCalled()
        {
            _doesTypeHaveProperties = _typeHelper.TypeHasProperties<PersonEntity>(_fields);
        }
    }
}