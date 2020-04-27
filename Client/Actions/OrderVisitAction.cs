using System.Threading.Tasks;
using Client.Actions.InputManagers;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Exceptions;
using Common;

namespace Client.Actions
{
    public class OrderVisitAction : ActionBase
    {
        public InputManagerBase<Visit> InputManager { get; set; }

        public OrderVisitAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            InputManager = new OrderVisitInput(client, streamIO);
        }

        public async override Task<ActionBase> Run()
        {
            Visit inputVisit = InputManager.GetInput();
            Visit scheduledvisit = await ScheduleVisit(inputVisit);
            if (scheduledvisit != null)
            {
                string doctorName = await _client.GetName(scheduledvisit.DoctorUsername, "doctors");
                _streamIO.TextElement.Interact($"A visit with Dr. {doctorName} " +
                                               $"has been scheduled for {scheduledvisit.StartTime}");
            }

            return new PatientMainMenuAction(_client, _streamIO);
        }

        private async Task<Visit> ScheduleVisit(Visit visit)
        {
            try
            {
                return await _client.ScheduleVisit(visit);
            }
            catch (RequestException e)
            {
                _streamIO.ErrorElement.Interact(e);
                return null;
            }
        }
    }
}
