using System;
using Core.Concrete.Entities;

namespace Business.Abstract
{
	public interface IOperationClaimService
	{
		OperationClaim GetEmployee();
		OperationClaim GetEmployer();
	}
}

