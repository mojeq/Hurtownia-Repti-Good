using FluentValidation;
using HurtowniaReptiGood.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Validators
{
    public class AddProductValidator : AbstractValidator<NewProductViewModel>
    {
        public AddProductValidator()
        {
            RuleFor(x => x.Stock)
                .GreaterThan(-1)
                .OnAnyFailure(x =>
                {
                    throw new Exception($"Parameter {nameof(x.Stock)} is invalid.");
                });
        }
    }
}
