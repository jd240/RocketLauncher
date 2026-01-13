using DataTransferObject.Enums;
using Entities;
using System;
using System.Reflection;
using System.Xml.Linq;


namespace DataTransferObject.DTO
{
    public class QuestionResponse
    {
        public Guid QuestionId { get; set; }
        public Guid ModuleId { get; set; }
        public string? QuestionText { get; set; }
        public string? QuestionType { get; set; }
        public bool? isRequired { get; set; }
        public string? OptionsJSON { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(QuestionResponse)) return false;

            QuestionResponse question = (QuestionResponse)obj;
            return QuestionId == question.QuestionId && ModuleId == question.ModuleId && QuestionText == question.QuestionText && QuestionType == question.QuestionType
                && isRequired == question.isRequired && OptionsJSON == question.OptionsJSON;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class QuestionExtensions
    {
        public static QuestionResponse ToQuestionResponse(this Question question)
        {
            return new QuestionResponse()
            {
                QuestionId = question.QuestionId,
                ModuleId = question.ModuleId,
                QuestionText = question.QuestionText,
                QuestionType = question.QuestionType,
                isRequired = question.isRequired,
                OptionsJSON = question.OptionsJSON
            };
        }
    }
}
