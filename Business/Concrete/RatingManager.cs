using System;
using System.Security.Claims;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Rating;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
	public class RatingManager: IRatingService
	{
        private IEmployeeRatingDal _employeeRatingDal;
        private IEmployerRatingDal _employerRatingDal;
        private IHttpContextAccessor _httpContextAccessor;

		public RatingManager(IEmployeeRatingDal employeeRatingDal, IEmployerRatingDal employerRatingDal)
		{
            _employeeRatingDal = employeeRatingDal;
            _employerRatingDal = employerRatingDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
		}

        public IResult AddEmployeeRating(EmployeeRatingToAddDto employeeRatingToAddDto)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            bool canRateResult = _employeeRatingDal.CanRate(employeeRatingToAddDto.EmployeeId, userId);
            if (canRateResult == false)
            {
                return new ErrorResult(Messages.CannotRateThisEmployee);
            }
            EmployeeRating employeeRating = new EmployeeRating
            {
                Comment = employeeRatingToAddDto.Comment,
                Date = DateTime.Now,
                EmployeeId = employeeRatingToAddDto.EmployeeId,
                Rating = (short)employeeRatingToAddDto.Rating,
                UserId = userId
            };
            _employeeRatingDal.Add(employeeRating);
            return new SuccessResult(Messages.RatingSaved);
        }

        public IResult AddEmployerRating(EmployerRatingToAddDto employerRatingToAddDto)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            bool canRateResult = _employerRatingDal.CanRate(employerRatingToAddDto.EmployerId, userId);
            if (canRateResult == false)
            {
                return new ErrorResult(Messages.CannotRateThisEmployer);
            }
            EmployerRating employerRating = new EmployerRating
            {
                Comment = employerRatingToAddDto.Comment,
                Date = DateTime.Now,
                EmployerId = employerRatingToAddDto.EmployerId,
                Rating = (short)employerRatingToAddDto.Rating,
                UserId = userId
            };
            _employerRatingDal.Add(employerRating);
            return new SuccessResult(Messages.RatingSaved);
        }

        public IResult DeleteEmployeeRating(int id)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var data = _employeeRatingDal.Get(x => x.Id == id);
            if (data == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            if (data.UserId != userId)
            {
                return new ErrorResult(Messages.AccessDenied);
            }
            _employeeRatingDal.Delete(data);
            return new SuccessResult(Messages.Deleted);
        }

        public IResult DeleteEmployerRating(int id)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var data = _employerRatingDal.Get(x => x.Id == id);
            if (data == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            if (data.UserId != userId)
            {
                return new ErrorResult(Messages.AccessDenied);
            }
            _employerRatingDal.Delete(data);
            return new SuccessResult(Messages.Deleted);
        }

        public IResult UpdateEmployeeRating(EmployeeRatingToUpdateDto employeeRatingToUpdateDto)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var data = _employeeRatingDal.Get(x => x.Id == employeeRatingToUpdateDto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            if (data.UserId != userId)
            {
                return new ErrorResult(Messages.AccessDenied);
            }
            data.Comment = employeeRatingToUpdateDto.Comment;
            data.Rating = (short)employeeRatingToUpdateDto.Rating;
            _employeeRatingDal.Update(data);
            return new SuccessResult(Messages.RatingSaved);
        }

        public IResult UpdateEmployerRating(EmployerRatingToUpdateDto employerRatingToUpdateDto)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var data = _employerRatingDal.Get(x => x.Id == employerRatingToUpdateDto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            if (data.UserId != userId)
            {
                return new ErrorResult(Messages.AccessDenied);
            }
            data.Comment = employerRatingToUpdateDto.Comment;
            data.Rating = (short)employerRatingToUpdateDto.Rating;
            _employerRatingDal.Update(data);
            return new SuccessResult(Messages.RatingSaved);
        }
    }
}

