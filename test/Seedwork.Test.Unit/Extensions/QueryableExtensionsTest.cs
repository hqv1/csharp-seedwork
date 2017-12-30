using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Hqv.Seedwork.Ordering;
using Hqv.Seedwork.Test.Unit.Entities;
using Hqv.Seedwork.Utilities;
using Xunit;

namespace Hqv.Seedwork.Test.Unit.Extensions
{
    public class QueryableExtensionsTest
    {
        private PersonEntity[] _people;
        private string _orderBy;
        private Dictionary<string, PropertyMappingValue> _propertyMappingValues;
        private IEnumerable<PersonEntity> _sortedPeople;

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_ApplySort_WithMultipleOrderBy_WithDesc()
        {
            GivenPeople();
            GivenOrderByStringWithMultipleOrderByWithDesc();
            GivenMappingDictionary();
            WhenPeopleIsSorted();

            _sortedPeople.ElementAt(0).FirstName.Should().Be("Allen");
            _sortedPeople.ElementAt(1).FirstName.Should().Be("John");
            _sortedPeople.ElementAt(2).FirstName.Should().Be("William");
            _sortedPeople.ElementAt(3).FirstName.Should().Be("Cindy");


            void GivenOrderByStringWithMultipleOrderByWithDesc()
            {
                _orderBy = "Age desc, Name";
            }

            void GivenMappingDictionary()
            {
                _propertyMappingValues = new Dictionary<string, PropertyMappingValue>
                {
                    {"Age", new PropertyMappingValue(new[] {"Age"})},
                    {"Name", new PropertyMappingValue(new[] {"FirstName", "LastName"})}
                };
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_ApplySort_WithReverse()
        {
            GivenPeople();
            GivenOrderByString();
            GivenMappingDictionary();
            WhenPeopleIsSorted();

            _sortedPeople.ElementAt(0).FirstName.Should().Be("Allen");
            _sortedPeople.ElementAt(1).FirstName.Should().Be("John");
            _sortedPeople.ElementAt(2).FirstName.Should().Be("William");
            _sortedPeople.ElementAt(3).FirstName.Should().Be("Cindy");

            void GivenOrderByString()
            {
                _orderBy = "DOB, FirstName";
            }

            void GivenMappingDictionary()
            {
                _propertyMappingValues = new Dictionary<string, PropertyMappingValue>
                {
                    {"DOB", new PropertyMappingValue(new[] {"Age"}, revert:true)},
                    {"FirstName", new PropertyMappingValue(new [] {"FirstName"}) }
                };
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_Throw_WhenOrderByStringHaveNoMapping()
        {
            GivenPeople();
            GivenOrderByString();
            GivenMappingDictionary();

            try
            {
                WhenPeopleIsSorted();
            }
            catch (Exception ex)
            {
                ex.Message.ToLower().Should().Contain("key mapping");
            }
                       
            void GivenOrderByString()
            {
                _orderBy = "East";
            }

            void GivenMappingDictionary()
            {
                _propertyMappingValues = new Dictionary<string, PropertyMappingValue>
                {                   
                    {"FirstName", new PropertyMappingValue(new [] {"FirstName"}) }
                };
            }
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_Throw_WhenMappingHasWrongDestination()
        {
            GivenPeople();
            GivenOrderByString();
            GivenMappingDictionary();

            try
            {
                WhenPeopleIsSorted();
            }
            catch (Exception ex)
            {
                ex.Message.ToLower().Should().Contain("no property or field");
            }

            void GivenOrderByString()
            {
                _orderBy = "FirstName";
            }

            void GivenMappingDictionary()
            {
                _propertyMappingValues = new Dictionary<string, PropertyMappingValue>
                {
                    {"FirstName", new PropertyMappingValue(new [] {"MiddleName"}) }
                };
            }
        }

        private void GivenPeople()
        {
            _people = new[]
            {
                new PersonEntity
                {
                    FirstName = "John",
                    LastName = "Hancock",
                    Age = 30
                },
                new PersonEntity
                {
                    FirstName = "Allen",
                    LastName = "Walker",
                    Age = 30
                },
                new PersonEntity
                {
                    FirstName = "Cindy",
                    LastName = "Bel",
                    Age = 20
                },
                new PersonEntity
                {
                    FirstName = "William",
                    LastName = "Fon",
                    Age = 30
                },
            };
        }
        
        private void WhenPeopleIsSorted()
        {
            _sortedPeople = _people.AsQueryable().ApplySort(_orderBy, _propertyMappingValues).ToList();
        }
    }
}