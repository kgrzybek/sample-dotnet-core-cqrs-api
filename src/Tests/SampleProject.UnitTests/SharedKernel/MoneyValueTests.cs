using NUnit.Framework;
using SampleProject.Domain.SharedKernel;
using SampleProject.UnitTests.SeedWork;
using System.Collections.Generic;

namespace SampleProject.UnitTests.SharedKernel
{
    [TestFixture]
    public class MoneyValueTests : TestBase
    {
        [Test]
        public void MoneyValueOf_WhenCurrencyIsProvided_IsSuccessful()
        {
            MoneyValue value = MoneyValue.Of(120, "EUR");

            Assert.That(value.Value, Is.EqualTo(120));
            Assert.That(value.Currency, Is.EqualTo("EUR"));
        }

        [Test]
        public void MoneyValueOf_WhenCurrencyIsNotProvided_ThrowsMoneyValueMustHaveCurrencyRuleBroken()
        {
            AssertBrokenRule<MoneyValueMustHaveCurrencyRule>(() =>
            {
                MoneyValue.Of(120, "");
            });
        }

        [Test]
        public void GivenTwoMoneyValuesWithTheSameCurrencies_WhenAddThem_IsSuccessful()
        {
            MoneyValue valueInEuros = MoneyValue.Of(100, "EUR");
            MoneyValue valueInEuros2 = MoneyValue.Of(50, "EUR");

            MoneyValue add = valueInEuros + valueInEuros2;

            Assert.That(add.Value, Is.EqualTo(150));
            Assert.That(add.Currency, Is.EqualTo("EUR"));
        }

        [Test]
        public void GivenTwoMoneyValuesWithTheSameCurrencies_SumThem_IsSuccessful()
        {
            MoneyValue valueInEuros = MoneyValue.Of(100, "EUR");
            MoneyValue valueInEuros2 = MoneyValue.Of(50, "EUR");

            IList<MoneyValue> values = new List<MoneyValue>
            {
                valueInEuros, valueInEuros2
            };

            MoneyValue add = values.Sum();

            Assert.That(add.Value, Is.EqualTo(150));
            Assert.That(add.Currency, Is.EqualTo("EUR"));
        }

        [Test]
        public void GivenTwoMoneyValuesWithDifferentCurrencies_WhenAddThem_ThrowsMoneyValueOperationMustBePerformedOnTheSameCurrencyRule()
        {
            MoneyValue valueInEuros = MoneyValue.Of(100, "EUR");
            MoneyValue valueInDollars = MoneyValue.Of(50, "USD");
            AssertBrokenRule<MoneyValueOperationMustBePerformedOnTheSameCurrencyRule>(() =>
            {
                MoneyValue add = valueInEuros + valueInDollars;
            });
        }
    }
}