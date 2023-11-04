using System;
using System.Security.Claims;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Job;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
	public class FavoriteManager: IFavoriteService
	{
        private IFavoriteDal _favoriteDal;
        private IHttpContextAccessor _httpContextAccessor;
        private IJobService _jobService;

		public FavoriteManager(IFavoriteDal favoriteDal, IJobService jobService)
		{
            _favoriteDal = favoriteDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _jobService = jobService;
		}

        public IResult Add(int jobId)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            Favorite favorite = new Favorite
            {
                AddedDate = DateTime.Now,
                EmployeeId = userId,
                JobId = jobId
            };
            _favoriteDal.Add(favorite);
            return new SuccessResult(Messages.Saved);
        }

        public IResult Delete(int id)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var data = _favoriteDal.Get(x => x.EmployeeId == userId);
            if (data == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            if (data.EmployeeId != userId)
            {
                return new ErrorResult(Messages.AccessDenied);
            }
            _favoriteDal.Delete(data);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<JobDto>> GetFavorites()
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var favoriteIds = _favoriteDal.GetAll(x => x.EmployeeId == userId).Select(x => x.JobId).ToList();
            var data = _jobService.GetByIds(favoriteIds);
            return new SuccessDataResult<List<JobDto>>(data);
        }
    }
}

