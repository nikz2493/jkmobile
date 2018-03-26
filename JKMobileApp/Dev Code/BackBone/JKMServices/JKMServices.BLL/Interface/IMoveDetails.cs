namespace JKMServices.BLL.Interface
{
    public interface IMoveDetails
    {
        string GetContactForMove(string moveID);
        string GetMoveData(string moveID);
        string GetMoveID(string customerID);
        string GetMoveList(string statusReason);
        string PutMoveData(string moveID, string requestData);
    }
}