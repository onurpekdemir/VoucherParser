using System.Text.Json;
using VoucherParser.Parsing.Exceptions;
using VoucherService.Parsing.Models;

namespace VoucherService.Parsing
{
    public class VoucherJsonParserService : IVoucherJsonParserService
    {
        public List<Voucher> Parse()
        {
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), Constants.ResponseJsonFileName);
            string jsonString = File.ReadAllText(jsonFilePath);

            ArgumentNullException.ThrowIfNull(jsonString);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var vouchers = JsonSerializer.Deserialize<List<Voucher>>(jsonString, options);

            if(!vouchers.Any())
            {
                throw new VoucherElementNotFoundException("No element found in the parsed list of json data");
            }

            return vouchers;
        }
    }
}
