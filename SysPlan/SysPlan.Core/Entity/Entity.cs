using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysPlan.Core.Entity
{
    public abstract class Entity : IEquatable<Entity>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }
        [Required()]
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public bool Equals(Entity other)
        {
            if (other == null) return false;

            if (other.Codigo == null)
            {
                return false;
            }
            // Se o Id for igual ao valor default de TId (0 para inteiros ou longos), 
            // estamos comparando duas entidades não persistidas.
            // Nesse caso devolvemos a verificação de referencia em memória
            if (other.Codigo.Equals(default(Guid)) && Codigo.Equals(default(Guid)))
                return ReferenceEquals(this, other);

            // Verifica se as entidades são do mesmo tipo e tem o mesmo ID. Nesse caso são iguais
            return (GetType() == other.GetType() && Codigo.Equals(other.Codigo));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        public override int GetHashCode()
        {
            if (Codigo == null)
                return 0;
            else
                return Codigo.GetHashCode();
        }
    }
}
