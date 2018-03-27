using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteRest.Api;
using LiteRest.Attributes;
using LiteRest.Testing.Model;

namespace LiteRest.Testing.Controllers
{
    [Controller("/api/v1/bookshelf")]
    public class BookshelfController : ApiController
    {
        private static readonly Book[] Bookshelf = {
            new Book("Testbook 1", "Mary Doe", "John Doe"),
            new Book("Testbook 2", "John Miller", "Obi Wan Kenobi")
        };

        [HttpGet]
        public Book[] GetBookshelf()
        {
            return Bookshelf;
        }

        [HttpGet]
        public Book GetBookshelf([UrlParameter] int index)
        {
            return Bookshelf[index];
        }
    }
}
