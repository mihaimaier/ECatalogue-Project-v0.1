﻿using ProjectOnlineCatalogue.Models;
using System.ComponentModel.DataAnnotations;

namespace ECatalogueApi.DTO
{
    public class StudentToGet
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public AddressToGet Address { get; set; }

    }
}
