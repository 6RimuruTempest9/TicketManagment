namespace TicketManagement.Web.HttpClients.Results
{
    public class Result
    {
        private readonly ResultType _resultType;

        protected Result(ResultType resultType)
        {
            _resultType = resultType;
        }

        public ResultType ResultType => _resultType;
    }
}