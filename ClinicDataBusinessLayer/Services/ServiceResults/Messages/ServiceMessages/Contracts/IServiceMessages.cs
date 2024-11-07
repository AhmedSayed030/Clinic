namespace ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages.Contracts
{
    public interface IServiceMessages
    {
        public IdErrorMessage NotFound(params object[] keyValues);
    }
}
