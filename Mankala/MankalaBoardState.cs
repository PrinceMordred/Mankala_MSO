namespace Mankala;

public struct MankalaBoardState : IBoardState
{
	private readonly byte _baseHoleP1;
	private readonly byte _baseHoleP2;
	private readonly byte[] _holesP1;
	private readonly byte[] _holesP2;
	
	public MankalaBoardState(byte baseHoleP1, byte baseHoleP2, byte[] holesP1, byte[] holesP2)
	{
		_baseHoleP1 = baseHoleP1;
		_baseHoleP2 = baseHoleP2;
		_holesP1 = holesP1;
		_holesP2 = holesP2;
	}

	public byte BaseHoleP1() => _baseHoleP1;
	public byte BaseHoleP2() => _baseHoleP2;
	public IEnumerable<byte> HolesP1() => _holesP1;
	public IEnumerable<byte> HolesP2() => _holesP2;
}