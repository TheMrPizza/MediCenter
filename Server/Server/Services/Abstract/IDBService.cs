namespace Server.Services.Abstract
{
    public interface IDBService
    {
        IDoctorsService DoctorsService { get; }
        IPatientsService PatientsService { get; }
        IMedicinesService MedicinesService { get; }
        IVisitsService VisitsService { get; }
    }
}
