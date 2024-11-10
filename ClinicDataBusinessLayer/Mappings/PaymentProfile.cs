namespace ClinicDataBusinessLayer.Mappings;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateProjection<Payment, PaymentDto>();

        CreateMap<Payment, PaymentDto>();

        CreateMap<PaymentDtoAdd, Payment>();

        CreateMap<PaymentDtoUpdate, Payment>();
    }

}