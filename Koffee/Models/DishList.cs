using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Koffee.ViewModels;
using ReactiveUI;

namespace Koffee.Models;

public partial class DishList : ViewModelBase
{
    public int IdList { get; set; }
    public int? IdOrder { get; set; }
    
    [ForeignKey("Dish") ]
    public int? IdDish { get; set; }
    public int? Count { get; set; }
    public virtual Dish IdDishNavigation { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;
    
}
