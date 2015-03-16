namespace FirmwareBurner.Validation
{
    internal class ValidationContextFactory : IValidationContextFactory
    {
        public IValidationContext GetValidationContext() { return new ValidationContext(); }
    }
}