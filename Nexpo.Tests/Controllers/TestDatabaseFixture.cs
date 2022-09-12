﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Nexpo.Models;
using Nexpo.Services;
using System;

namespace Nexpo.Tests.Controllers
{
    public class TestDatabaseFixture
    {
        //For use with .useSqlServer, need to find correct string
        private const string ConnectionString = "Host=localhost;Port=5432;Database=nexpo;User ID=nexpo;Password=nexpo;Pooling=true;Connection Lifetime=0;";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;
        private static PasswordService _passwordService = new PasswordService();

        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        // Companies
                        var company1 = new Company { /*Id = 1,*/ Name = "Apple", Description = "A fruit company" };
                        var company2 = new Company { /*Id = 2,*/ Name = "Google", Description = "You can find more about us by searching the web" };
                        var company3 = new Company { /*Id = 3,*/ Name = "Spotify", Description = "We like music" };
                        var company4 = new Company { /*Id = 4,*/ Name = "Facebook", Description = "We have friends in common" };
                        context.Companies.AddRange(company1, company2, company3, company4);
                        context.SaveChanges();

                        // Users
                        var user1 = new User { /*Id = 1,*/ Email = "admin@example.com", PasswordHash = _passwordService.HashPassword("password"), Role = Role.Administrator, FirstName = "Alpha", LastName = "Admin" };
                        var user2 = new User { /*Id = 2,*/ Email = "student1@example.com", PasswordHash = _passwordService.HashPassword("password"), Role = Role.Student, FirstName = "Alpha", LastName = "Student" };
                        var user3 = new User { /*Id = 3,*/ Email = "student2@example.com", PasswordHash = _passwordService.HashPassword("password"), Role = Role.Student, FirstName = "Beta", LastName = "Student" };
                        var user4 = new User { /*Id = 4,*/ Email = "student3@example.com", PasswordHash = _passwordService.HashPassword("password"), Role = Role.Student, FirstName = "Gamma", LastName = "Student" };
                        var user5 = new User { /*Id = 5,*/ Email = "rep1@company1.example.com", PasswordHash = _passwordService.HashPassword("password"), Role = Role.CompanyRepresentative, FirstName = "Alpha", LastName = "Rep", CompanyId = company1.Id.Value };
                        var user6 = new User { /*Id = 6,*/ Email = "rep2@company1.example.com", PasswordHash = _passwordService.HashPassword("password"), Role = Role.CompanyRepresentative, FirstName = "Beta", LastName = "Rep", CompanyId = company1.Id.Value };
                        var user7 = new User { /*Id = 7,*/ Email = "rep1@company2.example.com", PasswordHash = _passwordService.HashPassword("password"), Role = Role.CompanyRepresentative, FirstName = "Gamma", LastName = "Rep", CompanyId = company2.Id.Value };
                        var user8 = new User { /*Id = 8,*/ Email = "rep1@company3.example.com", PasswordHash = _passwordService.HashPassword("password"), Role = Role.CompanyRepresentative, FirstName = "Delta", LastName = "Rep", CompanyId = company3.Id.Value };
                        var user9 = new User { /*Id = 9,*/ Email = "rep1@company4.example.com", PasswordHash = _passwordService.HashPassword("password"), Role = Role.CompanyRepresentative, FirstName = "Epsilon", LastName = "Rep", CompanyId = company4.Id.Value };
                        context.Users.AddRange(user1, user2, user3, user4, user5, user6, user7, user8, user9);
                        context.SaveChanges();

                        // Students
                        var student1 = new Student { /*Id = 1,*/ Guild = Guild.D, Year = 4, MasterTitle = "Project management in software systems", UserId = user2.Id.Value };
                        var student2 = new Student { /*Id = 2,*/ Guild = Guild.I, Year = 2, UserId = user3.Id.Value, LinkedIn = "my-impressive-profile" };
                        var student3 = new Student { /*Id = 3,*/ Guild = Guild.V, Year = 3, UserId = user4.Id.Value };
                        context.Students.AddRange(student1, student2, student3);
                        context.SaveChanges();

                        // Events
                        var event1 = new Event { /*Id = 1,*/ Name = "Breakfast Mingle", Description = "Breakfast with SEB", Date = "2021-11-12", Start = "08:15", End = "10:00", Host = "SEB", Location = "Cornelis", Language = "Swedish", Capacity = 30 };
                        var event2 = new Event { /*Id = 2,*/ Name = "Bounce with Uber", Description = "Day event at Bounce with Uber", Date = "2021-11-13", Start = "09:00", End = "16:00", Host = "Uber", Location = "Bounce Malmö", Language = "English", Capacity = 20 };
                        var event3 = new Event { /*Id = 3,*/ Name = "CV Workshop with Randstad", Description = "Make your CV look professional with the help of Randstad", Date = "2021-11-14", Start = "13:30", End = "15:00", Host = "Randstad", Location = "E:A", Language = "Swedish", Capacity = 3 };
                        var event4 = new Event { /*Id = 4,*/ Name = "Inspirational lunch lecture", Description = "Get inspired and expand your horizons", Date = "2021-11-15", Start = "12:15", End = "13:00", Host = "SYV", Location = "MA:3", Language = "Swedish", Capacity = 10 };
                        context.Events.AddRange(event1, event2, event3, event4);
                        context.SaveChanges();

                        // Tickets
                        var ticket1 = new Ticket { /*Id = 1,*/ Code = Guid.NewGuid(), PhotoOk = true, EventId = 1, UserId = user2.Id.Value };
                        var ticket2 = new Ticket { /*Id = 2,*/ Code = Guid.NewGuid(), PhotoOk = false, EventId = 1, UserId = user3.Id.Value };
                        var ticket3 = new Ticket { /*Id = 3,*/ Code = Guid.NewGuid(), PhotoOk = false, EventId = 2, UserId = user2.Id.Value };
                        var ticket4 = new Ticket { /*Id = 4,*/ Code = Guid.NewGuid(), PhotoOk = true, EventId = 3, UserId = user3.Id.Value };
                        var ticket5 = new Ticket { /*Id = 5,*/ Code = Guid.NewGuid(), PhotoOk = true, EventId = 1, UserId = user2.Id.Value };
                        var ticket6 = new Ticket { /*Id = 6,*/ Code = Guid.NewGuid(), PhotoOk = true, EventId = 1, UserId = user3.Id.Value };
                        var ticket7 = new Ticket { /*Id = 7,*/ Code = Guid.NewGuid(), PhotoOk = true, EventId = 1, UserId = user4.Id.Value };
                        context.Tickets.AddRange(ticket1, ticket2, ticket3, ticket4, ticket5, ticket6, ticket7);
                        context.SaveChanges();

                        // StudentSessionTimeslots
                        var session1 = new StudentSessionTimeslot { /*Id = 1,*/ Start = DateTime.Parse("2021-11-21 10:00"), End = DateTime.Parse("2021-11-21 10:15"), Location = "Zoom", CompanyId = company1.Id.Value };
                        var session2 = new StudentSessionTimeslot { /*Id = 2,*/ Start = DateTime.Parse("2021-11-21 10:15"), End = DateTime.Parse("2021-11-21 10:30"), Location = "Zoom", CompanyId = company1.Id.Value };
                        var session3 = new StudentSessionTimeslot { /*Id = 3,*/ Start = DateTime.Parse("2021-11-21 10:30"), End = DateTime.Parse("2021-11-21 10:45"), Location = "Zoom", CompanyId = company1.Id.Value };
                        var session4 = new StudentSessionTimeslot { /*Id = 4,*/ Start = DateTime.Parse("2021-11-22 11:00"), End = DateTime.Parse("2021-11-22 11:15"), Location = "Zoom", CompanyId = company2.Id.Value };
                        var session5 = new StudentSessionTimeslot { /*Id = 5,*/ Start = DateTime.Parse("2021-11-22 11:15"), End = DateTime.Parse("2021-11-22 11:30"), Location = "Zoom", CompanyId = company2.Id.Value };
                        var session6 = new StudentSessionTimeslot { /*Id = 6,*/ Start = DateTime.Parse("2021-11-23 12:00"), End = DateTime.Parse("2021-11-22 12:15"), Location = "Zoom", CompanyId = company3.Id.Value };
                        var session7 = new StudentSessionTimeslot { /*Id = 7,*/ Start = DateTime.Parse("2021-11-23 12:15"), End = DateTime.Parse("2021-11-22 12:30"), Location = "Zoom", CompanyId = company3.Id.Value };
                        context.StudentSessionTimeslots.AddRange(session1, session2, session3, session4, session5, session6, session7);
                        context.SaveChanges();

                        // StudentSessionApplications
                        var application1 = new StudentSessionApplication { /*Id = 1,*/ Motivation = "I think you are an interesting company", Rating = 3, StudentId = student1.Id.Value, CompanyId = company1.Id.Value };
                        var application2 = new StudentSessionApplication { /*Id = 2,*/ Motivation = "I love my MacBook", Rating = 4, StudentId = student2.Id.Value, CompanyId = company1.Id.Value };
                        var application3 = new StudentSessionApplication { /*Id = 3,*/ Motivation = "User experience is very important for me", Rating = 5, StudentId = student3.Id.Value, CompanyId = company1.Id.Value };

                        var application4 = new StudentSessionApplication { /*Id = 4,*/ Motivation = "I would like to learn more about searching", Rating = 3, StudentId = student1.Id.Value, CompanyId = company2.Id.Value };
                        var application5 = new StudentSessionApplication { /*Id = 5,*/ Motivation = "I am applying for everything", Rating = 0, StudentId = student2.Id.Value, CompanyId = company2.Id.Value };
                        var application6 = new StudentSessionApplication { /*Id = 6,*/ Motivation = "Search algrorithms are very cool", Rating = 4, StudentId = student3.Id.Value, CompanyId = company2.Id.Value };

                        var application7 = new StudentSessionApplication { /*Id = 7,*/ Motivation = "Music is a big passion of mine", Rating = 4, StudentId = student2.Id.Value, CompanyId = company3.Id.Value };
                        context.StudentSessionApplications.AddRange(application1, application2, application3, application4, application5, application6, application7);
                        context.SaveChanges();

                        // CompanyConnections
                        var connection1 = new CompanyConnection { /*Id = 1,*/ Comment = "Someone that is very passionate about what they are doing", Rating = 4, StudentId = student1.Id.Value, CompanyId = company1.Id.Value };
                        var connection2 = new CompanyConnection { /*Id = 2,*/ Comment = "Seems like a interesting guy, contact him later about internship", Rating = 5, StudentId = company3.Id.Value, CompanyId = company4.Id.Value };
                        context.CompanyConnections.AddRange(connection1, connection2);
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public ApplicationDbContext CreateContext() {
            return new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(ConnectionString)
                .Options, new PasswordService());
        }
    }
}
