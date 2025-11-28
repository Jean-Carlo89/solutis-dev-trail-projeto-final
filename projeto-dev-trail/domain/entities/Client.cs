
public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Cpf { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    public List<BankAccount> Accounts { get; set; }

    public Client(int Id, string Name, string Email, DateTime? DateOfBirth = null)
    {
        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        this.DateOfBirth = DateOfBirth ?? DateTime.Now;
    }

    public Client(string Name, string Email, string cpf, DateTime? DateOfBirth = null)
    {
        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        this.DateOfBirth = DateOfBirth ?? DateTime.Now;
        this.Cpf = cpf;
    }

    public Client(int Id, string Name, string Email)
    {

        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        // this.DateOfBirth = DateOfBirth;
    }

    public Client(int id, string name, string cpf, string email, DateTime dateOfBirth, List<BankAccount> accounts)
    {
        this.Id = id;
        this.Name = name;
        this.Cpf = cpf;
        this.Email = email;
        this.DateOfBirth = dateOfBirth;
        this.Accounts = accounts ?? new List<BankAccount>();
    }
}