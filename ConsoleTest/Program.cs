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
                    "Виробник", "Процесор"
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

            if (!context.FitlerValues.Any())
            {
                Console.WriteLine("У табличні назв фільтрів пусто");
                string[] filterValues = {
                    "HP", "Dell", "Lenovo",
                    "Intel Core i5", "Intel Core i7"
                };

                foreach (string filterValue in filterValues)
                {
                    var fv = new FilterValueEntity
                    {
                        DateCreated = DateTime.Now,
                        Name = filterValue,
                    };
                    context.FitlerValues.Add(fv);
                    context.SaveChanges();
                }
            }
        }
    }
}
