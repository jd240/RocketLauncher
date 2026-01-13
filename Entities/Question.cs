using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Question
    {
        public Guid QuestionId { get; set; }
        public Guid ModuleId { get; set; }
        public string? QuestionText { get; set; }
        public string? QuestionType { get; set; }
        public bool? isRequired { get; set; }
        public string? OptionsJSON { get; set; }

    }
}
