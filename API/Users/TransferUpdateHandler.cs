using AlpacaApiClient.Model.Response.Events;
using Core;
using System;
using System.Linq;
using Users.Database.Repositories.Interfaces;

namespace Users
{
	public class TransferUpdateHandler : ITransferUpdateHandler
	{
		private IUserTransfersRepository userTransfersRepository { get; }
		private IUserAccountsRepository userAccountsRepository { get; }

		public TransferUpdateHandler(IUserTransfersRepository userTransfersRepository, IUserAccountsRepository userAccountsRepository)
		{
			this.userTransfersRepository = userTransfersRepository;
			this.userAccountsRepository = userAccountsRepository;
		}

		public void UpdateTransfer(TransferEvent transferEvent)
		{
			var user = userAccountsRepository.GetByAccountId(transferEvent.account_id).UserId;
			var transfers = userTransfersRepository.GetByUserId(user);

			var transfer = transfers.Transfers.First(x => x.TransferId == transferEvent.transfer_id);

			if(transferEvent.status_to == TransferStatusValue.REJECTED || transferEvent.status_to == TransferStatusValue.CANCELED) 
			{
				transfer.CancelledAt = DateTime.Now;
				transfer.Status = transferEvent.status_to;
			}
			else if (transferEvent.status_to == TransferStatusValue.COMPLETE)
			{
				transfer.CompletedAt = DateTime.Now;
				transfer.Status = transferEvent.status_to;
			}
		}
	}
}
