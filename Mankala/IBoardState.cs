namespace Mankala;

public interface IBoardState
{
	public byte BaseHoleP1();
	public byte BaseHoleP2();
	public IEnumerable<byte> HolesP1();
	public IEnumerable<byte> HolesP2();
}