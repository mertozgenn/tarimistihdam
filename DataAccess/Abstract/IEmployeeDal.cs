using System;
using Core.Abstract.DataAccess;
using Entities.Concrete;
using Entities.Dtos.User;

namespace DataAccess.Abstract
{
	public interface IEmployeeDal: IEntityRepository<Employee>
	{
		EmployeeInformationDto GetEmployeeInformation(int employeeId);
	}
}

