using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataAccess.RazorUtility
{
    public class RazorUtility
    {
        public static IList<SelectListItem> ConvertDepartments(IList<Department> categories)
        {
            var Items = (from c in categories
                         select new SelectListItem(c.Name, c.Id.ToString()))
                          .ToList();

            Items.Insert(0, new SelectListItem("Select", string.Empty));

            return Items;
        }

    }
}
