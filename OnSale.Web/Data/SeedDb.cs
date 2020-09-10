using OnSale.Common.Entities;
using OnSale.Common.Enums;
using OnSale.Web.Data.Entities;
using OnSale.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSale.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Timo", "Kock", "tkock.movicoders@gmail.com", "611 480 510", "Vía Universitas 81", UserType.Admin);

        }
        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Germany",
                    Departments = new List<Department>
                    {
                        new Department
                        {
                            Name = "Berlin",
                            Cities = new List<City>
                            {
                                new City { Name = "Berlin" }
                            }
                        },
                        new Department
                        {
                            Name = "Baden-Württemberg",
                            Cities = new List<City>
                            {
                                new City { Name = "Stuttgart" },
                                new City { Name = "Karlsruhe" },
                                new City { Name = "Mannheim" }
                            }
                        },
                        new Department
                        {
                            Name = "Bayern",
                            Cities = new List<City>
                            {
                                new City { Name = "München" },
                                new City { Name = "Nürnberg" },
                                new City { Name = "Augsburg" }
                            }
                        }
                    }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Spain",
                    Departments = new List<Department>
                    {
                        new Department
                        {
                            Name = "Madrid",
                            Cities = new List<City>
                            {
                                new City { Name = "Madrid" },
                                new City { Name = "Alcalá de Henares" },
                                new City { Name = "Pozuelo de Alcorcón" }
                            }
                        },
                        new Department
                        {
                            Name = "Aragón",
                            Cities = new List<City>
                            {
                                new City { Name = "Zaragoza" },
                                new City { Name = "Huesca" },
                                new City { Name = "Calatayud" }
                            }
                        }
                    }
                });
            }   
            await _context.SaveChangesAsync();
        }
    }
}
