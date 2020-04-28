using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Client.Actions.InputManagers;
using Client.Actions.OutputManagers;
using Client.HttpClients;
using Client.IO.Abstract;
using Client.Exceptions;
using Common;

namespace Client.Actions
{
    public class GivePrescriptionsAction : ActionBase
    {
        public InputManagerBase<Prescription> InputManager { get; set; }

        public GivePrescriptionsAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            InputManager = new GivePrescriptionsInput(client, streamIO);
        }

        public async override Task<ActionBase> Run()
        {
            List<VisitContent> visitsContents = await GetVisitsContents();
            List<Medicine> medicines = await GetMedicines();
            if (visitsContents != null && medicines != null)
            {
                InputManager.Init(new PrescriptionParams(visitsContents, medicines));
                InputManager.PrintInstructions();
                Prescription prescription = InputManager.GetInput();
                await HandlePrescription(prescription);
            }

            return new DoctorMainMenuAction(_client, _streamIO);
        }

        private async Task HandlePrescription(Prescription prescription)
        {

        }

        private async Task<List<VisitContent>> GetVisitsContents()
        {
            var visits = await GetVisits();
            if (visits == null)
            {
                return null;
            }

            var contents = new List<VisitContent>();
            foreach (Visit vis in visits)
            {
                contents.Add(new VisitContent(vis, await GetName(vis)));
            }

            return contents;
        }

        private async Task<List<Medicine>> GetMedicines()
        {
            try
            {
                return await _client.GetMedicines();
            }
            catch (RequestException e)
            {
                _streamIO.ErrorElement.Interact(e);
                return null;
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
                return await _client.GetName(visit.PatientUsername, "patients");
            }
            catch (MediCenterException e)
            {
                _streamIO.ErrorElement.Interact(e);
                return null;
            }
        }
    }
}
