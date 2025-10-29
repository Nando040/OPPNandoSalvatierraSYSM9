using OPPNandoSalvatierraSYSM9.Managers;

namespace OPPNandoSalvatierraSYSM9
{
    public class LoginViewModel
    {
        private UserManager userManager;

        public LoginViewModel(UserManager userManager)
        {
            this.userManager = userManager;
        }

        public string Password { get; internal set; }
    }
}