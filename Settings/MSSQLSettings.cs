namespace Catalog.Settings
{
  public class MSSQLSettings
  {
    public string Host { get; set; }

    public int Port { get; set; }

    public string Database { get; set; }

    public string User { get; set; }

    public string Password { get; set; }

    public string Options { get; set; }

    public string DSN { 
      get {
        return $@"Data Source={Host};
                    Initial Catalog={Database};User ID={User};Password={Password};{Options}";
      }
    }
  }
}