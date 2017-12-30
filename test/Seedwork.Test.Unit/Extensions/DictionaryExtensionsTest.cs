using System;
using System.Collections.Generic;
using FluentAssertions;
using Hqv.Seedwork.Test.Unit.Entities;
using Hqv.Seedwork.Utilities;
using Xunit;

namespace Hqv.Seedwork.Test.Unit.Extensions
{
    public class DictionaryExtensionsTest
    {       
        private Dictionary<string, object> _dictionary;
        private PersonEntity _person;

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_ConvertToObject()
        {
            GivenAValidDictionary();
            WhenToObjectIsCalled();

            // Then
            _person.FirstName.Should().Be(Convert.ToString(_dictionary["FirstName"]));
            _person.LastName.Should().Be(Convert.ToString(_dictionary["LastName"]));
            _person.Age.Should().Be(Convert.ToInt32(_dictionary["Age"]));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_Throw_IfClass_DoesNotContainDictionaryKey()
        {
            try
            {
                GivenADictionaryWithInvalidKey();
                WhenToObjectIsCalled();
            }
            catch (Exception ex)
            {
                ex.Message.Should().Contain("MiddleName");
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_Throw_IfClassPropertyTypeIsWrong()
        {
            try
            {
                GivenADictionaryWithInvalidValueProperty();
                WhenToObjectIsCalled();
            }
            catch (Exception ex)
            {
                ex.Data.Should().NotBeNull();
                ex.Data["DictionaryKey"].Should().NotBeNull();
                ex.Data["DictionaryKey"].ToString().Should().Contain("Age");
            }
        }

        private void GivenADictionaryWithInvalidKey()
        {
            _dictionary = new Dictionary<string, object>
            {
                {"FirstName", "John"},
                {"MiddleName", "Wilkes" },
                {"Age", 45 }
            };
        }

        private void GivenADictionaryWithInvalidValueProperty()
        {
            _dictionary = new Dictionary<string, object>
            {
                {"FirstName", "John"},
                {"LastName", "Wilkes" },
                {"Age", 45.99 }
            };
        }

        private void GivenAValidDictionary()
        {
            _dictionary = new Dictionary<string, object>
            {
                {"FirstName", "John"},
                {"LastName", "Wilkes" },
                {"Age", 45 }
            };
        }

        private void WhenToObjectIsCalled()
        {
            _person = _dictionary.ToObject<PersonEntity>();
        }      
    }
}
