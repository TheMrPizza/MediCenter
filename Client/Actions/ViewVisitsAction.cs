using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Actions.OutputManagers;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Exceptions;
using Common;

namespace Client.Actions
{
    public class ViewVisitsAction : ActionBase
    {
        public IOutputManager<VisitContent> OutputManager { get; set; }
        public ViewVisitsAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            OutputManager = new ViewVisitsOutput(client, streamIO);
        }

        public async override Task<ActionBase> Run()
        {
            List<Visit> visits = await GetVisits();
            if (visits != null)
            {
                await PrintVisits(visits);
            }

            return MainMenuAction;
        }

        private async Task PrintVisits(List<Visit> visits)
        {
            for (int i = 0; i < visits.Count; i++)
            {
                string personName = await GetName(visits[i]);
                VisitContent content = new VisitContent(visits[i], personName, i + 1);
                OutputManager.PrintOutput(content);
            }
        }

        private async Task<List<Visit>> GetVisits()
        {
            try
            {
                return await _client.GetVisits();
            }
            catch (RequestException e)
            {
                _streamIO.ErrorElement.Interact(e);
                return null;
            }
        }

        private async Task<string> GetName(Visit visit)
        {
            try
            {
                if (_client.User is Doctor)
                {
                    return await _client.GetName(visit.PatientUsername, "patients");
                }

                return await _client.GetName(visit.DoctorUsername, "doctors");
            }
            catch (MediCenterException e)
            {
                _streamIO.ErrorElement.Interact(e);
                return null;
            }
        }
    }
}
