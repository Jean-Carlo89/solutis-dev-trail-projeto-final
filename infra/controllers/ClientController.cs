using System.Net;
using BankSystem.API.Dtos;
using BankSystem.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService service;
    public ClientController(IClientService service)
    {
        this.service = service;
    }


    [HttpPost]

    public async Task<IActionResult> CreateClient([FromBody] ClientInputDto ClientDto)
    {

        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var createdClient = await service.AddNewClientAsync(ClientDto);

            return Ok(createdClient);
        }
        catch (ArgumentException ex)
        {
            var errorDetails = new
            {
                Status = (int)HttpStatusCode.BadRequest,
                Error = "ValidationFailed",
                Messages = ex.Message.Split('\n')
            };


            return BadRequest(errorDetails);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = "InternalServerError", Message = "Ocorreu um erro inesperado no servidor." });
        }


    }

    [HttpGet("/id/{id}")]
    public async Task<ActionResult<ClientOutputDto>> GetClient(int id)
    {


        var clientDSto = await service.GetClientByIdAsync(id);

        return Ok(clientDSto);
    }

    [HttpGet("/cpf/{cpf}")]
    public async Task<ActionResult<ClientOutputDto>> GetClientByCpf(string cpf)
    {


        var clientDSto = await service.GetClientByCpfAsync(cpf);

        return Ok(clientDSto);
    }


}