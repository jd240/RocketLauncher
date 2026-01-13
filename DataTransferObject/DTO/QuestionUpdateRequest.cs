using DataTransferObject.Enums;
using Entities;
using System.ComponentModel.DataAnnotations;


namespace DataTransferObject.DTO
{
    [RequireAtLeastOneAdditionalField]
    public class QuestionUpdateRequest
    {
        [Required (ErrorMessage = "Please Supply the Question ID you wish to update")]
        public Guid QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public TypeOfQuestion? QuestionType { get; set; }
        public bool? isRequired { get; set; }
        public string? OptionsJSON { get; set; }
        public Guid? ModuleId { get; set; }
        public Question toQuestion()
        {
            return new Question()
            {
                QuestionId = QuestionId,
                QuestionText = QuestionText,
                QuestionType = QuestionType.ToString(),
                isRequired = isRequired,
                OptionsJSON = OptionsJSON,
                ModuleId = (Guid)ModuleId
            };
        }
    }
}
