namespace FirmwareBurner.Validation
{
    public interface IValidationContextFactory
    {
        IValidationContext GetValidationContext();
    }

    class ValidationContextFactory : IValidationContextFactory {
        public IValidationContext GetValidationContext() { return new ValidationContext(); }
    }
}
