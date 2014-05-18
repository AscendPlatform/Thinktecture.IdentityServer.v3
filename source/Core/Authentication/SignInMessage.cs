﻿/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license
 */

using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.ServiceModel.Security.Tokens;
using Thinktecture.IdentityModel.Tokens;
namespace Thinktecture.IdentityServer.Core.Authentication
{
    public class SignInMessage
    {
        // page, popup (todo), touch, wap
        public string DisplayMode { get; set; }
        public string UILocales { get; set; }
        public string LoginHint { get; set; }
        // password, cert, 2fa
        public string AuthenticationMethod { get; set; }

        public string ReturnUrl { get; set; }
        public string LoginPromptCorrelationId { get; set; }

        public string ToJwt(string issuer, string audience, string key, int ttl)
        {
            var claims = new List<Claim>();
            if (ReturnUrl.IsPresent())
            {
                claims.Add(new Claim("returnUrl", ReturnUrl));
            }

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                new Lifetime(DateTime.UtcNow, DateTime.UtcNow.AddSeconds(ttl)),
                new HmacSigningCredentials(key));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static SignInMessage FromJwt(string jwt, string issuer, string audience, string key)
        {
            var message = new SignInMessage();
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidAudience = audience,
                ValidIssuer = issuer,
                IssuerSigningToken = new BinarySecretSecurityToken(Convert.FromBase64String(key))
            };

            var principal = handler.ValidateToken(jwt, parameters);

            var claim = principal.FindFirst("returnUrl");
            if (claim != null)
            {
                message.ReturnUrl = claim.Value;
            }

            return message;
        }
    }
}
