using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Validators
{
    public class OrderDetailValidator : AbstractValidator<OrderDetailViewModel>
    {
        public OrderDetailValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .OnAnyFailure(x =>
                {
                    throw new Exception($"Parameter {nameof(x.ProductId)} is invalid.");
                });

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .OnAnyFailure(x =>
                {
                    throw new Exception($"Parameter {nameof(x.Quantity)} is invalid.");
                });
        }
    }
}
