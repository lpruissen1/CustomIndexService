using NUnit.Framework;

namespace Users.Tests
{
	[TestFixture]
    public class PasswordHasherTest
    {
        [Test]
        public void Hash_ReturnsHasedPassword()
        {
			var sut = new BCryptHasher();

			var password = "P@ssword69!";
			var hash = sut.Hash(password);

            Assert.AreEqual(60, hash.Length);
        }

        [Test]
        public void Check_WithCorrectPasswords_ReturnsTrue()
        {
			var sut = new BCryptHasher();

			var password = "P@ssword69!";
			var hash = sut.Hash(password);

            Assert.True(sut.Check(password, hash));
        }

        [Test]
        public void Check_WithIncorrectPasswords_ReturnsFalse()
        {
			var sut = new BCryptHasher();

			var password = "P@ssword69!";
			var hash = sut.Hash(password);

            Assert.False(sut.Check("YourMommy", hash));
        }
    }
}
