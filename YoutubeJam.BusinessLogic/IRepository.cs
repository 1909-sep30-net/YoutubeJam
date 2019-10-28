using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeJam.BusinessLogic
{
    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepository
    {
        public List<Creator> GetCreators();

        public void AddCreator(Creator c);
    }
}
