using Database.Model.User.CustomIndices;
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
        Task<ActionResult<CustomIndexRequest>> GetIndex(Guid userId, string indexId);
        Task<ActionResult<IEnumerable<CustomIndexRequest>>> GetAllForUser(Guid userid);
        IActionResult CreateIndex(Guid userId, CustomIndexRequest customIndex);
        Task<IActionResult> UpdateIndex(Guid userId, CustomIndexRequest customIndexUpdated);
        Task<IActionResult> RemoveIndex(Guid userId, string id);
    }
}