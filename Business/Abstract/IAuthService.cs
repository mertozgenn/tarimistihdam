using System;
using System.Security.Claims;
using Core.Concrete.Entities;
using Core.Utilities.Results;
using Entities.Dtos.User;

namespace Business.Abstract
{
	public interface IAuthService
	{
        IResult SendPasswordResetEmail(string email);
        IDataResult<List<Claim>> Login(UserForLoginDto userForLoginDto);
        IDataResult<List<Claim>> EmployeeRegister(EmployeeForRegisterDto employeeForRegisterDto);
        IDataResult<List<Claim>> EmployerRegister(EmployerForRegisterDto employerForRegisterDto);
    }
}

