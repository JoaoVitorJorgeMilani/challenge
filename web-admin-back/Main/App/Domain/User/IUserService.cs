using MongoDB.Bson;

namespace Main.App.Domain.User
{

    public interface IUserService 
    {
        public bool Add(UserEntity bike);
        public List<UserDto> Get(UserFilterModel filter);
        public UserDto Login(string userName);
        public List<UserEntity> GetUsersById(List<ObjectId> userIds);

    }

}