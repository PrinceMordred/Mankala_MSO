namespace Mankala;

public interface ILogger : IObserver<string>
{
	GameLogics.GameLogic GameLogic { get; set; }
}

public class Logger : ILogger, IDisposable
{
	private const string FileName = "MankalaLog.txt";
	private readonly string _logDestination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + '\\' + FileName;
	private readonly StreamWriter logWriter;
	
	public Logger()
	{
		logWriter           = new StreamWriter(_logDestination);
		logWriter.AutoFlush = true;
	}
	
	public void OnCompleted()
	{
		throw new NotImplementedException();
	}

	public void OnError(Exception error) =>
		_Log($"[ERROR]: {error.Message}\n    Error occured in {error.Source}");
	public void OnNext(string value) => _Log(value);

	private void _Log(string value)
	{
		var time = DateTime.Now.ToString("G");
		logWriter.WriteLine($"[{time}]: {value}");
	}
	
	public void Dispose()
	{
		logWriter.Dispose();
		GC.SuppressFinalize(this);
	}
}