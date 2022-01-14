using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace BookStoreApp.BookOperations.UpdateBooks
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(c => c.Model.GenreId).GreaterThan(0);
            RuleFor(c => c.Model.PageCount).GreaterThan(0);
            RuleFor(c => c.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(c => c.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}
