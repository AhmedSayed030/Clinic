namespace ClinicDataBusinessLayer.DTOs.Payment.Contracts;

public interface IPaymentDtoAdd : IPaymentDtoBase, IDtoAdd
{
    public int AppointmentId { get; set; }
    public string Method { get; set; }
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public DateTime Date { get; set; }
}