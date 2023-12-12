using System;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Interest;

namespace Business.Concrete
{
    public class InterestManager : IInterestService
    {
        private IInterestDal _interestDal;
        private IJobTagService _jobTagService;

        public InterestManager(IInterestDal interestDal, IJobTagService jobTagService)
        {
            _interestDal=interestDal;
            _jobTagService=jobTagService;
        }

        public IResult Add(Interest interest)
        {
            _interestDal.Add(interest);
            return new SuccessResult(Messages.Saved);
        }

        public IResult Delete(int id)
        {
            var interest = _interestDal.Get(i => i.Id == id);
            if (interest == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            _interestDal.Delete(interest);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<InterestDto>> GetByEmployeeId(int employeeId)
        {
            var interests = _interestDal.GetAll(i => i.EmployeeId == employeeId);
            var tags = _jobTagService.GetJobTagsByIds(interests.Select(i => i.TagId).ToList());
            var data = interests.Select(i => new InterestDto
            {
                Id = i.Id,
                Name = tags.First(t => t.Id == i.TagId).DisplayName,
            }).ToList();
            return new SuccessDataResult<List<InterestDto>>(data);
        }
    }
}

