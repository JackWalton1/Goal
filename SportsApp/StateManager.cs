using MongoDB.Driver.Core.Authentication;
using System.ComponentModel.DataAnnotations;
using SportsApp;

namespace StateManager
{
    public static class StateManager
    {
        public static UserModel CurrentUser { get; set; }
        public static string MembershipSelection { get; set; }
        public static string CurrentEvent { get; set; }
    }
}