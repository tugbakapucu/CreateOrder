using ApiContract.Request.Command;
using FluentValidation;

namespace ApiContract.Validations.Product
{
    public class ProductCommandRequestValidator : ValidatorBase<ProductCommandRequest>
    {
        public override void IdentifyRules()
        {
            var value = RuleFor(s => s.Barcode).NotEmpty().WithMessage("Barcoe boş olamaz");
        }
    }
}
