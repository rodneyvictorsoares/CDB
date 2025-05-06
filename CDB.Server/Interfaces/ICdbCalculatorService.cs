using CDB.Server.Models;

namespace CDB.Server.Interfaces
{
    public interface ICdbCalculatorService
    {
        CdbCalculoResponse Calcular(CdbCalculoRequest request);
    }
}
