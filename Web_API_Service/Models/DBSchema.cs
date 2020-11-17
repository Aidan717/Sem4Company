using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;

namespace Web_API_Service.Models {
    public class DBSchema {


            public int took { get; set; }
            public bool timed_out { get; set; }
            public _Shards _shards { get; set; }
            public Hits hits { get; set; }
        

        public class _Shards {
            public int total { get; set; }
            public int successful { get; set; }
            public int skipped { get; set; }
            public int failed { get; set; }
        }

        public class Hits {
            public Total total { get; set; }
            public float max_score { get; set; }
            public Hit[] hits { get; set; }
        }

        public class Total {
            public int value { get; set; }
            public string relation { get; set; }
        }

        public class Hit {
            public string _index { get; set; }
            public string _type { get; set; }
            public string _id { get; set; }
            public float _score { get; set; }
            public _Source _source { get; set; }
        }

        public class _Source {
            public string action { get; set; }
            public string activities { get; set; }

            [JsonPropertyName("activities.exceptions.className")]
            public string activitiesexceptionsclassName { get; set; }

            [JsonPropertyName("activities.exceptions.clientConnectionId")]
            public string activitiesexceptionsclientConnectionId { get; set; }

            [JsonPropertyName("activities.exceptions.data.helpLink.BaseHelpUrl")]
            public string activitiesexceptionsdatahelpLinkBaseHelpUrl { get; set; }

            [JsonPropertyName("activities.exceptions.data.helpLink.EvtID")]
            public string activitiesexceptionsdatahelpLinkEvtID { get; set; }

            [JsonPropertyName("activities.exceptions.data.helpLink.EvtSrc")]
            public string activitiesexceptionsdatahelpLinkEvtSrc { get; set; }

            [JsonPropertyName("activities.exceptions.data.helpLink.LinkId")]
            public string activitiesexceptionsdatahelpLinkLinkId { get; set; }

            [JsonPropertyName("activities.exceptions.data.helpLink.ProdName")]
            public string activitiesexceptionsdatahelpLinkProdName { get; set; }

            [JsonPropertyName("activities.exceptions.data.helpLink.ProdVer")]
            public string activitiesexceptionsdatahelpLinkProdVer { get; set; }

            [JsonPropertyName("activities.exceptions.errors.class")]
            public string activitiesexceptionserrorsclass { get; set; }

            [JsonPropertyName("activities.exceptions.errors.lineNumber")]
            public string activitiesexceptionserrorslineNumber { get; set; }

            [JsonPropertyName("activities.exceptions.errors.message")]
            public string activitiesexceptionserrorsmessage { get; set; }

            [JsonPropertyName("activities.exceptions.errors.number")]
            public string activitiesexceptionserrorsnumber { get; set; }

            [JsonPropertyName("activities.exceptions.errors.procedure")]
            public string activitiesexceptionserrorsprocedure { get; set; }

            [JsonPropertyName("activities.exceptions.errors.server")]
            public string activitiesexceptionserrorsserver { get; set; }

            [JsonPropertyName("activities.exceptions.errors.source")]
            public string activitiesexceptionserrorssource { get; set; }

            [JsonPropertyName("activities.exceptions.errors.state")]
            public string activitiesexceptionserrorsstate { get; set; }

            [JsonPropertyName("activities.exceptions.exceptionMethod")]
            public string activitiesexceptionsexceptionMethod { get; set; }

            [JsonPropertyName("activities.exceptions.fileNotFound_FileName")]
            public string activitiesexceptionsfileNotFound_FileName { get; set; }

            [JsonPropertyName("activities.exceptions.hResult")]
            public string activitiesexceptionshResult { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.className")]
            public string activitiesexceptionsinnerExceptionclassName { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.clientConnectionId")]
            public string activitiesexceptionsinnerExceptionclientConnectionId { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.data.helpLink.BaseHelpUrl")]
            public string activitiesexceptionsinnerExceptiondatahelpLinkBaseHelpUrl { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.data.helpLink.EvtID")]
            public string activitiesexceptionsinnerExceptiondatahelpLinkEvtID { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.data.helpLink.EvtSrc")]
            public string activitiesexceptionsinnerExceptiondatahelpLinkEvtSrc { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.data.helpLink.LinkId")]
            public string activitiesexceptionsinnerExceptiondatahelpLinkLinkId { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.data.helpLink.ProdName")]
            public string activitiesexceptionsinnerExceptiondatahelpLinkProdName { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.data.helpLink.ProdVer")]
            public string activitiesexceptionsinnerExceptiondatahelpLinkProdVer { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.errors.class")]
            public string activitiesexceptionsinnerExceptionerrorsclass { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.errors.lineNumber")]
            public string activitiesexceptionsinnerExceptionerrorslineNumber { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.errors.message")]
            public string activitiesexceptionsinnerExceptionerrorsmessage { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.errors.number")]
            public string activitiesexceptionsinnerExceptionerrorsnumber { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.errors.procedure")]
            public string activitiesexceptionsinnerExceptionerrorsprocedure { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.errors.server")]
            public string activitiesexceptionsinnerExceptionerrorsserver { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.errors.source")]
            public string activitiesexceptionsinnerExceptionerrorssource { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.errors.state")]
            public string activitiesexceptionsinnerExceptionerrorsstate { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.exceptionMethod")]
            public string activitiesexceptionsinnerExceptionexceptionMethod { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.hResult")]
            public string activitiesexceptionsinnerExceptionhResult { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.className")]
            public string activitiesexceptionsinnerExceptioninnerExceptionclassName { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.clientConnectionId")]
            public string activitiesexceptionsinnerExceptioninnerExceptionclientConnectionId { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.data.helpLink.BaseHelpUrl")]
            public string activitiesexceptionsinnerExceptioninnerExceptiondatahelpLinkBaseHelpUrl { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.data.helpLink.EvtID")]
            public string activitiesexceptionsinnerExceptioninnerExceptiondatahelpLinkEvtID { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.data.helpLink.EvtSrc")]
            public string activitiesexceptionsinnerExceptioninnerExceptiondatahelpLinkEvtSrc { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.data.helpLink.LinkId")]
            public string activitiesexceptionsinnerExceptioninnerExceptiondatahelpLinkLinkId { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.data.helpLink.ProdName")]
            public string activitiesexceptionsinnerExceptioninnerExceptiondatahelpLinkProdName { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.data.helpLink.ProdVer")]
            public string activitiesexceptionsinnerExceptioninnerExceptiondatahelpLinkProdVer { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.errors.class")]
            public string activitiesexceptionsinnerExceptioninnerExceptionerrorsclass { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.errors.lineNumber")]
            public string activitiesexceptionsinnerExceptioninnerExceptionerrorslineNumber { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.errors.message")]
            public string activitiesexceptionsinnerExceptioninnerExceptionerrorsmessage { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.errors.number")]
            public string activitiesexceptionsinnerExceptioninnerExceptionerrorsnumber { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.errors.procedure")]
            public string activitiesexceptionsinnerExceptioninnerExceptionerrorsprocedure { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.errors.server")]
            public string activitiesexceptionsinnerExceptioninnerExceptionerrorsserver { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.errors.source")]
            public string activitiesexceptionsinnerExceptioninnerExceptionerrorssource { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.errors.state")]
            public string activitiesexceptionsinnerExceptioninnerExceptionerrorsstate { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.exceptionMethod")]
            public string activitiesexceptionsinnerExceptioninnerExceptionexceptionMethod { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.hResult")]
            public string activitiesexceptionsinnerExceptioninnerExceptionhResult { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.innerException.className")]
            public string activitiesexceptionsinnerExceptioninnerExceptioninnerExceptionclassName { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.innerException.hResult")]
            public string activitiesexceptionsinnerExceptioninnerExceptioninnerExceptionhResult { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.innerException.message")]
            public string activitiesexceptionsinnerExceptioninnerExceptioninnerExceptionmessage { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.innerException.nativeErrorCode")]
            public string activitiesexceptionsinnerExceptioninnerExceptioninnerExceptionnativeErrorCode { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.innerException.remoteStackIndex")]
            public string activitiesexceptionsinnerExceptioninnerExceptioninnerExceptionremoteStackIndex { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.message")]
            public string activitiesexceptionsinnerExceptioninnerExceptionmessage { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.nativeErrorCode")]
            public string activitiesexceptionsinnerExceptioninnerExceptionnativeErrorCode { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.remoteStackIndex")]
            public string activitiesexceptionsinnerExceptioninnerExceptionremoteStackIndex { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.source")]
            public string activitiesexceptionsinnerExceptioninnerExceptionsource { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.innerException.stackTraceString")]
            public string activitiesexceptionsinnerExceptioninnerExceptionstackTraceString { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.message")]
            public string activitiesexceptionsinnerExceptionmessage { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.nativeErrorCode")]
            public string activitiesexceptionsinnerExceptionnativeErrorCode { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.remoteStackIndex")]
            public string activitiesexceptionsinnerExceptionremoteStackIndex { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.source")]
            public string activitiesexceptionsinnerExceptionsource { get; set; }

            [JsonPropertyName("activities.exceptions.innerException.stackTraceString")]
            public string activitiesexceptionsinnerExceptionstackTraceString { get; set; }

            [JsonPropertyName("activities.exceptions.message")]
            public string activitiesexceptionsmessage { get; set; }

            [JsonPropertyName("activities.exceptions.remoteStackIndex")]
            public string activitiesexceptionsremoteStackIndex { get; set; }

            [JsonPropertyName("activities.exceptions.source")]
            public string activitiesexceptionssource { get; set; }

            [JsonPropertyName("activities.exceptions.stackTraceString")]
            public string activitiesexceptionsstackTraceString { get; set; }

            [JsonPropertyName("activities.executionTime")]
            public string activitiesexecutionTime { get; set; }

            [JsonPropertyName("activities.readableName")]
            public string activitiesreadableName { get; set; }

            [JsonPropertyName("activities.type")]
            public string activitiestype { get; set; }

            [JsonPropertyName("activities.warnings")]
            public string activitieswarnings { get; set; }
            public string activityId { get; set; }
            public string activityType { get; set; }

            [JsonPropertyName("arguments.cart.identification.customerNumber")]
            public string argumentscartidentificationcustomerNumber { get; set; }

            [JsonPropertyName("arguments.cart.orderId")]
            public string argumentscartorderId { get; set; }

            [JsonPropertyName("arguments.order.orderId")]
            public string argumentsorderorderId { get; set; }
            public string caller { get; set; }
            public string cartId { get; set; }
            public string _class { get; set; }
            public string command { get; set; }
            public string commandType { get; set; }
            public string controller { get; set; }
            public string cropNumber { get; set; }
            public string customer { get; set; }
            public string customerNumber { get; set; }
            public string duration { get; set; }
            public string emails { get; set; }
            public string entryCount { get; set; }
            public string exception { get; set; }

            [JsonPropertyName("exception._httpCode")]
            public string exception_httpCode { get; set; }

            [JsonPropertyName("exception.action")]
            public string exceptionaction { get; set; }

            [JsonPropertyName("exception.args")]
            public string exceptionargs { get; set; }

            [JsonPropertyName("exception.className")]
            public string exceptionclassName { get; set; }

            [JsonPropertyName("exception.clientConnectionId")]
            public string exceptionclientConnectionId { get; set; }

            [JsonPropertyName("exception.data.helpLink.BaseHelpUrl")]
            public string exceptiondatahelpLinkBaseHelpUrl { get; set; }

            [JsonPropertyName("exception.data.helpLink.EvtID")]
            public string exceptiondatahelpLinkEvtID { get; set; }

            [JsonPropertyName("exception.data.helpLink.EvtSrc")]
            public string exceptiondatahelpLinkEvtSrc { get; set; }

            [JsonPropertyName("exception.data.helpLink.LinkId")]
            public string exceptiondatahelpLinkLinkId { get; set; }

            [JsonPropertyName("exception.data.helpLink.ProdName")]
            public string exceptiondatahelpLinkProdName { get; set; }

            [JsonPropertyName("exception.data.helpLink.ProdVer")]
            public string exceptiondatahelpLinkProdVer { get; set; }

            [JsonPropertyName("exception.detail.message")]
            public string exceptiondetailmessage { get; set; }

            [JsonPropertyName("exception.detail.stackTrace")]
            public string exceptiondetailstackTrace { get; set; }

            [JsonPropertyName("exception.detail.type")]
            public string exceptiondetailtype { get; set; }

            [JsonPropertyName("exception.errors.class")]
            public string exceptionerrorsclass { get; set; }

            [JsonPropertyName("exception.errors.errorCode")]
            public string exceptionerrorserrorCode { get; set; }

            [JsonPropertyName("exception.errors.helpLink")]
            public string exceptionerrorshelpLink { get; set; }

            [JsonPropertyName("exception.errors.lineNumber")]
            public string exceptionerrorslineNumber { get; set; }

            [JsonPropertyName("exception.errors.message")]
            public string exceptionerrorsmessage { get; set; }

            [JsonPropertyName("exception.errors.number")]
            public string exceptionerrorsnumber { get; set; }

            [JsonPropertyName("exception.errors.procedure")]
            public string exceptionerrorsprocedure { get; set; }

            [JsonPropertyName("exception.errors.server")]
            public string exceptionerrorsserver { get; set; }

            [JsonPropertyName("exception.errors.source")]
            public string exceptionerrorssource { get; set; }

            [JsonPropertyName("exception.errors.state")]
            public string exceptionerrorsstate { get; set; }

            [JsonPropertyName("exception.exceptionMethod")]
            public string exceptionexceptionMethod { get; set; }

            [JsonPropertyName("exception.failedRecipient")]
            public string exceptionfailedRecipient { get; set; }

            [JsonPropertyName("exception.hResult")]
            public string exceptionhResult { get; set; }

            [JsonPropertyName("exception.helpURL")]
            public string exceptionhelpURL { get; set; }

            [JsonPropertyName("exception.innerException._httpCode")]
            public string exceptioninnerException_httpCode { get; set; }

            [JsonPropertyName("exception.innerException.className")]
            public string exceptioninnerExceptionclassName { get; set; }

            [JsonPropertyName("exception.innerException.clientConnectionId")]
            public string exceptioninnerExceptionclientConnectionId { get; set; }

            [JsonPropertyName("exception.innerException.data.helpLink.BaseHelpUrl")]
            public string exceptioninnerExceptiondatahelpLinkBaseHelpUrl { get; set; }

            [JsonPropertyName("exception.innerException.data.helpLink.EvtID")]
            public string exceptioninnerExceptiondatahelpLinkEvtID { get; set; }

            [JsonPropertyName("exception.innerException.data.helpLink.EvtSrc")]
            public string exceptioninnerExceptiondatahelpLinkEvtSrc { get; set; }

            [JsonPropertyName("exception.innerException.data.helpLink.LinkId")]
            public string exceptioninnerExceptiondatahelpLinkLinkId { get; set; }

            [JsonPropertyName("exception.innerException.data.helpLink.ProdName")]
            public string exceptioninnerExceptiondatahelpLinkProdName { get; set; }

            [JsonPropertyName("exception.innerException.data.helpLink.ProdVer")]
            public string exceptioninnerExceptiondatahelpLinkProdVer { get; set; }

            [JsonPropertyName("exception.innerException.errors.class")]
            public string exceptioninnerExceptionerrorsclass { get; set; }

            [JsonPropertyName("exception.innerException.errors.lineNumber")]
            public string exceptioninnerExceptionerrorslineNumber { get; set; }

            [JsonPropertyName("exception.innerException.errors.message")]
            public string exceptioninnerExceptionerrorsmessage { get; set; }

            [JsonPropertyName("exception.innerException.errors.number")]
            public string exceptioninnerExceptionerrorsnumber { get; set; }

            [JsonPropertyName("exception.innerException.errors.procedure")]
            public string exceptioninnerExceptionerrorsprocedure { get; set; }

            [JsonPropertyName("exception.innerException.errors.server")]
            public string exceptioninnerExceptionerrorsserver { get; set; }

            [JsonPropertyName("exception.innerException.errors.source")]
            public string exceptioninnerExceptionerrorssource { get; set; }

            [JsonPropertyName("exception.innerException.errors.state")]
            public string exceptioninnerExceptionerrorsstate { get; set; }

            [JsonPropertyName("exception.innerException.exceptionMethod")]
            public string exceptioninnerExceptionexceptionMethod { get; set; }

            [JsonPropertyName("exception.innerException.hResult")]
            public string exceptioninnerExceptionhResult { get; set; }

            [JsonPropertyName("exception.innerException.helpURL")]
            public string exceptioninnerExceptionhelpURL { get; set; }

            [JsonPropertyName("exception.innerException.innerException._httpCode")]
            public string exceptioninnerExceptioninnerException_httpCode { get; set; }

            [JsonPropertyName("exception.innerException.innerException.className")]
            public string exceptioninnerExceptioninnerExceptionclassName { get; set; }

            [JsonPropertyName("exception.innerException.innerException.clientConnectionId")]
            public string exceptioninnerExceptioninnerExceptionclientConnectionId { get; set; }

            [JsonPropertyName("exception.innerException.innerException.data.helpLink.BaseHelpUrl")]
            public string exceptioninnerExceptioninnerExceptiondatahelpLinkBaseHelpUrl { get; set; }

            [JsonPropertyName("exception.innerException.innerException.data.helpLink.EvtID")]
            public string exceptioninnerExceptioninnerExceptiondatahelpLinkEvtID { get; set; }

            [JsonPropertyName("exception.innerException.innerException.data.helpLink.EvtSrc")]
            public string exceptioninnerExceptioninnerExceptiondatahelpLinkEvtSrc { get; set; }

            [JsonPropertyName("exception.innerException.innerException.data.helpLink.LinkId")]
            public string exceptioninnerExceptioninnerExceptiondatahelpLinkLinkId { get; set; }

            [JsonPropertyName("exception.innerException.innerException.data.helpLink.ProdName")]
            public string exceptioninnerExceptioninnerExceptiondatahelpLinkProdName { get; set; }

            [JsonPropertyName("exception.innerException.innerException.errors.class")]
            public string exceptioninnerExceptioninnerExceptionerrorsclass { get; set; }

            [JsonPropertyName("exception.innerException.innerException.errors.lineNumber")]
            public string exceptioninnerExceptioninnerExceptionerrorslineNumber { get; set; }

            [JsonPropertyName("exception.innerException.innerException.errors.message")]
            public string exceptioninnerExceptioninnerExceptionerrorsmessage { get; set; }

            [JsonPropertyName("exception.innerException.innerException.errors.number")]
            public string exceptioninnerExceptioninnerExceptionerrorsnumber { get; set; }

            [JsonPropertyName("exception.innerException.innerException.errors.procedure")]
            public string exceptioninnerExceptioninnerExceptionerrorsprocedure { get; set; }

            [JsonPropertyName("exception.innerException.innerException.errors.server")]
            public string exceptioninnerExceptioninnerExceptionerrorsserver { get; set; }

            [JsonPropertyName("exception.innerException.innerException.errors.source")]
            public string exceptioninnerExceptioninnerExceptionerrorssource { get; set; }

            [JsonPropertyName("exception.innerException.innerException.errors.state")]
            public string exceptioninnerExceptioninnerExceptionerrorsstate { get; set; }

            [JsonPropertyName("exception.innerException.innerException.exceptionMethod")]
            public string exceptioninnerExceptioninnerExceptionexceptionMethod { get; set; }

            [JsonPropertyName("exception.innerException.innerException.hResult")]
            public string exceptioninnerExceptioninnerExceptionhResult { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.className")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionclassName { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.clientConnectionId")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionclientConnectionId { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.data.helpLink.BaseHelpUrl")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptiondatahelpLinkBaseHelpUrl { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.data.helpLink.EvtID")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptiondatahelpLinkEvtID { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.data.helpLink.EvtSrc")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptiondatahelpLinkEvtSrc { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.data.helpLink.LinkId")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptiondatahelpLinkLinkId { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.data.helpLink.ProdName")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptiondatahelpLinkProdName { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.data.helpLink.ProdVer")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptiondatahelpLinkProdVer { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.errors.class")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionerrorsclass { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.errors.lineNumber")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionerrorslineNumber { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.errors.message")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionerrorsmessage { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.errors.number")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionerrorsnumber { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.errors.procedure")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionerrorsprocedure { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.errors.server")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionerrorsserver { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.errors.source")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionerrorssource { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.errors.state")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionerrorsstate { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.exceptionMethod")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionexceptionMethod { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.hResult")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionhResult { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.innerException.className")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptioninnerExceptionclassName { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.innerException.hResult")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptioninnerExceptionhResult { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.innerException.message")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptioninnerExceptionmessage { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.innerException.nativeErrorCode")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptioninnerExceptionnativeErrorCode { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.innerException.remoteStackIndex")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptioninnerExceptionremoteStackIndex { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.message")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionmessage { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.nativeErrorCode")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionnativeErrorCode { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.remoteStackIndex")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionremoteStackIndex { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.source")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionsource { get; set; }

            [JsonPropertyName("exception.innerException.innerException.innerException.stackTraceString")]
            public string exceptioninnerExceptioninnerExceptioninnerExceptionstackTraceString { get; set; }

            [JsonPropertyName("exception.innerException.innerException.message")]
            public string exceptioninnerExceptioninnerExceptionmessage { get; set; }

            [JsonPropertyName("exception.innerException.innerException.nativeErrorCode")]
            public string exceptioninnerExceptioninnerExceptionnativeErrorCode { get; set; }

            [JsonPropertyName("exception.innerException.innerException.remoteStackIndex")]
            public string exceptioninnerExceptioninnerExceptionremoteStackIndex { get; set; }

            [JsonPropertyName("exception.innerException.innerException.source")]
            public string exceptioninnerExceptioninnerExceptionsource { get; set; }

            [JsonPropertyName("exception.innerException.innerException.stackTraceString")]
            public string exceptioninnerExceptioninnerExceptionstackTraceString { get; set; }

            [JsonPropertyName("exception.innerException.message")]
            public string exceptioninnerExceptionmessage { get; set; }

            [JsonPropertyName("exception.innerException.nativeErrorCode")]
            public string exceptioninnerExceptionnativeErrorCode { get; set; }

            [JsonPropertyName("exception.innerException.remoteStackIndex")]
            public string exceptioninnerExceptionremoteStackIndex { get; set; }

            [JsonPropertyName("exception.innerException.source")]
            public string exceptioninnerExceptionsource { get; set; }

            [JsonPropertyName("exception.innerException.stackTraceString")]
            public string exceptioninnerExceptionstackTraceString { get; set; }

            [JsonPropertyName("exception.innerException.watsonBuckets")]
            public string exceptioninnerExceptionwatsonBuckets { get; set; }

            [JsonPropertyName("exception.innerException.watsonBuckets-skipped-count")]
            public string exceptioninnerExceptionwatsonBucketsskippedcount { get; set; }

            [JsonPropertyName("exception.innerExceptions.className")]
            public string exceptioninnerExceptionsclassName { get; set; }

            [JsonPropertyName("exception.innerExceptions.exceptionMethod")]
            public string exceptioninnerExceptionsexceptionMethod { get; set; }

            [JsonPropertyName("exception.innerExceptions.hResult")]
            public string exceptioninnerExceptionshResult { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.className")]
            public string exceptioninnerExceptionsinnerExceptionclassName { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.clientConnectionId")]
            public string exceptioninnerExceptionsinnerExceptionclientConnectionId { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.data.helpLink.BaseHelpUrl")]
            public string exceptioninnerExceptionsinnerExceptiondatahelpLinkBaseHelpUrl { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.data.helpLink.EvtID")]
            public string exceptioninnerExceptionsinnerExceptiondatahelpLinkEvtID { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.data.helpLink.EvtSrc")]
            public string exceptioninnerExceptionsinnerExceptiondatahelpLinkEvtSrc { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.data.helpLink.LinkId")]
            public string exceptioninnerExceptionsinnerExceptiondatahelpLinkLinkId { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.data.helpLink.ProdName")]
            public string exceptioninnerExceptionsinnerExceptiondatahelpLinkProdName { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.errors.class")]
            public string exceptioninnerExceptionsinnerExceptionerrorsclass { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.errors.lineNumber")]
            public string exceptioninnerExceptionsinnerExceptionerrorslineNumber { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.errors.message")]
            public string exceptioninnerExceptionsinnerExceptionerrorsmessage { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.errors.number")]
            public string exceptioninnerExceptionsinnerExceptionerrorsnumber { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.errors.procedure")]
            public string exceptioninnerExceptionsinnerExceptionerrorsprocedure { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.errors.server")]
            public string exceptioninnerExceptionsinnerExceptionerrorsserver { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.errors.source")]
            public string exceptioninnerExceptionsinnerExceptionerrorssource { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.errors.state")]
            public string exceptioninnerExceptionsinnerExceptionerrorsstate { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.exceptionMethod")]
            public string exceptioninnerExceptionsinnerExceptionexceptionMethod { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.hResult")]
            public string exceptioninnerExceptionsinnerExceptionhResult { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.innerException.className")]
            public string exceptioninnerExceptionsinnerExceptioninnerExceptionclassName { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.innerException.hResult")]
            public string exceptioninnerExceptionsinnerExceptioninnerExceptionhResult { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.innerException.message")]
            public string exceptioninnerExceptionsinnerExceptioninnerExceptionmessage { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.innerException.nativeErrorCode")]
            public string exceptioninnerExceptionsinnerExceptioninnerExceptionnativeErrorCode { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.innerException.remoteStackIndex")]
            public string exceptioninnerExceptionsinnerExceptioninnerExceptionremoteStackIndex { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.message")]
            public string exceptioninnerExceptionsinnerExceptionmessage { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.remoteStackIndex")]
            public string exceptioninnerExceptionsinnerExceptionremoteStackIndex { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.source")]
            public string exceptioninnerExceptionsinnerExceptionsource { get; set; }

            [JsonPropertyName("exception.innerExceptions.innerException.stackTraceString")]
            public string exceptioninnerExceptionsinnerExceptionstackTraceString { get; set; }

            [JsonPropertyName("exception.innerExceptions.message")]
            public string exceptioninnerExceptionsmessage { get; set; }

            [JsonPropertyName("exception.innerExceptions.remoteStackIndex")]
            public string exceptioninnerExceptionsremoteStackIndex { get; set; }

            [JsonPropertyName("exception.innerExceptions.source")]
            public string exceptioninnerExceptionssource { get; set; }

            [JsonPropertyName("exception.innerExceptions.stackTraceString")]
            public string exceptioninnerExceptionsstackTraceString { get; set; }

            [JsonPropertyName("exception.lineNumber")]
            public string exceptionlineNumber { get; set; }

            [JsonPropertyName("exception.linePosition")]
            public string exceptionlinePosition { get; set; }

            [JsonPropertyName("exception.message")]
            public string exceptionmessage { get; set; }

            [JsonPropertyName("exception.objectName")]
            public string exceptionobjectName { get; set; }

            [JsonPropertyName("exception.paramName")]
            public string exceptionparamName { get; set; }

            [JsonPropertyName("exception.remoteStackIndex")]
            public string exceptionremoteStackIndex { get; set; }

            [JsonPropertyName("exception.remoteStackTraceString")]
            public string exceptionremoteStackTraceString { get; set; }

            [JsonPropertyName("exception.res")]
            public string exceptionres { get; set; }

            [JsonPropertyName("exception.source")]
            public string exceptionsource { get; set; }

            [JsonPropertyName("exception.sourceUri")]
            public string exceptionsourceUri { get; set; }

            [JsonPropertyName("exception.stackTraceString")]
            public string exceptionstackTraceString { get; set; }

            [JsonPropertyName("exception.status")]
            public string exceptionstatus { get; set; }

            [JsonPropertyName("exception.version")]
            public string exceptionversion { get; set; }

            [JsonPropertyName("exception.watsonBuckets")]
            public string exceptionwatsonBuckets { get; set; }

            [JsonPropertyName("exception.watsonBuckets-skipped-count")]
            public string exceptionwatsonBucketsskippedcount { get; set; }

            [JsonPropertyName("exception.zone")]
            public string exceptionzone { get; set; }

            public string exceptionMessage { get; set; }
            public string exceptionStackTrace { get; set; }
            public string exceptionType { get; set; }
            public string executionTime { get; set; }
            public string executionTimeMs { get; set; }
            public string expired { get; set; }
            public string expiredCount { get; set; }
            public string failedCount { get; set; }
            public string file { get; set; }
            public string fileCount { get; set; }
            public string growerId { get; set; }
            public string host { get; set; }
            public string implementation { get; set; }
            public string importedCount { get; set; }
            public string importedFile { get; set; }
            public string _interface { get; set; }
            public string internalId { get; set; }
            public string level { get; set; }
            public string logger { get; set; }
            public string message { get; set; }
            public string method { get; set; }
            public string name { get; set; }
            public string _namespace { get; set; }
            public string orderCompletedDate { get; set; }
           // public string orderID { get; set; }
            public string orderId { get; set; }
            public string orderRef { get; set; }
           // public string orderid { get; set; }
            public string output { get; set; }
            public string parameters { get; set; }
            public string pending { get; set; }
            public string pendingCount { get; set; }
            public string performanceHit { get; set; }
            public string productNumber { get; set; }
            public string reason { get; set; }
            public string recipient { get; set; }
            public string requestId { get; set; }
            public string result { get; set; }
            public string rows { get; set; }

            [JsonPropertyName("shops.sHOP1.expired")]
            public string shopssHOP1expired { get; set; }

            [JsonPropertyName("shops.sHOP1.newest")]
            public string shopssHOP1newest { get; set; }

            [JsonPropertyName("shops.sHOP1.oldest")]
            public string shopssHOP1oldest { get; set; }

            [JsonPropertyName("shops.sHOP1.pending")]
            public string shopssHOP1pending { get; set; }

            [JsonPropertyName("shops.sHOP2.expired")]
            public string shopssHOP2expired { get; set; }

            [JsonPropertyName("shops.sHOP2.pending")]
            public string shopssHOP2pending { get; set; }

            [JsonPropertyName("shops.sHOP3.expired")]
            public string shopssHOP3expired { get; set; }

            [JsonPropertyName("shops.sHOP3.newest")]
            public string shopssHOP3newest { get; set; }

            [JsonPropertyName("shops.sHOP3.oldest")]
            public string shopssHOP3oldest { get; set; }

            [JsonPropertyName("shops.sHOP3.pending")]
            public string shopssHOP3pending { get; set; }

            [JsonPropertyName("shops.sHOP4.expired")]
            public string shopssHOP4expired { get; set; }

            [JsonPropertyName("shops.sHOP4.newest")]
            public string shopssHOP4newest { get; set; }

            [JsonPropertyName("shops.sHOP4.oldest")]
            public string shopssHOP4oldest { get; set; }

            [JsonPropertyName("shops.sHOP4.pending")]
            public string shopssHOP4pending { get; set; }

            [JsonPropertyName("shops.sHOP5.expired")]
            public string shopssHOP5expired { get; set; }

            [JsonPropertyName("shops.sHOP5.newest")]
            public string shopssHOP5newest { get; set; }

            [JsonPropertyName("shops.sHOP5.oldest")]
            public string shopssHOP5oldest { get; set; }

            [JsonPropertyName("shops.sHOP5.pending")]
            public string shopssHOP5pending { get; set; }

            [JsonPropertyName("shops.sHOP6.expired")]
            public string shopssHOP6expired { get; set; }

            [JsonPropertyName("shops.sHOP6.newest")]
            public string shopssHOP6newest { get; set; }

            [JsonPropertyName("shops.sHOP6.oldest")]
            public string shopssHOP6oldest { get; set; }

            [JsonPropertyName("shops.sHOP6.pending")]
            public string shopssHOP6pending { get; set; }

            [JsonPropertyName("shops.sHOP7.expired")]
            public string shopssHOP7expired { get; set; }

            [JsonPropertyName("shops.sHOP7.newest")]
            public string shopssHOP7newest { get; set; }

            [JsonPropertyName("shops.sHOP7.oldest")]
            public string shopssHOP7oldest { get; set; }

            [JsonPropertyName("shops.sHOP7.pending")]
            public string shopssHOP7pending { get; set; }

            [JsonPropertyName("shops.sHOP8.expired")]
            public string shopssHOP8expired { get; set; }

            [JsonPropertyName("shops.sHOP8.newest")]
            public string shopssHOP8newest { get; set; }

            [JsonPropertyName("shops.sHOP8.oldest")]
            public string shopssHOP8oldest { get; set; }

            [JsonPropertyName("shops.sHOP8.pending")]
            public string shopssHOP8pending { get; set; }

            [JsonPropertyName("shops.sHOP9.expired")]
            public string shopssHOP9expired { get; set; }

            [JsonPropertyName("shops.sHOP9.newest")]
            public string shopssHOP9newest { get; set; }

            [JsonPropertyName("shops.sHOP9.oldest")]
            public string shopssHOP9oldest { get; set; }

            [JsonPropertyName("shops.sHOP9.pending")]
            public string shopssHOP9pending { get; set; }

            public string solution { get; set; }
            public string status { get; set; }
            public string statusCode { get; set; }
            public string successCount { get; set; }
            public string tags { get; set; }
            public string timestamp { get; set; }
            public string totalCount { get; set; }
            public string transactionNumber { get; set; }
            public string trigger { get; set; }
            public string type { get; set; }
            public string url { get; set; }
            public string urlFull { get; set; }
            public string urlInternal { get; set; }
            public string userId { get; set; }
            public string userName { get; set; }
            //public string username { get; set; }
            public string errorForTrainer { get; set; } = "0";

            public string errorForTrainer { get; set; } = "0";

        }
    }
}
