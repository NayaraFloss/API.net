using System.Collections.Generic;
using ApiClientes.Database.Models;
using ApiClientes.Services;
using ApiClientes.Services.DTOs;
using ApiClientes.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ApiClientes.Services.DTOs.ClienteDTO;

namespace ApiClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesService _service;

        public ClientesController(ClientesService service)
        {
            _service = service;
        }
        // adiciona novos clientes
        [HttpPost]
        public ActionResult<ClienteDTO> Adicionar([FromBody] CriarClienteDTO body)
        {
            try
            {
                var Response = _service.Criar(body);
                return Ok(Response); // 200
            }
            catch (BadRequestException B)
            {
                return BadRequest(B.Message); // 400
            }
            catch (System.Exception E)
            {
                return StatusCode(500, E.Message); // 500
            }
        }

        //busca todos os clientes cadastrados
        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> BuscarTodos()
        {
            try
            {
                var response = _service.BuscarTodos();
                return Ok(response);
            }
            catch (System.Exception E)
            {
                return StatusCode(500, E.Message);
            }
        }

        // busca por id os clientes
        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> BuscarPorId(int id)
        {
            try
            {
                var response = _service.BuscarPorId(id);
                return Ok(response);
            }
            catch (System.Exception E)
            {
                return StatusCode(500, E.Message);
            }
        }

        // atualiza o cliente selecionado
        [HttpPut("{id}")]
        public ActionResult<ClienteDTO> Atualizar(int id, [FromBody] AtualizarClienteDTO body)
        {
            try
            {
                var response = _service.Atualizar(id, body);
                return Ok(response);
            }
            catch (System.Exception E)
            {
                return StatusCode(500, E.Message);
            }
        }

        // DELETE o cliente selecionado
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _service.Deletar(id);
                return NoContent(); // 204
            }
            catch (System.Exception E)
            {
                return StatusCode(500, E.Message);
            }
        }
    }
}
