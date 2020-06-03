using LicenseKeyCore.Algorithm.Enums;

namespace LicenseKeyCore.Algorithm
{
	public interface IAlgorithmRijandael
	{
		void GenerateKey(string secretKey, AlgorithmKeyType type);
        byte[] Transform(byte[] data, TransformType type);
	}
}
