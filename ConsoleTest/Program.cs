using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            EFAppContext context = new EFAppContext();

            //ICategoryRepository categoryRepository = new CategoryRepository(context);

            //CategoryEntity cat = new CategoryEntity()
            //{
            //    Name = "Смачні стави",
            //    DateCreated = DateTime.Now
            //};

            //categoryRepository.Create(cat);

            if(!context.FilterNames.Any())
            {
                Console.WriteLine("У табличні назв фільтрів пусто");
                string[] filterNames = { 
                    "Dell", "HP", "Lenovo",
                    "Intel Core i5", "Intel Core i7"
                };

                foreach(string filterName in filterNames)
                {
                    var fn = new FilterNameEntity
                    {
                        DateCreated = DateTime.Now,
                        Name = filterName,
                    };
                    context.FilterNames.Add(fn);
                    context.SaveChanges();
                }
            }


        }
    }
}
