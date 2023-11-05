﻿using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.User;

namespace Business.Concrete
{
	public class EmployeeManager: IEmployeeService
	{
        private IUserService _userService;
        private IEmployeeDal _employeeDal;
        private IUserOperationClaimService _userOperationClaimService;
		public EmployeeManager(IUserService userService, IEmployeeDal employeeDal,
            IUserOperationClaimService userOperationClaimService)
		{
            _userService = userService;
            _employeeDal = employeeDal;
            _userOperationClaimService = userOperationClaimService;
		}

        public IResult Add(EmployeeForRegisterDto employeeForRegisterDto)
        {
            var user = _userService.Add(employeeForRegisterDto);
            Employee employee = new Employee
            {
                UserId = user.Id
            };
            _employeeDal.Add(employee);
            _userOperationClaimService.AddEmployeeClaim(user.Id);
            return new SuccessResult();
        }

        public Employee GetByUserId(int userId)
        {
            var data = _employeeDal.Get(x => x.UserId == userId);
            return data;
        }
    }
}
