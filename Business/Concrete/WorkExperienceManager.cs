using System;
using System.Security.Claims;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.WorkExperience;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class WorkExperienceManager : IWorkExperienceService
    {
        private IWorkExperienceDal _workExperienceDal;
        private IHttpContextAccessor _httpContextAccessor;

        public WorkExperienceManager(IWorkExperienceDal workExperienceDal)
        {
            _workExperienceDal = workExperienceDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public IResult Add(WorkExperienceToAddDto workExperienceToAddDto)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            WorkExperience workExperience = new WorkExperience
            {
                Description = workExperienceToAddDto.Description,
                Title = workExperienceToAddDto.Title,
                EmployeeId = userId
            };
            _workExperienceDal.Add(workExperience);
            return new SuccessResult(Messages.Saved);
        }

        public IResult Delete(int id)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var data = _workExperienceDal.Get(x => x.EmployeeId == userId);
            if (data == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            if (data.EmployeeId != userId)
            {
                return new ErrorResult(Messages.AccessDenied);
            }
            _workExperienceDal.Delete(data);
            return new SuccessResult(Messages.Deleted);
        }
    }
}

