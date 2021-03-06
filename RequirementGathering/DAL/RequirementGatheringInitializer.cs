﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RequirementGathering.Models;
using Attribute = RequirementGathering.Models.Attribute;

namespace RequirementGathering.DAL
{
    public class RequirementGatheringInitializer : DropCreateDatabaseIfModelChanges<RequirementGatheringDbContext>
    {
        protected override void Seed(RequirementGatheringDbContext context)
        {
            int result;

            // Seed Roles
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            var suRole = new IdentityRole { Name = "Super Administrator" };
            var adminRole = new IdentityRole { Name = "Administrator" };
            var researcherRole = new IdentityRole { Name = "Researcher" };
            var evaluatorRole = new IdentityRole { Name = "Evaluator" };

            context.Roles.Add(suRole);
            context.Roles.Add(adminRole);
            context.Roles.Add(researcherRole);
            context.Roles.Add(evaluatorRole);

            // Seed Users
            var dateOfBirth = DateTime.UtcNow.AddYears(-18);

            var adeel = new User { FirstName = "Adeel", Email = "adeel@uta.fi", UserName = "adeel", DateOfBirth = dateOfBirth };
            var cong = new User { FirstName = "Zhang", Email = "cong@uta.fi", UserName = "cong", DateOfBirth = dateOfBirth };
            var eija = new User { FirstName = "Eija", Email = "eija@uta.fi", UserName = "eija", DateOfBirth = dateOfBirth };
            var ghassan = new User { FirstName = "Ghassan", Email = "ghassan@uta.fi", UserName = "ghassan", DateOfBirth = dateOfBirth };
            var juho = new User { FirstName = "Juho", Email = "juho@uta.fi", UserName = "juho", DateOfBirth = dateOfBirth };
            var liu = new User { FirstName = "Hui", Email = "liu@uta.fi", UserName = "Liu", DateOfBirth = dateOfBirth };
            var teemu = new User { FirstName = "Teemu", Email = "teemu@uta.fi", UserName = "teemu", DateOfBirth = dateOfBirth };
            var toan = new User { FirstName = "Toan", Email = "toan@uta.fi", UserName = "toan", DateOfBirth = dateOfBirth };

            var users = new List<User>
            {
                adeel, cong, eija, ghassan, juho, liu, teemu, toan
            };

            var hasher = new PasswordHasher();

            foreach (var user in users)
            {
                user.PasswordHash = hasher.HashPassword("DefaultPasscode123!!");
                user.EmailConfirmed = true;
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.Roles.Add(new IdentityUserRole { RoleId = researcherRole.Id, UserId = user.Id });
            }

            eija.Roles.Add(new IdentityUserRole { RoleId = suRole.Id, UserId = eija.Id });
            teemu.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = teemu.Id });

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            result = context.SaveChangesAsync().Result;

            // Seed Evaluations

            var xPhone = new Product { Name = "XPhone", Description = "This is the description for xPhone", OwnerId = eija.Id };
            var yPhone = new Product { Name = "YPhone", Description = "This is the description for yPhone", OwnerId = eija.Id };
            var zPhone = new Product { Name = "ZPhone", Description = "This is the description for zPhone", OwnerId = eija.Id };

            var products = new List<Product>
            {
                xPhone,
                yPhone,
                zPhone
            };

            products.ForEach(p => context.Products.Add(p));
            result = context.SaveChangesAsync().Result;

            // Seed Evaluations
            var evaluations = new List<Evaluation>
            {
                new Evaluation{Product = xPhone, Version = "1.0.0", Steps= 5, Owner = eija},
                new Evaluation{Product = yPhone, Version = "1.0.0", Steps= 3, Owner = eija},
                new Evaluation{Product = zPhone, Version = "1.0.0", Steps= 5, Owner = eija},
                new Evaluation{Product = xPhone, Version = "2.0.0", Steps= 3, Owner = eija},
                new Evaluation{Product = yPhone, Version = "1.1.0", Steps= 5, Owner = eija},
                new Evaluation{Product = zPhone, Version = "1.0.1", Steps= 3, Owner = eija}
            };

            evaluations.ForEach(r => context.Evaluations.Add(r));
            result = context.SaveChangesAsync().Result;

            // Seed attributes
            var names = new List<string> { "Robust Embodiment","Long Operational Time","Design",
                                           "Flashlight", "Belthook", "Radio Transmitter",
                                           "Bluetooth", "WiFi", "GPS", "HQ Camera"};

            var attributes = new List<Attribute>();
            System.Random random = new System.Random();

            foreach (var evaluation in evaluations)
            {
                for (int i = 0; i < 12; i++)
                {
                    attributes.Add(new Attribute { Name = names[random.Next(names.Count - 1)], Evaluation = evaluation, Order = i });

                    if (i > 3 && random.Next(100) < 50)
                    {
                        break;
                    }
                }
            }

            attributes.ForEach(s => context.Attributes.Add(s));
            result = context.SaveChangesAsync().Result;

            // Seed EvaluationUser
            var evaluationUsers = new List<EvaluationUser>();

            for (int i = 0; i < 100; i++)
            {
                var evaluationVersion = evaluations[random.Next(evaluations.Count - 1)].Version;
                var userFirstName = users[random.Next(users.Count - 1)].FirstName;
                Evaluation evaluation = context.Evaluations.First(r => r.Version == evaluationVersion);
                User user = context.Users.First(a => a.FirstName == userFirstName);

                evaluationUsers.Add(
                    new EvaluationUser
                    {
                        EvaluationId = evaluation.Id,
                        UserId = user.Id,
                        User = user,
                        Evaluation = evaluation,
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow
                    });
            };

            evaluationUsers = evaluationUsers.Distinct(new DistinctVersionUserComparer())
                             .ToList();
            evaluationUsers.ForEach(u => context.EvaluationUsers.Add(u));

            result = context.SaveChangesAsync().Result;

            // Seed Ratings
            var ratings = new List<Rating>();

            attributes = context.Attributes.ToList();

            for (int i = 0; i < 500; i++)
            {
                var attributesCount = attributes.Count;
                var attribute1 = attributes[random.Next(attributesCount - 1)];
                var attribute2 = attributes[random.Next(attributesCount - 1)];
                var evaluationUser = evaluationUsers[random.Next(evaluationUsers.Count - 1)];

                ratings.Add(
                    new Rating
                    {
                        EvaluationUserId = evaluationUser.Id,
                        AttributeId1 = attribute1.Id.Value,
                        AttributeId2 = attribute2.Id.Value,
                        Value1 = random.Next(1, 5),
                        Value2 = random.Next(1, 5)
                    });
            };

            ratings.ForEach(r => context.Ratings.Add(r));
            result = context.SaveChangesAsync().Result;

            base.Seed(context);
        }
    }

    class DistinctVersionUserComparer : IEqualityComparer<EvaluationUser>
    {

        public bool Equals(EvaluationUser x, EvaluationUser y)
        {
            return x.User.Email == y.User.Email;
        }

        public int GetHashCode(EvaluationUser obj)
        {
            return obj.EvaluationId.GetHashCode() ^
                   obj.User.Email.GetHashCode();
        }
    }
}
