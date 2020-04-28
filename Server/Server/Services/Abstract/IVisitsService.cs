﻿using System.Collections.Generic;
using Common;

namespace Server.Services.Abstract
{
    public interface IVisitsService
    {
        bool Schedule(Visit visit);
        Visit AddPrescription(Visit visit, Patient patient, List<Medicine> medicines);
        Visit Get(string id);
    }
}
