using SysPlan.Application.Entity;

namespace SysPlan.Data.Entities
{
    public class Usuario: Entity
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
