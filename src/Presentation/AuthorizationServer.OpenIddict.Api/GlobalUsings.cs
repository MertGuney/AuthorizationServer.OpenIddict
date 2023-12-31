﻿global using AuthorizationServer.OpenIddict.Api.Extensions;
global using AuthorizationServer.OpenIddict.Application;
global using AuthorizationServer.OpenIddict.Application.Common.Extensions;
global using AuthorizationServer.OpenIddict.Application.Features.Commands.Auth.Codes.SendChangeEmailCode;
global using AuthorizationServer.OpenIddict.Application.Features.Commands.Auth.Codes.SendForgotPasswordCode;
global using AuthorizationServer.OpenIddict.Application.Features.Commands.Auth.Codes.SendRegisterCode;
global using AuthorizationServer.OpenIddict.Application.Features.Commands.Auth.Register;
global using AuthorizationServer.OpenIddict.Application.Features.Commands.Auth.ResetPassword;
global using AuthorizationServer.OpenIddict.Application.Features.Commands.Auth.TFAs.Activate;
global using AuthorizationServer.OpenIddict.Application.Features.Commands.Auth.TFAs.Deactivate;
global using AuthorizationServer.OpenIddict.Application.Features.Commands.Auth.TFAs.Enable;
global using AuthorizationServer.OpenIddict.Application.Features.Commands.Users.ChangeEmail;
global using AuthorizationServer.OpenIddict.Application.Features.Commands.Users.ChangePassword;
global using AuthorizationServer.OpenIddict.Application.Features.Queries.Users.GetCurrentUser;
global using AuthorizationServer.OpenIddict.Application.Services.Abstractions;
global using AuthorizationServer.OpenIddict.Domain.Entities;
global using AuthorizationServer.OpenIddict.Domain.Options;
global using AuthorizationServer.OpenIddict.Infrastructure;
global using AuthorizationServer.OpenIddict.Persistence;
global using AuthorizationServer.OpenIddict.Persistence.SeedData;
global using AuthorizationServer.OpenIddict.Shared.Models;
global using IdentityModel;
global using MediatR;
global using Microsoft.AspNetCore;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using Microsoft.OpenApi.Models;
global using OpenIddict.Abstractions;
global using OpenIddict.Server.AspNetCore;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using static OpenIddict.Abstractions.OpenIddictConstants;