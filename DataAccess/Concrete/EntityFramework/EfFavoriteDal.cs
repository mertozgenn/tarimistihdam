﻿using System;
using Core.Concrete.DataAccess.EntityFramework.Repositories;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
	public class EfFavoriteDal: EfEntityRepositoryBase<Favorite, Context>, IFavoriteDal
	{
	}
}

