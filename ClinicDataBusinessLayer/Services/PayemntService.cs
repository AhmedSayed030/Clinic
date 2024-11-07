using AutoMapper;
using ClinicDataAccessLayer.Data;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Payment.Contracts;
using ClinicDataBusinessLayer.Extensions;
using ClinicDataBusinessLayer.Services.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults;
using ClinicDataBusinessLayer.Services.ServiceResults.Factories;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicDataBusinessLayer.Services
{
    public class PaymentService : ServiceBase, IPaymentService, IScopedService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PaymentService(AppDbContext context,
            ServiceResultHandlerFactory serviceResultHandlerFactory,
               IMapper mapper,
               ILogger<PaymentService> logger) : base(serviceResultHandlerFactory, mapper, logger)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
            where TDtoResult : class, IPaymentDto
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var payments = await _context.Payments
                                            .OrderBy(p => p.Id)
                                            .ToDtosAsync<TDtoResult>(_mapper.ConfigurationProvider);

                return serviceResult.Success(payments);

            }, nameof(GetAll));
        }
        public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id)
            where TDtoResult : class, IPaymentDto
        {

            return await ExecuteOperationAsync<TDtoResult, PaymentServiceMessages>(async serviceResult =>
            {
                var payment = await _context.Payments
                                           .Where(p => p.Id == Id)
                                           .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

                if (payment == null)
                    return serviceResult.NotFound<TDtoResult>(Id);

                return serviceResult.Success(payment);

            }, nameof(GetById));
        }
        public async Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IPaymentDto
            where TDtoAdd : class, IPaymentDtoAdd
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var payment = (await _context.AddAsync(_mapper.Map<Payment>(dtoAdd))).Entity;

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Payment, TDtoAdd, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(payment)) :
                        await GetById<TDtoResult>(payment.Id);

            }, nameof(Add));
        }
        public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
             where TDtoResult : class, IPaymentDto
             where TDtoUpdate : class, IPaymentDtoUpdate
        {
            return await ExecuteOperationAsync<TDtoResult, PaymentServiceMessages>(async serviceResult =>
            {
                var payment = await _context.Payments
                                           .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                                           .FirstOrDefaultAsync(p => p.Id == dtoUpdae.Id);

                if (payment == null)
                    return serviceResult.NotFound<TDtoResult>(dtoUpdae.Id);

                _mapper.Map(dtoUpdae, payment);

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Payment, TDtoUpdate, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(payment)) :
                        await GetById<TDtoResult>(payment.Id);

            }, nameof(Update));
        }

        public async Task<IServiceResult> Delete(int Id)
        {
            return await ExecuteOperationAsync<PaymentServiceMessages>(async serviceResult =>
            {
                var payment = await _context.Payments.FirstOrDefaultAsync(d => d.Id == Id);

                if (payment == null)
                    return serviceResult.NotFound(Id);

                _context.Remove(payment);

                await _context.SaveChangesAsync();

                return serviceResult.Success();

            }, nameof(Delete));
        }
    }
}
