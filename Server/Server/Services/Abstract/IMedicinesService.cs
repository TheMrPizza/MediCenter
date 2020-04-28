using System.Collections.Generic;
using Common;

namespace Server.Services.Abstract
{
    public interface IMedicinesService
    {
        public List<Medicine> GetAll();
        public Medicine Get(string id);
    }
}
