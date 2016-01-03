using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Infrastructure;

namespace GenericRepository
{
    public abstract class EntityBase : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}