using Microsoft.Extensions.DependencyInjection;
using VoucherParser.Main;

var serviceProvider = new ServiceCollection();
serviceProvider.AddVoucherServices();
ServiceProvider prov = serviceProvider.BuildServiceProvider();

var voucher = prov.GetRequiredService<IVoucherService>();
voucher.CreateVoucher();
Console.WriteLine("Output file has been created in the same location in the folder where VoucherParser.App project was created");
Console.ReadLine();

