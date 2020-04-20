using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Common;

namespace Server.Controllers
{
    public abstract class ModelControllerBase<TModel> : ControllerBase
        where TModel : IModel
    {
        protected readonly IService<TModel> _service;

        public ModelControllerBase(IService<TModel> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual ActionResult<List<TModel>> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id:length(24)}", Name = "Get")]
        public virtual ActionResult<TModel> Get(string id)
        {
            TModel model = _service.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpPost]
        public virtual ActionResult<TModel> Add(TModel model)
        {
            _service.Add(model);
            return CreatedAtRoute("Get", new { id = model.Id.ToString() }, model);
        }

        [HttpPut("{id:length(24)}")]
        public virtual IActionResult Update(string id, TModel model)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }

            _service.Update(id, model);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public virtual IActionResult Remove(string id)
        {
            TModel model = _service.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _service.Remove(model);
            return NoContent();
        }
    }
}