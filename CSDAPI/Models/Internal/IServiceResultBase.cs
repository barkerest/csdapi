namespace CSDAPI.Models.Internal
{
	internal interface IServiceResultBase
	{
		string Message { get; }
		int Code { get; }
		string Status { get; }
	}
}