using YoutubeJam.Persistence.Entities;
using BL = YoutubeJam.BusinessLogic;

namespace YoutubeJam.Persistence
{
    /// <summary>
    /// Class that maps business logic items to db objects
    /// </summary>
    public class DBMapper : IMapper
    {
        public Creator ParseCreator(BL.Creator creator)
        {
            return new Creator()
            {
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                PhoneNumber = creator.PhoneNumber
            };
        }

        public BL.Creator ParseCreator(Creator creator)
        {
            return new BL.Creator()
            {
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                PhoneNumber = creator.PhoneNumber
            };
        }
    }
}
