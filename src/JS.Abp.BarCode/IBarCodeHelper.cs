using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JS.Abp.BarCode
{
    public interface IBarCodeHelper
    {
        /// <summary>
        /// 生成条码并转成字节
        /// </summary>
        /// <param name="barcodeType">条码类型</param>
        /// <param name="data">条码内容</param>
        /// <returns></returns>
        Task<byte[]> GetBarCodeByteArrayAsync(string barcodeType, string data);

        /// <summary>
        /// 生成条码并转成字节
        /// </summary>
        /// <param name="barcodeType">条码类型</param>
        /// <param name="data">条码内容</param>
        /// <param name="alignmentPositions">条码生成位置(center,left,right)</param>
        /// <returns></returns>
        Task<byte[]> GetBarCodeByteArrayAsync(string barcodeType, string data,string alignmentPositions);
    }
}
