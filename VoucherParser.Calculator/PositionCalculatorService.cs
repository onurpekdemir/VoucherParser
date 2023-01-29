using System.Drawing;
using VoucherParser.Calculator.Models;
using VoucherService.Parsing.Models;

namespace VoucherParser.Calculator
{
    public class PositionCalculatorService : IPositionCalculatorService
    {
        public List<VoucherRowInfo> GetPositionInformation(List<Voucher> vouchers)
        {
            int currentRow = 0;
            int currentTopY = 0;
            int currentBottomY = 0;
            var wordInfoList = new List<VoucherRowInfo>();

            foreach (var v in vouchers.Skip(1).OrderBy(w => w.BoundingPoly.Vertices[0].Y).ThenBy(w => w.BoundingPoly.Vertices[0].X))
            {
                VoucherRowInfo voucherRowInfo = new VoucherRowInfo();
                voucherRowInfo.Description = v.Description;

                var y1 = v.BoundingPoly.Vertices[0].Y; //top left Y --
                var y4 = v.BoundingPoly.Vertices[3].Y; //bottom left Y --


                if (y1 <= currentBottomY && y1 >= currentTopY)
                {
                    voucherRowInfo.Row = currentRow;
                }
                else
                {
                    voucherRowInfo.Row = ++currentRow;
                    currentBottomY = y4;
                    currentTopY = y1;
                }

                wordInfoList.Add(voucherRowInfo);
            }

            var results = from w in wordInfoList
                          group w by w.Row into g
                          select new VoucherRowInfo { Row = g.Key, Description = string.Join(" ", g.Select(s => s.Description)) };

            return results.ToList();
        }
    }
}