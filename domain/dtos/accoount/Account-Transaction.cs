
using System.ComponentModel.DataAnnotations;

public class AccountOutputWithTransactionsDto

{



    public AccountOutputDto DetalhesDaConta { get; set; }
    public List<TransactionOutputDto> Transacoes { get; set; }

}

