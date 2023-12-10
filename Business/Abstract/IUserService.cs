using System;
using System.Security.Claims;
using Core.Concrete.Entities;
using Core.Utilities.Results;
using Entities.Dtos.User;

namespace Business.Abstract
{
	public interface IUserService
	{
        List<OperationClaim> GetClaims(User user);
        User GetByMail(string email);
        IDataResult<User> Add(UserForRegisterDto userForRegisterDto);
        IDataResult<UserInformationDto> GetUserInformation(int userId);
        void UpdateLastLoginDate(User user);
        IResult Update(UserInformationToUpdateDto userInformationToUpdateDto);
    }
}

