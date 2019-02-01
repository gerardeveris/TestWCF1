using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TestWCF1
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICustomer_Service" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICustomer_Service
    {
        [OperationContract]
        void DoWork();
    }
}
