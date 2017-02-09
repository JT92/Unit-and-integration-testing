using System;
using NUnit.Framework;
using TestDemo.Calculator;
using System.IO;
using System.Linq;

namespace TestDemo.IntegrationTests
{
    public class StringCalculator_IntegrationTests
    {
        private string _filePath = @"IntegrationTest.txt";

        [OneTimeSetUp]
        public void Setup()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Add_ResultIsPrime_CreatesFile()
        {
            FileStore store = new FileStore(_filePath);
            StringCalculator calc = new StringCalculator(store);
            var result = calc.Add("3,4");
            Assert.IsTrue(File.Exists(_filePath));
        }

        [Test]
        public void Add_NumberIsPrime_WriteCorrectResultToFile()
        {
            FileStore store = new FileStore(_filePath);
            StringCalculator calc = new StringCalculator(store);
            calc.Add("3,4");

            var expectedResult = "7";
            var storedResult = File.ReadLines(_filePath).FirstOrDefault();
            Assert.AreEqual(expectedResult, storedResult);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }

    public class FileStore : IStore
    {
        private readonly string _filePath;
        public int Result { get; set; }

        public FileStore(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(int result)
        {
            using (var writer = File.CreateText(_filePath))
            {
                writer.Write(result);
            }
        }
    }

}
