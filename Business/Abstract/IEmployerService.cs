using System;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.User;

namespace Business.Abstract
{
	public interface IEmployerService
	{
        IResult Add(EmployerForRegisterDto employerForRegisterDto);
        Employer GetByUserId(int userId);
    }
}

