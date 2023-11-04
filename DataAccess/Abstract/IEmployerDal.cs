using System;
using Core.Abstract.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
	public interface IEmployerDal: IEntityRepository<Employer>
	{
	}
}

