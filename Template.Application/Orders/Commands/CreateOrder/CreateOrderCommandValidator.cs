using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator
        : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("نام مشتری الزامی است")
                .MinimumLength(3).WithMessage("حداقل ۳ کاراکتر");
        }
    }
}
