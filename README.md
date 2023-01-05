# Api XPTO

## Uso simplicado do Sso Connect

```
Para simplicar o uso de como implementar o pacote Sso Connect, foi criado este projeto de exemplo.

Api: .Net 7.
Swagger: Swashbuckle
SsoConnect: versão 3.0.4

```

---

## PROGRAM.cs

Configurar os serviços do SsoConnect

---

### _AddSsoConnectJwt_:

Responsável por toda orquestração de como validar o JWT Token, sendo necessário informar o Enviroment e o Audience apenas
Ele, já se encarrega de pegar o Authority padrão para validar de acordo com a Enviroment - Staging/Production

#### exemplo:

```csharp

builder.Services.AddSsoConnectJwt(builder.Environment, options =>
{
    options.Audience = "api-test";
});

```

---

#### _AddSsoConnect_:
Responsável pela disponibilização de uso dos métodos para geração de token.
#### registro serviço:

```csharp

builder.Services.AddSsoConnect(builder.Environment);

```
Para utilização.

Basta fazer injeção de dependência no serviço.
```csharp
private readonly ISsoConnectAuthentication _ssoConnect;

public MyService(ISsoConnectAuthentication ssoConnect)
{
    _ssoConnect = ssoConnect;
}
```

Aqui temos o exemplo de como utilizar os 4 modelos,
Basta ver qual modelo se enquadra na sua necessidade.

> Basic - parametro: code em base64 (clientid:clientSecret)
```csharp
var token = await _ssoConnect.PostBasicAsync(request);
```
---

> Credential - parametros: clientId, clientSecret, scope(opcional)
```csharp
var token = await _ssoConnect.PostClientAsync(request);
```
---
> Password - parametros: clientId, clientSecret, scope(opcional), username, password
```csharp
var token = await _ssoConnect.PostPasswordAsync(request);
```
---
> RefreshToken - parametros: clientId, clientSecret, refreshToken
```csharp
var token = await _ssoConnect.PostRefreshTokenAsync(request);
```
