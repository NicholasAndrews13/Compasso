using System;
using System.ComponentModel.DataAnnotations;

namespace Auditoria
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool LoginAtual { get; set; }

    }
}