﻿using System;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using Core.Concrete.Entities;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
	public class EfOperationClaimDal: EfEntityRepositoryBase<OperationClaim, Context>, IOperationClaimDal
	{
	}
}

