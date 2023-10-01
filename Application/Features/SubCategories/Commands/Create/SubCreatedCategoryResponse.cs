﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SubCategories.Commands.Create
{
    public class SubCreatedCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryID { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
