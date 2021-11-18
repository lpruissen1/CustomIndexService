using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserCustomIndices.Core.Model.Requests;
using UserCustomIndices.Model.Response;

namespace UserCustomIndices.Services
{
	public interface ICustomIndexService
    {
        Task<ActionResult<CustomIndexResponse>> GetIndex(string userId, string indexId);
        Task<ActionResult<IEnumerable<CustomIndexResponse>>> GetAllForUser(string userid);
		Task<IEnumerable<CustomIndexResponse>> GetAllForUserNew(string userid);
        IActionResult CreateIndex(string userId, CreateCustomIndexRequest customIndex);
        IActionResult UpdateIndex(string userId, CustomIndexRequest customIndexUpdated);
        IActionResult RemoveIndex(string userId, string indexId);
    }
}
