using System;
using System.Collections.Generic;
using Koffee.ViewModels;

namespace Koffee.Models;

public partial class User : ViewModelBase
{
    public int IdUser { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surename { get; set; } = null!;

    public string? Patronymic { get; set; }

    public double? HoursWorked { get; set; }

    public string Post { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
