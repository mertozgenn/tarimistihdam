using System;
using Core.Abstract.DataAccess;
using Entities.Concrete;
using Entities.Dtos.User;

namespace DataAccess.Abstract
{
	public interface IEmployerDal: IEntityRepository<Employer>
	{
		EmployerInformationDto GetEmployerInformation(int employerId);
	}
}

