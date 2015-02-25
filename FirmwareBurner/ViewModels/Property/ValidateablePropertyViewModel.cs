using FirmwareBurner.Validation;

namespace FirmwareBurner.ViewModels.Property
{
    public class ValidateablePropertyViewModel<TValue> : ValidateableViewModelBase<TValue>
    {
        private TValue _value;

        public ValidateablePropertyViewModel(params IValidationRule<TValue>[] ValidationRules) : base(ValidationRules) { }

        public ValidateablePropertyViewModel(TValue Value, params IValidationRule<TValue>[] ValidationRules) : this(ValidationRules) { this.Value = Value; }

        public TValue Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged(() => Value);
                OnValidateableValueChanged();
            }
        }

        protected override TValue ValidateableValue
        {
            get { return Value; }
        }
    }
}
