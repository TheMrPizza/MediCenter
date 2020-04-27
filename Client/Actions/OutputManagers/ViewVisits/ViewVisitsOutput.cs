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
