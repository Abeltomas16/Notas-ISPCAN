using FluentValidation;
using IspcaNotas.Commom.Resources;
using IspcaNotas.Model;

namespace IspcaNotas.Commom.Validation
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(e => e.email)
                .EmailAddress().WithErrorCode(statusCode.EmailInvalid);

            RuleFor(s => s.Senha)
                .NotNull()
                .NotEmpty().WithErrorCode(statusCode.SenhaIsNullOrEmpty);
        }
    }
}
