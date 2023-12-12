using System;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.WorkExperience;

namespace Business.Abstract
{
	public interface IWorkExperienceService
	{
		IDataResult<List<WorkExperience>> GetByEmployeeId(int employeeId);
		IResult Add(WorkExperienceToAddDto workExperienceToAddDto);
		IResult Delete(int id, int employeeId);
	}
}

