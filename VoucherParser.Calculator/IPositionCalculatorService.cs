using VoucherParser.Calculator.Models;
using VoucherService.Parsing.Models;

namespace VoucherParser.Calculator
{
    public interface IPositionCalculatorService
    {
        List<VoucherRowInfo> GetPositionInformation(List<Voucher> vouchers);
    }
}