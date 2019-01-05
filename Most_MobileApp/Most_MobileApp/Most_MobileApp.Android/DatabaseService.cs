using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Most_MobileApp.Database;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(Most_MobileApp.Droid.DatabaseService))]
namespace Most_MobileApp.Droid
{
    public class DatabaseService : IDBInterface
    {
        public DatabaseService()
        {

        }
        public SQLiteConnection CreateConnection()
        {
            var sqliteFilename = "MOSTv1_03.db";
            string documentsDirectoryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentsDirectoryPath, sqliteFilename);
            if (!File.Exists(path))
            {
                using (var binaryReader = new BinaryReader(Android.App.Application.Context.Assets.Open(sqliteFilename)))
                {
                    using (var binaryWriter = new BinaryWriter(new FileStream(path, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = binaryReader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            binaryWriter.Write(buffer, 0, length);
                        }
                    }
                }
            }
            //var plat = new SQLite.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var conn = new SQLite.SQLiteConnection(path);

            return conn;
        }
    }
}