namespace Deliver.Application.Exceptions
{
    public class CredentialNotValid : ApplicationException
    {
        public CredentialNotValid(string message) : base(message) { }
    }
}
