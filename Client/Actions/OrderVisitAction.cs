using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.HttpClients;
using Client.IO.Abstract;
using Common;

namespace Client.Actions
{
    public class OrderVisitAction : ActionBase
    {
        private List<string> _options { get; set; }

        public OrderVisitAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = Enum.GetNames(typeof(Speciality)).ToList();
        }

        public async override Task<ActionBase> Run()
        {
            Visit inputVisit = GetInput();
            Visit scheduledVisit = await _client.ScheduleVisit(inputVisit);
            _streamIO.TextElement.Interact("A visit with Dr. " + scheduledVisit.Doctor.Name +
                                           " has been scheduled for " + scheduledVisit.StartTime);
            return new MainMenuAction(_client, _streamIO);
        }

        public Visit GetInput()
        {
            _streamIO.TextElement.Interact("Choose speciality:");
            string specialityName = _streamIO.ListElement.Interact(_options) as string;
            DateTime date = _streamIO.FieldDateElement.Interact("Date");
            DateTime startTime = _streamIO.FieldDateElement.Interact("Start Time");
            DateTime endTime = _streamIO.FieldDateElement.Interact("End Time");
            return CreateVisit(specialityName, date, startTime, endTime);
        }

        public Visit CreateVisit(string specialityName, DateTime date, DateTime startTime, DateTime endTime)
        {
            Patient patient = _client.User as Patient;
            Speciality speciality = Enum.Parse<Speciality>(specialityName);
            startTime = date.Date.Add(startTime.TimeOfDay);
            endTime = date.Date.Add(endTime.TimeOfDay);
            return new Visit(patient, speciality, startTime, endTime);
        }
    }
}
