namespace eShift.Utilities
{
    public enum RegistrationResult
    {
        Success,
        MissingFields,
        EmailAlreadyExists,
        Failure
    }

    public enum LoginResult
    {
        Success,
        UserNotExit,
        InvalidEmailOrPassword
    }

    public enum CustomerUpdateResult
    {
        Success,
        EmailInUse,
        ValidationError,
        Failure
    }

    public enum JobStatus
    {
        Draft,        
        Pending,       
        InProgress,    
        Completed,     
        Cancelled      
    }

}
