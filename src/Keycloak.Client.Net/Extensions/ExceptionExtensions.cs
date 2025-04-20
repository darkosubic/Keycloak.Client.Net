using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keycloak.Client.Net.Extensions
{
    internal static class ExceptionExtensions
    {
        public static Result<T> FailureFromException<T>(this Exception ex)
        {
            List<string> errorList = new List<string>();
            StringBuilder sb = new StringBuilder();
            while (ex != null)
            {
                sb.AppendLine($"Type: {ex.GetType().FullName}");
                sb.AppendLine($"Message: {ex.Message}");
                sb.AppendLine($"StackTrace: {ex.StackTrace}");

                errorList.Add(sb.ToString());
                sb.Clear();

                ex = ex.InnerException;
            }

            return Result<T>.Error(new ErrorList(errorList));
        }

        public static Result FailureFromException(this Exception ex)
        {
            List<string> errorList = new List<string>();
            StringBuilder sb = new StringBuilder();
            while (ex != null)
            {
                sb.AppendLine($"Type: {ex.GetType().FullName}");
                sb.AppendLine($"Message: {ex.Message}");
                sb.AppendLine($"StackTrace: {ex.StackTrace}");

                errorList.Add(sb.ToString());
                sb.Clear();

                ex = ex.InnerException;
            }

            return Result.Error(new ErrorList(errorList));
        }
    }
}
