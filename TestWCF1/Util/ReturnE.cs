using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWCF1.Util
{
    public class ReturnE
    {
        public static String[] REQUIRED = { "900", "ERROR_REQUIRED", "Mandatory [XX] fields are missing" };
        public static String[] LONG = { "901", "ERROR_LONG_PROPERTY", "[XX] message to long" };
        public static String[] UKNOWN = { "902", "ERROR_UKNOWN", "XX" };
        public static String[] SERVICE = { "903", "ERROR_SERVICE", "CRM Acces failed" };
        public static String[] INVALID_JSON = { "904", "ERROR_INVALID_JSON", "XX" };

        public static String[] LOGIN_KO = { "1700", "LOGIN_KO", "You send an invalid email or password" };
        public static String[] REGISTER_KO = { "1701", "ERROR_REGISTER_KO", "Email already exists: XX" };
        public static String[] REGISTER_HOGARLINK_KO = { "1702", "ERROR_REGISTER_HOGARLINK_KO", "Client already relationed with ZonaHogar: XX" };
        public static String[] REGISTER_CREATE_CLIENT_KO = { "1703", "REGISTER_CREATE_CLIENT_KO", "XX" };
        public static String[] OBTENER_DATOS_CLIENTE_KO = { "1704", "OBTENER_DATOS_CLIENTE_KO", "Client not found for Clientid: XX" };
        public static String[] OBTENER_DATOS_ZONAHOGAR_KO = { "1705", "OBTENER_DATOS_ZONAHOGAR_KO", "ZonaHogar not found for ZonaHogarId: XX" };
        public static String[] OBTENER_APLATA_EMAIL_KO = { "1706", "OBTENER_APLATA_EMAIL_KO", "Email linked to another user: XX" };
        public static String[] OBTENER_APLATA_KO = { "1707", "OBTENER_APLATA_KO", "User not found for id: XX" };
        public static String[] ERROR_MODIFICAR_CLIENTE_KO = { "1708", "ERROR_MODIFICAR_CLIENTE_KO", "Id client not found: XX" };

    }
}