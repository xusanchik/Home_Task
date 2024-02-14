using Ado_net_Dapper_CRUD_API.Dto_s;
using Ado_net_Dapper_CRUD_API.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Ado_net_Dapper_CRUD_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        const string context = "Server=localhost;Port=5432;Database=Food_bot;username=postgres;Password=husan9090;";

        [HttpGet]
        public List<Product> GetAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(context))
            {
                return connection.Query<Product>("select * from Product;").ToList();
            }
        }
        [HttpGet]
        public List<Product> GetByID(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(context))
            {
                return connection.Query<Product>("select * from Product where id = @id;", new { id = id }).ToList();
            }
        }
        [HttpPost]
        public ProductDto_s Create(ProductDto_s product)
        {
            string sql = "INSERT INTO product (name, description , prices) VALUES (@name, @description,@prices );";
            using (NpgsqlConnection connection = new NpgsqlConnection(context))
            {
                connection.Execute(sql, new ProductDto_s
                {
                    Name = product.Name,
                    Description = product.Description,
                    prices = product.prices
                });
            }
            return product;
        }
        [HttpPut]

        public ProductDto_s Update(int id, ProductDto_s product)
        {
            string sql = $"update product set name = @name, description = @description , prices = @prices where id = {id}";
            using (NpgsqlConnection connection = new NpgsqlConnection(context))
            {
                connection.Execute(sql, new ProductDto_s
                {
                    Name = product.Name,
                    Description = product.Description,
                    prices = product.prices,
                });
            }
            return product;
        }
        [HttpDelete]
        public int Delete(int id)
        {
            string sql = $"Delete from product where id = @id";

            using (NpgsqlConnection connection = new NpgsqlConnection(context))
            {
                return connection.Execute(sql, new { Id = id });
            }
        }
    }
}
