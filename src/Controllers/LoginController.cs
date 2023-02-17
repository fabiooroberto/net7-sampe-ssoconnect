using Microsoft.AspNetCore.Mvc;
using Wiz.SsoConnect.Extension.Sso.Model;
using Wiz.SsoConnect.Extension.Sso.Model.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Connect.Net7.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{

    private readonly ILogger<LoginController> _logger;
    private readonly ISsoConnectAuthentication _ssoConnect;
    public LoginController(ILogger<LoginController> logger, ISsoConnectAuthentication ssoConnect)
    {
        _logger = logger;
        _ssoConnect = ssoConnect;
    }

    [HttpPost("basic")]
    public async Task<ActionResult<SsoAuth>> BasicAutenticacao(RequestBasic request)
    {
        var data = await _ssoConnect.PostBasicAsync(request);
        if (!data.Success) 
            return BadRequest(new ProblemDetails { Detail = data.ErrorMessage });

        return Ok(data.SsoAuth);
    }

    [HttpPost("credential")]
    public async Task<ActionResult<SsoAuth>> Credential(RequestClientCredential request)
    {
        var data = await _ssoConnect.PostClientAsync(request);
        
        if (!data.Success)
            return BadRequest(new ProblemDetails { Detail = data.ErrorMessage });

        return Ok(data.SsoAuth);
    }

    [HttpPost("password")]
    public async Task<ActionResult<SsoAuth>> Password(RequestClientPassword request)
    {
        var data = await _ssoConnect.PostPasswordAsync(request);
        if (!data.Success)
            return BadRequest(new ProblemDetails { Detail = data.ErrorMessage });

        return Ok(data.SsoAuth);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<SsoAuth>> Password(RequestRefreshToken request)
    {
        var data = await _ssoConnect.PostRefreshTokenAsync(request);
        if (!data.Success)
            return BadRequest(new ProblemDetails { Detail = data.ErrorMessage });

        return Ok(data.SsoAuth);
    }
}
