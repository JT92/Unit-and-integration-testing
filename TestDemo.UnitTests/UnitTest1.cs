using System;
using NUnit.Framework;
using Moq;
using TestDemo.Calculator;

namespace TestDemo.UnitTests
{
    public class StringCalculator_UnitTests
    {

        private Mock<IStore> _mockStore;

        private StringCalculator GetCalculator()
        {
            _mockStore = new Mock<IStore>();
            var calc = new StringCalculator(_mockStore.Object);
            return calc;
        }

        [Test]
        public void Add_EmptyString_Returns_0()
        {
            StringCalculator calc = GetCalculator();
            int expectedResult = 0;
            int result = calc.Add("");
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("1", 1)]
        [TestCase("4", 4)]
        [TestCase("31", 31)]
        public void Add_SingleNumbers_ReturnsTheNumber(string input, int expectedResult)
        {
            StringCalculator calc = GetCalculator();
            int result = calc.Add(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("2,3", 5)]
        public void Add_MultipleNumbers_SumOfAllNumbers(string input, int expectedResult)
        {
            StringCalculator calc = GetCalculator();
            int result = calc.Add(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("a,1")]
        [TestCase("abc, ''")]
        public void Add_InvalidString_ThrowsException(string input)
        {
            StringCalculator calc = GetCalculator();
            Assert.Throws<ArgumentException>(() => calc.Add(input));
        }

        [TestCase("-1", -1)]
        [TestCase("-12, 4", -8)]
        public void MinusNumbers_Scenario_AreSummedCorrectly(string input, int expectedResult)
        {
            StringCalculator calc = GetCalculator();
            var result = calc.Add(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("2")]             // 2
        [TestCase("5,6")]           // 11
        [TestCase("10, 10, 3")]     // 23
        public void Add_ResultIsAPrimeNumber_ResultsAreSaved(string input)
        {
            StringCalculator calc = GetCalculator();
            var result = calc.Add(input);
            _mockStore.Verify(m => m.Save(It.IsAny<int>()), Times.Once);
        }

        [TestCase("4")]             // 4
        [TestCase("5,5")]           // 10
        [TestCase("-5,30")]         // 25
        public void Add_ResultIs_NOT_APrimeNumber_InputsAndResultAre_NOT_Saved(string input)
        {
            StringCalculator calc = GetCalculator();
            var result = calc.Add(input);
            _mockStore.Verify(m => m.Save(It.IsAny<int>()), Times.Never);
        }

    }




}
