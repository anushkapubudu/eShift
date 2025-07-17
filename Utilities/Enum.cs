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
        InvalidEmailOrPassword,
        UserNotExit,
        Failure
    }

    public enum UserRole
    {
        Admin,
        Staff,
        Customer
    }

    public enum ShiftStatus
    {
        Pending,
        Scheduled,
        Completed,
        Cancelled
    }
}
