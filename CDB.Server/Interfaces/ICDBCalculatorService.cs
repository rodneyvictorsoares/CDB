using CDB.Server.Models;

namespace CDB.Server.Interfaces
{
    public interface ICDBCalculatorService
    {
        CDBCalculoResponse Calcular(CDBCalculoRequest request);
    }
}
