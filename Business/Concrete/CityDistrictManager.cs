using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
	public class CityDistrictManager: ICityDistrictService
	{
        private ICityDal _cityDal;
        private IDistrictDal _districtDal;

		public CityDistrictManager(ICityDal cityDal, IDistrictDal districtDal)
		{
            _cityDal = cityDal;
            _districtDal = districtDal;
		}

        public IDataResult<List<City>> GetCities()
        {
            var data = _cityDal.GetAll();
            return new SuccessDataResult<List<City>>(data);
        }

        public IDataResult<List<District>> GetDistricts()
        {
            var data = _districtDal.GetAll();
            return new SuccessDataResult<List<District>>(data);
        }
    }
}

