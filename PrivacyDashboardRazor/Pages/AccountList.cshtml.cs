using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PrivacyDashboardRazor.Models;

namespace PrivacyDashboardRazor.Pages
{
    public class AccountListModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public AccountListModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public static int EditId { get; set; }

        private static List<Account>? accounts;

        public List<Account> Accounts => accounts ??= new List<Account>();

        private static string showAccountsUpdate = "none";
        public string ShowAccountUpdate => showAccountsUpdate;

        [BindProperty]
        public Account AccountDetails { get; set; }

        public IActionResult OnPostShowAccountUpdate(int id)
        {

            if (ShowAccountUpdate == "none")
            {
                showAccountsUpdate = "block";
                EditId = id;
            }
            else if (ShowAccountUpdate == "block")
            {
                showAccountsUpdate = "none";
            }

            return Page();
        }

        public IActionResult OnPostUpdateAccount()
        {
            if (ModelState.IsValid && EditId == -1)
            {
                int maxId = Accounts.Count > 0 ? Accounts.Max(c => c.Id) : 0;
                AccountDetails.Id = maxId + 1;
                Accounts.Add(AccountDetails);
                AccountDetails = new Account();
                showAccountsUpdate = "none";
            }
            else if (ModelState.IsValid && EditId >= 0)
            {
                var existingAccount = accounts.Find(c => c.Id == EditId);

                if (existingAccount != null)
                {
                    existingAccount.Website = AccountDetails.Website;
                    existingAccount.Email = AccountDetails.Email;

                }
                showAccountsUpdate = "none";
            }

            return Page();
        }

        public string GetModeHeading()
        {
            if (EditId >= 0)
            {
                var existingAccount = accounts.Find(c => c.Id == EditId);
                if (existingAccount != null)
                {
                    return "Edit Account";
                }
            }

            return "Add New Account";
        }
        public Account GetPrefilledAccountValue()
        {
            if (EditId >= 0)
            {
                var existingAccount = accounts.Find(c => c.Id == EditId);
                if (existingAccount != null)
                {
                    return existingAccount;
                }
            }

            return new Account();
        }
    }
}