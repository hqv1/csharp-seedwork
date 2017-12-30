using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Hqv.Seedwork.Test.Unit.Entities;
using Hqv.Seedwork.Utilities;
using Xunit;

namespace Hqv.Seedwork.Test.Unit.Extensions
{
    public class ObjectExtensionsTest
    {
        private PersonEntity _person;
        private IDictionary<string, object> _dictOfProperties;

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_ConvertObjectPropertiesToDictionary()
        {
            GivenAPerson();
            WhenAPersonPropertiesIsConvertedToDictionary();
            _dictOfProperties.First(x => x.Key == "FirstName").Value.Should().Be(_person.FirstName);
            _dictOfProperties.First(x => x.Key == "Age").Value.Should().Be(_person.Age);
        }

        private void GivenAPerson()
        {
            _person = PersonEntity.Create();
        }

        private void WhenAPersonPropertiesIsConvertedToDictionary()
        {
            _dictOfProperties = _person.AsDictionary();
        }
    }
}