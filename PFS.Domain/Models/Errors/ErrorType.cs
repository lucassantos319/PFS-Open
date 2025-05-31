namespace PFS.Domain.Models.Errors;

public class ErrorType
{
    public string type{get;set;}
    public string title{get;set;}
    public int status{get;set;}
    public string detail {get;set;}
    public string instance{get;set;}
}