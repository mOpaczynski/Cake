using System.Threading;
using CakeDemo.Counter;
using CakeDemo.Riddler;
using NUnit.Framework;
using Shouldly;

using static System.FormattableString;

namespace CakeDemo.UnitTests
{
    [TestFixture]
    public class CounterTests
    {
        [SetUp]
        public void Setup()
        {
            Thread.Sleep(3000);
        }

        [TearDown]
        public void Teardown()
        {
            var result = TestContext.CurrentContext.Result.Outcome.Status;
            TestContext.WriteLine(Invariant($"Test result: {result}"));
            TestContext.WriteLine(string.Empty);
        }

        [Test]
        public void Add_ShouldWorkFine()
        {
            var counter = new Count();
            var x = 1;
            var y = 2;

            var result = counter.Add(x, y);

            result.ShouldBe(3);
        }

        [Test]
        public void Multiply_ShouldWorkFine()
        {
            var counter = new Count();
            var x = 1;
            var y = 2;

            var result = counter.Multiply(x, y);

            result.ShouldBe(2);
        }

        [Test]
        public void Substract_ShouldWorkFine()
        {
            var counter = new Count();
            var x = 1;
            var y = 2;

            var result = counter.Substract(x, y);

            result.ShouldBe(-1);
        }
    }
}
