namespace CSDAPI.Implementations.Internal
{
	internal interface IServiceResultBase
	{
		string Message { get; }
		int Code { get; }
		string Status { get; }
	}
}