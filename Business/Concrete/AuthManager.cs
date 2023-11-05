using System;
using System.Security.Claims;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Entities.Dtos.User;

namespace Business.Concrete
{
	public class AuthManager: IAuthService
	{
        private IUserService _userService;
        private IEmployeeService _employeeService;
        private IEmployerService _employerService;
		public AuthManager(IUserService userService, IEmployeeService employeeService,
            IEmployerService employerService)
		{
            _userService = userService;
            _employeeService = employeeService;
            _employerService = employerService;
		}

        public IDataResult<List<Claim>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<List<Claim>>(Messages.UserNotFound);
            }
            if (HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordSalt, userToCheck.PasswordHash))
            {
                _userService.UpdateLastLoginDate(userToCheck);
                var operationClaims = _userService.GetClaims(userToCheck);
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userToCheck.Id.ToString()),
                    new Claim(ClaimTypes.Email, userToCheck.Email),
                    new Claim(ClaimTypes.Name, userToCheck.Name),
                };
                foreach (var operationClaim in operationClaims)
                {
                    claims.Add(new Claim(ClaimTypes.Role, operationClaim.Name));
                }
                if (operationClaims.Any(x => x.Name == "Employee"))
                {
                    var employee = _employeeService.GetByUserId(userToCheck.Id);
                    claims.Add(new Claim("EmployeeId", employee.Id.ToString()));
                }
                if (operationClaims.Any(x => x.Name == "Employer"))
                {
                    var employer = _employerService.GetByUserId(userToCheck.Id);
                    claims.Add(new Claim("EmployerId", employer.Id.ToString()));
                }
                return new SuccessDataResult<List<Claim>>(claims);
            }
            return new ErrorDataResult<List<Claim>>(Messages.PasswordError);
        }

        public IDataResult<List<Claim>> EmployeeRegister(EmployeeForRegisterDto employeeForRegisterDto)
        {
            var userToCheck = _userService.GetByMail(employeeForRegisterDto.Email);
            if (userToCheck != null)
            {
                return new ErrorDataResult<List<Claim>>(Messages.UserAlreadyExists);
            }
            var result = _employeeService.Add(employeeForRegisterDto);
            if (result.Success)
            {
                return Login(new UserForLoginDto { Password = employeeForRegisterDto.Password, Email = employeeForRegisterDto.Email });
            }
            return new ErrorDataResult<List<Claim>>(result.Message);
        }

        public IDataResult<List<Claim>> EmployerRegister(EmployerForRegisterDto employerForRegisterDto)
        {
            var userToCheck = _userService.GetByMail(employerForRegisterDto.Email);
            if (userToCheck != null)
            {
                return new ErrorDataResult<List<Claim>>(Messages.UserAlreadyExists);
            }
            var result = _employerService.Add(employerForRegisterDto);
            if (result.Success)
            {
                return Login(new UserForLoginDto { Password = employerForRegisterDto.Password, Email = employerForRegisterDto.Email });
            }
            return new ErrorDataResult<List<Claim>>(result.Message);
        }

        public IResult SendPasswordResetEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}

