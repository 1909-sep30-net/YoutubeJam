using System.Collections.Generic;
using System.Linq;
using YoutubeJam.Persistence.Entities;
using BL = YoutubeJam.BusinessLogic;

namespace YoutubeJam.Persistence
{
    public class Repository : BL.IRepository
    {
        private readonly YouTubeJamContext _context;
        private readonly IMapper _map;

        public Repository(YouTubeJamContext context, IMapper map)
        {
            _context = context;
            _map = map;
        }

        public void AddAnalysis(BL.AverageSentiment sentimentAverage, BL.Creator c)
        {
            _context.Analysis1.Add(_map.ParseAnalysis(sentimentAverage, c));
            _context.SaveChanges();
        }

        public void AddCreator(BL.Creator c)
        {
            _context.Creator.Add(_map.ParseCreator(c));
            _context.SaveChanges();
        }

        public void AddVideo(string videourl)
        {
            _context.Video.Add(_map.ParseVideo(videourl));
            _context.SaveChanges();
        }

        public List<BL.AverageSentiment> GetAnalysisHistory(string videourl, BL.Creator c)
        {
            List<BL.AverageSentiment> analHist = new List<BL.AverageSentiment>();
            List<Analysis1> analHistfromDB = _context.Analysis1.Where(a => a.Vid.URL == videourl && a.Creatr.PhoneNumber == c.PhoneNumber).ToList();
            foreach (Analysis1 item in analHistfromDB)
            {
                analHist.Add(_map.ParseAnalysis(item));
            }
            return analHist;
        }

        public List<BL.Creator> GetCreators()
        {
            List<Creator> allCreators = _context.Creator.Select(c => c).ToList();
            List<BL.Creator> allCreatorsfromDB = new List<BL.Creator>();
            foreach (Entities.Creator item in allCreators)
            {
                allCreatorsfromDB.Add(_map.ParseCreator(item));
            }
            return allCreatorsfromDB;
        }

        public BL.Creator LogIn(string phoneNumber, string passsword)
        {
            if (_context.Creator.Where(c => c.PhoneNumber == phoneNumber && c.Password == passsword).ToList().Count() == 0) throw new CreatorDoesNotExistException("Invalid phone or password");
            return _map.ParseCreator( _context.Creator.Single(c => c.PhoneNumber == phoneNumber && c.Password == passsword));
        }
    }
}
