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
                if (_client.User is Doctor)
                {
                    await PrintDoctorVisits(visits);
                }
                else
                {
                    await PrintPatientVisits(visits);
                }
            }

            return new MainMenuAction(_client, _streamIO);
        }

        private async Task PrintDoctorVisits(List<Visit> visits)
        {
            for (int i = 0; i < visits.Count; i++)
            {
                string patientName = await GetName(visits[i].PatientUsername, "patients");
                if (patientName == null)
                {
                    _streamIO.TextElement.Interact($"{(i + 1)}. A visit with unknown patient");
                }
                else
                {
                    _streamIO.TextElement.Interact($"{(i + 1)}. A visit with {patientName}");
                }

                _streamIO.TextElement.Interact($"   From {visits[i].StartTime.ToLocalTime()} " +
                                               $"to {visits[i].EndTime.ToLocalTime()}");
            }
        }

        private async Task PrintPatientVisits(List<Visit> visits)
        {
            for (int i = 0; i < visits.Count; i++)
            {
                string doctorName = await GetName(visits[i].DoctorUsername, "doctors");
                if (doctorName == null)
                {
                    _streamIO.TextElement.Interact((i + 1) + ". A visit with unknown doctor");
                }
                else
                {
                    _streamIO.TextElement.Interact($"{(i + 1)}. A visit with Dr. {doctorName}");
                }

                _streamIO.TextElement.Interact($"   From {visits[i].StartTime.ToLocalTime()} " +
                                               $"to {visits[i].EndTime.ToLocalTime()}");
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

        private async Task<string> GetName(string username, string type)
        {
            try
            {
                return await _client.GetName(username, type);
            }
            catch (MediCenterException e)
            {
                _streamIO.ErrorElement.Interact(e);
                return null;
            }
        }
    }
}
