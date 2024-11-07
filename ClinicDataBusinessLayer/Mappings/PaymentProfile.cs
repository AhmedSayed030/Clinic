using AutoMapper;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Patient;
using ClinicDataBusinessLayer.DTOs.Payment;

namespace ClinicDataBusinessLayer.Mappings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateProjection<Payment, PaymentDto>();

            CreateMap<Payment, PaymentDto>();

            CreateMap<PaymentDtoAdd, Payment>();

            CreateMap<PaymentDtoUpdaet, Payment>();
        }

    }

}
