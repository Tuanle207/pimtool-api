using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Contract
{
    public static class RouteConstants
    {
        public class Project
        {
            public const string Api = "api/projects";
            public const string GetAll = "";
            public const string GetOne = "{projectId}";
            public const string GetOneForUpdate = "{projectId}/get-for-update";
            public const string CreateOne = "";
            public const string UpdateOne = "{projectId}";
            public const string DeleteOne = "{projectId}";
            public const string DeleteAll = "delete-all";
            public const string CheckProjectNumber = "check-project-number";
        }

        public class Group
        {
            public const string Api = "api/groups";
            public const string GetAllBasic = "";
        }

        public class Employee
        {
            public const string Api = "api/employees";
            public const string GetAllBasic = "";
            public const string CheckVisasExistence = "check-visas";
        }
    }
}
