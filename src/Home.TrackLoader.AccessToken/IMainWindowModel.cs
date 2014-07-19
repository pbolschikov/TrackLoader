using System;

namespace Home.TrackLoader.AccessToken
{
    internal interface IMainWindowModel
    {
        Uri Source { get; set; }
        string AccessToken { get; }
    }
}