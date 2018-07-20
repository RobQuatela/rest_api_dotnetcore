using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using rest_api_dotnetcore.Models;

namespace rest_api_dotnetcore.Config
{
    public class SeedData
    {
        private readonly DatabaseContext _dbContext;

        public SeedData(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void RemoveAndSeedData() {
            _dbContext.Burger.Database.DropCollection("Burger");
            _dbContext.Burger.InsertMany(new List<Burger>(){
                new Burger(){
                    Name = "Cheeseburger",
                    Price = 8.75m
                },
                new Burger(){
                    Name = "Plain Hamburger",
                    Price = 5.75m
                },
                new Burger(){
                    Name = "Kids Burger",
                    Price = 4.75m
                },
                new Burger(){
                    Name = "Turkey Burger",
                    Price = 8.75m
                },
                new Burger(){
                    Name = "1lb Burger",
                    Price = 12.75m
                },
            });
        }
    }
}