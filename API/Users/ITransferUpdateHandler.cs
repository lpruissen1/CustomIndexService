using AlpacaApiClient.Model.Response.Events;

namespace Users
{
	public interface ITransferUpdateHandler
	{
		public void UpdateTransfer(TransferEvent transferEvent);
	}
}
