using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Koffee.ViewModels;

namespace Koffee.Models;

public partial class Dish : ViewModelBase
{
    public int IdDish { get; set; }
    public string Name { get; set; } = null!;
    public float Price { get; set; }
    [NotMapped] public int Count { get; set; }

    public virtual ICollection<DishList> DishLists { get; } = new List<DishList>();
}
