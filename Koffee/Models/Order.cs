using System;
using System.Collections.Generic;
using Koffee.ViewModels;

namespace Koffee.Models;

public partial class Order : ViewModelBase
{
    public int IdOrder { get; set; }

    public int IdUser { get; set; }

    public DateTime Date { get; set; }

    public float Price { get; set; }

    public virtual ICollection<DishList> DishLists { get; set; } = new List<DishList>();

    public virtual User IdUserNavigation { get; set; } = null!;
}
