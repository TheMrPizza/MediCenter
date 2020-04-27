using System;
using System.Linq;
using System.Collections.Generic;
using Client.HttpClients;
using Client.IO.Abstract;
using Common;

namespace Client.Actions.IOManagers
{
    public class OrderVisitIO : IOManagerBase<Visit>
    {
        private List<string> _options { get; set; }

        public OrderVisitIO(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {
            _options = Enum.GetNames(typeof(Speciality)).ToList();
        }

        public override Visit GetInput()
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
