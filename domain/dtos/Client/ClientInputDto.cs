using System.ComponentModel.DataAnnotations;

namespace BankSystem.API.Dtos;

public class ClientInputDto
{





    [Required(ErrorMessage = "O nome do titular é obrigatório.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF do titular é obrigatório.")]
    public string Cpf { get; set; } = string.Empty;




    public string email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo de conta é obrigatório.")]

    [EnumDataType(typeof(AccountType), ErrorMessage = "O tipo de conta é inválido. Valores permitidos: {0}")]
    public AccountType TipoDeConta { get; set; }
}