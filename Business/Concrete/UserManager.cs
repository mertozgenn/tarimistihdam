using System;
using System.Security.Claims;
using Business.Abstract;
using Core.Concrete.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Dtos.User;

namespace Business.Concrete
{
	public class UserManager: IUserService
	{
		private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public User Add(UserForRegisterDto userForRegister)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegister.Password, out passwordSalt, out passwordHash);
            User user = new User
            {
                Email = userForRegister.Email,
                Guid = Guid.NewGuid(),
                IsActive = true,
                IsDeleted = false,
                LastLoginDate = DateTime.MinValue,
                Name = userForRegister.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Phone = userForRegister.Phone,
                RegisterDate = DateTime.Now,
                Surname = userForRegister.Surname,
                Tckn = userForRegister.Tckn
            };
            return _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            var data = _userDal.Get(x => x.Email == email && x.IsDeleted == false);
            return data;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void UpdateLastLoginDate(User user)
        {
            user.LastLoginDate = DateTime.Now;
            _userDal.Update(user);
        }
    }
}

