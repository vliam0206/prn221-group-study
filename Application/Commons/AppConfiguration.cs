using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Application.Commons;

public class AppConfiguration // use this for al cofiguration in project
{
    // the properties will sync with the value in appsetting.json (the name musch match)
    // for this connection string, it must be tuong minh to be auto generat the migration
    public ConnectionStrings ConnectionStrings { get; set; }
}

public class ConnectionStrings
{
    public string DefaultDB { get; set; } = default!;
}
