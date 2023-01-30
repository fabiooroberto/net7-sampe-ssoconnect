using Microsoft.AspNetCore.Mvc;
using Wiz.SsoConnect.Extension.Sso.Model;
using Wiz.SsoConnect.Extension.Sso.Model.Interfaces;

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
        var token = await _ssoConnect.PostBasicAsync(request);
        if (!token.Success)
            return BadRequest(new ProblemDetails
            {
                Detail = token.ErrorMessage
            });
        return Ok(token.SsoAuth);
    }

    [HttpPost("credential")]
    public async Task<ActionResult<SsoAuth>> Credential(RequestClientCredential request)
    {
        var token = await _ssoConnect.PostClientAsync(request);
        if (!token.Success)
            return BadRequest(new ProblemDetails
            {
                Detail = token.ErrorMessage
            });
        return Ok(token.SsoAuth);
    }

    [HttpPost("password")]
    public async Task<ActionResult<SsoAuth>> Password(RequestClientPassword request)
    {
        var token = await _ssoConnect.PostPasswordAsync(request);
        if (!token.Success)
            return BadRequest(new ProblemDetails
            {
                Detail = token.ErrorMessage
            });
        return Ok(token.SsoAuth);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<SsoAuth>> Password(RequestRefreshToken request)
    {
        var token = await _ssoConnect.PostRefreshTokenAsync(request);
        if (!token.Success)
            return BadRequest(new ProblemDetails
            {
                Detail = token.ErrorMessage
            });
        return Ok(token.SsoAuth);
    }
}
