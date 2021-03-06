﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_A_Lyzer.Models;
using Sport_A_Lyzer.Services;
using Sport_A_Lyzer.Services.Models;

namespace Sport_A_Lyzer.Controllers
{
	[Authorize]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

		public UsersController( IUserService userService )
		{
			_userService = userService;
		}

		[AllowAnonymous]
		[Route( "api/users/authenticate" )]
		[HttpPost]
		public async Task<IActionResult> Authenticate( [FromBody]AuthenticateModel model )
		{
			var user = await _userService.Authenticate( model.Username, model.Password );

			if ( user == null )
				return BadRequest( new { message = "Username or password is incorrect" } );

			return Ok( user );
		}

		[HttpGet]
		[Route( "api/users" )]
		public IActionResult GetAll()
		{
			var users = _userService.GetAll();
			return Ok( users );
		}

		[HttpPost]
		[Route( "api/users" )]
		public async Task<IActionResult> PostAddUser( UserRequest request )
		{
			request.RoleId = new Guid( "a3aea76a-a7e7-41e8-b752-7189062d5bbc" );
			await _userService.AddUser( request );
			return Ok();
		}

		[AllowAnonymous]
		[HttpPost]
		[Route( "api/users/register" )]
		public async Task<IActionResult> PostRegisterUser( UserRequest request )
		{
			request.RoleId = new Guid( "a3aea76a-a7e7-41e8-b752-7189062d5bbc" );
			await _userService.AddUser( request );
			return Ok();
		}
	}
}