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

        private async Task<Visit> HandlePrescription(Prescription prescription)
        {
            try
            {
                Visit visit = await _client.GivePrescription(prescription);
                CheckMedicines(prescription, visit);
                return visit;
            }
            catch (RequestException e)
            {
                _streamIO.ErrorElement.Interact(e);
                return null;
            }
        }

        private void CheckMedicines(Prescription prescription, Visit visit)
        {
            var unsafeMedicines = new List<Medicine>();
            foreach (Medicine med in prescription.Medicines)
            {
                if (!visit.MedicinesId.Contains(med.Id))
                {
                    unsafeMedicines.Add(med);
                }
            }

            if (unsafeMedicines.Count > 0)
            {
                string names = string.Join(", ", unsafeMedicines.Select(med => med.Name));
                _streamIO.ErrorElement.Interact($"Added all medicines except for {names} due to their" +
                    $" dangerous reactions with the patient");
            }
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
                return await _client.GetAllMedicines();
            }
            catch (MediCenterException e)
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
            catch (MediCenterException e)
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
