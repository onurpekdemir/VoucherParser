using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherParser.Drawing;
using VoucherService.Parsing;

namespace VoucherParser.Main
{
    public class VoucherService : IVoucherService
    {
        private readonly IDrawingService _drawingService;
        private readonly IVoucherJsonParserService _voucherJsonParserService;

        public VoucherService(IDrawingService drawingService, IVoucherJsonParserService voucherJsonParserService)
        {
            _drawingService = drawingService;
            _voucherJsonParserService = voucherJsonParserService;
        }

        public void CreateVoucher()
        {
            try
            {
                var vouchers = _voucherJsonParserService.Parse();
                _drawingService.Draw(vouchers);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
