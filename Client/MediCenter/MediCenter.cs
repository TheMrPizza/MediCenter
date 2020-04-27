using Client.HttpClients;
using Client.IO.Abstract;
using Client.Actions;

namespace Client.MediCenter
{
    public class MediCenter
    {
        public MediClient Client { get; }
        public IStreamIO StreamIO { get; }
        public ActionBase CurAction { get; set; }
        public ActionBase MainMenuAction { get; set; }

        public MediCenter(MediClient client, IStreamIO streamIO)
        {
            Client = client;
            StreamIO = streamIO;
            CurAction = new HomeMenuAction(client, streamIO);
        }

        public void Run()
        {
            while (true)
            {
                CurAction.OnMainMenuAction += OnSetMainMainAction;
                CurAction = CurAction.Run().Result;
                if (CurAction == null)
                {
                    CurAction = MainMenuAction;
                }
            }
        }

        public void OnSetMainMainAction(ActionBase mainMenuAction)
        {
            MainMenuAction = mainMenuAction;
        }
    }
}
