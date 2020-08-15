using NUnit.Framework;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleProject.UnitTests.SeedWork
{
    public abstract class TestBase
    {
        public static T AssertPublishedDomainEvent<T>(Entity aggregate) where T : IDomainEvent
        {
            T domainEvent = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().SingleOrDefault();

            if (domainEvent == null)
            {
                throw new Exception($"{typeof(T).Name} event not published");
            }

            return domainEvent;
        }

        public static List<T> AssertPublishedDomainEvents<T>(Entity aggregate) where T : IDomainEvent
        {
            List<T> domainEvents = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().ToList();

            if (!domainEvents.Any())
            {
                throw new Exception($"{typeof(T).Name} event not published");
            }

            return domainEvents;
        }

        public static void AssertBrokenRule<TRule>(TestDelegate testDelegate) where TRule : class, IBusinessRule
        {
            string message = $"Expected {typeof(TRule).Name} broken rule";
            BusinessRuleValidationException businessRuleValidationException = Assert.Catch<BusinessRuleValidationException>(testDelegate, message);
            if (businessRuleValidationException != null)
            {
                Assert.That(businessRuleValidationException.BrokenRule, Is.TypeOf<TRule>(), message);
            }
        }

        [TearDown]
        public void AfterEachTest()
        {
            SystemClock.Reset();
        }
    }
}