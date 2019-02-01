using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TestWCF3
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        //Fault error
        public int Divisio(Divide operacio)
        {
            int n1 = operacio.Numero;
            int n2 = operacio.Divisor;
            try
            {
                return n1 / n2;
            }
            catch (DivideByZeroException)
            {
                MathFault mf = new MathFault();
                mf.Operation = "division";
                mf.ProblemType = "divide by zero";
                throw new FaultException<MathFault>(mf);
                //throw new FaultException("Error al dividir");
            }
        }
    }
}
