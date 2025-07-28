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

    public enum StaffType
    {
        DRIVER,
        ASSISTANT
    }

    public enum ContainerType
    {
        Standard,
        Refrigerated,
        FlatRack,
        OpenTop,
        Tank
    }

    public enum InvoiceStatus
    {
        Draft,
        Sent,
        Paid,
        Overdue,
        Cancelled
    }

    public enum PaymentMethod
    {
        Cash,
        BankTransfer,
        CreditCard,
        Cheque,
        Other
    }

    public enum ReportType
    {
        Revenue,
        Customers,
        Jobs
    }

}
