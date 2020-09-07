using OnSale.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSale.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
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
