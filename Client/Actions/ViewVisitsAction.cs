using System.Collections.Generic;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Exceptions;
using Common;

namespace Client.Actions
{
    public class ViewVisitsAction : ActionBase
    {
        public ViewVisitsAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public async override Task<ActionBase> Run()
        {
            List<Visit> visits = await GetVisits();
            if (visits != null)
            {
                await PrintVisits(visits);
            }

            return new MainMenuAction(_client, _streamIO);
        }

        private async Task PrintVisits(List<Visit> visits)
        {
            for (int i = 0; i < visits.Count; i++)
            {
                string doctorName = await GetDoctorName(visits[i].DoctorUsername);
                if (doctorName == null)
                {
                    _streamIO.TextElement.Interact((i + 1) + ". A visit with unknown doctor");
                }
                else
                {
                    _streamIO.TextElement.Interact((i + 1) + ". A visit with Dr. " + doctorName);
                }

                _streamIO.TextElement.Interact("   From " + visits[i].StartTime + " to " + visits[i].EndTime);
            }
        }

        private async Task<List<Visit>> GetVisits()
        {
            try
            {
                return await _client.GetVisits();
            }
            catch (MediCenterException e)
            {
                _streamIO.ErrorElement.Interact(e);
                return null;
            }
        }

        private async Task<string> GetDoctorName(string username)
        {
            try
            {
                return await _client.GetDoctorName(username);
            }
            catch (MediCenterException e)
            {
                _streamIO.ErrorElement.Interact(e);
                return null;
            }
        }
    }
}
