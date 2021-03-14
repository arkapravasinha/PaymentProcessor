using AutoMapper;
using PaymentProcessor.API.Model;
using PaymentProcessor.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProcessor.API
{
    public class ModelMapperProfile: Profile
    {
        public ModelMapperProfile()
        {
            CreateMap<PaymentModel, Payment>()
                  .ForMember(dest =>
                dest.CreditCardNumber, options =>
                 options.MapFrom(source => source.CreditCardNumber))
                  .ForMember(dest =>
                dest.CardHolder, options =>
                 options.MapFrom(source => source.CardHolder))
                  .ForMember(dest =>
                dest.ExpirationDate, options =>
                 options.MapFrom(source => source.ExpirationDate))
                   .ForMember(dest =>
                dest.SecurityCode, options =>
                 options.MapFrom(source => source.SecurityCode))
                   .ForMember(dest =>
                dest.Amount, options =>
                 options.MapFrom(source => source.Amount))
                   .ReverseMap();
        }
    }
}
