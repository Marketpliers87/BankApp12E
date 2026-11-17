using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp12E
{
    [TestFixture]
    public class BankTest
    {
        Bank bank;

        [SetUp]
        public void SetUp()
        {
            bank = new Bank();
            bank.CreateAccount("1234");
            bank.CreateAccount("5678");
        }

        [Test]
        public void StartingBalanceIsZero()
        {
            Assert.That(bank.GetBalance("1234"), Is.Zero);
        }

        [Test]
        public void DepositOnce()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
        }

        [Test]
        public void DepositMultiple()
        {
            bank.Deposit("1234", 10000);
            bank.Deposit("1234", 15000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(25000));
        }

        [Test]
        public void DepositNegativeAmount()
        {
            Assert.That(() => bank.Deposit("1234", -10000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void DepositMultipleAccounts()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
            Assert.That(bank.GetBalance("5678"), Is.Zero);
        }

        [Test]
        public void GetBalanceOfInvalidAccount()
        {
            Assert.That(() => bank.GetBalance("9876"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void DepositToInvalidAccount()
        {
            Assert.That(() => bank.Deposit("9876", 5000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WithdrawNegativeAmount()
        {
            Assert.That(() => bank.Withdraw("1234", -500), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WithdrawInvalidAccount()
        {
            Assert.That(() => bank.Withdraw("4321", 500), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WithdrawTooMuch()
        {
            bank.Deposit("1234", 1000);
            bool result = bank.Withdraw("1234", 2000);

            Assert.That(result, Is.False);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(1000));
        }

        [Test]
        public void WithdrawSuccessful()
        {
            bank.Deposit("1234", 20000);
            bool result = bank.Withdraw("1234", 5000);

            Assert.That(result, Is.True);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(15000));
        }

        [Test]
        public void TransferSuccess()
        {
            bank.Deposit("1234", 10000);
            bool result = bank.Transfer("1234", "5678", 4000); 

            Assert.That(result, Is.True);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(6000));
            Assert.That(bank.GetBalance("5678"), Is.EqualTo(4000));
        }

        [Test]
        public void TransferInsufficientFunds()
        {
            Assert.That(bank.Transfer("1234", "5678", 5000), Is.False);
        }

        [Test]
        public void TransferFromInvalidAccount()
        {
            Assert.That(() => bank.Transfer("9876", "5678", 5000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TransferToInvalidAccount()
        {
            Assert.That(() => bank.Transfer("1234", "9876", 5000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TransferNegativeAmount()
        {
            Assert.That(() => bank.Transfer("1234", "5678", -5000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TransferZeroAmount()
        {
            Assert.That(() => bank.Transfer("1234", "5678", 0), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void CloseAccountSuccess()
        {

        }

        [Test]
        public void FundMoreThanZero()
        {

        }
    }
}
