using Entities.Concrete;
using System;
namespace Business.Abstract
{
	public interface IJobTagService
	{
		List<JobTag> GetJobTags();
		List<JobTag> GetJobTagsByIds(List<int> ids);
	}
}

