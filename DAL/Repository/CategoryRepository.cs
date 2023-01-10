using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class CategoryRepository : GenericRepository<CategoryEntity>, 
        ICategoryRepository
    {
        public CategoryRepository(EFAppContext context) : base(context)
        {
        }
    }
}
