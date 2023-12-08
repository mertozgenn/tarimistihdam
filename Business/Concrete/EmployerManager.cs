using System;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.User;

namespace Business.Concrete
{
	public class EmployerManager: IEmployerService
	{
        private IUserService _userService;
        private IEmployerDal _employerDal;
        private IUserOperationClaimService _userOperationClaimService;

		public EmployerManager(IUserService userService, IEmployerDal employerDal,
            IUserOperationClaimService userOperationClaimService)
		{
            _userService = userService;
            _employerDal = employerDal;
            _userOperationClaimService = userOperationClaimService;
		}

        public IResult Add(EmployerForRegisterDto employerForRegisterDto)
        {
            var user = _userService.Add(employerForRegisterDto);
            Employer employer = new Employer
            {
                CompanyName = employerForRegisterDto.CompanyName,
                TaxNumber = employerForRegisterDto.TaxNumber,
                TaxPlace = employerForRegisterDto.TaxPlace,
                UserId = user.Id
            };
            _employerDal.Add(employer);
            _userOperationClaimService.AddEmployerClaim(user.Id);
            return new SuccessResult();
        }

        public Employer GetByUserId(int userId)
        {
            var employer = _employerDal.Get(x => x.UserId == userId);
            return employer;
        }

        public IDataResult<EmployerInformationDto> GetEmployerInformation(int employerId)
        {
            var data = _employerDal.GetEmployerInformation(employerId);
            return new SuccessDataResult<EmployerInformationDto>(data);
        }

        public IResult UpdateEmployerInformation(EmployerInformationToUpdateDto employerInformationToUpdateDto)
        {
            var employer = _employerDal.Get(x => x.Id == employerInformationToUpdateDto.EmployerId);
            if (employer == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var result = _userService.Update(employerInformationToUpdateDto);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }
            employer.CompanyName = employerInformationToUpdateDto.CompanyName;
            employer.TaxNumber = employerInformationToUpdateDto.TaxNumber;
            employer.TaxPlace = employerInformationToUpdateDto.TaxPlace;
            _employerDal.Update(employer);
            return new SuccessResult(Messages.Updated);
        }
    }
}

