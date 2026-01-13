using System;
using DataTransferObject.DTO;
using DataTransferObject.Enums;
namespace DataTransferObject
{
    public interface QuestionIService
    {
        QuestionResponse AddQuestion (QuestionAddRequest? request);
        QuestionResponse UpdateQuestion (QuestionUpdateRequest? request);
        bool DeleteQuestion (Guid? QuestionId);
        List<QuestionResponse> ListAllQuestion ();
        QuestionResponse? GetQuestionByID(Guid? QuestionId);
        List<QuestionResponse> SearchQuestionBy (string searchBy, string? SearchString);
    }
}
