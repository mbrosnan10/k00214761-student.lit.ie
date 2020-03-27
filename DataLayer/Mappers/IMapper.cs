using System.Collections.Generic;

namespace DataLayer
{
    public interface IMapper<EntityInterface, PrimaryKeyType>
    {
        bool Insert(EntityInterface entity);

        List<EntityInterface> GetAll();

        EntityInterface GetByKey(PrimaryKeyType key);

        bool Update(EntityInterface entity);

        bool DeleteByKey(PrimaryKeyType key);
    }
}