using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Transferencia.Domain.Aggregates;
using Transferencia.Domain.Core.Message;
using Transferencia.Domain.Enums;
using Transferencia.Domain.Exceptions;
using Transferencia.Domain.Repositories;
using Transferencia.Infra.CrossCutting.Http.Helper;

namespace Transferencia.Infra.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public async Task Transfer(string accountNumber, float value, ETransferType type)
        {
            var data = JsonConvert.SerializeObject(new
            {
                accountNumber = accountNumber,
                value = value,
                type = type.ToString()
            });

            var content = new StringContent(data, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(ApiRoutes.Account.Post, content);

                if (!result.IsSuccessStatusCode) 
                    throw new TransferNotCompletedException(result.StatusCode.ToString());
            }            
        }

        public async Task<Account> GetByAccountNumber(string accountNumber)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(ApiRoutes.Account.GetByNumber(accountNumber));

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK: 
                        return JsonConvert.DeserializeObject<Account>(
                            await response.Content.ReadAsStringAsync());

                    case System.Net.HttpStatusCode.NotFound: 
                        throw new AccountNotFoundException(
                            DefaultMessages.AccountNotFound(accountNumber));

                    default: 
                        throw new UnexpectedConnectionErrorException(
                            DefaultMessages.UnexpectedAccountApiError(response.StatusCode.ToString()));
                }
            }
        }
    }
}
