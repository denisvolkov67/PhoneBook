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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<EmployeeDb> list = new List<EmployeeDb>();
            using (StreamReader sr = new StreamReader(@"e:\Employee.csv", System.Text.Encoding.Default))
            {
                string line;
                long id = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] mass = line.Split(',');
                    EmployeeDb employee = new EmployeeDb();
                    employee.Id = id;
                    employee.DepartmentDbId = mass[0];
                    employee.Name = mass[1];
                    employee.Position = mass[2];
                    list.Add(employee);
                    id++;
                }
            }

            List<DepartmentDb> listDepartment = new List<DepartmentDb>();
            using (StreamReader sr = new StreamReader(@"e:\departments.csv", System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] mass = line.Split(',');
                    DepartmentDb department = new DepartmentDb();
                    department.Id = mass[0];
                    department.Name = mass[1];
                    listDepartment.Add(department);
                }
            }

            modelBuilder.Entity<EmployeeDb>().Property(x => x.Name).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<EmployeeDb>().Property(x => x.Position).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<EmployeeDb>().Property(x => x.Email).HasColumnType("TEXT COLLATE NOCASE");

            modelBuilder.Entity<EmployeeDb>().HasData(
                list.ToArray());
            modelBuilder.Entity<DepartmentDb>().HasData(
                listDepartment.ToArray());
            base.OnModelCreating(modelBuilder);
        }
    }
}