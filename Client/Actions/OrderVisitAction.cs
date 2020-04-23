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

        public override Task<ActionBase> Run()
        {
            string specialityName = _streamIO.ListElement.Interact(_options) as string;
            Speciality speciality = Enum.Parse<Speciality>(specialityName);
            DateTime date = _streamIO.FieldDateElement.Interact("Date");
            DateTime startTime = _streamIO.FieldDateElement.Interact("Start Time");
            DateTime endTime = _streamIO.FieldDateElement.Interact("End Time");
            return null;
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
