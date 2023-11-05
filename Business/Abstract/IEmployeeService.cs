using System;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.User;

namespace Business.Abstract
{
	public interface IEmployeeService
	{
		IResult Add(EmployeeForRegisterDto employeeForRegisterDto);
		Employee GetByUserId(int userId);
	}
}

