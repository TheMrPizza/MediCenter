﻿using System;
using System.Threading.Tasks;
using Client.Exceptions;
using Client.HttpClients;
using Client.IO.Abstract;
using Common;

namespace Client.Actions
{
    public class RegisterPatientAction : ActionBase
    {
        public RegisterPatientAction(MediClient client, IStreamIO streamIO) : base(client, streamIO)
        {

        }

        public async override Task<ActionBase> Run()
        {
            Patient patient = GetInput();
            await Register(patient);
            return new HomeMenuAction(_client, _streamIO);
        }

        private async Task Register(Patient patient)
        {
            try
            {
                await _client.Register(patient, "patients");
            }
            catch (RequestException e)
            {
                _streamIO.ErrorElement.Interact(e);
            }
        }
    }
}
