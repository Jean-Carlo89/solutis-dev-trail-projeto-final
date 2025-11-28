using BankSystem.API.model;
using BankSystem.Domain.Entities;
using System.Linq;

namespace BankSystem.API.Mappers
{
    public static class TransactionMapper
    {

        public static TransactionModel ToModel(Transaction entity)
        {
            if (entity == null)
            {

                return null;
            }

            return new TransactionModel
            {
                Id = entity.Id,
                Type = entity.Type,
                Amount = entity.Amount,
                CreatedAt = entity.CreatedAt,


                SourceAccountId = entity.SourceAccountId,
                DestinationAccountId = entity.DestinationAccountId,


            };
        }


        public static List<TransactionModel> ToModelList(List<Transaction> entities)
        {
            if (entities == null)
            {
                return new List<TransactionModel>();
            }
            return entities.Select(ToModel).ToList();
        }


        public static Transaction ToEntity(TransactionModel model)
        {
            if (model == null)
            {
                return null;
            }

            var transaction = new Transaction(model);
            return transaction;


        }


        public static List<Transaction> ToEntityList(List<TransactionModel> models)
        {

            if (models == null)
            {
                return null;
            }

            var returnList = models
                            .Select(model => ToEntity(model))
                            .ToList();
            return returnList;
        }

        public static TransactionOutputDto ToOutputDto(Transaction model)
        {
            if (model == null)
            {
                return null;
            }


            return new TransactionOutputDto
            {
                TipoDeTransacao = model.Type,
                Valor = model.Amount,
                Data = model.CreatedAt,



                IdContaOriginaria = (int)model.SourceAccountId,

                IdContaDestino = (int)model.DestinationAccountId
            };
        }

        public static List<TransactionOutputDto> ToOutputDtoList(List<Transaction> entities)
        {
            if (entities == null)
            {
                return new List<TransactionOutputDto>();
            }


            return entities.Select(ToOutputDto).ToList();
        }
    }
}
