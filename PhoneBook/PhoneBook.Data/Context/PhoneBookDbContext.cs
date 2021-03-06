﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Models;
using System.Collections.Generic;
using System.IO;

namespace PhoneBook.Data.Context
{
    public class PhoneBookDbContext : IdentityDbContext
    {
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options)
            : base(options: options)
        { }

        public DbSet<EmployeeDb> Employees { get; set; }
        public DbSet<DepartmentDb> Departments { get; set; }
        public DbSet<FavoritesDb> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Import(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private ModelBuilder Import (ModelBuilder builder)
        {
             List < EmployeeDb > list = new List<EmployeeDb>();
            List<DepartmentDb> listDepartment = new List<DepartmentDb>();

            try
            {
                //using (StreamReader sr = new StreamReader(@"e:\TEMP\Employee.csv", System.Text.Encoding.Default))
                using (StreamReader sr = new StreamReader(@"c:\inetpub\wwwroot\phoneBook.api\Employee.csv", System.Text.Encoding.Default))
                {
                    string line;
                    long id = 1;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] mass = line.Split(';');
                        EmployeeDb employee = new EmployeeDb();
                        employee.Id = id;
                        employee.DepartmentDbId = mass[0];
                        employee.Name = mass[1];
                        employee.Position = mass[2];
                        employee.Name_Upper = mass[1].ToUpper();
                        employee.Position_Upper = mass[2].ToUpper();
                        list.Add(employee);
                        id++;
                    }
                }

                //using (StreamReader sr = new StreamReader(@"e:\TEMP\Departments.csv", System.Text.Encoding.Default))
                using (StreamReader sr = new StreamReader(@"c:\inetpub\wwwroot\phoneBook.api\Departments.csv", System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] mass = line.Split(';');
                        DepartmentDb department = new DepartmentDb();
                        department.Id = mass[0];
                        department.Name = mass[1];
                        listDepartment.Add(department);
                    }
                }
            }
            catch { }


            builder.Entity<EmployeeDb>().Property(x => x.Name).HasColumnType("TEXT COLLATE NOCASE");
            builder.Entity<EmployeeDb>().Property(x => x.Position).HasColumnType("TEXT COLLATE NOCASE");

            builder.Entity<EmployeeDb>().HasData(
                list.ToArray());
            builder.Entity<DepartmentDb>().HasData(
                listDepartment.ToArray());

            return builder;
        }
    }
}
