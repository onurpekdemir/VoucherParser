using VoucherService.Parsing.Models;

namespace VoucherService.Parsing
{
    public interface IVoucherJsonParserService
    {
        List<Voucher> Parse();
    }
}