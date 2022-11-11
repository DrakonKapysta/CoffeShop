using FluentValidation;
using СoffeShop.Models;

namespace СoffeShop.Services
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(0, 10).NotNull().WithMessage("Your name is too long!"); ;
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Age).InclusiveBetween(18, 60);
            RuleFor(x => x.Kg).NotNull().InclusiveBetween(40, 140).WithMessage("You shold go to the doctor...");
            RuleFor(x => x.FamilyStatus).NotNull().Must(FamilyStatus => FamilyStatus == "married" || FamilyStatus == "not married").WithMessage("Must be married or not married");
            RuleFor(x => x.Surname).Length(0, 10).NotNull().WithMessage("Your surname is too long!");
            RuleFor(x => x.Height).NotNull().WithMessage("Must not be null");
            RuleFor(x => x.Salary).NotNull().GreaterThan(3000).WithMessage("Well, you should find anouther job");
            RuleFor(x => x.Sex).NotNull().Must(sex => sex == "male" || sex=="female").WithMessage("Must be male or female");

        }
    }
}
