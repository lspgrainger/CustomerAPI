using Api.Customer.Domain;
using NUnit.Framework;

namespace Api.Customer.UnitTests
{
    internal class PasswordHashingTests
    {
        [Test]
        public void GivenCorrectPasswordReturnIsTrue()
        {
            //Arrange
            var correctPassword = "password1234";
            var hashedPassword = Hashing.HashPassword(correctPassword, Hashing.GetRandomSalt());

            //Act
            var enteredPassword = "password1234";
            var isValidPassword = Hashing.ValidatePassword(enteredPassword, hashedPassword);

            //Assert
            Assert.IsTrue(isValidPassword);
        }

        [Test]
        public void GivenIncorrectPasswordReturnIsFalse()
        {
            //Arrange
            var correctPass = "password1234";
            var hashedPassword = Hashing.HashPassword(correctPass, Hashing.GetRandomSalt());

            //Act
            var userEnteredPassword = "password9999";
            var isValidPassword = Hashing.ValidatePassword(userEnteredPassword, hashedPassword);

            //Assert
            Assert.IsFalse(isValidPassword);
        }

        [Test]
        [TestCase("password1111", "password1111", true)]
        [TestCase("password2222", "password2222", true)]
        [TestCase("password3333", "password3333", true)]
        [TestCase("!password£$%^", "!password£$%^", true)]
        [TestCase("qu1ckbr0wnf0x!", "qu1ckbr0wnf0x!", true)]
        [TestCase("verylongpasswordtest1234567890!!!", "verylongpasswordtest1234567890!!!", true)]
        [TestCase("password1111", "password11111", false)]
        [TestCase("password2222", "password222", false)]
        [TestCase("password3333", "password0003", false)]
        [TestCase("!password£$%^", "!password$$%^", false)]
        [TestCase("qu1ckbr0wnf0x!", "q u1ckbr0wnf0x!", false)]
        [TestCase("verylongpasswordtest1234567890!!!", "erylongpasswordtest1234567890!!!", false)]
        public void ValidatePassword(string correctPassword, string enteredPassword, bool expectedResult)
        {
            //Arrange
            var hashedPassword = Hashing.HashPassword(correctPassword, Hashing.GetRandomSalt());

            //Act
            var isValidPassword = Hashing.ValidatePassword(enteredPassword, hashedPassword);

            //Assert
            Assert.AreEqual(isValidPassword, expectedResult);
        }

        [Test]
        [TestCase("password1111")]
        [TestCase("password2222")]
        [TestCase("password3333")]
        [TestCase("!password£$%^")]
        [TestCase("qu1ckbr0wnf0x!")]
        [TestCase("verylongpasswordtest1234567890!!!")]
        [TestCase("password1111")]
        [TestCase("password2222")]
        [TestCase("password3333")]
        [TestCase("!password£$%^")]
        [TestCase("qu1ckbr0wnf0x!")]
        [TestCase("verylongpasswordtest1234567890!!!")]
        public void OneWayPasswordHashing(string password)
        {
            //Arrange
            var hashedPassword = Hashing.HashPassword(password, Hashing.GetRandomSalt());

            //Act
            var isValidPassword = Hashing.ValidatePassword(password, hashedPassword);

            //Assert
            Assert.IsTrue(isValidPassword);
        }

    }
}