namespace CNSMarketing.Infrastructure.Enums
{

    public enum TokenResult
    {
        Ok,
        Invalid,
        NotFound,
        Expired
    }

    public enum ApiName
    {
        Linkedln = 1,
        Facebook = 2,
        Instagram = 3,
    }

    public enum TokenStatus
    {
        Passive,
        Active
    }
}
