using System.ComponentModel.DataAnnotations;

public class DepositInputDto
{
    [Required(ErrorMessage = "O valor do depósito é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor do depósito deve ser positivo.")]
    public decimal Valor { get; set; }




}