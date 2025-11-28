using System.ComponentModel.DataAnnotations;

public class TransferInputDto
{
    [Required(ErrorMessage = "O número da conta de origem é obrigatório.")]
    public int SourceAccountNumber { get; set; }

    [Required(ErrorMessage = "O número da conta de destino é obrigatório.")]
    public int DestinationAccountNumber { get; set; }

    [Required(ErrorMessage = "O valor da transferência é obrigatório.")]
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
    public decimal Valor { get; set; }
}