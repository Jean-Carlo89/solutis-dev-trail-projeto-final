using BankSystem.API.model;

public class ClientModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public string Cpf { get; set; }

    public DateTime DateOfBirth { get; set; }


    public List<BankAccountModel> Accounts { get; set; }
}