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
		public AuthManager(IUserService userService)
		{
            _userService = userService;
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
                return new SuccessDataResult<List<Claim>>(claims);
            }
            return new ErrorDataResult<List<Claim>>(Messages.PasswordError);
        }

        public IDataResult<List<Claim>> Register(UserForRegisterDto userForRegisterDto)
        {
            var userToCheck = _userService.GetByMail(userForRegisterDto.Email);
            if (userToCheck != null)
            {
                return new ErrorDataResult<List<Claim>>(Messages.UserAlreadyExists);
            }
            var result = _userService.Add(userForRegisterDto);
            if (result.Success)
            {
                return Login(new UserForLoginDto { Password = userForRegisterDto.Password, Email = userForRegisterDto.Email });
            }
            return new ErrorDataResult<List<Claim>>(result.Message);
        }

        public IResult SendPasswordResetEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}

