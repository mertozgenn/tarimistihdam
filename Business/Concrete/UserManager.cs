using System;
using System.Security.Claims;
using Business.Abstract;
using Business.Constants;
using Business.Utilities;
using Core.Concrete.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Dtos.User;

namespace Business.Concrete
{
    public class UserManager : IUserService
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
                LastLoginDate = DateTime.Now,
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

        public IResult Update(UserInformationToUpdateDto userInformationToUpdateDto)
        {
            var user = _userDal.Get(x => x.Id == userInformationToUpdateDto.UserId);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            user.Name = userInformationToUpdateDto.Name;
            user.Surname = userInformationToUpdateDto.Surname;
            user.Phone = userInformationToUpdateDto.Phone;
            user.Tckn = userInformationToUpdateDto.Tckn;

            var newEmailUser = _userDal.Get(x => x.Email == userInformationToUpdateDto.Email);
            if (newEmailUser == null)
            {
                user.Email = userInformationToUpdateDto.Email;
            }

            if (!string.IsNullOrEmpty(userInformationToUpdateDto.NewPassword) && 
                !string.IsNullOrEmpty(userInformationToUpdateDto.NewPasswordAgain) &&
                userInformationToUpdateDto.NewPassword == userInformationToUpdateDto.NewPasswordAgain)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(userInformationToUpdateDto.NewPassword, out passwordSalt, out passwordHash);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            if (userInformationToUpdateDto.NewProfilePhoto != null)
            {
                if (!string.IsNullOrEmpty(user.ProfilePhoto))
                    FileHelper.DeleteFile(user.ProfilePhoto);
                var guid = Guid.NewGuid().ToString();
                user.ProfilePhoto = FileHelper.CreateFile(userInformationToUpdateDto.NewProfilePhoto, "/images/user", guid);
            }

            _userDal.Update(user);
            return new SuccessResult(Messages.Updated);
        }

        public void UpdateLastLoginDate(User user)
        {
            user.LastLoginDate = DateTime.Now;
            _userDal.Update(user);
        }
    }
}

