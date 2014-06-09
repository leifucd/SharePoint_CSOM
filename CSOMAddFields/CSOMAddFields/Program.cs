using System;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace CSOMAddFields
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor defaultForeground = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter the URL of the SharePoint Online site:");

            //Console.ForegroundColor = defaultForeground;
            //string webUrl = Console.ReadLine();
            string webUrl = "https://alexandriava1.sharepoint.com/sites/dev";

            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Enter your user name (ex: user@alexandriava.gov):");
            //Console.ForegroundColor = defaultForeground;
            //string userName = Console.ReadLine();
            string userName = "lei.fu@alexandriava.gov";

            Console.WriteLine("Welcome to " + webUrl);
            Console.WriteLine("Your user name is : " + userName);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter your password.");
            Console.ForegroundColor = defaultForeground;
            SecureString password = GetPasswordFromConsoleInput();

            using (var context = new ClientContext(webUrl))
            {
                context.Credentials = new SharePointOnlineCredentials(userName, password);

                Web web = context.Web;
                web.AllProperties["BING_MAPS_KEY"] = "Ak6nc4Ujx-mz99AXAdfVEvNplQJg81xBlnpXFpOxywLR1wf-4Y_Z3vldGzWIelOz";
                web.Update();
                context.ExecuteQuery();

                //List oList = context.Web.Lists.GetByTitle("EmployeeList");
                //oList.Fields.AddFieldAsXml("<Field  Type='Geolocation' DisplayName='Location'/>", true, AddFieldOptions.DefaultValue);
                //oList.Update();
                //context.ExecuteQuery();

                //context.Load(context.Web, w => w.Title);
                //context.ExecuteQuery();

                //Console.ForegroundColor = ConsoleColor.White;
                //Console.WriteLine("Your site title is: " + context.Web.Title);
                //Console.ForegroundColor = defaultForeground;
            }

            //AddGeolocationField(args);
            Console.WriteLine("Location field added successfully");
        }

        private static SecureString GetPasswordFromConsoleInput()
        {
            ConsoleKeyInfo info;

            //Get the user's password as a SecureString
            SecureString securePassword = new SecureString();
            do
            {
                info = Console.ReadKey(true);
                if (info.Key != ConsoleKey.Enter)
                {
                    securePassword.AppendChar(info.KeyChar);
                }
            }
            while (info.Key != ConsoleKey.Enter);
            return securePassword;

        }

        private static void AddGeolocationField(string[] args)
        {
            // Replace site URL and List Title with Valid values.
            ClientContext context = new ClientContext(args[0]);
            List oList = context.Web.Lists.GetByTitle(args[1]);
            oList.Fields.AddFieldAsXml("<Field  Type='Geolocation' DisplayName='Location'/>", true, AddFieldOptions.DefaultValue);
            oList.Update();
            context.ExecuteQuery();

        }

    }
}
