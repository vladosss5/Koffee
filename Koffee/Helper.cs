using System.Net.Mime;
using Koffee.Context;
using Koffee.Models;

namespace Koffee;

public class Helper
{
    private static MyDbContext _satellitecontext;

    public static User User { get; set; }
    public static MyDbContext GetContext()
    {
        return _satellitecontext ??= new();
    }
}