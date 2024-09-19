using FluentValidation;

namespace ApiContract.Validations.CustomerOrder
{
    //Validation middleware handlerda gelen requesti karşılayıp eğer email hatalı ise direk olarak hata mesajı dönecektir.
    //Şuan hızlı olsun diye cqrs mantıgı ile bir handler oluşturmadım ama onun da örneğini product controller içine koyacağım.
    public class CreateOrderQueryRequestHandler : ValidatorBase<CreateOrder>
    {
        public override void IdentifyRules()
        {
            RuleFor(s => s.Email).NotEmpty().WithMessage("Email address is required")
                                 .EmailAddress().WithMessage("A valid email is required");
        }
    }

    public class CreateOrder
    {
        public string Email { get; set; }
    }
}
