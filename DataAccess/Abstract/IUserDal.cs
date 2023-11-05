using System;
using Core.Abstract.DataAccess;
using Core.Concrete.Entities;

namespace DataAccess.Abstract
{
	public interface IUserDal: IEntityRepository<User>
	{
		List<OperationClaim> GetClaims(User user);
	}
}

