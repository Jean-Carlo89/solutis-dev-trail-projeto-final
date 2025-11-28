using System.Collections.Generic;

namespace BankSystem.API.Dtos
{
    public class ClientOutputDto
    {

        public int Id { get; set; }


        public string Nome { get; set; }


        public string Email { get; set; }

        public DateTime DOB { get; set; }


        public DateTime DateOfBirth { get; set; }

        public List<AccountOutputDto> Contas { get; set; }


    }
}