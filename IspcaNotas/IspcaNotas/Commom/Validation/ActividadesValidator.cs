using FluentValidation;
using IspcaNotas.Commom.Resources;
using IspcaNotas.Model;

namespace IspcaNotas.Commom.Validation
{
    public class ActividadesValidator : AbstractValidator<ActividadeDTO>
    {
        public ActividadesValidator()
        {
            RuleFor(e => e.Descricao)
            .NotNull()
            .NotEmpty().WithErrorCode(statusCode.DescricaoIsNotNullOrEmpty)
            .Must((desc) =>
            {
                bool convertido = int.TryParse(desc, out int result) ? false : true;
                return convertido;
            }).WithErrorCode(statusCode.NaoPodeSerApenasNumero);

            RuleFor(z => z.IDActividade)
                 .NotNull()
                .NotEmpty().WithErrorCode(statusCode.IdDeveSerInformado);
        }
    }
}
