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
            RuleFor(x => x.OrderId)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new Exception($"Parameter {nameof(x.OrderId)} can't be empty.");
                });
        }
    }
}
