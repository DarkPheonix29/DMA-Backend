using QRCoder;

namespace DMA_BLL
{
	public class QrCodeService
	{
		public byte[] GenerateQrCode(string url)
		{
			using var qrGenerator = new QRCodeGenerator();
			using var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
			using var qrCode = new PngByteQRCode(qrCodeData);
			return qrCode.GetGraphic(20);
		}
	}
}
