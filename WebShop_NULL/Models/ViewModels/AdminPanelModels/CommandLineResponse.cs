namespace WebShop_NULL.Models.ViewModels.AdminPanelModels
{
    public class CommandLineResponse
    {
        public bool IsResponse = true;
        public bool IsSuccessful = true;
        public string Message;

        public CommandLineResponse()
        {
        }

        public CommandLineResponse(bool isResponse, bool isSuccessful, string message)
        {
            IsResponse = isResponse;
            IsSuccessful = isSuccessful;
            Message = message;
        }

        public static CommandLineResponse Success(string message = null)
        {
            return new CommandLineResponse(true, true, message);
        }

        public static CommandLineResponse Failure(string message = null)
        {
            return new CommandLineResponse(true, false, message);
        }

        public static CommandLineResponse Empty()
        {
            return new CommandLineResponse(false, false, null);
        }

    }
}