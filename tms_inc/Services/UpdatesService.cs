using Microsoft.EntityFrameworkCore;
using tms_inc.Models.Employees;
using tms.DataAccess;
using System.Linq;
using System.Threading.Tasks;

namespace tms_inc.Services
{
    public class UpdatesService
    {
        private readonly PortalContext _context;
        public static int ApplicantsNum;
        public static int ContactMeRequestsNum;
        public static int QuoteRequestsNum;

        static UpdatesService()
        {
            ApplicantsNum = -1;
            ContactMeRequestsNum = -1;
            QuoteRequestsNum = -1;
        }
        public UpdatesService(PortalContext portalContext)
        {
            _context = portalContext;
        }

        private async Task<int> GetApplicantsNum()
        {
            return await _context.People.Where(x => x.IsDeleted == false && x.IsProcessed == false && x.Status == Status.Pending).CountAsync();
        }

        private async Task<int> GetContactMeRequestsNum()
        {
            return await _context.GetInTouches.Where(x => x.IsDeleted == false && x.IsProcessed == false).CountAsync();
        }
        private async Task<int> GetQuoteRequestsNum()
        {
            return await _context.RequestedQuotes.Where(x => x.IsDeleted == false && x.IsProcessed == false).CountAsync();
        }
        /// <summary>
        /// Updates the numbers of applicants, contact me requests, and quote requests.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateNumbers()
        {
            UpdatesService.ApplicantsNum = await GetApplicantsNum();
            UpdatesService.ContactMeRequestsNum = await GetContactMeRequestsNum();
            UpdatesService.QuoteRequestsNum = await GetQuoteRequestsNum();
        }
    }
}
