using DAL.Data;
using DAL.Interfaces;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Services
{
    public class CategoryCreateModel
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
    public interface ICategoryService
    {
        int CrateCategory(CategoryCreateModel categoryModel);
    }
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService()
        {
            EFAppContext context = new EFAppContext();
            _categoryRepository = new CategoryRepository(context);

        }

        public int CrateCategory(CategoryCreateModel categoryModel)
        {
            //звантажили фото на сервер
            //додали категорію в БД
            //повернули id категорії
            throw new NotImplementedException();
        }
    }
}
