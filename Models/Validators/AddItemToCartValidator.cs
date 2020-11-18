using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Validators
{
    public class AddItemToCartValidator : AbstractValidator<ItemCartViewModel>
    {
        public AddItemToCartValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .OnAnyFailure(x =>
                {
                    throw new Exception($"Parameter {nameof(x.Quantity)} can't be 0.");
                });

            RuleFor(x => x.ProductId)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new Exception($"Parameter {nameof(x.Quantity)} can't be 0.");
                });
        }
    }
}
