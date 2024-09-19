using FluentValidation;

namespace ApiContract.Validations
{
    public abstract class ValidatorBase<TRequest> : AbstractValidator<TRequest>
    {
        public ValidatorBase()
        {
            IdentifyRules();
        }

        public abstract void IdentifyRules();
    }
}
