using Database.Model.User.CustomIndices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserCustomIndices.Model.Response;

namespace UserCustomIndices.Services
{
    public interface ICustomIndexService
    {
        Task<ActionResult<CustomIndexResponse>> GetIndex(Guid userId, string indexId);
        Task<ActionResult<IEnumerable<CustomIndexResponse>>> GetAllForUser(Guid userid);
        Task<IActionResult> CreateIndex(Guid userId, CustomIndexResponse customIndex);
        Task<IActionResult> UpdateIndex(Guid userId, CustomIndexResponse customIndexUpdated);
        Task<IActionResult> RemoveIndex(Guid userId, string id);
    }
}