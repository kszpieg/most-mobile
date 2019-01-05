using System;
using System.Collections.Generic;
using System.Text;

namespace Most_MobileApp.Database
{
    public interface IDBInterface
    {
        SQLite.SQLiteConnection CreateConnection();

    }
}
