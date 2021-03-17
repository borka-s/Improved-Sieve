using System;
using System.Collections.Generic;
using System.Linq;
using ImprovedSieve.Core.Extensions;
using ImprovedSieve.Core.Models;

namespace AutoFilter.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var list = new List<Person>
            {
                new Person { Name = "Alice", Age = 21, Child = new Child { Age = 10 } },
                new Person { Name = "Bob", Age = 12, Child = new Child { Age = 12 } },
                new Person { Name = "John", Age = 15, Child = new Child { Age = 25 } },
                new Person { Name = "Peter", Age = 65, Child = new Child { Age = 21 } },
                new Person { Name = "Meter", Age = 85, Child = null },
            };

            var sieveModel = new SieveModel
            {
                Filters = "Child!=null,Name!_='Pet'",
                Sorts = "-Child.LastName",
            };

            var query = list.AsQueryable().ApplyFilters(sieveModel);
            // var query = filter.CreateFilterExpression(list.AsQueryable(), "Child==null or (Child.Age<18 and (Age>=21 or Name=='Peter'))");
            // var query = filter.CreateFilterExpression(list.AsQueryable(), "(Child.Age<18 and Age>=21) or Name=='Peter'");

            query = query.ApplySortBy(sieveModel);

            foreach (var person in query.ToList())
            {
                Console.WriteLine(person.Name);
            }

            Console.WriteLine("------------");

            var order = list.OrderBy(x => x.Age).ThenByDescending(x => x.Name);

            foreach (var person in order)
            {
                Console.WriteLine(person.Name);
            }
        }
    }

    public class Child : Person { }

    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Child Child { get; set; }
    }
}