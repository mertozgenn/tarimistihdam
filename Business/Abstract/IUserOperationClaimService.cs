using System;
namespace Business.Abstract
{
	public interface IUserOperationClaimService
	{
		void AddEmployerClaim(int userId);
		void AddEmployeeClaim(int userId);
	}
}

