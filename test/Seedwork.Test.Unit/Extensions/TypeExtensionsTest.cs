using System;
using System.Dynamic;
using System.Linq;
using FluentAssertions;
using Hqv.Seedwork.Test.Unit.Entities;
using Hqv.Seedwork.Utilities;
using Xunit;

namespace Hqv.Seedwork.Test.Unit.Extensions
{
    public class TypeExtensionsTest
    {
        private PersonEntity _person;
        private string _fieldsToShape;
        private ExpandoObject _shapePerson;

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_ShapeData()
        {
            GivenPeople();
            GivenASetOfFieldsToShape();
            WhenPeopleAreShaped();

            _shapePerson.First(x => x.Key == "FirstName").Value.Should().Be(_person.FirstName);
            _shapePerson.First(x => x.Key == "Age").Value.Should().Be(_person.Age);
            _shapePerson.Any(x => x.Key == "LastName").Should().BeFalse();
            _shapePerson.Any(x => x.Key == "HairColor").Should().BeFalse();

            void GivenASetOfFieldsToShape()
            {
                _fieldsToShape = "FirstName, Age";
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_ShapeData_WhenFieldsToShapeIsEmpty()
        {
            GivenPeople();
            GivenNoFieldsToShape();
            WhenPeopleAreShaped();
            
            _shapePerson.First(x => x.Key == "FirstName").Value.Should().Be(_person.FirstName);
            _shapePerson.First(x => x.Key == "LastName").Value.Should().Be(_person.LastName);
            _shapePerson.First(x => x.Key == "Age").Value.Should().Be(_person.Age);
            _shapePerson.First(x => x.Key == "HairColor").Value.Should().BeNull();

            void GivenNoFieldsToShape()
            {
                _fieldsToShape = null;
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_ThrowException_WhenFieldsToShapeDoesNotExist()
        {
            GivenPeople();
            GivenAFieldThatDoesNotExistInFieldsToShape();

            try
            {
                WhenPeopleAreShaped();
            }
            catch (Exception ex)
            {
                ex.Message.Should().Contain("DateOfBirth");
            }

            void GivenAFieldThatDoesNotExistInFieldsToShape()
            {
                _fieldsToShape = "FirstName,DateOfBirth";
            }
        }

        private void GivenPeople()
        {
            _person = PersonEntity.Create();
        }

        private void WhenPeopleAreShaped()
        {
            _shapePerson = _person.ShapeData(_fieldsToShape);
        }

    }
}