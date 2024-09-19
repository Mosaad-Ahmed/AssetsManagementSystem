global using AssetsManagementSystem.Others.Middlewares.ExceptionMiddleware;
global using Newtonsoft.Json;
global using SendGrid.Helpers.Errors.Model;
global using System.ComponentModel.DataAnnotations;
global using AssetsManagementSystem.Data.Context;
global using AssetsManagementSystem.Models.Commons.ICommon;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Query;
global using System.Linq.Expressions;
global using AssetsManagementSystem.Others.Interfaces.IRepositories;
global using AssetsManagementSystem.Data.Repository.Repositories;
global using AssetsManagementSystem.Others.Interfaces.IUnitOfWork;
global using AssetsManagementSystem.Models.DbSets;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using AssetsManagementSystem.Others.Interfaces.ITokens;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using System.Security.Cryptography;
global using AssetsManagementSystem.Others.Tokens;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using AssetsManagementSystem.Data.UnitOfWorks.UnitOfWork;
global using AssetsManagementSystem.Models.Commons.Common;
global using AssetsManagementSystem.Models.Enums;
global using AssetsManagementSystem.Others.Validation_Attribute;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.ComponentModel;
global using AssetsManagementSystem.Others.Bases;
global using AssetsManagementSystem.Services.Auth.Rule;
global using AssetsManagementSystem.Services.Auth.Exceptions;
global using AutoMapper.Internal;
global using AutoMapper;
global using AssetsManagementSystem.DTOs.AccountServiceDTOs.LoginDTOs;
global using AssetsManagementSystem.DTOs.AccountServiceDTOs.RefreshToken;
global using AssetsManagementSystem.DTOs.AccountServiceDTOs.AddUserRequestDTOs;
global using AssetsManagementSystem.DTOs.AccountServiceDTOs.ResetPassword;
global using AssetsManagementSystem.Services.Auth;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using AssetsManagementSystem.DTOs.LocationDTOs;
global using AssetsManagementSystem.Services.Locations;
global using AssetsManagementSystem.Data;
global using AssetsManagementSystem.Others;
global using AssetsManagementSystem.Services;
global using AssetsManagementSystem.DTOs.AssetDisposalDTOs;
global using AssetsManagementSystem.Services.AssetDisposal;
global using AssetsManagementSystem.DTOs.AssetDTOs;
global using AssetsManagementSystem.Services.Assets;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using System.Reflection;
global using System.Text;
global using AssetsManagementSystem.Services.AssetMaintenance;
global using AssetsManagementSystem.Services.AssetTransfer;
global using AssetsManagementSystem.Services.Categories;
global using AssetsManagementSystem.Services.DataConsistencyCheck;
global using AssetsManagementSystem.Services.Document;
global using AssetsManagementSystem.Services.ReceiveMaintainedAsset;
global using AssetsManagementSystem.Services.Suppliers;


















