using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.Interest;
using System;
namespace Business.Abstract
{
	public interface IInterestService
	{
		IDataResult<List<InterestDto>> GetByEmployeeId(int employeeId);
		IResult Delete(int id);
		IResult Add(Interest interest);
	}
}

