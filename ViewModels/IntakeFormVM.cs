namespace ViewModels
{
    public class IntakeFormVM
    {
        public Guid ModuleId { get; set; }
        public List<IntakeQuestionVM> Questions { get; set; } = new();
    }

}
