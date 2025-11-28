using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BankSystem.API.model;
using BankSystem.API.Repositories;
using BankSystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace projeto7_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountService _accountService;

        private ITransferService _transferService;
        private readonly IConfiguration _configuration;


        public AccountsController(IAccountService accountService, ITransferService transferService, IConfiguration configuration)
        {
            _accountService = accountService;
            _transferService = transferService;
            _configuration = configuration;
        }

        private string GenerateJwtToken(string username)
        {

            var jwtKey = _configuration["JwtSettings:Key"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            var key = Encoding.ASCII.GetBytes(jwtKey);


            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Cliente"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> GetAccountByNumber(int accountNumber)
        {
            var account = await this._accountService.GetAccountByNumberAsync(accountNumber);


            if (account == null)
            {
                return NotFound($"Conta com o número {accountNumber} não encontrada.");
            }


            return Ok(account);
        }

        [HttpGet("{accountNumber}/history")]
        public async Task<IActionResult> GetAccountHistory(int accountNumber)
        {
            var account = await this._accountService.GetAccountWithTransactionsByNumberAsync(accountNumber);


            if (account == null)
            {
                return NotFound($"Conta com o número {accountNumber} não encontrada.");
            }


            return Ok(account);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAccountsAsync(int numero)
        {
            var account = await this._accountService.GetAllAccountsAsync();


            if (account == null)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }


            return Ok(account);
        }




        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountInputDto AccountDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientExists = await this._accountService.checkIfClientExistsByIdAsync(AccountDto.ClientId);

            if (!clientExists)
            {
                return NotFound($"Cliente com Id {AccountDto.ClientId} não encontrado.");
            }

            await this._accountService.AddNewAccountAsync(AccountDto);

            return Ok();
        }


        [HttpPut("{numero}/deposit")]
        public async Task<IActionResult> Deposit(int numero, [FromBody] DepositInputDto depositeDto)
        {

            if (!ModelState.IsValid || depositeDto.Valor <= 0)
            {

                return BadRequest("O valor do depósito deve ser positivo.");
            }

            var accountExists = await this._accountService.checkIfAccountExistsByNumberAsync(numero);

            if (accountExists == false)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }

            await this._accountService.depositInAccountAsync(numero, depositeDto.Valor);

            return Ok();
        }

        [HttpPut("{numero}/withdraw")]
        public async Task<IActionResult> Withdraw(int numero, [FromBody] WithdrawInputDto withdrawDto)
        {


            var accountExists = await this._accountService.checkIfAccountExistsByNumberAsync(numero);

            if (accountExists == false)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }

            await this._accountService.withdrawFromAccountAsync(numero, withdrawDto.Valor);

            return Ok();
        }

        [HttpDelete("{numero}")]
        public async Task<IActionResult> DeleteConta(int numero)
        {
            var accountsExists = await this._accountService.checkIfAccountExistsByNumberAsync(numero);

            if (accountsExists == false)
            {
                return NotFound($"Conta com o número {numero} não encontrada.");
            }

            await this._accountService.deleteAccountAsync(numero);

            return NoContent();
        }


        [HttpPut("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferInputDto transferDto)
        {

            Console.WriteLine("Entrou no controller de transferência");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (transferDto.Valor <= 0)
            {
                return BadRequest("O valor da transferência deve ser positivo.");
            }
            if (transferDto.SourceAccountNumber == transferDto.DestinationAccountNumber)
            {
                return BadRequest("As contas de origem e destino não podem ser as mesmas.");
            }

            try
            {

                await _transferService.ExecuteTransferAsync(
                    transferDto.SourceAccountNumber,
                    transferDto.DestinationAccountNumber,
                    transferDto.Valor
                );

                return Ok(new { Message = "Transferência realizada com sucesso." });
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }


    }
}