using System;
using System.Collections.Generic;
using System.Linq;
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
            InputManager = new OrderVisitIO(client, streamIO);
        }

        public async override Task<ActionBase> Run()
        {
            Visit inputVisit = GetInput();
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

        private Visit GetInput()
        {
            _streamIO.TextElement.Interact("Choose speciality:");
            string specialityName = _streamIO.ListElement.Interact(_options) as string;
            DateTime date = _streamIO.FieldDateElement.Interact("Date");
            DateTime startTime = _streamIO.FieldDateElement.Interact("Start Time");
            DateTime endTime = _streamIO.FieldDateElement.Interact("End Time");

            return CreateVisit(specialityName, date, startTime, endTime);
        }

        private Visit CreateVisit(string specialityName, DateTime date, DateTime startTime, DateTime endTime)
        {
            Patient patient = _client.User as Patient;
            Speciality speciality = Enum.Parse<Speciality>(specialityName);
            startTime = date.Date.Add(startTime.TimeOfDay);
            endTime = date.Date.Add(endTime.TimeOfDay);

            return new Visit(patient.Username, speciality, startTime, endTime);
        }
    }
}
