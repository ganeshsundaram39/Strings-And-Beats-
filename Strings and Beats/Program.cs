using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strings_and_Beats
{
    static class Program
    {
    
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
       
        static void Main()
        {  
            Process[] result = Process.GetProcessesByName("Strings and Beats");
        if (result.Length > 1)
        {
            System.Environment.Exit(0);
        }
        else
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Loading());

            string connectionString = Connection.getConnection();


            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand command;

            connection.Open();

            command = connection.CreateCommand();
            command.CommandText = "SELECT count(*) FROM LoginTbl where Role='Owner';";

            var i = command.ExecuteScalar();

            if (Convert.ToInt16(i) != 1)
            {




                Application.Run(new NewOwner());


                Application.Exit();

            }
            else
            {

                Application.Run(new Login());



            }


        }
        }


    }
}
