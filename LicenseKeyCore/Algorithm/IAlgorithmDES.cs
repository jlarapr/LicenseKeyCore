using LicenseKeyCore.Algorithm.Enums;

namespace LicenseKeyCore.Algorithm
{
	public interface IAlgorithmDes
	{
		void GenerateKey(string secretKey, AlgorithmKeyType type);
        byte[] Transform(byte[] data, TransformType type);

	}
}
