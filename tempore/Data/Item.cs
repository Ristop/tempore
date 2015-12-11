using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tempore.Data
{
     public class Item
     {
          public int Id { get; set; }
          public int totalSeconds { get; set; }
          public int remainingSeconds { get; set; }
          public string Title { get; set; }
          public string Text { get; set; }
     }
}
