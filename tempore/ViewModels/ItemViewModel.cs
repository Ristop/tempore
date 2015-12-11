using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using tempore.Data;
using Windows.Globalization.DateTimeFormatting;
namespace tempore.ViewModels
{
     public class ItemViewModel
     {
          private int _itemId;

          public int ItemId
          {
               get
               {
                    return _itemId;
               }
          }

          public string DateCreatedHourMinute
          {
               get
               {
                    var formatter = new DateTimeFormatter("hour minute");
                    return formatter.Format(DateCreated);
               }
          }

          public string Title { get; set; }
          public string Text { get; set; }
          public DateTime DateCreated { get; set; }

          public ItemViewModel()
          {
          }

          public static ItemViewModel FromItem(Item item)
          {
               var viewModel = new ItemViewModel();

               viewModel._itemId = item.Id;
               viewModel.Title = item.Title;
               viewModel.Text = item.Text;

               return viewModel;
          }
     }
}
