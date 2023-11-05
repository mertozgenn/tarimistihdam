using System;
using Business.Abstract;
using Core.Concrete.Entities;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        public OperationClaim GetEmployee()
        {
            return _operationClaimDal.Get(x => x.Name == "employee");
        }

        public OperationClaim GetEmployer()
        {
            return _operationClaimDal.Get(x => x.Name == "employer");
        }
    }
}

