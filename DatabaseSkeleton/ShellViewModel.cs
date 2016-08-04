using System;
using System.Linq;
using Persistence;

namespace DatabaseSkeleton {
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell{ 
        public ShellViewModel()
        {
            var Factory = new Npgsql.NpgsqlConnectionFactory();

            Db.ConfigureMigrations();
            using (var C = new Db())
            {
                var n = C.Client.Count();
                n = n;
            }
        }
    }
}