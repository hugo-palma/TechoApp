using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Techo_App;
using Xamarin.Auth;

[assembly: Xamarin.Forms.Dependency(typeof(Techo_App.Droid.AccountManagement.AccountManagementImplementation))]
namespace Techo_App.Droid.AccountManagement
{
    public class AccountManagementImplementation : IAccountManagerService
    {
        public static void Init() { var now = DateTime.Now; }
        public List<LoginServices> Accounts
        {
            get
            {
                var accounts = new List<LoginServices>();
                var accountStore = Xamarin.Auth.AccountStore.Create();
                foreach (var service in Enum.GetNames(typeof(LoginServices)))
                {
                    var acc = accountStore.FindAccountsForService(service).FirstOrDefault();
                    if (acc != null)
                    {
                        accounts.Add((LoginServices)Enum.Parse(typeof(LoginServices), service));
                    }
                }
                return accounts;
            }
        }
        public Account getAccount()
        {
            var service = Enum.GetName(typeof(LoginServices), LoginServices.Twitter);
            var account = AccountStore.Create().FindAccountsForService(service).FirstOrDefault();
            return account;
        }
        public void EraseAll()
        {
            var accountStore = Xamarin.Auth.AccountStore.Create();
            foreach (var service in Enum.GetNames(typeof(LoginServices)))
            {
                var acc = accountStore.FindAccountsForService(service).FirstOrDefault();
                if (acc != null)
                {
                    accountStore.Delete(acc, service);
                }
            }
        }
        public string GetPropertyFromAccount(LoginServices service, string property)
        {
            var accountStore = Xamarin.Auth.AccountStore.Create();
            var account = accountStore.FindAccountsForService(service.ToString()).FirstOrDefault();
            if (account != null)
            {
                return account.Properties[property];
            }
            return null;
        }
    }
}