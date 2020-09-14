using SysPlan.Core.Entity;

namespace SysPlan.Data.Entities
{
    public class Usuario: EntityBase
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
