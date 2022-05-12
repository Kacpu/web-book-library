using BookLibrary.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.DTO
{
    public static class Mapper
    {
        public static Author ToDomain(this AuthorCreate author)
        {
            return new Author()
            {
                Name = author.Name,
                Surname = author.Surname,
                Description = author.Description,
                DateOfBirth = author.DateOfBirth
            };
        }

        public static AuthorResponse ToResponse(this Author author)
        {
            return new AuthorResponse()
            {
                Id = author.Id,
                Name = author.Name,
                Surname = author.Surname,
                Description = author.Description,
                DateOfBirth = author.DateOfBirth
            };
        }

        
    }
}
