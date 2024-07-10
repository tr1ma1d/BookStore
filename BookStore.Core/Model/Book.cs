﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Model
{
    public class Book
    {
        public const int MAX_TITLE_LENGTH = 250;
        public Book()
        {
            
        }
        private Book(Guid id, string title, string description, decimal price)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }
        public Guid Id { get; }
        public string Title { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public decimal Price { get; }

        public static (Book Book, string Error) Create(Guid id, string title, string description, decimal price)
        {
            var error = string.Empty;

            if(title.Length > MAX_TITLE_LENGTH || string.IsNullOrEmpty(title))
            {
                error = "title is longer";
            }

            var book = new Book(id, title, description, price);

            return (book, error);
        }
    }
}
