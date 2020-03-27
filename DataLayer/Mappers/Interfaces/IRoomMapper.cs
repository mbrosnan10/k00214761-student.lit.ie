using BusinessEntities;

namespace DataLayer
{
    public interface IRoomMapper : IMapper<IRoom, int>
    {
        IRoom GetByKey(int RoomId);

        bool Insert(IRoom entity);

        bool SetAvalible(int id, bool value);
    }
}