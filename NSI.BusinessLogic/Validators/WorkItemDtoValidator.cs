using System.Diagnostics.CodeAnalysis;
using NSI.BusinessLogic.Validators.Base;
using NSI.DataContracts.Models;
using FluentValidation;
using NSI.Resources.Messages;

namespace NSI.BusinessLogic.Validators
{
    [ExcludeFromCodeCoverage]
    public class WorkItemDtoValidator : NsiAbstractValidator<WorkItemDto>
    {
        public WorkItemDtoValidator()
        {
            RuleFor(x => x.Description)
                .Length(10, 500).WithMessage(string.Format(WorkItemExceptionMessages.DescriptionLengthExceeded, "10", "500"));
            
            RuleFor(x => x.BacklogPriority)
                .GreaterThan(0).WithMessage(WorkItemExceptionMessages.InvalidBacklogPriority);

            RuleFor(x => x.Priority)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(WorkItemExceptionMessages.PriorityNotProvided)
               .GreaterThan(0).When(x => x.Id != null).WithMessage(WorkItemExceptionMessages.InvalidPriority);
        }
    }
}
