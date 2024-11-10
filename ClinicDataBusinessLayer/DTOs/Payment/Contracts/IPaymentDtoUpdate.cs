namespace ClinicDataBusinessLayer.DTOs.Payment.Contracts;

public interface IPaymentDtoUpdate : IPaymentDtoBase, IDtoUpdate
{
    public int Id { get; set; }
}