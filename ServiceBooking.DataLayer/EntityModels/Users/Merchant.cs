﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBooking.DataLayer.EntityModels;

public class Merchant : UserBase
{
    public ICollection<Business> Businesses { get; set; } = [];
}
