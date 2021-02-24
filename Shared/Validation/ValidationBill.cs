using ERacuniNovi.Shared.Models;
using FluentValidation;

namespace ERacuniNovi.Shared.Validation
{
    public class ValidationBill : AbstractValidator<Bill>
    {
        public ValidationBill()
        {
            RuleFor(bill => bill.Barcode).NotNull().WithMessage("Ne moze da bude null").NotEmpty().WithMessage("Morate uneti barcode");
            RuleFor(bill => bill.Naziv).NotNull().WithMessage("Ne moze prazno").NotEmpty().WithMessage("Unesi naziv");
            RuleFor(bill => bill.Postarina).GreaterThanOrEqualTo(0).WithMessage("Postarina mora biti veca ili jednaka nuli");
            RuleFor(bill => bill.NabavnaCena).GreaterThanOrEqualTo(0).WithMessage("NabavnaCena mora biti veca ili jednaka nuli");
            RuleFor(bill => bill.ProdajnaCena).GreaterThanOrEqualTo(0).WithMessage("ProdajnaCena mora biti veca ili jednaka nuli");
            RuleFor(bill => bill.Rashod).GreaterThanOrEqualTo(0).WithMessage("Rashod mora biti veci ili jednak nuli");
            RuleFor(bill => bill.PresekStanja).GreaterThanOrEqualTo(0).WithMessage("PresekStanja mora biti veci ili jednak nuli");



        }
    }
}
