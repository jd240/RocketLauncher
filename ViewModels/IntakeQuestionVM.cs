namespace ViewModels
{
    public class IntakeQuestionVM
    {
        public Guid QuestionId { get; set; }
        public string Key { get; set; } = null!;
        public string Prompt { get; set; } = null!;
        public string QuestionType { get; set; } = null!;
        public bool IsRequired { get; set; }

        public List<IntakeOptionVM> Options { get; set; } = new();

        // SingleSelect/YesNo/Text/etc.
        public string? Answer { get; set; }

        // MultiSelect
        public List<string> Answers { get; set; } = new();
    }
}
