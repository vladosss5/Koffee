namespace Koffee.Context;

public class Connector
{
    private static MyDbContext _db;

    public static MyDbContext GetContext()
    {
        return _db ??= new MyDbContext();
    }
}