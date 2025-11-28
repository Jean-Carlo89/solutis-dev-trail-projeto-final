
using System.ComponentModel.DataAnnotations;

public class AccountInputDto

{



    [Range(0, 1000000, ErrorMessage = "O saldo inicial deve ser positiv.")]
    public decimal Saldo { get; set; }

    [Required(ErrorMessage = "O nome do titular é obrigatório.")]
    public string Titular { get; set; }

    [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
    public int ClientId { get; set; }

    [Required(ErrorMessage = "O tipo de conta é obrigatório.")]

    [EnumDataType(typeof(AccountType), ErrorMessage = "O tipo de conta é inválido. Valores permitidos: {0}")]
    public AccountType Tipo { get; set; }

}
