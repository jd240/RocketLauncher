using DataTransferObject;
using DataTransferObject.DTO;
using DataTransferObject.Enums;
using Entities;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class QuestionService : QuestionIService
    {
        private readonly List<Question> _questions;
        
        public QuestionResponse AddQuestion(QuestionAddRequest? request)
        {
            //if (request == null)
            //{
            //    throw new ArgumentNullException(nameof(request), "QuestionAddRequest cannot be null.");
            //}
            //ValidationHelper.ModelValidation(request);
            //Question question = request.toQuestion();
            //question.QuestionId = Guid.NewGuid();
            //_questions.Add(question);
            //return convertQuestionIntoQuestionResponse(question);
            throw new NotImplementedException();
        }
        private QuestionResponse convertQuestionIntoQuestionResponse(Question question)
        {
            //QuestionResponse questionResponse = question.ToQuestionResponse();
            //return questionResponse;
            throw new NotImplementedException();
        }

        public bool DeleteQuestion(Guid? QuestionId)
        {
            if (QuestionId == null)
            {
                throw new ArgumentNullException(nameof(QuestionId));
            }

            Question? question = _questions.FirstOrDefault(temp => temp.QuestionId == QuestionId);
            if (question == null)
                return false;

            _questions.RemoveAll(temp => temp.QuestionId == QuestionId);

            return true;
        }

        public QuestionResponse? GetQuestionByID(Guid? QuestionId)
        {
            //if (QuestionId == null)
            //    return null;

            //var question_response_from_list = _questions.FirstOrDefault(temp => temp.QuestionId == QuestionId);

            //if (question_response_from_list == null)
            //    return null;
            //return question_response_from_list.ToQuestionResponse();
            throw new NotImplementedException();
        }

        public List<QuestionResponse> ListAllQuestion()
        {
            //return _questions.Select(temp => temp.ToQuestionResponse()).ToList();
            throw new NotImplementedException();
        }

        public List<QuestionResponse> SearchQuestionBy(string searchBy, string? SearchString)
        {
            List<QuestionResponse> result = ListAllQuestion();
            List<QuestionResponse> MatchedResult = result;
            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(SearchString))
                return MatchedResult;
            switch (searchBy)
            {
                case nameof(QuestionResponse.QuestionText):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.QuestionText) ?
                    temp.QuestionText.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(QuestionResponse.QuestionType):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.QuestionType) ?
                    temp.QuestionType.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;

                default:
                    MatchedResult = result;
                    break;
            }
            return MatchedResult;
        }

        public QuestionResponse UpdateQuestion(QuestionUpdateRequest? request)
        {
            //if (request == null)
            //    throw new ArgumentNullException(nameof(request));

            //// Validation
            //ValidationHelper.ModelValidation(request);
            //Question? matchingQuestion= _questions.FirstOrDefault(temp => temp.QuestionId == request.QuestionId);

            //if (matchingQuestion == null)
            //{
            //    throw new ArgumentException("Given id doesn't exist");
            //}

            //// Update question details
            //if (!string.IsNullOrWhiteSpace(request.QuestionText))
            //    matchingQuestion.QuestionText = request.QuestionText;
            //if (!string.IsNullOrWhiteSpace(request.OptionsJSON))
            //    matchingQuestion.OptionsJSON = request.OptionsJSON;
            //if (request.isRequired.HasValue)
            //    matchingQuestion.isRequired = request.isRequired;
            //if (request.QuestionType.HasValue)
            //    matchingQuestion.QuestionType = request.QuestionType.ToString();
            //return matchingQuestion.ToQuestionResponse();
            throw new NotImplementedException();
        }
    }
}
