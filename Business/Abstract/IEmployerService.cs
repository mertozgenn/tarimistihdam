using System;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.User;

namespace Business.Abstract
{
	public interface IEmployerService
	{
        IResult Add(EmployerForRegisterDto employerForRegisterDto);
        IDataResult<EmployerInformationDto> GetEmployerInformation(int employerId);
        IResult UpdateEmployerInformation(EmployerInformationToUpdateDto employerInformationToUpdateDto);
        Employer GetByUserId(int userId);
    }
}

