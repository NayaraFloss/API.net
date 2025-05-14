using System;

namespace ApiClientes.Controllers
{
    public class AtualizarClienteDTO
    {
        public string Nome { get; internal set; }
        public string Telefone { get; internal set; }
        public DateTime? Nascimento { get; internal set; }
        public string Documento { get; internal set; }
        public DateTime Alteradoem { get; internal set; }
        public DateTime Criadoem { get; internal set; }
        public int Tipodoc { get; internal set; }
    }
}