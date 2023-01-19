using ConsoleTest.Models;
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

            var userId = 4;
            var search = context.Orders.AsQueryable();
            search = search.Where(x => !x.IsDelete);
            var ordersUser = search.Where(o => o.UserId == userId).ToList();

            //ICategoryRepository categoryRepository = new CategoryRepository(context);

            //CategoryEntity cat = new CategoryEntity()
            //{
            //    Name = "Смачні стави",
            //    DateCreated = DateTime.Now
            //};

            //categoryRepository.Create(cat);

            if (!context.FilterNames.Any())
            {
                Console.WriteLine("У табличні назв фільтрів пусто");
                string[] filterNames = {
                    "Виробник", "Процесор"
                };

                foreach (string filterName in filterNames)
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
                        FilterNameId = data.Value,
                        FilterValueId = data.Key
                    };
                    context.FilterNameGroups.Add(entity);
                    context.SaveChanges();
                }
            }

            if (!context.Products.Any())
            {
                var hp = new ProductEntity
                {
                    CategoryId = 1,
                    Name = "HP ProBook 640 G8",
                    DateCreated = DateTime.Now,
                    Price = 40899,
                    Description = "Екран 14\" IPS (1920x1080) Full HD, матовий / Intel Core i5-1135G7 (2.4 - 4.2 ГГц) / RAM 8 ГБ / SSD 256 ГБ / Intel Iris Xe Graphics / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / DOS / 1.38 кг / срібний"
                };
                context.Products.Add(hp);
                context.SaveChanges();

                var dell = new ProductEntity
                {
                    CategoryId = 1,
                    Name = "Dell Latitude 5420",
                    DateCreated = DateTime.Now,
                    Price = 104272,
                    Description = "Екран 14\" IPS (1920x1080) Full HD, глянсовий з антивідблисковим покриттям / Intel Core i7-1185G7 (3.0 - 4.8 ГГц) / RAM 64 ГБ / SSD 1 ТБ / Intel Iris Xe Graphics / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / Linux / 1.37 кг / срібний"
                };
                context.Products.Add(dell);
                context.SaveChanges();
            }

            if (!context.Filters.Any())
            {
                FilterEntity[] newFilters =
                {
                    new FilterEntity { FilterNameId=1, FilterValueId=1, ProductId=1 },
                    new FilterEntity { FilterNameId=2, FilterValueId=4, ProductId=1 },

                    new FilterEntity { FilterNameId=1, FilterValueId=2, ProductId=2 },
                    new FilterEntity { FilterNameId=2, FilterValueId=5, ProductId=2 },

                    new FilterEntity { FilterNameId=2, FilterValueId=4, ProductId=3 },

                };
                context.Filters.AddRange(newFilters);
                context.SaveChanges();
            }

            var filters = GetFilterSelect(context);

            int[] filterValueSearch = { 5 }; //усі товари, у яких процесор i5
            var query = context.Products.AsQueryable();
            foreach (var fName in filters)
            {
                int countFilter = 0; //Кількість співпадінь у даній групі
                var predicate = PredicateBuilder.False<ProductEntity>();
                //іду по дочірніх елементах групи, тобто по значеннях фільтра
                foreach (var fValue in fName.Children)
                {
                    for (int i = 0; i < filterValueSearch.Length; i++)
                    {
                        var idValue = fValue.Id;
                        if (filterValueSearch[i] == idValue)
                        {
                            predicate = predicate
                                .Or(p => p.Filters.Any(f => f.FilterValueId == idValue));
                            countFilter++;
                        }
                    }
                }
                if (countFilter != 0)
                {
                    query = query.Where(predicate);
                }
            }

            int count = query.Count();

            var products = query.Select(
                p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Filters = p.Filters.Select(f=>new { Value = f.FilterValue.Name })
                }
                ).ToList();
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id} - {p.Name} {p.Price} ");
            }


        }

        public static List<FilterNameModel> GetFilterSelect(EFAppContext context)
        {
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
                        };
            //var info = query.Where(x=>x.FValueId!=0).ToList();

            var groupData = query
                .Where(x => x.FValueId != 0)
                .AsEnumerable()
                .GroupBy(f => new { Id = f.FNameId, Name = f.FName })
                .Select(g => g)
                .OrderBy(x => x.Key.Name);

            var result = groupData.Select(fName => new FilterNameModel
            {
                Id = fName.Key.Id,
                Name = fName.Key.Name,
                Children = fName
                    .GroupBy(v => new FilterItem { Id = v.FValueId, Name = v.FValue })
                    .Select(g => g.Key)
                    .OrderBy(x => x.Name)
                    .ToList()
            }).ToList();

            return result;
        }


    }
}
