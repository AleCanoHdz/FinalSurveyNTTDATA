using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalSurveyNTTDATA.Data;
using FinalSurveyNTTDATA.Models;
using AutoMapper;
using FinalSurveyNTTDATA.DTOs.Role;
using FinalSurveyNTTDATA.Services.RoleService;

namespace FinalSurveyNTTDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public RolesController(DataContext context, IMapper mapper, IRoleService roleService)
        {
            _context = context;
            _mapper = mapper;
            _roleService = roleService;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetRoleDto>>>> GetRoles()
        {
            return Ok(await _roleService.GetRoles());
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRoleDto>>> GetRole(Guid id)
        {
            return Ok(await _roleService.GetRole(id));
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRoleDto>>> PutRole(UpdateRoleDto update, Guid id)
        {
            var response = await _roleService.UpdateRole(update, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetRoleDto>>> AddRole(AddRoleDto role)
        {
            return Ok(await _roleService.AddRole(role));
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRoleDto>>> DeleteRole(Guid id)
        {
            var response = await _roleService.DeleteRole(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        private bool RoleExists(Guid id)
        {
            return _context.Role.Any(e => e.IdRole == id);
        }
    }
}
