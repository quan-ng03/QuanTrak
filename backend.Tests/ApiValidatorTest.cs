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
    }
}
