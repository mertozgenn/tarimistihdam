using Core.Utilities.Results;
using System;
namespace Business.Abstract
{
	public interface IJobApplicationService
	{
		IResult Apply(int jobId, int employeeId);
	}
}

