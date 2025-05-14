using System;
using System.Collections.Generic;
using System.Linq;
using ApiClientes.Controllers;
using ApiClientes.Database.Models;
using ApiClientes.Services.DTOs;
using ApiClientes.Services.Parsers;
using ApiClientes.Services.Validations;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Services
{
    public class ClientesService
    {
        private readonly ClientesContext _dbcontext;

        public ClientesService(ClientesContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public ClienteDTO Criar(CriarClienteDTO dto)
        {
            ClienteValidation.ValidarCriarCliente(dto);

            TbCliente novoCliente = ClienteParser.ToTbCliente(dto);

            novoCliente.Criadoem = DateTime.UtcNow;
            novoCliente.Alteradoem = DateTime.UtcNow;

            _dbcontext.TbClientes.Add(novoCliente);
            _dbcontext.SaveChanges();

            return ClienteParser.ToClienteDTO(novoCliente);
        }

        internal object BuscarTodos()
        {
            var clientes = _dbcontext.TbClientes
                .Include(c => c.TbEnderecos)
                .ToList();

            return clientes.Select(c => ClienteParser.ToClienteDTO(c)).ToList();
        }

        internal object BuscarPorId(int id)
        {
            var cliente = _dbcontext.TbClientes
                .Include(c => c.TbEnderecos)
                .FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            return ClienteParser.ToClienteDTO(cliente);
        }

        internal object Atualizar(int id, AtualizarClienteDTO body)
        {
            var cliente = _dbcontext.TbClientes.Find(id);

            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            cliente.Nome = body.Nome;
            cliente.Nascimento = body.Nascimento;
            cliente.Telefone = body.Telefone;
            cliente.Documento = body.Documento;
            cliente.Tipodoc = body.Tipodoc;
            cliente.Alteradoem = DateTime.UtcNow;

            _dbcontext.SaveChanges();

            return ClienteParser.ToClienteDTO(cliente);
        }

        internal void Deletar(int id)
        {
            var cliente = _dbcontext.TbClientes.Find(id);

            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            _dbcontext.TbClientes.Remove(cliente);
            _dbcontext.SaveChanges();
        }
    }
}
