using System.ComponentModel.DataAnnotations;

public class WithdrawInputDto
{
    [Required(ErrorMessage = "O valor do saque é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor do saque deve ser positivo.")]
    public decimal Valor { get; set; }
}