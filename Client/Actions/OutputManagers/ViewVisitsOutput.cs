using System.Collections.Generic;
using System.Linq;
using Client.HttpClients;
using Client.IO.Abstract;
using Common;

namespace Client.Actions.OutputManagers
{
    public class ViewVisitsOutput : IOutputManager<VisitContent>
    {
        public MediClient Client { get; }
        public IStreamIO StreamIO { get; }
        public VisitContent Value { get; set; }

        public ViewVisitsOutput(MediClient client, IStreamIO streamIO)
        {
            Client = client;
            StreamIO = streamIO;
        }

        public void PrintOutput(VisitContent value)
        {
            Value = value;
            StreamIO.TextElement.Interact(GetOutput());
            StreamIO.TextElement.Interact($"   From {Value.Visit.StartTime.ToLocalTime()} " +
                                            $"to {Value.Visit.EndTime.ToLocalTime()}");
            if (value.Medicines.Count > 0)
            {
                string medicinesName = string.Join(", ", Value.Medicines.Select(med => med.Name));
                StreamIO.TextElement.Interact($"   Given medicines: {medicinesName}");
            }
        }

        private string GetOutput()
        {
            if (Client.User is Doctor)
            {
                return GetDoctorOutput();
            }

            return GetPatientOutput();
        }

        private string GetDoctorOutput()
        {
            if (Value.Visit == null)
            {
                return $"{Value.Num}. A visit with unknown patient";
            }

            return $"{Value.Num}. A visit with {Value.PersonName}";
        }

        private string GetPatientOutput()
        {
            if (Value.Visit == null)
            {
                return $"{Value.Num}. A visit with unknown doctor";
            }

            return $"{Value.Num}. A visit with Dr. {Value.PersonName}";
        }
    }
}
