using System.Drawing;
using VoucherService.Parsing.Models;

namespace VoucherParser.Drawing
{
    public interface IDrawingService
    {
        void Draw(List<Voucher> vouchers);
    }
}