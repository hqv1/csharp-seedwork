using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using FluentAssertions;
using Hqv.Seedwork.Test.Unit.Entities;
using Hqv.Seedwork.Utilities;
using Xunit;

namespace Hqv.Seedwork.Test.Unit.Extensions
{
    public class EnumerableExtensionsTest
    {
        private IEnumerable<PersonEntity> _people;
        private string _fieldsToShape;
        private IEnumerable<ExpandoObject> _shapePeople;

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_ShapeData()
        {
            GivenPeople();
            GivenASetOfFieldsToShape();
            WhenPeopleAreShaped();

            _shapePeople.Count().Should().Be(3);
            var shapePerson = _shapePeople.ElementAt(0);
            var person = _people.ElementAt(0);

            shapePerson.First(x => x.Key == "FirstName").Value.Should().Be(person.FirstName);
            shapePerson.First(x => x.Key == "Age").Value.Should().Be(person.Age);
            shapePerson.Any(x => x.Key == "LastName").Should().BeFalse();
            shapePerson.Any(x => x.Key == "HairColor").Should().BeFalse();

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

            _shapePeople.Count().Should().Be(3);
            var shapePerson = _shapePeople.ElementAt(0);
            var person = _people.ElementAt(0);

            shapePerson.First(x => x.Key == "FirstName").Value.Should().Be(person.FirstName);
            shapePerson.First(x => x.Key == "LastName").Value.Should().Be(person.LastName);
            shapePerson.First(x => x.Key == "Age").Value.Should().Be(person.Age);
            shapePerson.First(x => x.Key == "HairColor").Value.Should().BeNull();

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
            _people = new[]
            {
                PersonEntity.Create(),
                PersonEntity.Create(),
                PersonEntity.Create(),
            };
        }

        private void WhenPeopleAreShaped()
        {
            _shapePeople = _people.ShapeData(_fieldsToShape);
        }
        
    }
}