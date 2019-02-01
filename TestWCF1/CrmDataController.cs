using System;
using System.Collections.Generic;
using Microsoft.Pfe.Xrm;
using System.Net;
using System.Reflection;
using Microsoft.Xrm.Sdk;
using System.Web.Script.Serialization;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;
using Newtonsoft.Json;
using TestWCF1.Messages;
using TestWCF1.Messages.SolicitudVisita;
using TestWCF1.Objects;
using TestWCF1.Messages;
using TestWCF1.Messages.APlata;
using TestWCF1.Util;
using TestWCF1.Messages.Return_APlata;

namespace TestWCF1
{
    public class CrmDataController 
    {
        #region Configuración
        OrganizationServiceManager Manager { get; set; }
        IOrganizationService _orgService;

        Dictionary<int, StateEstadoPeticion> TablaEstados { get; set; }
        Dictionary<string, string> condition { get; set; }
        List<string> column { get; set; }


        public CrmDataController(string organization, string username, string password)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var serverUri = XrmServiceUriFactory.CreateOnlineOrganizationServiceUri(organization, CrmOnlineRegion.EMEA);

            this.Manager = new OrganizationServiceManager(serverUri, username, password);
            _orgService = this.Manager.GetProxy();

            TablaEstados = new Dictionary<int, StateEstadoPeticion>();
            var activo_pdte = new StateEstadoPeticion() { State = 0, EstadoPeticion = 0 };
            TablaEstados.Add(964610004, activo_pdte);
            TablaEstados.Add(964610005, activo_pdte);
            TablaEstados.Add(964610006, activo_pdte);
            TablaEstados.Add(964610007, activo_pdte);
            TablaEstados.Add(964610008, activo_pdte);
            TablaEstados.Add(964610009, activo_pdte);
            TablaEstados.Add(964610010, activo_pdte);
            TablaEstados.Add(964610011, activo_pdte);

            var resuelto_aprobadaSinEfecto = new StateEstadoPeticion() { State = 1, EstadoPeticion = 3 };
            TablaEstados.Add(964610012, resuelto_aprobadaSinEfecto);
            TablaEstados.Add(964610013, resuelto_aprobadaSinEfecto);

            var resuelto_aprobada = new StateEstadoPeticion() { State = 1, EstadoPeticion = 1 };
            TablaEstados.Add(964610014, resuelto_aprobada);

            var resuelto_rechazada = new StateEstadoPeticion() { State = 1, EstadoPeticion = 2 };
            TablaEstados.Add(964610015, resuelto_rechazada);

            var cancelado_cancelado = new StateEstadoPeticion() { State = 2, EstadoPeticion = 4 };
            TablaEstados.Add(964610016, cancelado_cancelado);

            var cancelado_error = new StateEstadoPeticion() { State = 2, EstadoPeticion = 5 };
            TablaEstados.Add(964610017, cancelado_error);
        }

        #endregion

        #region Integraciones

        //INT12 - SolicitarVisita
        /*
        public DisponibilidadVisita SolicitarVisitaOld(SolicitudVisita solicitud)
        {
            DisponibilidadVisita respuesta = new DisponibilidadVisita();

            try
            {
                List<string> required = FieldsRequired(solicitud, SolicitarVisitaRequired());
                List<string> longer = FieldsLong(solicitud, SolicitarVisitaLong());
                Return_Error error_ChekRequiredLong = ObjectReturn_ChekRequiredLong(required, longer);

                if (error_ChekRequiredLong == null)
                {
                    Inmueble inmueble = new Inmueble();
                    inmueble.name = solicitud.id_inmueble;
                     
                    inmueble = InmuebleQuery(inmueble);
                    EntityCollection visitas = VisitasQuery(inmueble);
                    //FechasOcupadas listaFechas = new FechasOcupadas();
                    var listafechasOcupadas = new List<FechasOcupadas>();
                    foreach (var visita in visitas.Entities)
                    {
                        listafechasOcupadas.Add(new FechasOcupadas(visita.GetAttributeValue<DateTime>("eve_fechanuevacita")));
                    }
                    respuesta.FechasOcupadas = listafechasOcupadas;
                }
                else
                {
                    respuesta.error = error_ChekRequiredLong;
                }
            }
            catch (Exception e)
            {
                respuesta.error = Error("902", "ERROR_UKNOWN", errorMaxLength(e.Message));
            }

            return respuesta;
        }
        */

        public Return_SolicitarDisponibilidadVisita SolicitarDisponibilidadVisita(SolicitudVisita solicitud)
        {
            Return_SolicitarDisponibilidadVisita respuesta = new Return_SolicitarDisponibilidadVisita();
            try
            {
                List<string> required = FieldsRequired(solicitud, SolicitarVisitaRequired());
                List<string> longer = FieldsLong(solicitud, SolicitarVisitaLong());
                Return_Error error_ChekRequiredLong = ObjectReturn_ChekRequiredLong(required, longer);

                if (error_ChekRequiredLong == null)
                {
                    Inmueble inmueble = new Inmueble();
                    inmueble.name = solicitud.IdInmueble;
                    inmueble = InmuebleQuery(inmueble);

                    EntityCollection visitas = VisitasQuery(inmueble);

                    var listafechasOcupadas = new List<FechasOcupadas>();
                    int contador = visitas.Entities.Count;
                    respuesta.FechasOcupadas = new FechasOcupadas[contador];

                    for (int i= 0; i< contador; i++)
                    {
                        
                        respuesta.FechasOcupadas[i] = new FechasOcupadas(visitas[i].GetAttributeValue<DateTime>("eve_fechanuevacita"));
                    }
                }
                else
                {
                    respuesta.Error = error_ChekRequiredLong;
                }
            }
            catch (Exception e)
            {
                //respuesta.Error = Error("902", "ERROR_UKNOWN", errorMaxLength(e.Message));
            }
            return respuesta;
        }

        public Inmueble InmuebleQuery(Inmueble inmueble)
        {
            var cond = new Dictionary<string, string>();
            var col = new List<string>();
            cond.Add("eve_iderp", inmueble.name);

            IEnumerable<string> campos = new string[] { "eve_inmueblesid"};
            col.AddRange(campos);
            Entity res = RetrieveCustomFields("eve_inmuebles", cond, col);
            inmueble.IdInmueble = res.GetAttributeValue<Guid>("eve_inmueblesid");
            return inmueble;
        }

        public EntityCollection VisitasQuery(Inmueble inmueble)
        {
            QueryExpression query = new QueryExpression();
            query.EntityName = "appointment";
            query.ColumnSet = new ColumnSet("eve_fechanuevacita");

            ConditionExpression condition1 = new ConditionExpression();
            condition1.AttributeName = "eve_inmueble";
            condition1.Operator = ConditionOperator.Equal;
            condition1.Values.Add(inmueble.IdInmueble);

            ConditionExpression condition2 = new ConditionExpression();
            condition2.AttributeName = "eve_fechanuevacita";
            condition2.Operator = ConditionOperator.NextXDays;
            condition2.Values.Add(30);

            FilterExpression filter1 = new FilterExpression();
            filter1.Conditions.Add(condition1);
            filter1.Conditions.Add(condition2);
            query.Criteria.AddFilter(filter1);
            
            EntityCollection visitas = _orgService.RetrieveMultiple(query);
            return visitas;
        }

        public List<string> SolicitarVisitaRequired()
        {
            List<string> required = new List<string>();
            required.Add("id_inmueble");
            return required;
        }

        public Dictionary<string, int> SolicitarVisitaLong()
        {
            var fieldslonger = new Dictionary<string, int>();
            fieldslonger.Add("id_inmueble", 36);
            return fieldslonger;
        }

        #endregion

        //########## WS 5 - APLATA
        /// <summary>
        /// Realizar la búsqueda del cliente y zona hogar, buscando por “id_zona_hogar” e “id_cliente” indicado en los parámetros de entrada.
        /// c.Si se encuentra un cliente y zona hogar coincidentes:
        /// i.Devolveremos dos nuevos campos que se calcularan en CRM
        /// i.Valor preautorizado del cliente
        /// ii.	Valor preautorizado de la zona hogar
        /// </summary>
        /// <param name="aplata"></param>
        /// <returns></returns>
        public ReturnAPlata APlata(APlata aplata)
        {
            ReturnAPlata obj = new ReturnAPlata();
            try
            {
                List<string> required = FieldsRequired(aplata, APlataRequired());
                List<string> longer = FieldsLong(aplata, APlataLong());
                Return_Error error_ChekRequiredLong = ObjectReturn_ChekRequiredLong(required, longer);

                if (error_ChekRequiredLong == null)
                {
                    bool client_exists = checkClientIdExistence(aplata.id_cliente);
                    if (client_exists)
                    {
                        bool email_linked = checkEmailLinked_ToAnotherUser(aplata.id_cliente, aplata.email);
                        if (!email_linked)
                        {


                            decimal preautorizado_cliente = CalculatePreautorizado_Cliente(aplata);
                            Entity cliente = CreateEntityCuenta_Aplata(aplata, preautorizado_cliente);
                            UpdateEntity(cliente);

                            decimal preautorizado_hogar = CalculatePreautorizado_Hogar(aplata);
                            Entity zonahogar = CreateEntityZonaHogar_Aplata(aplata, preautorizado_hogar);
                            UpdateEntity(zonahogar);


                            obj = returnObject_APlata(preautorizado_cliente, preautorizado_hogar);

                        }
                        else
                        {
                            obj.Error = Error(ReturnE.OBTENER_APLATA_EMAIL_KO, aplata.email);
                        }
                    }
                    else
                    {
                        obj.Error = Error(ReturnE.OBTENER_APLATA_KO, aplata.id_cliente);
                    }
                }
                else
                {
                    obj.Error = error_ChekRequiredLong;
                }
            }
            catch (Exception e)
            {
                obj.Error = Error(ReturnE.UKNOWN, errorMaxLength(e.ToString()));
            }
            return obj;
        }
        private Entity CreateEntityZonaHogar_Aplata(APlata aplata, decimal preautorizado_hogar)
        {
            Entity zonaHogar = new Entity("eve_unidadfamiliar");
            zonaHogar.Id = new Guid(aplata.id_zona_hogar);
            zonaHogar["eve_valorpreautorizado"] = preautorizado_hogar;

            return zonaHogar;
        }
        private decimal CalculatePreautorizado_Hogar(APlata aplata)
        {

            EntityCollection clientes = Relationed_CuentaForZonaHogar(new Guid(aplata.id_zona_hogar));

            decimal valorfinal = decimal.Zero;
            foreach (Entity cliente in clientes.Entities)
            {
                decimal valoret = cliente.GetAttributeValue<decimal>("eve_valorpreautorizado");
                valorfinal += valoret;
            }

            return valorfinal;
        }
        private Entity QueryZonaHogarPreautorizado(string id_zona_hogar)
        {
            var cond = new Dictionary<string, string>();
            var col = new List<string>();
            cond.Add("eve_unidadfamiliarid", id_zona_hogar);
            IEnumerable<string> campos = new string[] { "eve_valorpreautorizado" };
            col.AddRange(campos);


            Entity client = RetrieveCustomFields("eve_unidadfamiliar", cond, col);
            return client;
        }
        private decimal CalculatePreautorizado_Cliente(APlata aplata)
        {
            Entity parametrosAplicacion = ObtenerParametrosAplicacionAPlata();
            int valorSP = parametrosAplicacion.GetAttributeValue<int>("eve_surplus");
            decimal valorEndeudamiento = parametrosAplicacion.GetAttributeValue<decimal>("eve_endeudamiento");

            decimal preautorizado_cliente = ((aplata.ingresos_mensuales - aplata.gastos_mensuales) - (aplata.num_personas * valorSP)) * (valorEndeudamiento);
            return preautorizado_cliente;

        }
        private Entity ObtenerParametrosAplicacionAPlata()
        {
            var cond = new Dictionary<string, string>();
            var col = new List<string>();
            cond.Add("eve_parametrosdeaplicacionid", "C76A523A-2B1A-E711-80F4-5065F38B3601");
            col.Add("eve_surplus");
            col.Add("eve_endeudamiento");
            Entity account = RetrieveCustomFields("eve_parametrosdeaplicacion", cond, col);

            return account;
        }
        private bool checkEmailLinked_ToAnotherUser(string id_cliente, string email)
        {
            bool linked = true;

            Entity account = Retrieve("account", "emailaddress1", email);

            if (id_cliente.Equals(account.Id.ToString()))
            {
                linked = false;
            }


            return linked;
        }
        private Entity CreateEntityCuenta_Aplata(APlata aplata, decimal valorpreautorizado)
        {
            Entity cliente = new Entity("account");
            cliente.Id = new Guid(aplata.id_cliente);

            cliente["eve_validacionselfie"] = aplata.validacion_selfie;
            cliente["eve_nombre"] = aplata.nombre;
            cliente["eve_apellidos"] = aplata.apellidos;
            cliente["name"] = aplata.nombre + " " + aplata.apellidos;
            cliente["telephone3"] = aplata.telefono;
            cliente["emailaddress1"] = aplata.email;
            cliente["eve_nacionalidad"] = aplata.nacionalidad;
            cliente["eve_nif"] = aplata.dni;
            cliente["address1_line1"] = aplata.direccion;
            cliente["eve_sexo"] = aplata.genero;
            cliente["eve_numeropersonas"] = aplata.num_personas;

            cliente["eve_destinooperacionalquiler"] = new OptionSetValue(aplata.destino_operacion);
            cliente["eve_responsabilidadpublica"] = aplata.responsabilidad_publica;
            cliente["eve_relacionresponsabilidad"] = aplata.relacion_responsabilidad;
            cliente["eve_tipodocumento"] = new OptionSetValue(aplata.tipo_documento);
            cliente["eve_ingresosmensuales"] = aplata.ingresos_mensuales;
            cliente["eve_gastosmensuales"] = aplata.gastos_mensuales;
            cliente["eve_profesion"] = aplata.profesion;
            cliente["eve_tipologiaweb"] = new OptionSetValue(2);
            cliente["eve_valorpreautorizado"] = valorpreautorizado;


            if (aplata.fecha_nacimiento != null)
            {
                cliente["eve_fechadenacimiento"] = ConvertToDateTime(aplata.fecha_nacimiento);
            }
            else
            {
                cliente["eve_fechadenacimiento"] = aplata.fecha_nacimiento;
            }
            if (!(String.IsNullOrEmpty(aplata.estado_civil)))
            {
                cliente["eve_estadocivil"] = new EntityReference("eve_estadocivilregimen", estado_civilGetId(aplata.estado_civil));
            }
            if (!(String.IsNullOrEmpty(aplata.situacion_laboral)))
            {
                cliente["eve_situacionlaboral"] = new EntityReference("eve_situacionlaboral", situacion_laboralGetId(aplata.situacion_laboral));
            }
            if (!(String.IsNullOrEmpty(aplata.pais_nacimiento)))
            {
                cliente["eve_paisdenacimiento"] = new EntityReference("eve_pais", paisGetId(aplata.pais_nacimiento));
            }
            if (!(String.IsNullOrEmpty(aplata.pais_residencia)))
            {
                cliente["eve_paisderesidencia"] = new EntityReference("eve_pais", paisGetId(aplata.pais_residencia));
            }





            return cliente;
        }
        private ReturnAPlata returnObject_APlata(decimal preautorizado_cliente, decimal preautorizado_hogar)
        {
            ReturnAPlata respAplata = new ReturnAPlata();
            respAplata.PreautorizadoCliente = preautorizado_cliente;
            respAplata.PreautorizadoHogar = preautorizado_hogar;
            return respAplata;
        }
        public List<string> APlataRequired()
        {
            List<string> required = new List<string>();
            required.Add("id_zona_hogar");
            required.Add("id_cliente");
            required.Add("validacion_selfie");
            required.Add("nombre");
            required.Add("apellidos");
            required.Add("telefono");
            required.Add("email");
            required.Add("fecha_nacimiento");
            required.Add("nacionalidad");
            required.Add("dni");
            required.Add("tipo_documento");
            required.Add("direccion");
            required.Add("genero");
            required.Add("estado_civil");
            required.Add("responsabilidad_publica");
            required.Add("relacion_responsabilidad");
            required.Add("profesion");
            required.Add("situacion_laboral");
            required.Add("destino_operacion");
            required.Add("gastos_mensuales");
            required.Add("ingresos_mensuales");
            required.Add("num_personas");


            return required;
        }
        public Dictionary<string, int> APlataLong()
        {
            var fieldslonger = new Dictionary<string, int>();
            fieldslonger.Add("id_zona_hogar", 36);
            fieldslonger.Add("id_cliente", 36);
            return fieldslonger;
        }
        public EntityCollection Relationed_CuentaForZonaHogar(Guid hogarid)
        {
            EntityCollection hogares = null;
            try
            {
                QueryExpression query = new QueryExpression("account");
                query.ColumnSet = new ColumnSet(new[] { "eve_valorpreautorizado" });

                LinkEntity linkInmueblesIntermediate = new LinkEntity("account", "eve_account_eve_unidadfamiliar", "accountid", "accountid", JoinOperator.Inner);
                //linkInmueblesIntermediate.Columns = new ColumnSet(new[] { "eve_valorpreautorizado" });

                LinkEntity linkIntermediateOpportunity = new LinkEntity("eve_account_eve_unidadfamiliar", "eve_unidadfamiliar", "eve_unidadfamiliarid", "eve_unidadfamiliarid", JoinOperator.Inner);
                linkInmueblesIntermediate.LinkEntities.Add(linkIntermediateOpportunity);



                //query.LinkEntities[0].EntityAlias = "account";


                query.LinkEntities.Add(linkInmueblesIntermediate);

                linkIntermediateOpportunity.LinkCriteria = new FilterExpression();
                linkIntermediateOpportunity.LinkCriteria.AddCondition(new ConditionExpression("eve_unidadfamiliarid", ConditionOperator.Equal, hogarid));

                hogares = this.Manager.GetProxy().RetrieveMultiple(query);

            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Error on function Relationed_CuentaForZonaHogar: ", errorMaxLength(ex.Message));
            }

            return hogares;

        }

        //######################## UTILS ###########################//
        private Guid situacion_laboralGetId(string situacion_laboral)
        {
            Entity res = Retrieve("eve_situacionlaboral", "eve_codigo", situacion_laboral);
            if (res == null)
            {
                throw new System.ArgumentException("Error on funcion situacion_laboralGetId: Situacion Laboral not found for reference [" + situacion_laboral + "]");
            }
            return res.Id;
        }
        private Guid estado_civilGetId(string estado_civil)
        {
            Entity res = Retrieve("eve_estadocivilregimen", "eve_codigo", estado_civil);
            if (res == null)
            {
                throw new System.ArgumentException("Error on funcion estado_civilGetId: Estado civil not found for reference [" + estado_civil + "]");
            }
            return res.Id;
        }
        private Guid paisGetId(string pais)
        {
            Entity res = Retrieve("eve_pais", "eve_codigo", pais);
            if (res == null)
            {
                throw new System.ArgumentException("Error on funcion paisGetId: Pais not found for reference [" + pais + "]");
            }

            return res.Id;
        }
        private string situacion_laboralGetCode(Guid id)
        {
            var cond = new Dictionary<string, string>();
            var col = new List<string>();
            cond.Add("eve_situacionlaboralid", id.ToString());
            col.Add("eve_codigo");
            Entity res = RetrieveCustomFields("eve_situacionlaboral", cond, col);

            return res.GetAttributeValue<string>("eve_codigo");
        }
        private string estado_civilGetCode(Guid id)
        {
            var cond = new Dictionary<string, string>();
            var col = new List<string>();
            cond.Add("eve_estadocivilregimenid", id.ToString());
            col.Add("eve_codigo");
            Entity res = RetrieveCustomFields("eve_estadocivilregimen", cond, col);

            return res.GetAttributeValue<string>("eve_codigo");
        }
        private string paisGetCode(Guid id)
        {
            var cond = new Dictionary<string, string>();
            var col = new List<string>();
            cond.Add("eve_paisid", id.ToString());
            col.Add("eve_codigo");
            Entity res = RetrieveCustomFields("eve_pais", cond, col);

            return res.GetAttributeValue<string>("eve_codigo");
        }
        private DateTime ConvertToDateTime(string value)
        {
            /*
            // Try to convert various date strings.
            dateString = "05/01/1996";
            ConvertToDateTime(dateString);
            dateString = "Tue Apr 28, 2009";
            ConvertToDateTime(dateString);
            dateString = "Wed Apr 28, 2009";
            ConvertToDateTime(dateString);
            dateString = "06 July 2008 7:32:47 AM";
            ConvertToDateTime(dateString);
            dateString = "17:32:47.003";
            ConvertToDateTime(dateString);
            // Convert a string returned by DateTime.ToString("R").
            dateString = "Sat, 10 May 2008 14:32:17 GMT";
            ConvertToDateTime(dateString);
            // Convert a string returned by DateTime.ToString("o").
            dateString = "2009-05-01T07:54:59.9843750-04:00";
            ConvertToDateTime(dateString);
            */

            DateTime convertedDate;
            try
            {
                convertedDate = Convert.ToDateTime(value);
            }
            catch (FormatException ex)
            {
                throw new System.ArgumentException("Error al ConvertToDateTime. Error: [{0}]", ex.Message);
            }
            return convertedDate;

            // The example displays the following output:
            //    '' converts to 1/1/0001 12:00:00 AM Unspecified time.
            //    '' is not in the proper format.
            //    'not a date' is not in the proper format.
            //    '05/01/1996' converts to 5/1/1996 12:00:00 AM Unspecified time.
            //    'Tue Apr 28, 2009' converts to 4/28/2009 12:00:00 AM Unspecified time.
            //    'Wed Apr 28, 2009' is not in the proper format.
            //    '06 July 2008 7:32:47 AM' converts to 7/6/2008 7:32:47 AM Unspecified time.
            //    '17:32:47.003' converts to 5/30/2008 5:32:47 PM Unspecified time.
            //    'Sat, 10 May 2008 14:32:17 GMT' converts to 5/10/2008 7:32:17 AM Local time.
            //    '2009-05-01T07:54:59.9843750-04:00' converts to 5/1/2009 4:54:59 AM Local time.
        }
        private bool checkClientIdExistence(string clientid)
        {
            bool exists = false;
            Entity account = Retrieve("account", "accountid", clientid);
            if (account != null)
            {
                exists = true;
            }
            return exists;
        }
        public void DeleteEntity(string EntityName, Guid entityid)
        {
            try
            {
                this.Manager.GetProxy().Delete(EntityName, entityid);
            }
            catch (System.ServiceModel.FaultException ex)
            {
                throw new System.ArgumentException("Error DeleteEntity. Error:", errorMaxLength(ex.Message));
            }
        }
        public void UpdateEntity(Entity entity)
        {
            try
            {
                this.Manager.GetProxy().Update(entity);
            }
            catch (System.ServiceModel.FaultException ex)
            {
                throw new System.Exception("Error on function UpdateEntity. Error: " + errorMaxLength(ex.Message));
            }
        }
        private Guid CreateEntity(Entity entity)
        {
            Guid entityid = Guid.Empty;
            try
            {
                entityid = this.Manager.GetProxy().Create(entity);
            }
            catch (System.ServiceModel.FaultException ex)
            {
                throw new System.ArgumentException("Error on function CreateEntity. Error: " + errorMaxLength(ex.Message));

            }
            return entityid;
        }
        public Return_Error ObjectReturn_ChekRequiredLong(List<string> required, List<string> longer)
        {
            Return_Error obj = null;
            if (required.Count == 0)
            {
                if (longer.Count == 0)
                {

                }
                else
                {
                    obj = Error(ReturnE.LONG, ListString(longer));
                }
            }
            else
            {
                obj = Error(ReturnE.REQUIRED, ListString(required));
            }

            return obj;
        }
        protected internal void AssociateRegisters(string entityName, Guid entityId, string relationship_name, EntityReferenceCollection related_entities)
        {
            try
            {
                Relationship relationship = new Relationship(relationship_name);
                this.Manager.GetProxy().Associate(entityName, entityId, relationship, related_entities);
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Error on function AssociateRegisters. Error: " + errorMaxLength(ex.Message));
            }
        }
        public string errorMaxLength(string e)
        {
            if (e.Length > 500)
            {
                e = e.Substring(0, 500);
            }
            return e;
        }
        public List<string> FieldsLong(Object obj, Dictionary<string, int> errorFieldsLong)
        {
            List<string> results = new List<string>();
            try
            {
                var properties = GetProperties(obj);
                foreach (KeyValuePair<string, int> field in errorFieldsLong)
                {
                    foreach (var p in properties)
                    {
                        if (field.Key.Equals(p.Name))
                        {
                            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(p.Name);
                            String value = (String)(pi.GetValue(obj, null));

                            if (!(string.IsNullOrEmpty(value)))
                            {
                                if (value.ToString().Length > field.Value)
                                {
                                    results.Add(p.Name);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Error on function FieldsLong. Error: " + errorMaxLength(ex.Message));
            }
            return results;
        }
        public List<string> FieldsRequired(Object obj, List<string> errorFieldsRequired)
        {

            List<string> results = new List<string>();
            try
            {


                var properties = GetProperties(obj);

                /*
                foreach (PropertyInfo field in properties)
                {

                    string e = field.PropertyType.Name;
                    if (!(e.Equals("String")))
                    {

                        System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(e);
                        object value = (object)(pi.GetValue(obj, null));

                        List<string> results2 = FieldsRequired(value, errorFieldsRequired);
                        foreach (var item in results2)
                        {
                            results.Add(item);
                        }
                    }
                }
                */


                foreach (var field in errorFieldsRequired)
                {
                    foreach (var p in properties)
                    {

                        if (p.Name.Equals(field))
                        {
                            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(p.Name);
                            var epp = pi.ToString();

                            if (epp.Contains("System.Decimal"))
                            {
                                decimal dec = (decimal)(pi.GetValue(obj, null));
                                if (decimal.Equals(decimal.Zero, dec))
                                {
                                    results.Add(p.Name);
                                }
                            }
                            else if (epp.Contains("Int32"))
                            {
                                int value = (int)(pi.GetValue(obj, null));
                                if (value == 0)
                                {
                                    results.Add(p.Name);
                                }
                            }
                            else if (epp.Contains("Boolean"))
                            {

                            }
                            else
                            {
                                String value = (String)(pi.GetValue(obj, null));
                                if (string.IsNullOrEmpty(value))
                                {
                                    results.Add(p.Name);
                                }
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Error on function FieldsRequired. Error: ", errorMaxLength(ex.Message));
            }
            return results;
        }
        private static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }
        public static string ListString(List<string> list)
        {
            return String.Join(", ", list.ToArray());
        }
        public Return_Error Error(String[] reponse, string descripcion)
        {

            Return_Error respError = new Return_Error();
            respError.response_code = reponse[0];
            respError.response_title = reponse[1];

            if (reponse[2].Contains("XX"))
            {
                respError.response_description = reponse[2].Replace("XX", descripcion);
            }

            return respError;
        }
        public Return_Error Error(String[] reponse)
        {
            Return_Error respError = new Return_Error();
            respError.response_code = reponse[0];
            respError.response_title = reponse[1];
            respError.response_description = reponse[2];
            return respError;
        }
        private bool checkEmailExistence(string email)
        {
            bool exists = false;
            Entity contact = Retrieve("contact", "emailaddress1", email);
            Entity cuenta = Retrieve("account", "emailaddress1", email);
            if (contact != null || cuenta != null)
            {
                exists = true;
            }
            return exists;
        }
        private Entity Retrieve(string pEntity, string pField, string pValue)
        {
            Entity retVal = null;
            try
            {
                var query = new QueryByAttribute(pEntity);
                query.AddAttributeValue(pField, pValue);
                query.ColumnSet = new ColumnSet(pField);
                var records = this.Manager.GetProxy().RetrieveMultiple(query);
                if (records.Entities != null && records.Entities.FirstOrDefault() != null) retVal = records.Entities.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw new System.ArgumentException("Error on function Retrive. " + errorMaxLength("RETRIVE ERROR"));
            }
            return retVal;
        }
        private Entity RetrieveCustomFields(string pEntity, Dictionary<string, string> attributes, List<string> columns)
        {
            Entity retVal = null;
            try
            {
                var query = new QueryByAttribute(pEntity);
                foreach (var pair in attributes)
                {
                    query.AddAttributeValue(pair.Key, pair.Value);
                }

                if (columns.Count > 0)
                {
                    ColumnSet col = new ColumnSet();
                    foreach (var column in columns)
                    {
                        col.AddColumn(column);
                    }
                    query.ColumnSet = col;
                }

                var records = this.Manager.GetProxy().RetrieveMultiple(query);
                if (records.Entities != null && records.Entities.FirstOrDefault() != null) retVal = records.Entities.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Error on function RetrieveCustomFields. " + errorMaxLength(ex.Message));
            }
            return retVal;
        }
        private Entity Retrieve(string pEntity, string pField, long pValue)
        {
            Entity retVal = null;
            try
            {
                var query = new QueryByAttribute(pEntity);
                query.AddAttributeValue(pField, pValue);
                query.ColumnSet = new ColumnSet(pField);
                var records = this.Manager.GetProxy().RetrieveMultiple(query);
                if (records.Entities != null && records.Entities.FirstOrDefault() != null) retVal = records.Entities.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw new System.ArgumentException("Error on function Retrieve: " + errorMaxLength(ex.Message));
            }
            return retVal;
        }
        private EntityCollection RetrieveList(string pEntity, string pField, string pValue)
        {
            EntityCollection records = null;
            try
            {
                var query = new QueryByAttribute(pEntity);
                query.AddAttributeValue(pField, pValue);
                query.ColumnSet = new ColumnSet(pField);
                records = this.Manager.GetProxy().RetrieveMultiple(query);
            }
            catch (Exception ex)
            {

                throw new System.ArgumentException("Error on function RetrieveList: " + errorMaxLength(ex.Message));
            }
            return records;
        }
        private EntityCollection RetrieveCustomFieldsList(string pEntity, Dictionary<string, string> attributes, IEnumerable<string> columns)
        {
            EntityCollection records = null;
            try
            {
                var query = new QueryByAttribute(pEntity);
                foreach (var pair in attributes)
                {
                    query.AddAttributeValue(pair.Key, pair.Value);
                }

                if (columns.Count() > 0)
                {
                    ColumnSet col = new ColumnSet();
                    foreach (var column in columns)
                    {
                        col.AddColumn(column);
                    }

                    query.ColumnSet = col;
                }

                records = this.Manager.GetProxy().RetrieveMultiple(query);
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Error on function RetrieveCustomFieldsList: " + errorMaxLength(ex.Message));
            }
            return records;
        }

    }
}