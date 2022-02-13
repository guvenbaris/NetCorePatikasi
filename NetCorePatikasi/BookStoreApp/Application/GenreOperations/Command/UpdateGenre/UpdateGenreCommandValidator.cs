using FluentValidation;

namespace BookStoreApp.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(x=> x.GenreId).GreaterThan(0);
        }

    }
}
