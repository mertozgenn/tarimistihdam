using System;
using Core.Utilities.Results;
using Entities.Dtos.WorkExperience;

namespace Business.Abstract
{
	public interface IWorkExperienceService
	{
		IResult Add(WorkExperienceToAddDto workExperienceToAddDto);
		IResult Delete(int id, int employeeId);
	}
}

