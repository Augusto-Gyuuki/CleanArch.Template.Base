﻿namespace CleanArch.Base.Template.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken();
}