using System.Threading;
using CakeDemo.Riddler;
using NUnit.Framework;
using Shouldly;

using static System.FormattableString;

namespace CakeDemo.UnitTests
{
    [TestFixture]
    public class RiddlerTests
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
        public void WhatIsLove_ShouldNotHurt()
        {
            var riddler = new RiddleMe();
            var answer = "Baby, don't hurn me";

            var result = riddler.WhatIsLove(answer);

            result.ShouldBe("Don't hurt me, no more");
        }

        [Test]
        public void WhatIsLove_MightHurtALittle()
        {
            var riddler = new RiddleMe();
            var answer = "A mutual feeling";

            var result = riddler.WhatIsLove(answer);

            result.ShouldBe("You know nothing, Jon Snow");
        }

        [Test]
        public void TheUltimateAnswer_ShouldBeSimple()
        {
            var riddler = new RiddleMe();
            var answer = "42";

            var result = riddler.WhatIsTheAnswerToLifeUniverseAndEverything(answer);

            result.ShouldBe("Indeed");
        }

        [Test]
        public void TheUltimateAnswer_MightBeTooComplicated()
        {
            var riddler = new RiddleMe();
            var answer = "Pancakes?";

            var result = riddler.WhatIsTheAnswerToLifeUniverseAndEverything(answer);

            result.ShouldBe("It is too complicated");
        }

        [Test]
        public void Why_ShouldBeFun()
        {
            var riddler = new RiddleMe();
            var answer = "For fun";

            var result = riddler.Why(answer);

            result.ShouldBe("Yolo");
        }

        [Test]
        public void Why_ShouldBeCalculated()
        {
            var riddler = new RiddleMe();
            var answer = "For money";

            var result = riddler.Why(answer);

            result.ShouldBe("Get riches");
        }

        [Test]
        public void Why_MightBeUnanswered()
        {
            var riddler = new RiddleMe();
            var answer = "For everyone";

            var result = riddler.Why(answer);

            result.ShouldBe("Nope, guess again");
        }

    }
}
