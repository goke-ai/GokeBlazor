﻿using GokeBlazor.Shared;
using GokeBlazor.Shared.AspnetIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GokeBlazor.Client.Services.AspnetIdentity.Contracts
{
    public interface IAuthorizeApi
    {
        Task Login(LoginParameters loginParameters);
        Task Register(RegisterParameters registerParameters);
        Task Logout();
        Task<UserInfo> GetUserInfo();

    }
}
