using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherParser.Drawing
{
    internal class Constants
    {
        /// <summary>
        /// Output path of the image which is created by the drawing service
        /// </summary>
        internal const string OutputFileName = "voucher.bmp";

        /// <summary>
        /// Bitmap expansion in X coordinate over the voucher area to provide space for the left bar showing the rows
        /// </summary>
        internal const int BitmapExpandX = 20;

        /// <summary>
        /// Bitmap expansion in Y coordinate over the voucher area to provide space for the header
        /// </summary>
        internal const int BitmapExpandY = 20;

        /// <summary>
        /// Default padding for the working area of bitmap
        /// </summary>
        internal const int BitmapPadding = 5;

    }
}
