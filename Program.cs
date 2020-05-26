using TaskRollback.Accounts;

namespace TaskRollback
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var accountsBank = new AccountsBank();
            var userInterface = new UserInterface();

            while (true)
            {
                var commandNames = userInterface.GetCommandNames();
                var commands = CommandFactory.Create(commandNames);
                commands.Each(command => command.Do(accountsBank, userInterface));
                userInterface.RequestContinuance();
            }
        }
    }
}
