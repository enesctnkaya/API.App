﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities.Common
{
    public class BaseEntity<T>
    {
        public T ID { get; set; } = default!;
    }
}
