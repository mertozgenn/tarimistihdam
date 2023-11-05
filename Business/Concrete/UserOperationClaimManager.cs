using System;
using Business.Abstract;
using Core.Concrete.Entities;
using DataAccess.Abstract;

namespace Business.Concrete
{
	public class UserOperationClaimManager: IUserOperationClaimService
	{
		private IUserOperationClaimDal _userOperationClaimDal;
        private IOperationClaimService _operationClaimService;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IOperationClaimService operationClaimService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimService = operationClaimService;
        }

        public void AddEmployeeClaim(int userId)
        {
            UserOperationClaim userOperationClaim = new UserOperationClaim
            {
                UserId = userId,
                OperationClaimId = _operationClaimService.GetEmployee().Id
            };
            _userOperationClaimDal.Add(userOperationClaim);
        }

        public void AddEmployerClaim(int userId)
        {
            UserOperationClaim userOperationClaim = new UserOperationClaim
            {
                UserId = userId,
                OperationClaimId = _operationClaimService.GetEmployer().Id
            };
            _userOperationClaimDal.Add(userOperationClaim);
        }
    }
}

