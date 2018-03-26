using JKMWindowsService.Model;
using JKMWindowsService.Utility;
using JKMWindowsService.Utility.Log;
using System.Collections.Generic;
using System.Net.Http;

namespace JKMWindowsService.MoveManager
{
    public class MoveDetails : IMoveDetails
    {
        private readonly IAPIHelper apiHelper;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;

        //Constructor
        public MoveDetails(IAPIHelper apiHelper,
                           IResourceManagerFactory resourceManager,
                           ILogger logger)
        {
            this.apiHelper = apiHelper;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Constructor Name        : GetMoveList
        /// Author                  : Pratik Soni
        /// Creation Date           : 15 Feb 2018
        /// Purpose                 : To get list of active Moves from JIM
        /// Revision                : 
        /// </summary>
        public List<MoveModel> GetMoveList(string statusReason)
        {
            try
            {
                string apiURL = General.GetAPIPath(resourceManager.GetString("MoveService"), "list/" + statusReason);

                HttpResponseMessage httpResponseMessage = apiHelper.InvokeGetAPI(apiURL);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    APIResponse<List<MoveModel>> serviceResponse = General.ConvertFromJson<APIResponse<List<MoveModel>>>(httpResponseMessage.RequestMessage.ToString());
                    return serviceResponse.Data;
                }
                return new List<MoveModel>();
            }
            catch (System.Exception ex)
            {
                logger.Error("Error occured: ", ex);
                return new List<MoveModel>();
            }
        }
    }
}
