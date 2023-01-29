using Microsoft.Extensions.DependencyInjection;
using VoucherParser.Calculator;
using VoucherParser.Drawing;
using VoucherService.Parsing;

namespace VoucherParser.Main
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddVoucherServices(this IServiceCollection services)
        {
            services.AddSingleton<IPositionCalculatorService,PositionCalculatorService>();
            services.AddSingleton<IVoucherJsonParserService, VoucherJsonParserService>();
            services.AddSingleton<IDrawingService, DrawingService>();
            services.AddSingleton<IVoucherService, VoucherService>();
            return services;
        }

    }
}