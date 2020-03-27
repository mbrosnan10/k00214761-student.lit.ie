using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IModel<EntityInterface, PrimaryKeyType>
    {
        List<EntityInterface> EntityList { get; }

        bool Create(EntityInterface entity);

        List<EntityInterface> GetAll();

        EntityInterface GetByKey(PrimaryKeyType key);

        bool Update(EntityInterface entity);

        bool DeleteByKey(PrimaryKeyType key);
    }
}