using backend.Services;
using Xunit;

namespace backend.Tests
{
    public class ApiValidatorTests
    {
        private readonly string _validApiKey = "secretKey";
        private readonly ApiValidator _validator;

        public ApiValidatorTests()
        {
            _validator = new ApiValidator(_validApiKey);
        }

        [Fact]
        public void Validate_WithValidKey_ReturnsTrue()
        {
            // Act
            bool result = _validator.Validate("secretKey");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WithInvalidKey_ReturnsFalse()
        {
            // Act
            bool result = _validator.Validate("wrongKey");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WithNullKey_ReturnsFalse()
        {
            // Act
            bool result = _validator.Validate(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WithEmptyString_ReturnsFalse()
        {
            // Act
            bool result = _validator.Validate("");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WithExtraWhitespace_ReturnsFalse()
        {
            // Act
            bool result = _validator.Validate(" secretKey "); // Leading/trailing space

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WithDifferentCase_ReturnsFalse()
        {
            // Act
            bool result = _validator.Validate("Secretkey"); // Case differs

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WithPartialKey_ReturnsFalse()
        {
            // Act
            bool result = _validator.Validate("secret"); // Partial of the secret key

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WithSpecialCharacters_ReturnsFalse()
        {
            // Act
            bool result = _validator.Validate("secretKey$");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WithNewlineCharacter_ReturnsFalse()
        {
            // Act
            bool result = _validator.Validate("secretKey\n");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WithLongApiKey_ReturnsFalse()
        {
            // Act
            bool result = _validator.Validate("secretKey" + new string('a', 100));

            // Assert
            Assert.False(result);
        }


    }
}
