using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repository;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            EFAppContext context = new EFAppContext();

            ICategoryRepository categoryRepository = new CategoryRepository(context);

            CategoryEntity cat = new CategoryEntity()
            {
                Name = "Смачні стави",
                DateCreated = DateTime.Now
            };

            categoryRepository.Create(cat);


        }
    }
}
