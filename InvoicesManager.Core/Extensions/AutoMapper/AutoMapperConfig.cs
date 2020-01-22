using AutoMapper;
using InvoicesManager.Core.Entities;
using InvoicesManager.Core.Invoices.Models;
using InvoicesManager.Core.Products.Models;
using InvoicesManager.Core.Users.Models;

namespace InvoicesManager.Core.Extensions.AutoMapper
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize() => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Invoice, InvoiceDto>();
            cfg.CreateMap<Product, ProductDto>();
            cfg.CreateMap<User, UserDto>();
        })
            .CreateMapper();
    }
}

