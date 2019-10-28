using System;
using System.Collections.Generic;
using System.Text;
using BL = YoutubeJam.BusinessLogic;
using YoutubeJam.Persistence.Entities;
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


        public BL.Creator ParseCreator(Creator item)
        {
            return new BL.Creator()
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                PhoneNumber = item.PhoneNumber
            };
        }
    }
}
