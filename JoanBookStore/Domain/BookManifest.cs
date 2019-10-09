﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JoanBookStore.Domain
{
    public class BookManifest
    {
        public String Title { get; set; }

        public String Description { get; set; }

        public String ISBN { get; set; }

        public String Author { get; set; }

        public String Genre { get; set; }

        public int Pages { get; set; }

        public String AgeRange { get; set; }

        public String Price { get; set; }

        public int Quantity { get; set; }

    }
}
