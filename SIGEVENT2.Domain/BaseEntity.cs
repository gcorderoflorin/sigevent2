using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEVENT2.Domain
{
    /// <summary>
    /// Base interface for entities with generic primary key named "Id".
    /// </summary>
    public abstract class BaseEntity<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }
    }

    /// <summary>
    /// Base interface for entities with integer primary key named "Id".
    /// </summary>
    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
