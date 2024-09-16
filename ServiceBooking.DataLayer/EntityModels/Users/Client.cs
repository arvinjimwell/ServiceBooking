using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBooking.DataLayer.EntityModels;

public class Client : UserBase
{
    public ICollection<BookServices> Bookings { get; set; } = [];
}
