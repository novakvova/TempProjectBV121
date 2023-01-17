using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;


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
                Console.WriteLine("У табличні значення фільтрів пусто");
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

            if (!context.FilterNameGroups.Any())
            {
                Console.WriteLine("У табличні групування фільтрів пусто");
                Dictionary<int, int> fng = new Dictionary<int, int>
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 2},
                    { 5, 2 }
                };

                foreach (var data in fng)
                {
                    var entity = new FilterNameGroupEntity
                    {
                        FilterNameId= data.Value,
                        FilterValueId = data.Key
                    };
                    context.FilterNameGroups.Add(entity);
                    context.SaveChanges();
                }
            }

            var queryName = from f in context.FilterNames.AsQueryable()
                            select f;
            var queryGroup = from g in context.FilterNameGroups.AsQueryable()
                             select g;
            //Загальна множина значень
            var query = from u in queryName
                        join g in queryGroup on u.Id equals g.FilterNameId into ua
                        from empty in ua.DefaultIfEmpty()
                        select new
                        {
                            FNameId = u.Id,
                            FName = u.Name,
                            FValueId = empty != null ? empty.FilterValueId : 0,
                            FValue = empty != null ? empty.FilterValue.Name : null,
                        }                        ;
            //var info = query.Where(x=>x.FValueId!=0).ToList();

            var groupData = query
                .Where(x => x.FValueId != 0)
                .AsEnumerable()
                .GroupBy(f => new { Id = f.FNameId, Name = f.FName })
                .Select(g=>g)
                .OrderBy(x=>x.Key.Name);

            var result = groupData.Select(fName => new
            {
                Id = fName.Key.Id,
                Name = fName.Key.Name,

                Children = fName
                    .GroupBy(v => new { Id = v.FValueId, Name = v.FValue })
                    .Select(g=>g.Key)
                    .OrderBy(x=>x.Name)
            }); 


        }


    }
}
