using NUnit.Framework;
using BoziNETAplikace;
using System;
using System.Collections.Generic;
using Moq;

namespace TestProject1
{
    public class Tests
    {
        private BoziApp boziApp;
        private BankAccount ba;
        //private List<Job> jobs;
        [SetUp]
        public void Init()
        {
            boziApp = new BoziApp();
            ba = new BankAccount("Mr. Bryan Walton", 11.99);
            /*var jobMock = new Mock<Job>();
            jobMock.Setup(m => m.Salary).Returns(1000);
            jobs = new List<Job>()
            {
              jobMock.Object
            };
            ba.Jobs = jobs; */
        }
        [Test]
        [TestCase(1, 5, 5)]
        [TestCase(2, 5, 5)]
        [TestCase(5, 9, 8)]
        public void IsTriangle(int a, int b, int c)
        {
            //boziApp.isTriangle(1, 1, 5);
            Assert.IsTrue(boziApp.isTriangle(a, b, c));
        }

        [Test]
        [TestCase(0, 1, 10)]
        [TestCase(1, 2, 4)]
        [TestCase(3, 4, 8)]
        public void IsNotTriangle(int a, int b, int c)
        {
            //boziApp.isTriangle(1, 1, 5);
            Assert.IsFalse(boziApp.isTriangle(a, b, c));
        }
        [Test]
        [TestCase(12)]
        [TestCase(99)]
        [TestCase(48)]
        [TestCase(311580)]
        [TestCase(12.451)]
        [TestCase(415.140)]
        public void NotEnoughMoney(double amount)
        {
            //Assert.IsTrue(ba.Debit(amount));
            //Assert.Throws<ArgumentOutOfRangeException>(ba.Debit(amount));
            Assert.That(() => ba.Debit(amount), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-4)]
        [TestCase(-11)]
        [TestCase(-0.00001)]
        [TestCase(-0.5652361)]
        [TestCase(-12.451)]
        [TestCase(-415.140)]
        public void EnoughMoneyNegativeAmount(double amount)
        {
            //Assert.IsTrue(ba.Debit(amount));
            //Assert.Throws<ArgumentOutOfRangeException>(ba.Debit(amount));
            Assert.That(() => ba.Debit(amount), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(4)]
        [TestCase(11.99)]
        [TestCase(5)]
        [TestCase(3)]
        [TestCase(10.1455)]
        [TestCase(4.14012)]
        public void EnoughMoneyPositiveAmount(double amount)
        {
            double balance = ba.Balance;
            //Assert.IsTrue(ba.Debit(amount));
            //Assert.Throws<ArgumentOutOfRangeException>(ba.Debit(amount));
            ba.Debit(amount);
            Assert.That(balance > ba.Balance);
            // Assert.That(() => ba.Debit(amount), balance > ba.Balance);
        }

        [Test]
        [TestCase(4)]
        [TestCase(11.99)]
        [TestCase(5)]
        [TestCase(3)]
        [TestCase(10.1455)]
        [TestCase(4.14012)]
        public void EnoughMoneyPositiveAmountPrecise(double amount)
        {
            double initBalance = ba.Balance;
            double balance = initBalance - amount;
            //Assert.IsTrue(ba.Debit(amount));
            //Assert.Throws<ArgumentOutOfRangeException>(ba.Debit(amount));
            ba.Debit(amount);
            Assert.That(balance, Is.EqualTo(ba.Balance));
            // Assert.That(() => ba.Debit(amount), balance > ba.Balance);
        }

        [Test]
        public void PayCheckWithOneJob()
        {
            var amount = 1011.99;
            var mock = new Mock<IJobs>();
            mock.Setup(x => x.GetJobs()).Returns(new List<Job>(){ new Job ("Plumber", 1000)});
            ba.PayCheck(mock.Object);
            Assert.AreEqual(ba.Balance, amount);
        }
        [Test]
        public void PayCheckWithZeroJob()
        {
            var amount = 11.99;
            var mock = new Mock<IJobs>();
            mock.Setup(x => x.GetJobs()).Returns(new List<Job>(){});
            ba.PayCheck(mock.Object);
            Assert.AreEqual(ba.Balance, amount);
        }
        [Test]
        public void PayCheckWithNJob()
        {
            /*Job job1 = new Job("Plumber", 1000);
            Job job2 = new Job("Sheriff", 2700);
            Job job3 = new Job("Actor", 37000);
            Job job4 = new Job("Doctor", 4900);
            ba.Jobs.Add(job4);
            ba.Jobs.Add(job3);
            ba.Jobs.Add(job2);
            ba.Jobs.Add(job1);*/
            var amount = 45611.99;
            var mock = new Mock<IJobs>();
            mock.Setup(x => x.GetJobs()).Returns(new List<Job>(){ new Job ("Plumber", 1000),
                                                 new Job("Sheriff", 2700),
                                                 new Job("Actor", 37000),
                                                 new Job("Doctor", 4900)});
            ba.PayCheck(mock.Object);
            Assert.AreEqual(ba.Balance, amount);
        }
        [Test]
        public void PayCheckWithOneJobAndDebt()
        {
            ba.Debt = 732.15;
            var amount = 279.84;
            var mock = new Mock<IJobs>();
            mock.Setup(x => x.GetJobs()).Returns(new List<Job>() { new Job("Plumber", 1000) });
            ba.PayCheck(mock.Object);
            Assert.AreEqual(ba.Balance, amount);
        }
        [Test]
        public void PayCheckWithZeroJobAndDebt()
        {
            ba.Debt = 732.15;
            var amount = 11.99;
            var mock = new Mock<IJobs>();
            mock.Setup(x => x.GetJobs()).Returns(new List<Job>() { });
            //Assert.AreEqual(ba.Balance, amount);
            Assert.That(() => ba.PayCheck(mock.Object), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }
        [Test]
        public void PayCheckWithNJobAndDebt()
        {
            ba.Debt = 732.15;
            var amount = 44879.84;
            var mock = new Mock<IJobs>();
            mock.Setup(x => x.GetJobs()).Returns(new List<Job>(){ new Job ("Plumber", 1000),
                                                 new Job("Sheriff", 2700),
                                                 new Job("Actor", 37000),
                                                 new Job("Doctor", 4900)});
            ba.PayCheck(mock.Object);
            Assert.AreEqual(ba.Balance, amount);
        }

        [Test]
        [TestCase(12)]
        [TestCase(99)]
        [TestCase(48)]
        [TestCase(311580)]
        [TestCase(12.451)]
        [TestCase(415.140)]
        public void Credit_WithoutATMPositiveAmount(double amount)
        {
            double initBalance = ba.Balance;
            ba.Credit(amount, null);
            Assert.AreEqual(ba.Balance, initBalance + amount);
        }
        [Test]
        [TestCase(-12)]
        [TestCase(-99)]
        [TestCase(-48)]
        [TestCase(-311580)]
        [TestCase(-12.451)]
        [TestCase(-415.140)]
        public void Credit_WithoutATMNegativeAmount(double amount)
        {
            Assert.That(() => ba.Credit(amount, null), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Credit_WithATMPositiveAmount()
        {
            double amount = 100.2513;
            double initBalance = ba.Balance;
            ba.Credit(amount, new ATM("Equa Bank", 7.24));
            Assert.AreEqual(ba.Balance, initBalance + amount - 7.24);
        }
        [Test]
        public void Credit_WithATMPositiveAmountLow()
        {
            double amount = 6.2513;
            double initBalance = ba.Balance;
            Assert.That(() => ba.Credit(amount, new ATM("Equa Bank", 7.24)), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }
        [Test]
        public void Credit_WithATMNegativeAmount()
        {
            double amount = -100.2513;
            double initBalance = ba.Balance;
            Assert.That(() => ba.Credit(amount, new ATM("Equa Bank", 7.24)), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}