using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Validators
{
    public class EditOrderValidator : AbstractValidator<OrderViewModel>
    {
        public EditOrderValidator()
        {
            RuleFor(x => x.DateOrder)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new Exception($"Parameter {nameof(x.DateOrder)} can't be empty.");
                });

            RuleFor(x => x.ValueOrder)
                .GreaterThan(0)
                .OnAnyFailure(x =>
                {
                    throw new Exception($"Parameter {nameof(x.ValueOrder)} can't be 0.");
                });

            RuleFor(x => x.StateOrder)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new Exception($"Parameter {nameof(x.StateOrder)} can't be empty.");
                });

            RuleFor(x => x.StatusOrder)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new Exception($"Parameter {nameof(x.StatusOrder)} can't be empty.");
                });
        }
    }
}
