using System;

/// <summary>
/// ルール違反やエラーを検知する専用例外
/// </summary>
namespace Common
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
