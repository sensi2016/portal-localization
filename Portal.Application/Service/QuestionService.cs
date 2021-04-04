using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.Context;
using Portal.DTO;
using Portal.DTO.Covid;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Service
{
    public class QuestionService : IQuestionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Question> _questionRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public QuestionService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _questionRepository = _unitOfWork.Set<Question>();
        }

        public async Task<BaseResponseDto> GetQuestionAndAnswer()
        {
            var lst =await _questionRepository
                .OrderBy(d => d.Arrange)
                .Select(g => new ResponseQuestion
                  {
                   Id= g.Id,
                   Code= g.Code,
                   Title = g.Title,
                   Arrange=g.Arrange,
                   Answers = g.Answer.Select(t => new 
                   {
                       Id = t.Id,
                       Code = t.Code,
                       Title = t.Title,
                       Arrange = t.Arrange
                   }).ToList()

                }).ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lst
            };
        }
    }
}
