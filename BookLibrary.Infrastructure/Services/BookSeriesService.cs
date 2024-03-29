﻿using BookLibrary.Core.Domain;
using BookLibrary.Core.Repositories;
using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.DTO;
using BookLibrary.Infrastructure.DTO.BookSeriesDTO;
using BookLibrary.Infrastructure.Exceptions;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.Services
{
    public class BookSeriesService : IBookSeriesService
    {
        private readonly IRepository<BookSeries> _bookSeriesRepository;

        public BookSeriesService(IRepository<BookSeries> bookSeriesRepository)
        {
            _bookSeriesRepository = bookSeriesRepository;
        }

        public async Task<BookSeriesResponse> GetByIdAsync(int id)
        {
            string[] includeProperties = {"Books"};

            var bookSeries = await _bookSeriesRepository.GetByIdAsync(id, includeProperties);

            if (bookSeries is null)
            {
                throw new NotFoundException("book series not found");
            }

            return await Task.FromResult(bookSeries.ToResponse());
        }

        public async Task<IEnumerable<BookSeriesResponse>> BrowseAllAsync(string name)
        {
            Expression<Func<BookSeries, bool>> filter = PredicateBuilder.New<BookSeries>(true);

            if (!string.IsNullOrEmpty(name))
            {
                filter = filter.And(bookSeries => bookSeries.Name.Contains(name));
            }

            string[] includeProperties = { "Books" };

            var bookSeries = await _bookSeriesRepository.BrowseAllAsync(filter, includeProperties);
            return await Task.FromResult(bookSeries.Select(x => x.ToResponse()));
        }

        public async Task<BookSeriesResponse> CreateAsync(BookSeriesCreate bookSeriesCreate)
        {
            var checkBs = await _bookSeriesRepository
                .BrowseAllAsync(x => x.Name == bookSeriesCreate.Name);

            if (checkBs.Any())
            {
                throw new BadRequestException("book series already exists");
            }

            var bs = await _bookSeriesRepository.CreateAsync(bookSeriesCreate.ToDomain());
            return await Task.FromResult(bs.ToResponse());
        }

        public async Task<BookSeriesResponse> UpdateAsync(int id, BookSeriesUpdate bookSeriesUpdate)
        {
            var bs = await _bookSeriesRepository.GetByIdAsync(id);

            if (bs is null)
            {
                throw new NotFoundException("book series not found");
            }

            if (!string.IsNullOrEmpty(bookSeriesUpdate.Name))
            {
                var checkBs = await _bookSeriesRepository
                    .BrowseAllAsync(x => x.Name == bookSeriesUpdate.Name);

                if (checkBs.Any())
                {
                    throw new BadRequestException("book series already exists");
                }

                bs.Name = bookSeriesUpdate.Name;
                bs = await _bookSeriesRepository.UpdateAsync(bs);
            }
            
            return await Task.FromResult(bs.ToResponse());
        }

        public async Task DeleteAsync(int id)
        {
            var bs = await _bookSeriesRepository.GetByIdAsync(id);

            if (bs is null)
            {
                throw new NotFoundException("book series not found");
            }

            await _bookSeriesRepository.DeleteAsync(id);
        }
    }
}
