using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using VoucherParser.Calculator;
using VoucherParser.Calculator.Models;
using VoucherService.Parsing.Models;

namespace VoucherParser.Drawing
{
    public class DrawingService : IDrawingService
    {
        private readonly IPositionCalculatorService _positionCalculatorService;
        private readonly string voucherOutputPath = Path.Combine(Directory.GetCurrentDirectory().Split("\\bin")[0], Constants.OutputFileName);

        public DrawingService(IPositionCalculatorService positionCalculatorService)
        {
            _positionCalculatorService = positionCalculatorService;
        }
        public void Draw(List<Voucher> vouchers)
        {
            var vocuherRowInfos = _positionCalculatorService.GetPositionInformation(vouchers);
            var voucherBoundaries = vouchers[0].BoundingPoly.Vertices;
            DrawLayout(voucherBoundaries);
            DrawRows(voucherBoundaries, vocuherRowInfos);
        }

        private void DrawLayout(Point[] voucherBoundaries)
        {
            int x1 = voucherBoundaries[0].X;
            int y1 = voucherBoundaries[0].Y;
            int x2 = voucherBoundaries[1].X;
            int y2 = voucherBoundaries[1].Y;
            int x3 = voucherBoundaries[2].X;
            int y3 = voucherBoundaries[2].Y;
            int x4 = voucherBoundaries[3].X;
            int y4 = voucherBoundaries[3].Y;

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(voucherOutputPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var bitmap = new Bitmap(x3 + Constants.BitmapExpandX, y3 + Constants.BitmapExpandY))
                    {
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            using (SolidBrush brush = new SolidBrush(Color.White))
                            {
                                g.FillRectangle(brush, 0, 0, x3 + Constants.BitmapExpandX, y3 + Constants.BitmapExpandY);
                            }

                            //Green background for the header
                            using (SolidBrush brush = new SolidBrush(Color.PaleGreen))
                            {
                                Rectangle rect = new Rectangle(Constants.BitmapPadding, Constants.BitmapPadding, x2 - Constants.BitmapPadding, y2 - Constants.BitmapPadding);
                                g.FillRectangle(brush, rect);
                            }

                            Pen blackPen = new Pen(Color.Black, 1);

                            //Voucher area
                            g.DrawLine(blackPen, x1, y1, x2, y2);
                            g.DrawLine(blackPen, x2, y2, x3, y3);
                            g.DrawLine(blackPen, x3, y3, x4, y4);
                            g.DrawLine(blackPen, x1, y1, x4, y4);

                            //Line bar
                            g.DrawLine(blackPen, Constants.BitmapPadding, y1, Constants.BitmapPadding, y4);
                            g.DrawLine(blackPen, Constants.BitmapPadding, y1, x1, y1);
                            g.DrawLine(blackPen, Constants.BitmapPadding, y4, x4, y4);

                            //Header 
                            g.DrawLine(blackPen, Constants.BitmapPadding, y1, Constants.BitmapPadding, Constants.BitmapPadding);
                            g.DrawLine(blackPen, Constants.BitmapPadding, Constants.BitmapPadding, x2, Constants.BitmapPadding);
                            g.DrawLine(blackPen, x2, Constants.BitmapPadding, x2, y2);
                            g.DrawLine(blackPen, x1, y1, x1, Constants.BitmapPadding);

                            RectangleF rectLine = new RectangleF(Constants.BitmapPadding + Constants.BitmapPadding, (y1 - Constants.BitmapPadding) / 2, 90, 50);
                            RectangleF rectText = new RectangleF(x1 + Constants.BitmapPadding, (y1 - Constants.BitmapPadding) / 2, 90, 50);

                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.DrawString("line", new Font("Tahoma", 8, FontStyle.Bold), Brushes.Black, rectLine);
                            g.DrawString("text", new Font("Tahoma", 8, FontStyle.Bold), Brushes.Black, rectText);

                            g.Flush();

                            bitmap.Save(memory, ImageFormat.Jpeg);
                            byte[] bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                            blackPen.Dispose();

                        }
                    }
                }

            }
        }

        private void DrawRows(Point[] voucherBoundaries, List<VoucherRowInfo> voucherRowInfos)
        {
            int width = Math.Abs(voucherBoundaries[1].X - voucherBoundaries[0].X);
            int height = Math.Abs(voucherBoundaries[1].Y - voucherBoundaries[2].Y);
            int rowHeight = height / voucherRowInfos.Count;
            int offset = 0;
            Point startPoint = voucherBoundaries[0];

            using (var ms = new MemoryStream(File.ReadAllBytes(voucherOutputPath)))
            {
                using (var bitmap = new Bitmap(ms))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        foreach (VoucherRowInfo voucherRowInfo in voucherRowInfos)
                        {
                            RectangleF recDescription = new RectangleF(startPoint.X, startPoint.Y + offset, width, rowHeight);
                            RectangleF recRow = new RectangleF((startPoint.X - Constants.BitmapPadding) / 2, startPoint.Y + offset, width, rowHeight);
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.DrawString(voucherRowInfo.Description, new Font("Tahoma", 8, FontStyle.Bold), Brushes.Black, recDescription);
                            g.DrawString(voucherRowInfo.Row.ToString(), new Font("Tahoma", 8, FontStyle.Bold), Brushes.Black, recRow);

                            g.Flush();
                            offset += rowHeight;
                        }

                        bitmap.Save(voucherOutputPath, ImageFormat.Jpeg);


                    }
                }
            }
        }
    }
}