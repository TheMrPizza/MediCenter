using Client.HttpClients;
using Client.IO.Abstract;
using Client.Actions;

namespace Client.MediCenter
{
    public class MediCenter
    {
        public MediClient Client { get; }
        public IStreamIO StreamIO { get; }
        public ActionBase Action { get; set; }

        public MediCenter(MediClient client, IStreamIO streamIO)
        {
            Client = client;
            StreamIO = streamIO;
            Action = new HomeMenuAction(client, streamIO);
        }

        public async void Run()
        {
            while (true)
            {
                Action = await Action.Run();
            }
        }
    }
}
