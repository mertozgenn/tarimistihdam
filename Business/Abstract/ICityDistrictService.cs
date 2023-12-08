using System;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
	public interface ICityDistrictService
	{
		IDataResult<List<City>> GetCities();
        IDataResult<List<District>> GetDistrictsByCityId(int cityId);
    }
}

