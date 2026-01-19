using DataTransferObject.Enums;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
namespace DataTransferObject.DTO
{
    public class QuestionAddRequest
    {
        [Required (ErrorMessage = "Question Must Not Be Blanked!")]
        public string? QuestionText { get; set; }
        [Required(ErrorMessage = "Question Type Not Be Blanked!")]
        public TypeOfQuestion QuestionType { get; set; }
        public bool isRequired { get; set; }
        public string? OptionsJSON { get; set; }
        public Guid ModuleId { get; set; }
       // public Question toQuestion()
        //{
          //  return new Question() { QuestionText = QuestionText, QuestionType = QuestionType.ToString(), isRequired= isRequired, OptionsJSON = OptionsJSON, ModuleId = ModuleId};
        //}
    }
}