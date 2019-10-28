using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeJam.Persistence
{
    public interface IMapper
    {
        public BusinessLogic.Creator ParseCreator(Entities.Creator creator);
        public Entities.Creator ParseCreator(BusinessLogic.Creator creator);
    }
}
