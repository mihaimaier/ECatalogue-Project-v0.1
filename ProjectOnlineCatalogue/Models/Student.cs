﻿using System.ComponentModel.DataAnnotations;

namespace ProjectOnlineCatalogue.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Address Address { get; set; }

        public List<Mark> Marks { get; set; } = new List<Mark>();

    }
}