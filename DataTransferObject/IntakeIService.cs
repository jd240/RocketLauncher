using ViewModels;

namespace DataTransferObject
{
    public interface IntakeIService
    {
        Task<IntakeFormVM> GetIntakeFormAsync(Guid userId);
        Task SubmitIntakeAsync(Guid userId, IntakeFormVM form);
    }
}
