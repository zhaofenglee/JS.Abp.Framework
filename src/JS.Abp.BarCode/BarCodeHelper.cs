using BarcodeLib;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JS.Abp.BarCode
{
    public  class BarCodeHelper : IBarCodeHelper
    {
        public Task<byte[]> GetBarCodeByteArrayAsync(string barcodeType, string data)
        {
            return Task.FromResult<byte[]>(GetBarCodeByteArray(barcodeType, data, "left"));
        }

        public Task<byte[]> GetBarCodeByteArrayAsync(string barcodeType, string data, string alignmentPositions = "left")
        {
            return Task.FromResult<byte[]>(GetBarCodeByteArray(barcodeType, data, alignmentPositions));
        }

        /// <summary>
        /// 生成条码
        /// </summary>
        /// <param name="barcodeType">条码类型</param>
        /// <param name="data">条码内容</param>
        /// <returns></returns>
        private Image GetBarCode(string barcodeType, string data, string alignmentPositions)
        {
            if (barcodeType != "QRCode")
            {
                BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                switch(alignmentPositions.ToLower())
                {
                    case "left":
                        b.Alignment = AlignmentPositions.LEFT;
                        break;
                    case "center":
                        b.Alignment = AlignmentPositions.CENTER;
                        break;
                    case "right":
                        b.Alignment = AlignmentPositions.RIGHT;
                        break;
                    default:
                        b.Alignment = AlignmentPositions.LEFT;
                        break;
                }
                
                TYPE type = TYPE.UNSPECIFIED;
                switch (barcodeType)
                {
                    case "UPC-A": type = TYPE.UPCA; break;
                    case "UPC-E": type = TYPE.UPCE; break;
                    case "UPC 2 Digit Ext.": type = TYPE.UPC_SUPPLEMENTAL_2DIGIT; break;
                    case "UPC 5 Digit Ext.": type = TYPE.UPC_SUPPLEMENTAL_5DIGIT; break;
                    case "EAN-13": type = TYPE.EAN13; break;
                    case "JAN-13": type = TYPE.JAN13; break;
                    case "EAN-8": type = TYPE.EAN8; break;
                    case "ITF-14": type = TYPE.ITF14; break;
                    case "Codabar": type = TYPE.Codabar; break;
                    case "PostNet": type = TYPE.PostNet; break;
                    case "Bookland/ISBN": type = TYPE.BOOKLAND; break;
                    case "Code 11": type = TYPE.CODE11; break;
                    case "Code 39": type = TYPE.CODE39; break;
                    case "Code 39 Extended": type = TYPE.CODE39Extended; break;
                    case "Code 39 Mod 43": type = TYPE.CODE39_Mod43; break;
                    case "Code 93": type = TYPE.CODE93; break;
                    case "LOGMARS": type = TYPE.LOGMARS; break;
                    case "MSI": type = TYPE.MSI_Mod10; break;
                    case "Interleaved 2 of 5": type = TYPE.Interleaved2of5; break;
                    case "Interleaved 2 of 5 Mod 10": type = TYPE.Interleaved2of5_Mod10; break;
                    case "Standard 2 of 5": type = TYPE.Standard2of5; break;
                    case "Standard 2 of 5 Mod 10": type = TYPE.Standard2of5_Mod10; break;
                    case "Code 128": type = TYPE.CODE128; break;
                    case "Code 128-A": type = TYPE.CODE128A; break;
                    case "Code 128-B": type = TYPE.CODE128B; break;
                    case "Code 128-C": type = TYPE.CODE128C; break;
                    case "Telepen": type = TYPE.TELEPEN; break;
                    case "FIM": type = TYPE.FIM; break;
                    case "Pharmacode": type = TYPE.PHARMACODE; break;
                    default:
                        break;
                }
                Image img = b.Encode(type, data);
                return img;
            }
            else
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                return qrCode.GetGraphic(20);
            }
          
        }


        /// <summary>
        /// 生成条码并转成字节
        /// </summary>
        /// <param name="barcodeType">条码类型</param>
        /// <param name="data">条码内容</param>
        /// <returns></returns>
        private byte[] GetBarCodeByteArray(string barcodeType, string data, string alignmentPositions)
        {
            var img = GetBarCode(barcodeType, data, alignmentPositions);
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Jpeg);
                return ms.GetBuffer();
            }
        }
    }
}
