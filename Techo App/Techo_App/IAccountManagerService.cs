using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace Techo_App
{
    public interface IAccountManagerService
    {
        List<LoginServices> Accounts { get; }
        string GetPropertyFromAccount(LoginServices service, string property);
        Account getAccount();
        void EraseAll();
    }
}
